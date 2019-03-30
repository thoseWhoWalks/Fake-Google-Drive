using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FGD.Data.Configuration.Notification
{
    public class NotificationStateConfiguration : IEntityTypeConfiguration<NotificationStateModel>
    {
        public void Configure(EntityTypeBuilder<NotificationStateModel> builder)
        {

            builder.Property(s => s.Title)
                             .HasConversion<string>();

            builder.HasKey(s => s.Title);

            builder.ToTable("NotificationState");
        }
    }
}
