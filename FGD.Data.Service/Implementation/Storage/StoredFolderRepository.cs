using FGD.Bussines.Model;
using FGD.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FGD.Data.Service
{
    public class StoredFolderRepository : IStoredFolderRepository<StoredFolderModelBussines<int>, int>
    {

        private FakeGoogleDriveContext _context;

        public StoredFolderRepository(FakeGoogleDriveContext context)
        {
            _context = context;
        }

        public async Task<StoredFolderModelBussines<int>> CreateAsync(StoredFolderModelBussines<int> model)
        {
            var mappedModel = AutoMapperConfig.Mapper.Map<StoredFolderModel<int>>(model);

            var savedModel = await this._context.StoredFolders.AddAsync(mappedModel);

            await this._context.SaveChangesAsync();

            return await this.GetByIdAsync(savedModel.Entity.Id);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var raw = await this.GetRawByIdAsync(id);

            raw.IsDeleted = true;

            var updated = await this.UpdateAsync(id, AutoMapperConfig.Mapper.Map<StoredFolderModelBussines<int>>(raw));

            return updated.IsDeleted;
        }

        public async Task DeleteForeverByIdAsync(int Id)
        {
            var raw = await this.GetRawByIdAsync(Id);

            this._context.StoredFolders.Remove(raw);

            await this._context.SaveChangesAsync();
        }

        public async Task<ICollection<StoredFolderModelBussines<int>>> GetAllAsync()
        {
            return AutoMapperConfig.Mapper
            .Map<List<StoredFolderModel<int>>, List<StoredFolderModelBussines<int>>>(
               await _context.StoredFolders.ToListAsync()
            );
        }

        public async Task<ICollection<StoredFolderModelBussines<int>>> GetAllByPredicate()
        {
            var res = await this._context.StoredFolders.Where(sf=>sf.IsDeleted==true).ToListAsync();

            return AutoMapperConfig.Mapper.Map<List<StoredFolderModelBussines<int>>>(res);
        }

        public Task<ICollection<StoredFolderModelBussines<int>>> GetAllDeletedAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<StoredFolderModelBussines<int>> GetByIdAsync(int id)
        {
            var folder = await GetRawByIdAsync(id);

            if (folder == null)
                return null;

            return AutoMapperConfig.Mapper.Map<StoredFolderModelBussines<int>>(folder);
        }

        public async Task<ICollection<StoredFolderModelBussines<int>>> GetByParentFolderIdAsync(int Id)
        {
            var raw = await this._context.StoredFolders.Where(
                  f => f.StoredFolderId == Id
              ).ToListAsync();

            return AutoMapperConfig.Mapper.Map<List<StoredFolderModelBussines<int>>>(raw);
        }

        public async Task<ICollection<StoredFolderModelBussines<int>>> GetByRootIdAsync(int Id)
        {
            var raw = await this._context.StoredFolders.Where(
                    f => f.RootFolderId == Id  
                ).ToListAsync();

            return AutoMapperConfig.Mapper.Map<List<StoredFolderModelBussines<int>>>(raw);
        }

        public async Task<StoredFolderModelBussines<int>> UpdateAsync(int id, StoredFolderModelBussines<int> model)
        {
            var raw = await this.GetRawByIdAsync(id);

            raw.IsDeleted = model.IsDeleted;

            raw.Path = model.Path??raw.Path;

            raw.Size = model.Size;

            raw.Title = model.Title??raw.Title;

            raw.StoredFolderId = model?.StoredFolderId;

            raw.RootFolderId = (model.RootFolderId==null&& raw.StoredFolderId==null)?raw.RootFolderId:model.RootFolderId;

            _context.StoredFolders.Update(raw);

            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        internal async Task<StoredFolderModel<int>> GetRawByIdAsync(int id) =>
          await _context.StoredFolders.FirstOrDefaultAsync(st => st.Id == id);
    }
}
