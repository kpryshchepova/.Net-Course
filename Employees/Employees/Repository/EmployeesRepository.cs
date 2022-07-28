using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employees
{
    
    public class EmployeesRepository : IRepository
    {
        private ApplicationDbContext _db;

        public EmployeesRepository(ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;
        }

        //public async Task InsertAsync(Employee data)
        //{
        //    await _db.EmployeesData.AddAsync(data);
        //    await _db.SaveChangesAsync();
        //}

        public async Task InsertAsync(IAsyncEnumerable<Employee> employeeData)
        {
            await foreach (Employee employee in employeeData) await _db.EmployeesData.AddAsync(employee);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _db.EmployeesData.ToListAsync();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
