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
    public class StoredFileController : ControllerBase
    {

        private IStoredFileService<StoredFileModelApi<int>, int> _storedFileService;

        public StoredFileController(IStoredFileService<StoredFileModelApi<int>, int> storedFolderService)
        {
            this._storedFileService = storedFolderService;
        }

        [Authorize]
        [HttpGet("GetByRootByUserId/{userId}")]
        public async Task<IActionResult> GetRootByUserIdAsync([FromRoute]int userId)
        {
            var res = await this._storedFileService.GetRootByUserIdAsync(userId);

            return Ok(new ResponseModel<ICollection<StoredFileModelApi<int>>>(res));
        }

        [Authorize]
        [HttpGet("GetByStoredFolderId/{parentId}")]
        public async Task<IActionResult> GetByParentIdAsync([FromRoute]int parentId)
        {
            var res = await this._storedFileService.GetByStoredFolderIdAsync(parentId);

            return Ok(new ResponseModel<ICollection<StoredFileModelApi<int>>>(res));
        }

        [Authorize]
        [DisableRequestSizeLimit]
        [HttpPost("{userId}")]
        public async Task<IActionResult> Create([FromForm]StoredFileModelApi<int> file, [FromRoute]int userId)
        {
            var res = await this._storedFileService.CreateAsync(file, userId);

            return Ok(new ResponseModel<StoredFileModelApi<int>>(res));
        }

        [Authorize]
        [HttpPut("{Id}")]
        public async Task<IActionResult> Update([FromBody]StoredFileModelApi<int> file, int Id)
        {
            var res = await this._storedFileService.UpdateAsync(Id, file);

            return Ok(new ResponseModel<StoredFileModelApi<int>>(res));
        }

        [Authorize]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            var res = await this._storedFileService.DeleteAsync(Id);

            return Ok(new ResponseModel<bool>(res));
        }

        [Authorize]
        [HttpGet("Download/{Id}")]
        public async Task<IActionResult> DownloadById([FromRoute] int Id)
        {
            var res = await this._storedFileService.DownloadById(Id);

            return File(res.FileStream, res.ContentType.ToString(), res.Title);
        }

        [Authorize]
        [HttpGet("GetDeleted/{Id}")]
        public async Task<IActionResult> GetDeleted([FromRoute] int Id)
        {
            var res = await this._storedFileService.GetDeletedByUserId(Id);

            return Ok(new ResponseModel<ICollection<StoredFileModelApi<int>>>(res));
        }

    }
}
