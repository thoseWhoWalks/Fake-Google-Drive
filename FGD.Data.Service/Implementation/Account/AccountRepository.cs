using FGD.Api.Model;
using FGD.Bussines.Model;
using FGD.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace FGD.Data.Service
{
    public class AccountRepository : IAccountRepository<AccountModelBussines<int>, int>
    {
        private FakeGoogleDriveContext _context;

        private IAccountCryptoRepository<AccountCryptoModelBussines<int>, int> _accountCryptoRepository;

        public AccountRepository(FakeGoogleDriveContext context,
            IAccountCryptoRepository<AccountCryptoModelBussines<int>, int> accountCryptoRepository)
        {
            this._context = context;
            this._accountCryptoRepository = accountCryptoRepository;
        }

        public async Task<AccountModelBussines<int>> CreateAsync(AccountModelBussines<int> model)
        {

            var account = await this.GetByEmailAsync(model.Email);

            if (account != null)
                return null;

            var acc = await CreateAccountAsync(model);

            await CreateAccountInfoAsync(model, acc);

            return await GetByIdAsync(acc.Entity.Id);

        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var accAccInfo = await GetRawByIdAsync(id);
            var acc = accAccInfo.Item1;

            acc.IsDeleted = true;

            _context.Accounts.Update(acc);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ICollection<AccountModelBussines<int>>> GetAllAsync()
        {
            IList<AccountModelBussines<int>> accountApiModels = new List<AccountModelBussines<int>>();
            var accounts = await _context.Accounts.ToListAsync();
            var accountInfos = await _context.AccountInfos.ToListAsync();
            
            foreach (var acc in accounts)
            {
                var accInfo = accountInfos.FirstOrDefault(ai => ai.AccountId == acc.Id);

                accountApiModels.Add(AutoMapperConfig.Mapper.
                            Map<AccountModelBussines<int>>(accInfo)
                            .Map(acc)
                        );
            }

            return accountApiModels;
        }

        public async Task<AccountModelBussines<int>> GetByEmailAsync(string email)
        {
            var acc = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == email);

            if (acc == null)
                return null;

            var accInfo = await _context.AccountInfos.FirstOrDefaultAsync(ai => ai.AccountId == acc.Id);

            return AutoMapperConfig.Mapper.Map<AccountModelBussines<int>>(accInfo).Map(acc);
        }

        public async Task<Tuple<AccountModel<int>, AccountInfoModel<int>>> GetRawByEmailAsync(string email)
        {
            var acc = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == email);
            var accInfo = await _context.AccountInfos.FirstOrDefaultAsync(ai => ai.AccountId == acc.Id);
            
            return Tuple.Create(acc, accInfo);
        }

        public async Task<AccountModelBussines<int>> GetByIdAsync(int id)
        {
            var acc = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
            var accInfo = await _context.AccountInfos.FirstOrDefaultAsync(ai => ai.AccountId == acc.Id);

            return AutoMapperConfig.Mapper.Map<AccountModelBussines<int>>(accInfo).Map(acc);

        }
         
        public async Task<AccountModelBussines<int>> UpdateAsync(int id, AccountModelBussines<int> model)
        {
            var accAccInfo = await GetRawByIdAsync(id);
            var acc = accAccInfo.Item1;
            var accInfo = accAccInfo.Item2;

            updateAcoountModel(model, acc);

            updateAccountInfoModel(model, accInfo);

            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);

        }

        public async Task<bool> VerifyUserAsync(LoginModelApi model)
        {
            var accountAccountInfo = await GetRawByEmailAsync(model.Email);

            if (accountAccountInfo == null)
                return false;

            bool res = Crypto.VerifyHashedPassword(accountAccountInfo.Item1.PasswordHash,
                model.Password + accountAccountInfo.Item1.Salt);

            if (res)
                return true;

            return false;

        }

        private async Task CreateAccountInfoAsync(AccountModelBussines<int> model, EntityEntry<AccountModel<int>> acc)
        {
            var accInfo = AutoMapperConfig.Mapper.Map<AccountInfoModel<int>>(model);

            accInfo.AccountId = acc.Entity.Id;

            await _context.AccountInfos.AddAsync(accInfo);
            await _context.SaveChangesAsync();
        }

        private async Task<EntityEntry<AccountModel<int>>> CreateAccountAsync(AccountModelBussines<int> model)
        {
            var salt = Crypto.GenerateSalt();

            var accMapped = AutoMapperConfig.Mapper.Map<AccountModel<int>>(model);

            accMapped.PasswordHash = Crypto.HashPassword(model.Password + salt);
            accMapped.Salt = salt;

            var acc = await _context.Accounts.AddAsync(accMapped);
            await _context.SaveChangesAsync();
            return acc;
        }
        
        internal async Task<Tuple<AccountModel<int>, AccountInfoModel<int>>> GetRawByIdAsync(int id)
        {
            var acc = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
            var accInfo = await _context.AccountInfos.FirstOrDefaultAsync(ai => ai.AccountId == acc.Id);

            return Tuple.Create(acc, accInfo);
        }

        private void updateAccountInfoModel(AccountModelBussines<int> model, AccountInfoModel<int> accInfo)
        {
            accInfo.Age = model.Age;
            accInfo.FirstName = model.FirstName;
            accInfo.LastName = model.LastName;

            _context.AccountInfos.Update(accInfo);
        }

        private void updateAcoountModel(AccountModelBussines<int> model, AccountModel<int> acc)
        {
            if (model.Email != null)
                acc.Email = model.Email;

            if (model.Role != null)
                acc.Role = model.Role;

            if (model.Password != null)
            {
                acc.Salt = Crypto.GenerateSalt();
                acc.PasswordHash = Crypto.HashPassword(model.Password);
            }

            _context.Accounts.Update(acc);
        }
    }
}
