using AutoMapper;
using hamituslukan.PaymentSystem.Dto.Concrete;
using hamituslukan.PaymentSystem.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace hamituslukan.PaymentSystem.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager, IConfiguration configuration, IMapper mapper)
        {
            _userManager = userManager;
            _signManager = signManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(ApplicationUserDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user != null)
            {
                var checkPassword = await _signManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (checkPassword.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    var token = GenerateAccessToken(DateTime.Now.AddMinutes(60), user.UserName, roles.FirstOrDefault());

                    return Ok(token);
                }

                return BadRequest("Invalid credentials");
            }

            return BadRequest("Invalid credentials");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(ApplicationUserDto request)
        {
            var user = new ApplicationUser { Email = request.Email, Name = request.Name, UserName = request.Email };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");

                return Ok("Successfully registered");
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        public async Task<IActionResult> CurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user != null)
            {
                var mapped = _mapper.Map<ApplicationUserDto>(user);

                var roles = await _userManager.GetRolesAsync(user);

                mapped.Roles = string.Join(", ", roles);

                return Ok(mapped);
            }

            return BadRequest("Current user not found");
        }

        [HttpGet]
        public async Task<IActionResult> CurrentRoles()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);

                return Ok(roles);
            }

            return BadRequest("Current user not found");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CheckAdminRole()
        {
            return Ok("You have admin role !");
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CheckUserRole()
        {
            return Ok("You have user role !");
        }

        private string GenerateAccessToken(DateTime tokenExpiration, string userName, string role)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                expires: tokenExpiration,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials,
                claims: new Claim[] { new Claim(ClaimTypes.Name, userName), new Claim(ClaimTypes.Role, role) }
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(securityToken);
        }
    }
}