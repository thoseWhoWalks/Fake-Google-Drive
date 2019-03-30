using System;
using System.Collections.Generic;
using System.Text;

namespace FGD.Api.Model
{
    public class SubscriptionModelApi<TKey>
    {
        public TKey Id { get; set; }

        public String Title { get; set; }

        public decimal Price { get; set; }

        public int TotalSpace { get; set; }

        public String Description { get; set; }

        public bool IsDeleted { get; set; }

    }
}
