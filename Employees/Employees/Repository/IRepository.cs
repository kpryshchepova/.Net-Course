using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employees
{
    public interface IRepository : IDisposable
    {
        //Task InsertAsync(Employee data);
        Task InsertAsync(IAsyncEnumerable<Employee> employeeData);
        Task<IEnumerable<Employee>> GetEmployeesAsync();
    }
}
