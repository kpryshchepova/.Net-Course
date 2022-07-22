using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public interface IRepository : IDisposable
    {
        Task InsertAsync(Employee data);
        Task<IEnumerable<Employee>> GetEmployeesAsync();
    }
}
