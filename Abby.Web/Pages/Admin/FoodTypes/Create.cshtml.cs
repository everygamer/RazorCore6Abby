using Abby.Models;
using Abby.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Abby.Web.Pages.Admin.FoodTypes
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        // BindProperty allows us to use this attribute automatically in the OnPost call
        // instead of passing it in as a value EX: OnPost(Category category) is replaced
        // by the BindProperty DataAnnotations
        [BindProperty]
        public FoodType FoodType { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _db.FoodType.AddAsync(FoodType);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
