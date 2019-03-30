using FGD.Api.Model;
using FGD.Bussines.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FGD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountSubscriptionController : ControllerBase
    {
        private IAccountSubscriptionService<AccountSubscriptionModelApi<int>, int> _accountSubscriptionService;

        public AccountSubscriptionController(IAccountSubscriptionService<AccountSubscriptionModelApi<int>, int> accountSubscriptionService)
        {
            _accountSubscriptionService = accountSubscriptionService;
        }

        [Authorize]
        [HttpGet("SubscribeUser/{subscriptionId}")]
        public async Task<IActionResult> SubscribeUser([FromRoute]int subscriptionId)
        {
            var res = await _accountSubscriptionService.SubscribeUserAsync(subscriptionId, User.Identity);

            return Ok(new ResponseModel<AccountSubscriptionModelApi<int>>(res));

        }

        [Authorize]
        [HttpGet("GetByUserId/{Id}")]
        public async Task<IActionResult> GetAccountSubscriptionByUserId([FromRoute]int Id)
        {
            var accSub = await this._accountSubscriptionService.GetByUserIdAsync(Id);

            return Ok(new ResponseModel<AccountSubscriptionModelApi<int>>(accSub));
        }
    }
}
