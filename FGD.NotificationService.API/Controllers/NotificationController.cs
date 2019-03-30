using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FGD.NotificationService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private NotifyHub _hubContext;

        public NotificationController(NotifyHub hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost("{Id}")]
        public async Task<ActionResult> Post([FromBody]NotificationModelApi<int> notification,[FromRoute] int Id)
        {
            string retMessage = string.Empty;
            try
            {
               await _hubContext.SendNotificationToSubscriber(notification, Id);
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