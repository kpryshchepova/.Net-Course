using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Employees
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Employee> EmployeesData { get; set; }

        public ApplicationDBContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=employeesdb;Trusted_Connection=True;");
        }

    }
}
