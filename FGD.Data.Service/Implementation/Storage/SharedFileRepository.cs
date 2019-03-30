using FGD.Bussines.Model;
using FGD.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FGD.Data.Service
{
    public class SharedFileRepository : ISharedFileRepository<SharedFileModelBussines<int>, int>
    {
        private FakeGoogleDriveContext _context;

        public SharedFileRepository(FakeGoogleDriveContext context)
        {
            _context = context;
        }

        public async Task<SharedFileModelBussines<int>> CreateAsync(SharedFileModelBussines<int> model)
        {
            var added = await this._context.SharedFiles.AddAsync(
                 AutoMapperConfig.Mapper.Map<SharedFileModel<int>>(model)
              );

            await this._context.SaveChangesAsync();

            return await this.GetByIdAsync(added.Entity.Id);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var raw = await this.GetRawByIdAsync(id);

            this._context.SharedFiles.Remove(raw);

            await this._context.SaveChangesAsync();

            return true;
        }

        public Task<ICollection<SharedFileModelBussines<int>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<SharedFileModelBussines<int>>> GetAllByUserIdAsync(int UserId)
        {
            var res = await this._context.SharedFiles.Where(sh => sh.AccountId == UserId).ToListAsync();

            return AutoMapperConfig.Mapper.Map<List<SharedFileModelBussines<int>>>(res);

        }

        public async Task<SharedFileModelBussines<int>> GetByIdAsync(int id)
        {
            return AutoMapperConfig.Mapper.Map<SharedFileModelBussines<int>>(
                  await this.GetRawByIdAsync(id)
              );
        }

        public Task<SharedFileModelBussines<int>> UpdateAsync(int id, SharedFileModelBussines<int> model)
        {
            throw new NotImplementedException();
        }

        internal async Task<SharedFileModel<int>> GetRawByIdAsync(int Id) =>
           await this._context.SharedFiles.FirstOrDefaultAsync(sf => sf.Id == Id);
    }
}
