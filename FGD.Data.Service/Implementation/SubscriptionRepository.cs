using FGD.Api.Model;
using FGD.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FGD.Data.Service
{
    public class SubscriptionRepository : ISubscriptionRepository<SubscriptionModelApi<int>, int>
    {
        private FakeGoogleDriveContext _context;

        public SubscriptionRepository(FakeGoogleDriveContext context)
        {
            _context = context;
        }

        public async Task<SubscriptionModelApi<int>> CreateAsync(SubscriptionModelApi<int> model)
        {
            var res = await _context.Subscriptions.AddAsync(
                    AutoMapperConfig.Mapper.Map<SubscriptionModel<int>>(model)
                );

            _context.SaveChanges();

            return await GetByIdAsync(res.Entity.Id);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var subType = await GetRawById(id);

            subType.IsDeleted = true;

            _context.Subscriptions.Update(subType);

            await _context.SaveChangesAsync();

            return (await this.GetRawById(id)).IsDeleted;
        }

        public async Task<ICollection<SubscriptionModelApi<int>>> GetAllAsync()
        {
            return AutoMapperConfig.Mapper
                .Map<List<SubscriptionModel<int>>, List<SubscriptionModelApi<int>>>(
                   await _context.Subscriptions.ToListAsync()
                );
        }

        public async Task<SubscriptionModelApi<int>> GetByIdAsync(int id)
        {

            var subType = await GetRawById(id);

            if (subType == null)
                return null;

            return AutoMapperConfig.Mapper.Map<SubscriptionModelApi<int>>(subType);

        }

        public async Task<SubscriptionModelApi<int>> GetByTitleAsync(string title)
        {
            var sub = await _context.Subscriptions.FirstOrDefaultAsync(i => i.Title == title);

            return AutoMapperConfig.Mapper.Map<SubscriptionModelApi<int>>(sub);
        }

        public async Task<SubscriptionModelApi<int>> UpdateAsync(int id, SubscriptionModelApi<int> model)
        {
            model.Id = id;

            _context.Subscriptions.Update(AutoMapperConfig.Mapper.Map<SubscriptionModel<int>>(model));

            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        internal async Task<SubscriptionModel<int>> GetRawById(int id) => await _context.Subscriptions.FirstOrDefaultAsync(st => st.Id == id);

    }
}
