using ManageEmployeApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ManageEmployeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthSrivces _authSrivces;
        public AuthController(IAuthSrivces authSrivces)
        {
            _authSrivces = authSrivces;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromQuery] string username, [FromQuery] string password)
        {
            if (username != "admin" || password != "password")
            {
                return Unauthorized();
            }

            var token = _authSrivces.GenerateToken(username);
            return Ok(new { Token = token });
        }


    }
}




