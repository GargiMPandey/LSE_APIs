using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LSE.Core.DTO;
using LSE.Core;
using LSE.Core.ServiceContract; 

namespace LSE.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IBrokerService _brokerService;
        public AuthController(IBrokerService brokerService)
        {
            _brokerService = brokerService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var response = await _brokerService.Login(loginRequest);
            if (response == null)
            {
                return Unauthorized();
            }
            return Ok(response);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var response = await _brokerService.Register(registerRequest);
            if (response == null)
            {
                return BadRequest("Registration failed");
            }
            return Ok(response);
        }
    }
}
