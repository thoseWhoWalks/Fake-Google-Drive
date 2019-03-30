using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FGD.Data.Configuration.Storage
{
    public class StoredFileConfiguration : IEntityTypeConfiguration<StoredFileModel<int>>
    {
        public void Configure(EntityTypeBuilder<StoredFileModel<int>> builder)
        {
            builder.Property(s => s.HashedTitle).IsRequired();

            builder.Property(s => s.Title).IsRequired();

            builder.Property(s => s.Extention).IsRequired();

            builder.Property(s => s.Path).IsRequired();

            builder.Property(a => a.DateCreated)
                .HasDefaultValueSql("getdate()");
            
            builder.ToTable("StoredFile");

            builder.HasOne(c => c.RootFolder)
               .WithMany(a=>a.StoredFiles)
               .HasForeignKey(c => c.RootFolderId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired(false);

            builder.HasOne(c => c.StoredFolder)
               .WithMany(k=>k.StoredFiles)
               .HasForeignKey(c => c.StoredFolderId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired(false);
             
        }
    }
}
