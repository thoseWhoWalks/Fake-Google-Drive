using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FGD.Data.Configuration.Storage
{
    public class StoredFolderConfiguration : IEntityTypeConfiguration<StoredFolderModel<int>>
    {
        public void Configure(EntityTypeBuilder<StoredFolderModel<int>> builder)
        {
            builder.Property(s => s.HashTitle).IsRequired();

            builder.Property(s => s.Title).IsRequired();

            builder.Property(a => a.DateCreated)
                .HasDefaultValueSql("getdate()");

            builder.HasOne(c => c.StoredFolder)
               .WithMany()
               .HasForeignKey(c => c.StoredFolderId) 
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired(false); 

            builder.HasOne(c => c.RootFolder)
               .WithMany(d=>d.StoredFolders)
               .HasForeignKey(c => c.RootFolderId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired(false);
            

            builder.Property(s => s.Path).IsRequired();

            builder.ToTable("StoredFolder");

        }
    }
}
