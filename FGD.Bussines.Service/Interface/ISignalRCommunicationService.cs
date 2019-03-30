using FGD.Api.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FGD.Bussines.Service 
{
    public interface ISignalRCommunicationService
    {
        Task<string> SendNotificationSignalR(NotificationModelApi<int> notification, int Id);

        Task<string> SendSubscriptionUpdateSignalR(AccountSubscriptionModelApi<int> subscription, int Id);
    }
}
