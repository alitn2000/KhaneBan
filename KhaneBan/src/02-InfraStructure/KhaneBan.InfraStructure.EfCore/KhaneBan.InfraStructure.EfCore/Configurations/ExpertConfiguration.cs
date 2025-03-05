using KhaneBan.Domain.Core.Entites.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.InfraStructure.EfCore.Configurations
{
    public class ExpertConfiguration :  IEntityTypeConfiguration<Expert>
{
    public void Configure(EntityTypeBuilder<Expert> builder)
    {
        builder.HasData(

            new Expert { Id = 1, UserId = 2 },
            new Expert { Id = 2, UserId = 5 },
            new Expert { Id = 3, UserId = 6 }
            );
        }
}
}
