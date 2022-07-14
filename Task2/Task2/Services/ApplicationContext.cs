using Microsoft.EntityFrameworkCore;

namespace Task2
{
    public class ApplicationContext : DbContext
    {
        public DbSet<DataForDB> GameInfo { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=mylocaldb;Trusted_Connection=True;");
        }

    }
}
