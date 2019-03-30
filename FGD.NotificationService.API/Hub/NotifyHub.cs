using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FGD.NotificationService.Api
{
    public class NotifyHub : Hub
    {
        public static List<Tuple<string, string>> connectionIdToUserId = new List<Tuple<string, string>>();

        public override Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();

            var connectionId = Context.ConnectionId;
            var userId = httpContext.Request.Query["UserId"];

            connectionIdToUserId.Add(Tuple.Create<string, string>(connectionId, userId));

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;

            connectionIdToUserId.Remove(connectionIdToUserId.Find(c => c.Item1 == connectionId));

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendNotificationToSubscriber(NotificationModelApi<int> notification, int Id)
        {
            await Clients.Clients(GetRecieverConnectionId(Id)).SendAsync("notify", notification);
        }

        public async Task SendSubscriptionUpdateToSubscriber(SubscriptionUpdateModelApi<int> update, int Id)
        {
            await Clients.Clients(GetRecieverConnectionId(Id)).SendAsync("updateSubscription", update);
        }

        private static string GetRecieverConnectionId(int Id)
        {
            return connectionIdToUserId.Find(c => c.Item2 == Id.ToString()).Item1;
        }
    }
}
