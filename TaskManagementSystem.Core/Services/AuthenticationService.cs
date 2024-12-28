using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementSystem.Core.DTOs.UserDTOs;
using TaskManagementSystem.Core.Helper;
using TaskManagementSystem.Core.Services.interfaces;

namespace TaskManagementSystem.Core.Services
{
    public class AuthenticationService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JWT _JWT;

        public AuthenticationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> JWT)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _JWT = JWT.Value;
        }

        public async Task<UserDTO> Register(RegisterDTO registerDTO)
        {


            if (await userManager.FindByNameAsync(registerDTO.Username) is not null || await userManager.FindByEmailAsync(registerDTO.Email) is not null)
            {
                return new UserDTO { Message = "Email or Username is already registered" };
            }

            ApplicationUser user = new ApplicationUser
            {
                Email = registerDTO.Email,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                UserName = registerDTO.Username,
            };

            IdentityResult Result = await userManager.CreateAsync(user, registerDTO.Password);

            if (!Result.Succeeded)
            {
                string errors = "";
                foreach (var error in Result.Errors)
                {
                    errors += $"{error.Description}, ";
                }
                return new UserDTO { Message = errors };
            }

            await userManager.AddToRoleAsync(user, "Member");

            JwtSecurityToken jwtSecurityCode = await CreateJwtToken(user);
            IList<string> roles = await userManager.GetRolesAsync(user);

            UserDTO userDTO = new UserDTO
            {
                Username = registerDTO.Username,
                Email = registerDTO.Email,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Roles = roles,
                IsAuthenticated = true,
                Experation = jwtSecurityCode.ValidTo,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityCode),
            };
            return userDTO;
        }

        public async Task<UserDTO> Login(LoginDTO loginDTO)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
                await userManager.FindByNameAsync(loginDTO.Email);

            if (user is null || !await userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                return new UserDTO { Message = "Incorrect Email or Password" };
            }

            JwtSecurityToken jwtSecurityCode = await CreateJwtToken(user);
            IList<string> roles = await userManager.GetRolesAsync(user);

            UserDTO userDTO = new UserDTO
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityCode),
                Roles = roles,
                IsAuthenticated = true,
                Experation = jwtSecurityCode.ValidTo,
            };

            return userDTO;

        }

        public async Task<AssignRoleDTO> AssignToRole(AssignToRoleDTO assignToRoleDTO)
        {
            var user = await userManager.FindByIdAsync(assignToRoleDTO.UserID);
            AssignRoleDTO result = new AssignRoleDTO();
            if (user is null)
            {
                result.Message = "Incorrect UserID";
                result.success = false;
                return result;
            }
            if (await roleManager.FindByNameAsync(assignToRoleDTO.RoleName) is null)
            {
                result.Message = "Incorrect Role Name";
                result.success = false;
                return result;
            }
            if (await userManager.IsInRoleAsync(user, assignToRoleDTO.RoleName))
            {
                result.Message = "User is already assigned to this role";
                result.success = false;
                return result;
            }

            var assignToRole = await userManager.AddToRoleAsync(user, assignToRoleDTO.RoleName);

            if (assignToRole.Succeeded)
            {
                result.Message = $"{user.UserName} is asssigned to {assignToRoleDTO.RoleName} role";
                result.success = true;
                return result;
            }
            result.Message = "Wrong";
            return result;

        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWT.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _JWT.Issuer,
                audience: _JWT.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_JWT.Duration),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

    }
}
