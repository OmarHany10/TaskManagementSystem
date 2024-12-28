using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Core.DTOs.ProjectDTOs;
using TaskManagementSystem.Core.Services.interfaces;

namespace TaskManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Project Manger")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectServices projectServices;
        private readonly ITaskService taskService;

        public ProjectController(IProjectServices projectServices, ITaskService taskService)
        {
            this.projectServices = projectServices;
            this.taskService = taskService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(projectServices.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var project = projectServices.GetByID(id);
            if (project == null)
                return NotFound();
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProjectDTO projectDTO)
        {
            var result = await projectServices.Add(projectDTO);
            if (result != null)
                return BadRequest(result);

            return Ok(projectDTO);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, ProjectDTO projectDTO)
        {
            var result = await projectServices.Update(id, projectDTO);
            if (result != null)
                return BadRequest(result);

            return Ok(projectDTO);
        }

        [HttpDelete]
        public IActionResult Delete(int id) 
        {
            var result = projectServices.Delete(id);
            if(result == 2)
                return BadRequest("There are tasks in the project");
            else if(result==0)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("{projectId:int}/Users")]
        public async Task<IActionResult> GetAllUsers(int projectId)
        {
            var result = await projectServices.GetAllUsers(projectId);
            return Ok(result);
        }

        [HttpGet("{projectId:int}/Tasks")]
        public IActionResult GetALlTasks(int projectId)
        {
            var result = taskService.GetTasksByProject(projectId);
            return Ok(result);
        }
    }
}
