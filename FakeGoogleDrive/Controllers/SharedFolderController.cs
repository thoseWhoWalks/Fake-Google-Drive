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
    public class SharedFolderController : ControllerBase
    {
        private ISharedFolderService<int, SharedFolderModelApi<int>, StoredFolderModelApi<int>> _sharedFolderService;

        public SharedFolderController(ISharedFolderService<int, SharedFolderModelApi<int>, StoredFolderModelApi<int>> sharedFolderService)
        {
            this._sharedFolderService = sharedFolderService;
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllByUserId([FromRoute]int userId)
        {
            var res = await this._sharedFolderService.GetAllByUserId(userId);

            return Ok(new ResponseModel<ICollection<StoredFolderModelApi<int>>>(res));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> StoredFile(SharedFolderModelApi<int> model)
        {
            var res = await this._sharedFolderService.ShareFolder(model);

            return Ok(res);
        }
    }
}
