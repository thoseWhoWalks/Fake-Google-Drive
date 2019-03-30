using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FGD.Api.Model;
using FGD.Bussines.Service.Util;
using Newtonsoft.Json;

namespace FGD.Bussines.Service
{
    public class SignalRCommunicationService : ISignalRCommunicationService
    {
        HttpClient _httpClient;

        private IRedisCachingService _redisCachingService;

        public SignalRCommunicationService(
            IRedisCachingService redisCachingService
            )
        {
            this._httpClient = new HttpClient();

            this._redisCachingService = redisCachingService;
        }

        public async Task<string> SendNotificationSignalR(NotificationModelApi<int> notification, int Id)
        {
            var payload = JsonConvert.SerializeObject(notification);

            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            await this._redisCachingService.RemoveItemAsync(RedisCachingKeysUtil.GET_NOTIFICATIONS_BY_USER_ID_KEY+Id);

            var response = await _httpClient.PostAsync("http://fakegoogledrive.eastus.cloudapp.azure.com:5001/api/notification/"+Id, content);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> SendSubscriptionUpdateSignalR(AccountSubscriptionModelApi<int> subscription, int Id)
        {
            var payload = JsonConvert.SerializeObject(subscription);

            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://fakegoogledrive.eastus.cloudapp.azure.com:5001/api/subscription/" + Id, content);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
