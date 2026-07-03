using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Path2Grad.Application.Interfaces.Repositories;
using Path2Grad.Application.Interfaces.Services;

namespace Path2Grad.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly ISupervisorRepository _supervisorRepo;
        private readonly IRepository<Domain.Entities.ProjectsAdmin> _adminRepo;
        private readonly IConfiguration _configuration;

        public AccountService(
            IStudentRepository studentRepo,
            ISupervisorRepository supervisorRepo,
            IRepository<Domain.Entities.ProjectsAdmin> adminRepo,
            IConfiguration configuration)
        {
            _studentRepo = studentRepo;
            _supervisorRepo = supervisorRepo;
            _adminRepo = adminRepo;
            _configuration = configuration;
        }

        public async Task<object?> LoginAsync(string email, string password, string role)
        {
            if (role == "Student")
            {
                var student = await _studentRepo.GetByEmailAsync(email);
                if (student != null && student.StudentPassword == password)
                {
                    var token = await GenerateJwtTokenAsync(student.StudentEmail, "Student");
                    return new { token };
                }
            }
            else if (role == "Doctor" || role == "TeachingAssistant")
            {
                var staff = await _supervisorRepo.GetByEmailAsync(email);
                if (staff != null && staff.SupervisorPassword == password && staff.Position == role)
                {
                    var token = await GenerateJwtTokenAsync(staff.SupervisorEmail, staff.Position);
                    return new { token };
                }
            }
            else if (role == "ProjectsAdmin")
            {
                var admins = await _adminRepo.FindAsync(x => x.AdminEmail == email);
                var admin = admins.FirstOrDefault();
                if (admin != null && admin.AdminPassword == password)
                {
                    var token = await GenerateJwtTokenAsync(admin.AdminEmail, "ProjectsAdmin");
                    return new { token };
                }
            }

            return null;
        }

        public Task<string> GenerateJwtTokenAsync(string email, string role)
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

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
