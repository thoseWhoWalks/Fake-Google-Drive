using System;
using System.Collections.Generic;
using System.Text;

namespace FGD.Api.Model
{
    public class AccountSubscriptionModelApi<T>
    {
        public T AccountId { get; set; }

        public T SubscriptionId { get; set; } 

        public int TakenSpace { get; set; }

        public bool IsActive { get; set; }

        public DateTime StartDate { get; set; }

        public int DurationDays { get; set; }

    }
}
