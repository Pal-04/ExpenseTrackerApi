using ExpenseTrackerApi.DTOs;
using ExpenseTrackerApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IConfiguration _config;

        public AuthController(AuthService authService, IConfiguration config)
        {
            _authService = authService;
            _config = config;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            var result = _authService.Register(dto.Email, dto.Password);

            return Ok(result);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _authService.Login(dto.Email, dto.Password);

            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var token = _authService.GenerateToken(user, _config);

            return Ok(new { Token = token });
        }
    }
}
