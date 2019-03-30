using FGD.SharedTypes.Enums;
using System.Collections.Generic;

namespace FGD.Data
{
    public class NotificationStateModel
    {
        public NotificationStateEnum Title { get; set; }
        
        public ICollection<NotificationModel<int>> Notifications { get; set; }
    }
}
