using Abby.Models;
using Abby.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Abby.DataAccess.Repository.IRepository;

namespace Abby.Web.Pages.Admin.FoodTypes
{
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        // BindProperty allows us to use this attribute automatically in the OnPost call
        // instead of passing it in as a value EX: OnPost(Category category) is replaced
        // by the BindProperty DataAnnotations
        [BindProperty]
        public FoodType FoodType { get; set; }

        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async void OnGet(int Id)
        {
            // throws execption if record not found
            FoodType = _unitOfWork.FoodType.GetFirstOrDefault(u=>u.Id==Id);
        }

        public async Task<IActionResult> OnPost(int Id)
        {
            
            // find object
            var foodTypeFromDb = _unitOfWork.FoodType.GetFirstOrDefault(u => u.Id == Id);

            if (foodTypeFromDb != null)
            {
                _unitOfWork.FoodType.Remove(foodTypeFromDb);
                await _unitOfWork.SaveAsync();
                TempData["success"] = "Successfully deleted Food Type";
                return RedirectToPage("Index");
            }
            TempData["error"] = "Error deleting Food Type";
            return Page();
 
        }
    }
}
