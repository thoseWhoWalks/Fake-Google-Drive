using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FGD.Data.Configuration.Account
{
    public class AccountInfoConfiguration : IEntityTypeConfiguration<AccountInfoModel<int>>
    {
        public void Configure(EntityTypeBuilder<AccountInfoModel<int>> builder)
        {
            builder.ToTable("AccountInfo");
        }
    }
}
