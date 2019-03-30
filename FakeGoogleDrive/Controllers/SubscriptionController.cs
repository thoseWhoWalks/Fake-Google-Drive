using FGD.Api.Model;
using FGD.Bussines.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FGD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        ISubscriptionService<SubscriptionModelApi<int>,int> _subscriptionService;

        public SubscriptionController(ISubscriptionService<SubscriptionModelApi<int>, int> subscriptionService)
        {
            this._subscriptionService = subscriptionService;
        }

        [Authorize]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute]int Id)
        {
            var sub = await this._subscriptionService.GetByIdAsync(Id);

            return Ok(new ResponseModel<SubscriptionModelApi<int>>(sub));
        }
    }
}
