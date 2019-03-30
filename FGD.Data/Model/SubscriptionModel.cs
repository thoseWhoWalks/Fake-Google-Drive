using System;
using System.Collections.Generic;

namespace FGD.Data
{
    public class SubscriptionModel<TKey>
    {
        public TKey Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public int TotalSpace { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<AccountSubscriptionModel<TKey>> AccountSubscriptions { get; set; }
    }
}
