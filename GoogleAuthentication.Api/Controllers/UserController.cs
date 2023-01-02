using GoogleAuthentication.Api.Dto;
using GoogleAuthentication.Api.Helpers.Jwt.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoogleAuthentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IGoogleIdTokenValidationService _googleIdTokenValidationService;
        public UserController(IGoogleIdTokenValidationService googleIdTokenValidationService)
        { 
            _googleIdTokenValidationService = googleIdTokenValidationService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            Token token = await _googleIdTokenValidationService.ValidateIdTokenAsync(model);
            return Ok(token);
        }
    }
}
