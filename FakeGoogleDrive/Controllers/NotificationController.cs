using FGD.Api.Model;
using FGD.Bussines.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FGD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        INotificationService<NotificationModelApi<int>,int> _notificationService;

        public NotificationController(INotificationService<NotificationModelApi<int>, int> notificationService)
        {
            this._notificationService = notificationService;
        }

        [Authorize]
        [HttpGet("GetAllByUserId/{Id}")]
        public async Task<IActionResult> GetAsync([FromRoute]int Id)
        {
            var res = await _notificationService.GetAllByUserId(Id);

            return Ok(new ResponseModel<ICollection<NotificationModelApi<int>>>(res));
        }

        [HttpPut("MarkAsRead/{Id}")]
        public async Task<IActionResult> MarkAsRead([FromRoute]int Id)
        {
            var res = await _notificationService.UpdateState(Id);

            return Ok(new ResponseModel<NotificationModelApi<int>>(res));
        }

    }
}
