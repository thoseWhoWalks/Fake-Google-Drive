using System;
using System.Collections.Generic;
using System.Text;

namespace FGD.Bussines.Model
{
    public class AccountSubscriptionModelBussines<Tkey>
    {
        public Tkey AccountId { get; set; }

        public Tkey SubscriptionId { get; set; }

        public Tkey RootFolderId { get; set; }

        public int TakenSpace { get; set; }

        public bool IsActive { get; set; }

        public DateTime StartDate { get; set; }

        public int DurationDays { get; set; }
    }
}
