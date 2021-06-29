using jdoodle.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace jdoodle.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly DBContext _context;

        public TasksController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tasks.ToListAsync());
        }
    }
}