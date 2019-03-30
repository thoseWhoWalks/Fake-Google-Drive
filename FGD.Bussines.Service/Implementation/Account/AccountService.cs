using FGD.Api.Model;
using FGD.Bussines.Model;
using FGD.Bussines.Service.Util;
using FGD.Configuration;
using FGD.Data.Service;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FGD.Bussines.Service
{
    public class AccountService : IAccountService<AccountModelApi<int>, int>
    {
        IAccountRepository<AccountModelBussines<int>, int> _accountRepository;

        IAccountSubscriptionService<AccountSubscriptionModelApi<int>, int> _accountSubscriptionService;

        private IRedisCachingService _redisCachingService;

        public AccountService(
            IAccountRepository<AccountModelBussines<int>, int> accountDataService,
            IAccountSubscriptionService<AccountSubscriptionModelApi<int>, int> accountSubscriptionService,
            IRedisCachingService redisCachingService
            )
        {
            this._accountRepository = accountDataService;
            this._accountSubscriptionService = accountSubscriptionService;
            this._redisCachingService = redisCachingService;
        }

        public async Task<ResponseModel<ICollection<AccountModelApi<int>>>> GetAllAsync()
        {
             
            var allAccs = await this._redisCachingService.GetItemAsync<ICollection<AccountModelBussines<int>>>(
                    RedisCachingKeysUtil.GET_ALL_ACCOUNTS_KEY
                ); 

            if (allAccs == null) {
               allAccs = await this._accountRepository.GetAllAsync();

               await this._redisCachingService.PutItemAsync<ICollection<AccountModelBussines<int>>>(
                        RedisCachingKeysUtil.GET_ALL_ACCOUNTS_KEY,
                        allAccs,
                        new DistributedCacheEntryOptions()
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3)
                        }
                    );
            }

            ResponseModel<ICollection<AccountModelApi<int>>> resp = new ResponseModel<ICollection<AccountModelApi<int>>>(
                AutoMapperConfig.Mapper.Map<ICollection<AccountModelApi<int>>>(allAccs)
                );

            if (allAccs == null)
                resp.AddError(new Error($"Db error."));

            return resp;
        }

        public async Task<ResponseModel<AccountModelApi<int>>> GetUserByEmailAsync(String email)
        {
            var accByEmail = await this._redisCachingService.GetItemAsync<AccountModelBussines<int>>(
                RedisCachingKeysUtil.GET_ACCOUNT_BY_EMAIL_KEY + email
                );

            if (accByEmail == null)
            {
                accByEmail = await this._accountRepository.GetByEmailAsync(email);

                await this._redisCachingService.PutItemAsync<AccountModelBussines<int>>(
                         RedisCachingKeysUtil.GET_ACCOUNT_BY_EMAIL_KEY + email,
                         accByEmail,
                         new DistributedCacheEntryOptions()
                         {
                             AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(3),
                             SlidingExpiration = TimeSpan.FromDays(1)
                         }
                     );
            }
             
            ResponseModel<AccountModelApi<int>> resp = new ResponseModel<AccountModelApi<int>>(
                AutoMapperConfig.Mapper.Map<AccountModelApi<int>>(accByEmail)
                );

            if (accByEmail == null)
                resp.AddError(new Error($"Item with email {email} not found."));

            return resp;

        }

        public async Task<ResponseModel<AccountModelApi<int>>> RegisterUserAsync(AccountModelApi<int> model)
        {
            var registeredUser = await this._accountRepository.CreateAsync(
                AutoMapperConfig.Mapper.Map<AccountModelBussines<int>>(model)
                );

            ResponseModel<AccountModelApi<int>> resp = new ResponseModel<AccountModelApi<int>>(
               AutoMapperConfig.Mapper.Map<AccountModelApi<int>>(registeredUser)
               );

            if (registeredUser == null)
            {
                resp.AddError(new Error($"User with {model.Email} alredy exists."));
                return resp;
            }

            await this._redisCachingService.RemoveItemAsync(RedisCachingKeysUtil.GET_ACCOUNT_BY_EMAIL_KEY);

            await this._accountSubscriptionService.SubscribeUserByIdDefaultAsync(registeredUser.Id);

            //ToDo : make crypto 

            return resp;
        }
    }
}
