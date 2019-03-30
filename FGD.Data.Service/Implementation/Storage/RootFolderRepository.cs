using FGD.Bussines.Model;
using FGD.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FGD.Data.Service
{
    public class RootFolderRepository : IRootFolderRepository<RootFolderModelBussines<int>, int>
    {
        private FakeGoogleDriveContext _context;

        public RootFolderRepository(FakeGoogleDriveContext context)
        {
            _context = context;
        }

        public async Task<RootFolderModelBussines<int>> CreateAsync(RootFolderModelBussines<int> model)
        {
            var res = await _context.RootFolders.AddAsync(
                   AutoMapperConfig.Mapper.Map<RootFolderModel<int>>(model)
               );

            _context.SaveChanges();

            return await GetByIdAsync(res.Entity.Id);
        }

        [Obsolete]
        public Task<bool> DeleteByIdAsync(int id) => throw new NotImplementedException();

        public async Task<ICollection<RootFolderModelBussines<int>>> GetAllAsync()
        {
            return AutoMapperConfig.Mapper
             .Map<List<RootFolderModel<int>>, List<RootFolderModelBussines<int>>>(
                await _context.RootFolders.ToListAsync()
             );
        }

        public async Task<RootFolderModelBussines<int>> GetByIdAsync(int id)
        {
            var root = await GetRawById(id);

            if (root == null)
                return null;

            return AutoMapperConfig.Mapper.Map<RootFolderModelBussines<int>>(root);
        }

        public async Task<RootFolderModelBussines<int>> UpdateAsync(int id, RootFolderModelBussines<int> model)
        {
            model.Id = id;

            _context.RootFolders.Update(AutoMapperConfig.Mapper.Map<RootFolderModel<int>>(model));

            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        internal async Task<RootFolderModel<int>> GetRawById(int id) =>
            await _context.RootFolders.FirstOrDefaultAsync(st => st.Id == id);

    }
}
