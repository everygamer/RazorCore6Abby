using Abby.Models;
using Abby.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Abby.DataAccess.Repository.IRepository;

namespace Abby.Web.Pages.Admin.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        // BindProperty allows us to use this attribute automatically in the OnPost call
        // instead of passing it in as a value EX: OnPost(Category category) is replaced
        // by the BindProperty DataAnnotations
        [BindProperty]
        public Category Category { get; set; }

        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async void OnGet(int Id)
        {
            // throws execption if record not found
            Category = _unitOfWork.Category.GetFirstOrDefault(u=>u.Id == Id);

        }

        public async Task<IActionResult> OnPost(int Id)
        {
            
            // find object
            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == Id);

            if (categoryFromDb != null)
            {
                _unitOfWork.Category.Remove(categoryFromDb);
                await _unitOfWork.SaveAsync();
                TempData["success"] = "Successfully deleted Category";
                return RedirectToPage("Index");
            }
            TempData["error"] = "Error deleting Category";
            return Page();
 
        }
    }
}
