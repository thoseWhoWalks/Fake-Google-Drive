using FGD.Bussines.Model;
using FGD.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FGD.Data.Service
{
    public class SharedFolderRepository : ISharedFolderRepository<SharedFolderModelBussines<int>,int> 
    {
        private FakeGoogleDriveContext _context;

        public SharedFolderRepository(FakeGoogleDriveContext context)
        {
            _context = context;
        }

        public async Task<SharedFolderModelBussines<int>> CreateAsync(SharedFolderModelBussines<int> model)
        {
            var added = await this._context.SharedFolders.AddAsync(
                 AutoMapperConfig.Mapper.Map<SharedFolderModel<int>>(model)
              );

            await this._context.SaveChangesAsync();

            return await this.GetByIdAsync(added.Entity.Id);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var raw = await this.GetRawByIdAsync(id);

            this._context.SharedFolders.Remove(raw);

            await this._context.SaveChangesAsync();

            return true;
        }

        public Task<ICollection<SharedFolderModelBussines<int>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<SharedFolderModelBussines<int>>> GetAllByUserIdAsync(int UserId)
        {
            var res = await this._context.SharedFolders.Where(sh => sh.AccountId == UserId).ToListAsync();

            return AutoMapperConfig.Mapper.Map<List<SharedFolderModelBussines<int>>>(res);

        }

        public async Task<SharedFolderModelBussines<int>> GetByIdAsync(int id)
        {
            return AutoMapperConfig.Mapper.Map<SharedFolderModelBussines<int>>(
                  await this.GetRawByIdAsync(id)
              );
        }

        public Task<SharedFolderModelBussines<int>> UpdateAsync(int id, SharedFolderModelBussines<int> model)
        {
            throw new NotImplementedException();
        }

        internal async Task<SharedFolderModel<int>> GetRawByIdAsync(int Id) =>
           await this._context.SharedFolders.FirstOrDefaultAsync(sf => sf.Id == Id);
    }
}
