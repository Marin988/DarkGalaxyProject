using DarkGalaxyProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DarkGalaxyProject
{
    public class DesignTimeApplicationDbContext : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            // pass your design time connection string here
            optionsBuilder.UseSqlServer("Server=.;Database=DarkGalaxy;Integrated Security=True;");
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}