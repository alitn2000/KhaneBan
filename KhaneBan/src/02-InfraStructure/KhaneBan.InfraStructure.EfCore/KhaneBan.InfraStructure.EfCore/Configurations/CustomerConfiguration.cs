using KhaneBan.Domain.Core.Entites.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.InfraStructure.EfCore.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasData(

             new Customer { Id = 1, UserId = 3 },
             new Customer { Id = 2, UserId = 4 },
             new Customer { Id = 3, UserId = 7 }
            );
    }
}
