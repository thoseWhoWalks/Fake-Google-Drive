using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FGD.Data.Configuration
{
    public class AccountSubscriptionConfiguration : IEntityTypeConfiguration<AccountSubscriptionModel<int>>
    {
        public void Configure(EntityTypeBuilder<AccountSubscriptionModel<int>> builder)
        {
            builder.Property(a => a.IsActive).HasDefaultValue(true);

            builder.Property(a => a.StartDate)
                     .HasDefaultValueSql("getdate()")
                     .ValueGeneratedOnAdd()
                     .IsRequired();

            builder.HasKey(a => a.Id);
            
            builder.ToTable("AccountSubscription");
        }
    }
}
