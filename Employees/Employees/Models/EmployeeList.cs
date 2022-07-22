using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public class EmployeeList
    {
        public List<Employee> ListOfEmployees { get; set; }

        public EmployeeList()
        {
            ListOfEmployees = new List<Employee>();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return ListOfEmployees;
        }
    }
}
