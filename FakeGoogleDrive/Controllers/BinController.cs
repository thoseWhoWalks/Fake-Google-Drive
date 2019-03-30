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
    public class BinController : ControllerBase
    {
        private IBinService<int,StoredFileModelApi<int>,StoredFolderModelApi<int>> _binService; 

        public BinController(IBinService<int, StoredFileModelApi<int>, StoredFolderModelApi<int>> binService)
        {
            this._binService = binService;
        }

        [Authorize]
        [HttpGet("RecoverFolderById/{Id}")]
        public async Task<IActionResult> RrestoreFolderByIdAsync([FromRoute]int Id)
        {
            var res = await this._binService.RestoreFolderAsync(Id);

            return Ok(new ResponseModel<StoredFolderModelApi<int>>(res));
        }

        [Authorize]
        [HttpGet("RecoverFileById/{Id}")]
        public async Task<IActionResult> RestoreFileByIdAsync([FromRoute]int Id)
        {
            var res = await this._binService.RestoreFileAsync(Id);

            return Ok(new ResponseModel<StoredFileModelApi<int>>(res));
        }

        [Authorize]
        [HttpDelete("DeleteFileForeverById/{Id}")]
        public async Task<IActionResult> DeleteFileForeverByIdAsync([FromRoute]int Id)
        {
            var res = await this._binService.DeleteFileForeverAsync(Id, Convert.ToInt32(User.Identity.Name));

            return Ok(new ResponseModel<StoredFileModelApi<int>>(res));
        }

        [Authorize]
        [HttpDelete("DeleteFolderForeverById/{Id}")]
        public async Task<IActionResult> DeleteFolderForeverByIdAsync([FromRoute]int Id)
        {
            var res = await this._binService.DeleteFolderForeverAsync(Id, Convert.ToInt32(User.Identity.Name));

            return Ok(new ResponseModel<StoredFolderModelApi<int>>(res));
        }

    }
}
