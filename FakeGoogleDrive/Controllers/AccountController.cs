using System.Threading.Tasks;
using FGD.Api.Model;
using FGD.Bussines.Service;
using Microsoft.AspNetCore.Mvc;

namespace FakeGoogleDrive.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService<AccountModelApi<int>, int> _accountService;

        public AccountController(IAccountService<AccountModelApi<int>, int> accountService)
        {
            _accountService = accountService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await this._accountService.GetAllAsync();

            return Ok(res);
        }

        [HttpGet("GetByEmail/{email}")]
        public async Task<IActionResult> GetAsync([FromRoute]string email)
        {
            var res = await _accountService.GetUserByEmailAsync(email);

            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> SignUpAsync([FromBody]AccountModelApi<int> model)
        {
            var res = await _accountService.RegisterUserAsync(model);

            return Ok(res);
        }
    }
}
