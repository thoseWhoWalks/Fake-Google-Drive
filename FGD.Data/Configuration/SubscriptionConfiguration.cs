using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FGD.Data.Configuration
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<SubscriptionModel<int>>
    {
        public void Configure(EntityTypeBuilder<SubscriptionModel<int>> builder)
        {
            builder.HasIndex(st => st.Title).IsUnique();

            builder.Property(st => st.Price).IsRequired();

            builder.Property(st => st.Title).IsRequired();
 
            builder.Property(st => st.TotalSpace).IsRequired();

            builder.ToTable("Subscription");
        }

      
    }
}
