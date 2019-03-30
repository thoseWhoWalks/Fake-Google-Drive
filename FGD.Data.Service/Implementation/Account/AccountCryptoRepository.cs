using FGD.Bussines.Model;
using FGD.Configuration;
using FGD.Encryption.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FGD.Data.Service
{
    public class AccountCryptoRepository : IAccountCryptoRepository<AccountCryptoModelBussines<int>, int>
    {
        private FakeGoogleDriveContext _context;

        public AccountCryptoRepository(FakeGoogleDriveContext fakeGoogleDriveContext)
        {
            this._context = fakeGoogleDriveContext;
        }

        public async Task<AccountCryptoModelBussines<int>> CreateAsync(AccountCryptoModelBussines<int> model)
        {
            var accCrypto = await _context.AccountCryptos.AddAsync(
                    AutoMapperConfig.Mapper.Map<AccountCryptoModel<int>>(model)
                );

            await _context.SaveChangesAsync();

            return await GetByIdAsync(accCrypto.Entity.Id);
        }

        [Obsolete]
        public Task<bool> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<AccountCryptoModelBussines<int>>> GetAllAsync()
        {
            return AutoMapperConfig.Mapper.Map<List<AccountCryptoModelBussines<int>>>(
                    await _context.AccountCryptos.ToListAsync()
                );
        }

        public async Task<AccountCryptoModelBussines<int>> GetByIdAsync(int id)
        {
            return AutoMapperConfig.Mapper.Map<AccountCryptoModelBussines<int>>(
                    await _context.AccountCryptos.FirstOrDefaultAsync(ac => ac.Id == id)
                );
        }

        internal async Task<AccountCryptoModel<int>> GetRawByIdAsync(int id)
        {
            return await _context.AccountCryptos.FirstOrDefaultAsync(ac => ac.Id == id);
        }

        public async Task<AccountCryptoModelBussines<int>> GetByUserIdAsync(int id)
        {
            return AutoMapperConfig.Mapper.Map<AccountCryptoModelBussines<int>>(
                await _context.AccountCryptos.FirstOrDefaultAsync(ac => ac.AccountId == id)
                );
        }

        public async Task<AccountCryptoModelBussines<int>> UpdateAsync(int id, AccountCryptoModelBussines<int> model)
        {
            var accCrypto = await GetRawByIdAsync(model.Id);

            if (model.AESKeySecondPart != null)
                accCrypto.AESKeySecondPart = model.AESKeySecondPart;

            if (model.TokenKeySecondPart != null)
                accCrypto.TokenKeySecondPart = model.TokenKeySecondPart;

            _context.AccountCryptos.Update(accCrypto);

            await _context.SaveChangesAsync();

            return await GetByIdAsync(model.Id);
        }

        public async Task<string> GetAESKeyByYserIdAsync(int id)
        {
            var accCrypto = await this.GetByUserIdAsync(id);

            return accCrypto.AESKeySecondPart;
        }

        public async Task<AccountCryptoModelBussines<int>> CreateAsync(int userId)
        {
            var accCrypto = new AccountCryptoModelBussines<int>
            {
                AccountId = userId,
                TokenKeySecondPart = "emotyToDo",
                AESKeySecondPart = AESKeyGeneratorEncryptionHelper.GetAESKey()
            };

           var generated = await this.CreateAsync(accCrypto);

            return generated;
        }
    }
}
