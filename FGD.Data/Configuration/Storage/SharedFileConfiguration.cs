using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FGD.Data.Configuration.Storage.Shared
{
    public class SharedFileConfiguration : IEntityTypeConfiguration<SharedFileModel<int>>
    {
        public void Configure(EntityTypeBuilder<SharedFileModel<int>> builder)
        { 

            builder.ToTable("SharedFile");

            builder.HasOne(c => c.StoredFile)
               .WithMany()
               .HasForeignKey(c => c.StoredFileId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Account)
               .WithMany()
               .HasForeignKey(c => c.AccountId)
               .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
