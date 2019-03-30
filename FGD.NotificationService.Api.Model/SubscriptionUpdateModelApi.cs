namespace FGD.NotificationService.Api
{
    public class SubscriptionUpdateModelApi<TKey>
    {
        public TKey TakenSpace { get; set; }

        public bool IsActive { get; set; }
    }
}
