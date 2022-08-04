using Microsoft.AspNetCore.Mvc;

namespace EmployeesApp.Areas.Employees.Controllers
{
    [Area("Employee")]
    public class EmployeeController : Controller
    {
        [Route("Employee")]
        public IActionResult Employee()
        {
            return View();
        }

        [Route("CreateEditEmployee")]
        public IActionResult CreateEditEmployee()
        {
            return View();
        }
    }
}
