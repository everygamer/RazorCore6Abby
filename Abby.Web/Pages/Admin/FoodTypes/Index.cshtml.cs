using Abby.Models;
using Abby.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Abby.Web.Pages.Admin.FoodTypes
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IEnumerable<FoodType> FoodTypes { get; set; }

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;   
        }

        public void OnGet()
        {
            FoodTypes = _db.FoodType;
        }
    }
}
