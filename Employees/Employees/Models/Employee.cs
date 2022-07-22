using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime Birthday { get; set; }

        public Employee() { }

        public Employee(string firstName, string lastName, string patronymic, string city, string country, DateTime birthday)
        {
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            City = city;
            Country = country;
            Birthday = birthday;
        }
    }
}
