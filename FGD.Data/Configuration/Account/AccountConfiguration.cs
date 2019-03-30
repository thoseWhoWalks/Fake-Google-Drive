using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FGD.Data.Configuration.Account
{
    public class AccountConfiguration : IEntityTypeConfiguration<AccountModel<int>>
    {
        public void Configure(EntityTypeBuilder<AccountModel<int>> builder)
        {
            builder.HasIndex(u => u.Email).IsUnique();

            builder.HasOne<AccountInfoModel<int>>(a => a.AccountInfo)
                .WithOne(ai => ai.Account)
                .HasForeignKey<AccountInfoModel<int>>(ai => ai.AccountId);

            builder.Property(a => a.Role)
                .HasDefaultValue("User");

            builder.Property(a => a.DateCreated)
                .HasDefaultValueSql("getdate()");

            builder.ToTable("Account");
        }
    }
}
