using Abby.Models;
using Abby.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Abby.DataAccess.Repository.IRepository;

namespace Abby.Web.Pages.Admin.Categories
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        // BindProperty allows us to use this attribute automatically in the OnPost call
        // instead of passing it in as a value EX: OnPost(Category category) is replaced
        // by the BindProperty DataAnnotations
        [BindProperty]
        public Category Category { get; set; }

        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async void OnGet(int Id)
        {
            // throws execption if record not found
            Category = _unitOfWork.Category.GetFirstOrDefault(u=>u.Id==Id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Category.Name", "The Display Order cannot exactly match the name!");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(Category);
                await _unitOfWork.SaveAsync();
                TempData["success"] = "Successfully edited Category";
                return RedirectToPage("Index");
            }
            TempData["error"] = "Error editing Category";
            return Page();
        }
    }
}
