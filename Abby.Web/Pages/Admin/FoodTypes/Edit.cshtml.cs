using Abby.Models;
using Abby.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Abby.Web.Pages.Admin.FoodTypes
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        // BindProperty allows us to use this attribute automatically in the OnPost call
        // instead of passing it in as a value EX: OnPost(Category category) is replaced
        // by the BindProperty DataAnnotations
        [BindProperty]
        public FoodType FoodType { get; set; }

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public async void OnGet(int Id)
        {
            // throws execption if record not found
            FoodType = _db.FoodType.Find(Id);

            // just returns null if not found
            //Category = _db.Category.FirstOrDefault(u=>u.Id == Id);

            // if multiple entity returned, it throws exception
            //Category = _db.Category.Single(u => u.Id == Id);

            // if nothing is found, returns null, if multiple returns, exception
            //Category = _db.Category.SingleOrDefault(u => u.Id == Id);

            // can return multiple records, so we have to add .FirstOrDefault to get 1 record
            //Category = _db.Category.Where(u => u.Id == Id).FirstOrDefault();

        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.FoodType.Update(FoodType);
                await _db.SaveChangesAsync();
                TempData["success"] = "Successfully edited Food Type";
                return RedirectToPage("Index");
            }
            TempData["error"] = "Error editing Food Type";
            return Page();
        }
    }
}
