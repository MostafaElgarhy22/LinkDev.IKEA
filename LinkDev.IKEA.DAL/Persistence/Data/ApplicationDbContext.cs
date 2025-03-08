
using LinkDev.IKEA.DAL.Entities.Departments;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.DAL.Persistence.Data
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=IKEA;Trusted_Connection=True;");
        }

        public DbSet<Department> Departments { get; set; }
    }
}
