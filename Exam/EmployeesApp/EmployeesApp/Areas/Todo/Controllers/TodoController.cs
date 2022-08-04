using Microsoft.AspNetCore.Mvc;

namespace EmployeesApp.Areas.Todo.Controllers
{
    [Area("Todo")]
    public class TodoController : Controller
    {
        [Route("Todo")]
        public IActionResult Todo()
        {
            return View();
        }

        [Route("CreateEditTodo")]
        public IActionResult CreateEditTodo()
        {
            return View();
        }

    }
}
