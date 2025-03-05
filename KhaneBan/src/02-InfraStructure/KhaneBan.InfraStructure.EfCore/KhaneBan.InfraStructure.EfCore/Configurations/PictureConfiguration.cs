using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using KhaneBan.Domain.Core.Entites.BaseEntities;

namespace KhaneBan.InfraStructure.EfCore.Configurations
{
    public class PictureConfigurations : IEntityTypeConfiguration<Picture>

    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Pictures");
            builder.Property(x => x.Path).HasMaxLength(500).IsRequired();



        }
    }
}
