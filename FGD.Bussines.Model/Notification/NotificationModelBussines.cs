using FGD.SharedTypes.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FGD.Bussines.Model 
{
    public class NotificationModelBussines<TKey>
    {
        public TKey Id { get; set; }
        public String Title { get; set; }
        public String Descritpion { get; set; }
        public bool IsDeleted { get; set; }

        public TKey AccountId { get; set; }

        public NotificationStateEnum NotificationState { get; set; }
    }
}
