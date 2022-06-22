using Abby.Web.Model;
using Microsoft.EntityFrameworkCore;

namespace Abby.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        // passes DbContextOptions for ApplicationDbContext via Dependancy Injection, and then passes that to the base class
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Category { get; set; }
    }
}
