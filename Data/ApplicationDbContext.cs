using Microsoft.EntityFrameworkCore;
using StudentPortal.Models.Entity;

namespace StudentPortal.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        public DbSet<Student> students { get; set; }
    }
}
