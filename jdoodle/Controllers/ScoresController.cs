using jdoodle.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace jdoodle.Controllers
{
    [Authorize]
    public class ScoresController : Controller
    {
        private readonly DBContext _context;

        public ScoresController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _context.UserTask
                .Include(ut => ut.User)
                .Include(ut => ut.Task)
                .ToListAsync();

            return View(model);
        }
    }
}