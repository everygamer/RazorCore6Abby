using Abby.Models;
using Abby.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Abby.Web.Pages.Admin.FoodTypes
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        // BindProperty allows us to use this attribute automatically in the OnPost call
        // instead of passing it in as a value EX: OnPost(Category category) is replaced
        // by the BindProperty DataAnnotations
        [BindProperty]
        public FoodType FoodType { get; set; }

        public DeleteModel(ApplicationDbContext db)
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
            
            // find object
            var foodTypeFromDb = _db.FoodType.Find(FoodType.Id);

            if (foodTypeFromDb != null)
            {
                _db.FoodType.Remove(foodTypeFromDb);
                await _db.SaveChangesAsync();
                TempData["success"] = "Successfully deleted Food Type";
                return RedirectToPage("Index");
            }
            TempData["error"] = "Error deleting Food Type";
            return Page();
 
        }
    }
}
