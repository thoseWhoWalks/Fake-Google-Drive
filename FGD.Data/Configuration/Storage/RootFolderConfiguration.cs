using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FGD.Data.Configuration.Storage
{
    public class RootFolderConfiguration : IEntityTypeConfiguration<RootFolderModel<int>>
    {
        public void Configure(EntityTypeBuilder<RootFolderModel<int>> builder)
        {
            builder.ToTable("RootFolder");
             
        }
    }
}
