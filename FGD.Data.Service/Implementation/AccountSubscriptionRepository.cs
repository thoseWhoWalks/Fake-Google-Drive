using FGD.Bussines.Model;
using FGD.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FGD.Data.Service
{
    public class AccountSubscriptionRepository : IAccountSubscriptionRepository<AccountSubscriptionModelBussines<int>, int>
    {
        private FakeGoogleDriveContext _context;

        public AccountSubscriptionRepository(FakeGoogleDriveContext context)
        {
            _context = context;
        }

        public async Task<AccountSubscriptionModelBussines<int>> CreateAsync(AccountSubscriptionModelBussines<int> model)
        {
            var res = await _context.AccountSubscriptions.AddAsync(
            AutoMapperConfig.Mapper.Map<AccountSubscriptionModel<int>>(model)
                );

            await _context.SaveChangesAsync();

            return await GetByUserIdAsync(res.Entity.AccountId);
        }

        [Obsolete]
        public Task<bool> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<AccountSubscriptionModelBussines<int>>> GetAllAsync()
        {
            return AutoMapperConfig.Mapper.Map<List<AccountSubscriptionModelBussines<int>>>(
                    await _context.AccountSubscriptions.ToListAsync()
                );
        }

        [Obsolete]
        public Task<AccountSubscriptionModelBussines<int>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountSubscriptionModelBussines<int>> GetByUserIdAsync(int id)
        {
            var raw = await GetRawByUserIdAsync(id);

            return AutoMapperConfig.Mapper.Map<AccountSubscriptionModelBussines<int>>(raw);
        }

        public async Task<AccountSubscriptionModelBussines<int>> GetByUserEmailAsync(string email)
        {
            var user = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == email);

            var raw = await _context.AccountSubscriptions.FirstOrDefaultAsync(a => a.AccountId == user.Id);

            return AutoMapperConfig.Mapper.Map<AccountSubscriptionModelBussines<int>>(raw);
        }

        public async Task<AccountSubscriptionModelBussines<int>> UpdateAsync(int id, AccountSubscriptionModelBussines<int> model)
        {
            var accSubb = await GetRawByUserIdAsync(model.AccountId);

            accSubb.IsActive = model.IsActive;

            accSubb.TakenSpace = model.TakenSpace;

            _context.AccountSubscriptions.Update(accSubb);

            await _context.SaveChangesAsync();

            return await GetByUserIdAsync(accSubb.AccountId);
        }

        internal async Task<AccountSubscriptionModel<int>> GetRawByUserIdAsync(int id)
        {
            return await _context.AccountSubscriptions.FirstOrDefaultAsync(a => a.AccountId == id);
        }
    }
}
