using InGameServices.Application.Services.Abstractions;
using InGameServices.Models.Auth.Messages.Request;
using Microsoft.AspNetCore.Mvc;

namespace InGameServices.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult> Authenticate([FromBody] AuthRequest request)
        {
            try
            {
                var result = await _authService.Authenticate(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}