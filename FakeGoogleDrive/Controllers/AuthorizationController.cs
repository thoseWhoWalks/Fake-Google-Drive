using FGD.Api.Model;
using FGD.Bussines.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FGD.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthtorizationController : ControllerBase
    {
        private IAuthJWTService _authJWTService;

        public AuthtorizationController(IAuthJWTService authJWTService)
        {
            _authJWTService = authJWTService;
        }

        [HttpPost]
        public async Task<IActionResult> Authorize([FromBody]LoginModelApi model)
        {
            var res = await _authJWTService.GetTokenResponseAsync(model);

            return Ok(res);
        }
    }
}
