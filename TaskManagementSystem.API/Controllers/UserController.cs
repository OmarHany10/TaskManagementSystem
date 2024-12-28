using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementSystem.Core;
using TaskManagementSystem.Core.DTOs.UserDTOs;
using TaskManagementSystem.Core.Services.interfaces;

namespace TaskManagementSystem.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IActivityService activityService;
        private readonly ITaskService taskService;

        public UserController(IUserService userService ,IActivityService activityService, ITaskService taskService)
        {
            this.userService = userService;
            this.activityService = activityService;
            this.taskService = taskService;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = userService.GetAll();
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(string id)
        {
            var user = await userService.GetByID(id);
            if (user == null)
            {
                return NotFound(user);
            }
            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(string id, UpdateUserDTO updateUserDTO)
        {
            
            var result = await userService.Update(id, updateUserDTO);
            if(result != null)
                return BadRequest(result);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IActionResult Delete(string id)
        {
            int result = userService.Delete(id);
            if (result == 0)
                return NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{Id}/activity")]
        public IActionResult GetAllUserActivity(string Id)
        {
            var result = activityService.GetAllUserActivity(Id);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{userId}/Tasks")]
        public IActionResult GetAllTasks(string userId)
        {
            var result = taskService.GetTasksAssignToUser(userId);
            return Ok(result);
        }

        [HttpGet("MyActivity")]
        public IActionResult GetMyActivity()
        {
            var userId = User.FindFirst("uid")?.Value;
            var result = activityService.GetAllUserActivity(userId);
            return Ok(result);
        }

        [HttpGet("MyTasks")]
        public IActionResult GetMyTasks()
        {
            var userId = User.FindFirst("uid")?.Value;
            var result = taskService.GetTasksAssignToUser(userId);
            return Ok(result);
        }
    }
}
