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
    public class SharedFileController : ControllerBase
    {
        private ISharedFileService<int, SharedFileModelApi<int>, StoredFileModelApi<int>> _sharedFileService;

        public SharedFileController(ISharedFileService<int, SharedFileModelApi<int>, StoredFileModelApi<int>> sharedFileService)
        {
            this._sharedFileService = sharedFileService;
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllByUserId([FromRoute] int userId)
        {
            var res = await this._sharedFileService.GetAllByUserId(userId);

            return Ok(new ResponseModel<ICollection<StoredFileModelApi<int>>>(res));
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ShareFile(SharedFileModelApi<int> model)
        {
            var res = await this._sharedFileService.ShareFile(model);

            return Ok(res);
        }

    }
}
