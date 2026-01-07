using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Core.DTOs;
using TaskManagementSystem.Core.Services.interfaces;

namespace TaskManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService taskService;
        private readonly ICommentService commentService;

        public TaskController(ITaskService taskService, ICommentService commentService)
        {
            this.taskService = taskService;
            this.commentService = commentService;
        }

        [Authorize(Roles = "Project Manger")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var tasks = taskService.GetAll();
            return Ok(tasks);
        }

        [Authorize(Roles = "Project Manger")]
        [HttpGet("{Id:int}")]
        public IActionResult GetById(int Id)
        {
            var task = taskService.GetById(Id);
            if (task == null)
                return NotFound();
            return Ok(task);
        }

        [HttpGet("{taskId:int}/Comments")]
        public IActionResult GetTaskComments(int taskId)
        {
            var result = commentService.GetTaskComments(taskId);
            return Ok(result);
        }

        [Authorize(Roles = "Project Manger")]
        [HttpGet("Overdue")]
        public IActionResult GetAllOverdue()
        {
            var result = taskService.GetAllOverdue();
            return Ok(result);
        }

        [Authorize(Roles = "Project Manger")]
        [HttpGet("Upcoming")]
        public IActionResult GetAllUpcoming()
        {
            var result = taskService.GetAllUpcoming();
            return Ok(result);
        }

        [Authorize(Roles = "Project Manger")]
        [HttpGet("Finished")]
        public IActionResult GetAllFinished()
        {
            var result = taskService.GetAllFinished();
            return Ok(result);
        }

        [HttpGet("MyOverdue")]
        public IActionResult GetMyOverdue()
        {
            var userId = User.FindFirst("uid")?.Value;
            var result = taskService.GetAllUserOverdue(userId);
            return Ok(result);
        }

        [HttpGet("MyUpcoming")]
        public IActionResult GetMyUpcoming()
        {
            var userId = User.FindFirst("uid")?.Value;
            var result = taskService.GetAllUserUpcoming(userId);
            return Ok(result);
        }

        [HttpGet("MyFinished")]
        public IActionResult GetMyFinished()
        {
            var userId = User.FindFirst("uid")?.Value;
            var result = taskService.GetAllUserFinished(userId);
            return Ok(result);
        }

        [Authorize(Roles = "Project Manger")]
        [HttpPost]
        public async Task<IActionResult> Add(TaskDTO taskDTO)
        {
            var result = await taskService.Add(taskDTO);
            if (result != null)
                return BadRequest(result);
            return Ok(taskDTO);
        }

        [Authorize(Roles = "Project Manger")]
        [HttpPut]
        public async Task<IActionResult> Update(int Id, TaskDTO taskDTO)
        {
            var result = await taskService.Update(Id, taskDTO);
            if (result != null)
                return BadRequest(result);
            return Ok(taskDTO);
        }

        [Authorize(Roles = "Project Manger")]
        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            var result = taskService.Delete(Id);
            if (result == 0)
                return NotFound();
            return Ok();
        }


        [Authorize(Roles = "Project Manger")]
        [HttpPost("AssignUser")]
        public async Task<IActionResult> AssignUser(string userId, int taskId)
        {
            var result = await taskService.AssignTaskToUser(userId, taskId);
            if (result != null)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("FinishTask")]
        public IActionResult FinishTask(int taskId)
        {
            var result = taskService.FinishTheTask(taskId);
            if (result == "Task Not Found")
                return NotFound();
            if (result == "Task Already Finished")
                return BadRequest(result);
            return Ok(result);
        }
    }
}
