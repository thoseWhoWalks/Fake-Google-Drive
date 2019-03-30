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
    public class StoredFolderController : ControllerBase
    {

        private IStoredFolderService<StoredFolderModelApi<int>, int> _storedFolderService;

        public StoredFolderController(IStoredFolderService<StoredFolderModelApi<int>, int> storedFolderService)
        {
            this._storedFolderService = storedFolderService;
        }

        [Authorize]
        [HttpGet("GetByRootByUserId/{userId}")]
        public async Task<IActionResult> GetRootByUserIdAsync([FromRoute]int userId)
        {
            var res = await this._storedFolderService.GetRootByUserIdAsync(userId);

            return Ok(new ResponseModel<ICollection<StoredFolderModelApi<int>>>(res));
        }

        [Authorize]
        [HttpGet("GetByParentId/{parentId}")]
        public async Task<IActionResult> GetByParentIdAsync([FromRoute]int parentId)
        {
            var res = await this._storedFolderService.GetByParentIdAsync(parentId);

            return Ok(new ResponseModel<ICollection<StoredFolderModelApi<int>>>(res));
        }

        [Authorize]
        [HttpPost("{userId}")]
        public async Task<IActionResult> Create([FromBody]StoredFolderModelApi<int> folder, [FromRoute]int userId)
        {

            var res = await this._storedFolderService.CreateAsync(folder, userId);

            return Ok(new ResponseModel<StoredFolderModelApi<int>>(res));
        }

        [Authorize]
        [HttpPut("{Id}")]
        public async Task<IActionResult> Update([FromBody]StoredFolderModelApi<int> folder,int Id)
        {
            var res = await this._storedFolderService.UpdateAsync(Id, folder);

            return Ok(new ResponseModel<StoredFolderModelApi<int>>(res));
        }

        [Authorize]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            var res = await this._storedFolderService.DeleteAsync(Id);

            return Ok(new ResponseModel<bool>(res));
        }

        [Authorize]
        [HttpGet("GetDeleted/{Id}")]
        public async Task<IActionResult> GetDeleted([FromRoute] int Id)
        {

            var res = await this._storedFolderService.GetDeletedByUserId(Id);

            return Ok(new ResponseModel<ICollection<StoredFolderModelApi<int>>>(res));
        }
    }
}
