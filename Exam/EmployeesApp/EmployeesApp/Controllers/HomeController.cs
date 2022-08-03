using Microsoft.AspNetCore.Mvc;

namespace EmployeesApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Employees()
        {
            return View();
        }

        public IActionResult Todo()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult CreateEditEmployee()
        {
            return View();
        }

        public IActionResult CreateEditTodo()
        {
            return View();
        }
    }
}
