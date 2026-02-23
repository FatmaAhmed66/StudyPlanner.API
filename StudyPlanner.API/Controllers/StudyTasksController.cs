using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyPlanner.API.Data;
using StudyPlanner.API.DTOs;
using StudyPlanner.API.Models;
using System.Security.Claims;

namespace StudyPlanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // this means only authorized user can create a new task
    public class StudyTasksController : ControllerBase
    {
        private readonly ApplicationDbcontext _context;

        public StudyTasksController(ApplicationDbcontext context)
        {
            _context = context;
        }

        //create task

        [HttpPost]
        public async Task<IActionResult> Create(string title)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var task = new StudyTask
            {
                Title = title,
                IsCompleted = false,
                UserId = userId
            };

            _context.StudyTasks.Add(task);
            await _context.SaveChangesAsync();

            return Ok(task);
        }

        //get my tasks
        [HttpGet]
        public IActionResult GetMyTasks()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var tasks = _context.StudyTasks
                .Where(t => t.UserId == userId)
                .ToList();

            return Ok(tasks);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTaskDTO model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var task = _context.StudyTasks
                .FirstOrDefault(t => t.Id == id && t.UserId == userId);

            if (task == null)
                return NotFound();

            task.Title = model.Title;
            task.IsCompleted = model.IsCompleted;

            await _context.SaveChangesAsync();

            return Ok(task);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var task = _context.StudyTasks
                .FirstOrDefault(t => t.Id == id && t.UserId == userId);

            if (task == null)
                return NotFound();

            _context.StudyTasks.Remove(task);
            await _context.SaveChangesAsync();

            return Ok("Deleted successfully");
        }


        // get all tasks

        [Authorize(Roles = "Admin")]
        [HttpGet("all-tasks")]
        public IActionResult GetAllTasks()
        {
            var tasks = _context.StudyTasks.ToList();
            return Ok(tasks);
        }

    }
}
