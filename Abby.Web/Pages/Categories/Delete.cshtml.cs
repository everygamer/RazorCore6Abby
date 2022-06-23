using Abby.Models;
using Abby.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Abby.Web.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        // BindProperty allows us to use this attribute automatically in the OnPost call
        // instead of passing it in as a value EX: OnPost(Category category) is replaced
        // by the BindProperty DataAnnotations
        [BindProperty]
        public Category Category { get; set; }

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public async void OnGet(int Id)
        {
            // throws execption if record not found
            Category = _db.Category.Find(Id);

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
            var categoryFromDb = _db.Category.Find(Category.Id);

            if (categoryFromDb != null)
            {
                _db.Category.Remove(categoryFromDb);
                await _db.SaveChangesAsync();
                TempData["success"] = "Successfully deleted Category";
                return RedirectToPage("Index");
            }
            TempData["error"] = "Error deleting Category";
            return Page();
 
        }
    }
}
