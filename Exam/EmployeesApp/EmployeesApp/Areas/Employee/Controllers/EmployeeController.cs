using Microsoft.AspNetCore.Mvc;
using EmployeesApp.ApplicationContext;
using EmployeesApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace EmployeesApp.Areas.Employees.Controllers
{
    [Area("Employee")]
    [Route("Employee")]
    public class EmployeeController : Controller
    {
        ApplicationContext.ApplicationContext db;
        public EmployeeController(ApplicationContext.ApplicationContext context)
        {
            db = context;
        }

        [Route("EmployeeList")]
        public async Task<IActionResult> EmployeeList()
        {
            return View(await db.Employees.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeFilteredList(string? searchString)
        {
            string patternDate = "^([0]?[0-9]|[12][0-9]|[3][01])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$";
            var employees = from m in db.Employees
                         select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(employee =>
                    employee.Name.Contains(searchString) ||
                    employee.Age.ToString().Contains(searchString) ||
                    employee.Speciality.Contains(searchString) ||
                    (Regex.IsMatch(searchString, patternDate) && employee.EmployementDate == DateTime.Parse(searchString)));
            }
            return View("EmployeeList", await employees.ToListAsync());
        }

        [Route("Create")]
        public IActionResult Create()
        {
            return View("CreateEditEmployee");
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Employee employee)
        {
            //------------TODO ERROR-------------
            await db.Employees.AddAsync(employee);
            await db.SaveChangesAsync();
            return RedirectToAction("EmployeeList");
        }


        [Route("Edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Employee? employee = await db.Employees.FirstOrDefaultAsync(x => x.Id == id);
                if (employee != null) return View("CreateEditEmployee", employee);
                //------------TODO ERROR-------------
                else return NotFound();
            }
            return View();

        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(Employee employee)
        {
            //------------TODO ERROR-------------
            db.Employees.Update(employee);
            await db.SaveChangesAsync();
            return RedirectToAction("EmployeeList");
        }

        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Employee user = new Employee { Id = id.Value };
                db.Entry(user).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("EmployeeList");
            }
            //------------TODO ERROR-------------
            return NotFound();
        }
    }
}
