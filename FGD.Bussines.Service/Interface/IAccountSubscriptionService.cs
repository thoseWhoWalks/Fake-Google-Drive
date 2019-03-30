using FGD.Api;
using FGD.Api.Model;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FGD.Bussines.Service
{
    public interface IAccountSubscriptionService<TModel, TKey>
    {
        Task<TModel> SubscribeUserAsync(TKey subscriptionId, IIdentity indetity);

        Task<AccountSubscriptionModelApi<int>> SubscribeUserByIdAsync(int subscriptionId, int userId);

        Task<AccountSubscriptionModelApi<int>> SubscribeUserByIdDefaultAsync(int userId);

        Task<AccountSubscriptionModelApi<int>> GetByUserIdAsync(int userId);
    }
}
