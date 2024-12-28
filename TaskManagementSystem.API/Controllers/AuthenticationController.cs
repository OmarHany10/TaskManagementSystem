using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Core.DTOs.UserDTOs;
using TaskManagementSystem.Core.Services.interfaces;

namespace TaskManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService authenticationService;

        public AuthenticationController(IAuthService authenticationService)
        {
            this.authenticationService = authenticationService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserDTO result = await authenticationService.Register(registerDTO);
            
            if(!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserDTO result = await authenticationService.Login(loginDTO);
            if(!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("AssignToRole")]
        public async Task<IActionResult> AssignToRole(AssignToRoleDTO assignToRoleDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await authenticationService.AssignToRole(assignToRoleDTO);
            
            if(!result.success)
                return BadRequest(result.Message);

            return Ok(result.Message);
            
        }
    }
}
