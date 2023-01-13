using InGameServices.Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace InGameServices.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PasswordRecoveryController : ControllerBase
    {
        private readonly IPasswordRecoveryService _passwordRecoveryService;
        public PasswordRecoveryController(IPasswordRecoveryService passwordRecoveryService)
        {
            _passwordRecoveryService = passwordRecoveryService;
        }

        [HttpPost]
        public async Task<ActionResult> Recover(string email)
        {
            try
            {
                await _passwordRecoveryService.RecoverPassword(email);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}