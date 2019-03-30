using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FGD.NotificationService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private NotifyHub _hubContext;

        public SubscriptionController(NotifyHub hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost("{Id}")]
        public async Task<ActionResult> Post([FromBody]SubscriptionUpdateModelApi<int> msg, [FromRoute] int Id)
        {
            string retMessage = string.Empty;
            try
            {
                await _hubContext.SendSubscriptionUpdateToSubscriber(msg, Id);
                retMessage = "Success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }
            return Ok(retMessage);
        }
    }
}
