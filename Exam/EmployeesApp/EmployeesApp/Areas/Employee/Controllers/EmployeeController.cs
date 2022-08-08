using Microsoft.AspNetCore.Mvc;
using EmployeesApp.ApplicationContext;
using EmployeesApp.Models;
using Microsoft.EntityFrameworkCore;

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

        [Route("Create")]
        public IActionResult Create()
        {
            return View("CreateEditEmployee");
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Employee employee)
        {
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
                //------------TODO-------------
                else return NotFound();
            }
            return View();

        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(Employee employee)
        {
            db.Employees.Update(employee);
            await db.SaveChangesAsync();
            return RedirectToAction("EmployeeList");
        }
    }
}
