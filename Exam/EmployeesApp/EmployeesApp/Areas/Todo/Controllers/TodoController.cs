using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeesApp.Areas.Todo.Controllers
{
    [Area("Todo")]
    public class TodoController : Controller
    {
        ApplicationContext.ApplicationContext db;
        public TodoController(ApplicationContext.ApplicationContext context)
        {
            db = context;
        }

        [Route("Todo")]
        public async Task<IActionResult> Todo()
        {
            return View(await db.Todos.ToListAsync());
        }

        [Route("CreateEditTodo")]
        public async Task<IActionResult> CreateEditTodo()
        {
            ViewData["Employees"] = await db.Employees.ToListAsync();
            return View();
        }

    }
}
