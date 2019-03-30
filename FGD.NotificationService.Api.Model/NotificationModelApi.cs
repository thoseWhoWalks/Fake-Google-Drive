using FGD.SharedTypes.Enums;
using System;

namespace FGD.NotificationService.Api
{
    public class NotificationModelApi<TKey>
    {
        public TKey Id { get; set; }
        public String Title { get; set; }
        public String Descritpion { get; set; }

        public NotificationStateEnum NotificationState { get; set; }

        public bool IsDeleted { get; set; }
    }
}
