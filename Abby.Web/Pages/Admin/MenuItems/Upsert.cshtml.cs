using Abby.Models;
using Abby.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Abby.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abby.Web.Pages.Admin.MenuItems
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        // BindProperty allows us to use this attribute automatically in the OnPost call
        // instead of passing it in as a value EX: OnPost(Category category) is replaced
        // by the BindProperty DataAnnotations
        [BindProperty]
        public MenuItem MenuItem { get; set; }

        // this attributes will hold the dropdown list values for Category and FoodType
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> FoodTypeList { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            MenuItem = new();
            _hostingEnvironment = hostingEnvironment;
        }

        public void OnGet(int? id)
        {
            if (id != null)
            {
                MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == id);
            }

            // populate Category using projections to create SelectListItems
            CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            // populate Food Type
            FoodTypeList = _unitOfWork.FoodType.GetAll().Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public async Task<IActionResult> OnPost()
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            // id == 0 then create otherwise it is edit
            if (MenuItem.Id == 0)
            {
                // CREATE

                // construct new image filename
                string fileName_new = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\menuItems");
                var extension = Path.GetExtension(files[0].FileName);

                // create new file, and copy uploaded images content into it
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                // set the MenuItem's ImageURL to the newly created image
                MenuItem.ImageURL = @"\images\menuItems\" + fileName_new + extension;

                // add the MenuItem object to the database (will add once Save is called)
                _unitOfWork.MenuItem.Add(MenuItem);
            }
            else
            {
                // EDIT
                var objFromDb = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == MenuItem.Id);
                // check for new image
                if (files.Count>0)
                {
                    // construct new image filename
                    string fileName_new = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\menuItems");
                    var extension = Path.GetExtension(files[0].FileName);

                    // delete old image
                    var oldImagePath = Path.Combine(webRootPath, objFromDb.ImageURL.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    // upload new file
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    // set the MenuItem's ImageURL to the newly created image
                    MenuItem.ImageURL = @"\images\menuItems\" + fileName_new + extension;

                    // update 
                }
                else
                {
                    MenuItem.ImageURL = objFromDb.ImageURL;
                }

                // update record
                _unitOfWork.MenuItem.Update(MenuItem);

            }

            await _unitOfWork.SaveAsync();

            return RedirectToPage("./Index");
        }
    }
}
