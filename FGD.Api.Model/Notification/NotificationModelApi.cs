using FGD.SharedTypes.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FGD.Api.Model 
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
