using jdoodle.Core.Processor;
using jdoodle.Models;
using jdoodle.VIewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace jdoodle.Controllers
{
    [Authorize]
    public class SolveController : Controller
    {
        private readonly DBContext _context;

        public SolveController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _context.Tasks.ToListAsync();

            var tasksList = new List<SelectListItem>();

            foreach (var item in result)
                tasksList.Add(new SelectListItem { Value = item.Id, Text = item.TaskName });

            tasksList.Insert(0, new SelectListItem { Value = "", Text = "" });

            return View(new SolveViewModel()
            {
                Tasks = tasksList
            });
        }

        [HttpGet]
        public async Task<string> Description(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var result = await _context.Tasks.FindAsync(id);
                return result.Description;
            }

            return "Error. No desc found.";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(SolveViewModel solveViewModel)
        {
            string msg = "Done";

            if (ModelState.IsValid)
            {
                if (!TasksExists(solveViewModel.Task))
                    msg = "NotFound";

                var jDoodleProcessor = new JDoodleProcessor();
                var jDoodleRequest = new JDoodleRequest()
                {
                    Script = solveViewModel.SolutionCode
                };

                var result = await jDoodleProcessor.PostAsync(jDoodleRequest).ConfigureAwait(false);

                if (result?.StatusCode == "200")
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    var task = _context.Tasks.Find(solveViewModel.Task);

                    var userTask = _context.UserTask
                        .Where(s => s.Task.Id == task.Id)
                        .ToList();

                    var score = userTask
                        .Where(s => s.UserId == userId)
                        .SingleOrDefault();

                    if (score == null)
                    {
                        score = new UserTask()
                        {
                            Id = Guid.NewGuid().ToString(),
                            TaskId = task.Id,
                            UserId = userId,
                            Attempt = 0,
                            Finished = false
                        };

                        _context.Add(score);
                        await _context.SaveChangesAsync();
                    }

                    score.Attempt += 1;

                    if (result.Output.Contains("Compilation failed"))
                        msg = "Compilation failed";
                    else if (task.OutputParams != result.Output)
                        msg = "Wrong output";
                    else
                        score.Finished = true;

                    _context.Update(score);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    msg = "API Fail";
                }
            }

            ViewData["Result"] = msg;

            return RedirectToAction("Index");
        }

        private bool TasksExists(string id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}