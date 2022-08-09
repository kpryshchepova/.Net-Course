using Microsoft.AspNetCore.Mvc;
using EmployeesApp.ApplicationContext;
using EmployeesApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using EmployeesApp.Repository;

namespace EmployeesApp.Areas.Employees.Controllers
{
    [Area("Employee")]
    [Route("Employee")]
    public class EmployeeController : Controller
    {
        private IRepository<Employee> _employeesRepository;
        public EmployeeController(IRepository<Employee> employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        [Route("EmployeeList")]
        public async Task<IActionResult> EmployeeList()
        {
            try
            {
                var employees = await _employeesRepository.GetAllAsync();
                return View(employees);
            } catch
            {//------------TODO ERROR-------------
                return NotFound();
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> EmployeeFilteredList(string? searchString)
        //{
        //    string patternDate = "^([0]?[0-9]|[12][0-9]|[3][01])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$";
        //    var employees = from m in db.Employees
        //                 select m;
        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        employees = employees.Where(employee =>
        //            employee.Name.Contains(searchString) ||
        //            employee.Age.ToString().Contains(searchString) ||
        //            employee.Speciality.Contains(searchString) ||
        //            (Regex.IsMatch(searchString, patternDate) && employee.EmployementDate == DateTime.Parse(searchString)));
        //    }
        //    return View("EmployeeList", await employees.ToListAsync());
        //}

        [Route("Create")]
        public IActionResult Create()
        {
            return View("CreateEditEmployee");
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Employee employee)
        {
            try
            {
                await _employeesRepository.InsertAsync(employee);
                await _employeesRepository.SaveAsync();
                return RedirectToAction("EmployeeList");
            } catch
            {//------------TODO ERROR-------------
                return NotFound();
            }
            
        }


        [Route("Edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id != null)
                {
                    Employee? employee = await _employeesRepository.FindById(id);
                    if (employee != null) return View("CreateEditEmployee", employee);
                    //------------TODO ERROR-------------
                    else return NotFound();
                }
                return View();
            } catch
            {//------------TODO ERROR-------------
                return NotFound();
            }

        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(Employee employee)
        {
            try
            {
                _employeesRepository.Update(employee);
                await _employeesRepository.SaveAsync();
                return RedirectToAction("EmployeeList");
            } catch
            {
                //------------TODO ERROR-------------
                return NotFound();
            }
        }

        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id != null)
                {
                    Employee employee = new Employee { Id = id.Value };
                    _employeesRepository.Delete(employee);

                    await _employeesRepository.SaveAsync();
                    return RedirectToAction("EmployeeList");
                }
            } catch
            {
                //------------TODO ERROR-------------
                return NotFound();
            }

            return NotFound();
        }
    }
}
