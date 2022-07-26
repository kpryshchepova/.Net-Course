using Microsoft.EntityFrameworkCore;

namespace Employees
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> EmployeesData { get; set; }
        private string _dbConnectionString;

        public ApplicationDbContext()
        {
            _dbConnectionString = new Configuration().GetConfigurationString();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbConnectionString);
        }

    }
}
