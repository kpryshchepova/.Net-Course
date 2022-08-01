using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employees
{
    public interface IRepository : IDisposable
    {
        Task InsertAsync(Employee data);
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task SaveAsync();
    }
}
