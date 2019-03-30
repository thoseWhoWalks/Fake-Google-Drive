using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FGD.Data.Configuration.Account
{
    public class AccountCryptoConfiguration : IEntityTypeConfiguration<AccountCryptoModel<int>>
    {
        public void Configure(EntityTypeBuilder<AccountCryptoModel<int>> builder)
        {
            builder.ToTable("AccountCrypto");
        }
    }
}
