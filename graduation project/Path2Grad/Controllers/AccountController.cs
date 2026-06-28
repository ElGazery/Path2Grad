using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Path2Grad.Dtos;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Path2Grad.Models;

namespace Path2Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public AccountController(ApplicationDbContext context, IConfiguration configuration)
        { 
            _context = context;
            _configuration= configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            if (request.Role == "Student")
            {
                var student = await _context.Students
                    .FirstOrDefaultAsync(std => std.StudentEmail == request.Email);

                if (student != null && student.StudentPassword == request.Password)
                {
                    var token = GenerateJwtToken(student.StudentEmail, "Student");
                    return Ok(new { token });
                }
            }


            else if (request.Role == "Doctor" || request.Role == "TeachingAssistant")
            {
                var staff = await _context.Supervisors
                    .FirstOrDefaultAsync(super => super.SupervisorEmail == request.Email && super.Position == request.Role);

                if (staff != null && staff.SupervisorPassword == request.Password)
                {
                    var token = GenerateJwtToken(staff.SupervisorEmail, staff.Position); // Position = "Doctor" or "TeachingAssistant"
                    return Ok(new { token });
                }
            }

            else if (request.Role == "ProjectsAdmin")
            {
                var admin = await _context.ProjectsAdmins
                    .FirstOrDefaultAsync(x => x.AdminEmail == request.Email);

                if (admin != null && admin.AdminPassword == request.Password)
                {
                    var token = GenerateJwtToken(admin.AdminEmail, "ProjectsAdmin");
                    return Ok(new { token });
                }
            }

            return Unauthorized("Invalid email, password, or role");
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete(".AspNetCore.Application.Id");

            return Ok(new { message = "Logged out successfully" });
        }
        private string GenerateJwtToken(string email, string role)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
           
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
