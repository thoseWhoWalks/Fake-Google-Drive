using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FGD.Data.Configuration.Notification
{
    public class NotificationConfiguration : IEntityTypeConfiguration<NotificationModel<int>>
    {
        public void Configure(EntityTypeBuilder<NotificationModel<int>> builder)
        {
            builder.Property(p => p.Title).IsRequired();

            builder.Property(c => c.NotificationState)
                .HasConversion<string>();

            builder.HasOne(c => c.NotificationStateRelation)
                        .WithMany(c => c.Notifications)
                        .HasForeignKey(c => c.NotificationState);

            builder.HasKey(n => n.Id);

            builder.ToTable("Notification");
        }

    }
}
