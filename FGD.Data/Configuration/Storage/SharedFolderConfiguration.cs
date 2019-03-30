using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FGD.Data.Configuration.Storage.Shared
{
    public class SharedFolderConfiguration : IEntityTypeConfiguration<SharedFolderModel<int>>
    {
        public void Configure(EntityTypeBuilder<SharedFolderModel<int>> builder)
        {

            builder.ToTable("SharedFolder");

            builder.HasOne(c => c.StoredFolder)
               .WithMany()
               .HasForeignKey(c => c.StoredFolderId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Account)
               .WithMany()
               .HasForeignKey(c => c.AccountId)
               .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
