using Microsoft.AspNetCore.Mvc;

namespace CompucorVtas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request.Email == "admin@admin.com" && request.Password == "1234")
            {
                // Simulación de token
                return Ok(new { token = "fake-jwt-token" });
            }

            return Unauthorized(new { message = "Credenciales inválidas" });
        }
    }

    public class LoginRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
