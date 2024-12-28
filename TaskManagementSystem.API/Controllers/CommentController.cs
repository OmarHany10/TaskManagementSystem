using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Core.DTOs.CommentDTOs;
using TaskManagementSystem.Core.Services.interfaces;

namespace TaskManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = commentService.GetAllComments();
            return Ok(result);
        }

        [HttpGet("{Id:int}")]
        public IActionResult GetById(int Id)
        {
            var result = commentService.GetCommentById(Id);
            if(result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("{userId}/User")]
        public IActionResult GetUserComments(string userId)
        {
            var result = commentService.GetUserComments(userId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CommentDTO commentDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await commentService.AddComment(commentDTO);
            if(result != null)
                return BadRequest(result);
            return Ok(commentDTO);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int Id, CommentDTO commentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await commentService.UpdateComment(Id, commentDTO);
            if (result != null)
                return BadRequest(result);
            return Ok(commentDTO);
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            var result = commentService.DeleteComment(Id);
            if(result==0)
                return NotFound();
            return Ok(result);
        }
    }
}
