using Microsoft.AspNetCore.Mvc;
using Path2Grad.Application.Dtos;
using Path2Grad.Application.Interfaces.Services;

namespace Path2Grad.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            var result = await _accountService.LoginAsync(request.Email, request.Password, request.Role);
            if (result == null)
                return Unauthorized("Invalid email, password, or role");

            return Ok(result);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete(".AspNetCore.Application.Id");
            return Ok(new { message = "Logged out successfully" });
        }
    }
}
