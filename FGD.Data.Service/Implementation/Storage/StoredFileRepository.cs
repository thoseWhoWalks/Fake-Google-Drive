using FGD.Bussines.Model;
using FGD.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FGD.Data.Service
{
    public class StoredFileRepository : IStoredFileRepository<StoredFileModelBussines<int>, int>
    {
        private FakeGoogleDriveContext _context;

        public StoredFileRepository(FakeGoogleDriveContext context)
        {
            _context = context;
        }

        public async Task<StoredFileModelBussines<int>> CreateAsync(StoredFileModelBussines<int> model)
        {
            
            var added = await this._context.StoredFiles.AddAsync(
                   AutoMapperConfig.Mapper.Map<StoredFileModel<int>>(model)
                );

            await this._context.SaveChangesAsync();

            return await this.GetByIdAsync(added.Entity.Id);

        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var raw = await this.GetRawByIdAsync(id);

            raw.IsDeleted = true;

            this._context.StoredFiles.Update(raw);

            await this._context.SaveChangesAsync();

            return raw.IsDeleted;
        }

        public async Task DeleteForeverById(int Id)
        {
            var raw = await this.GetRawByIdAsync(Id);

            this._context.StoredFiles.Remove(raw);

            await this._context.SaveChangesAsync();
        }

        public async Task<ICollection<StoredFileModelBussines<int>>> GetAllAsync()
        {
            var raw = await this._context.StoredFiles.ToListAsync();

            return AutoMapperConfig.Mapper.Map<ICollection<StoredFileModelBussines<int>>>(raw);
        }

        public async Task<ICollection<StoredFileModelBussines<int>>> GetAllDeletedAsync()
        {
            var res = await this._context.StoredFiles.Where(sf => sf.IsDeleted == true).ToListAsync();

            return AutoMapperConfig.Mapper.Map<List<StoredFileModelBussines<int>>>(res);
        }

        public async Task<StoredFileModelBussines<int>> GetByIdAsync(int id)
        {
            return AutoMapperConfig.Mapper.Map<StoredFileModelBussines<int>>(
                    await this.GetRawByIdAsync(id)
                );
        }

        public async Task<ICollection<StoredFileModelBussines<int>>> GetByParentFolderIdAsync(int Id)
        {
            var raw = await this._context.StoredFiles.Where(f => f.StoredFolderId == Id).ToListAsync();

            return AutoMapperConfig.Mapper.Map<ICollection<StoredFileModelBussines<int>>>(raw);
        }

        public async Task<ICollection<StoredFileModelBussines<int>>> GetByRootIdAsync(int Id)
        {
            var raw = await this._context.StoredFiles.Where(f=>f.RootFolderId==Id).ToListAsync();

            return AutoMapperConfig.Mapper.Map<ICollection<StoredFileModelBussines<int>>>(raw);
        }

        public async Task<StoredFileModelBussines<int>> UpdateAsync(int id, StoredFileModelBussines<int> model)
        {
            var raw = await this.GetRawByIdAsync(id);
             
            raw.Title = model.Title ?? raw.Title;
            
            raw.ThumbnailPath = model.ThumbnailPath ?? raw.ThumbnailPath;

            raw.RootFolderId = model.RootFolderId ?? raw.RootFolderId;

            raw.StoredFolderId = model.StoredFolderId ?? raw.StoredFolderId;

            raw.IsDeleted = model.IsDeleted;

            if (default(Int32) != model.SizeInKbs)
                raw.SizeInKbs = model.SizeInKbs;

            var updated = this._context.StoredFiles.Update(raw);

            await this._context.SaveChangesAsync();

            return AutoMapperConfig.Mapper.Map<StoredFileModelBussines<int>>(updated.Entity);
             
        }

        internal async Task<StoredFileModel<int>> GetRawByIdAsync(int Id) => 
            await this._context.StoredFiles.FirstOrDefaultAsync(sf => sf.Id == Id);
        
    }
}
