using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Naggaro.AccountStatment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naggaro.AccountStatment.Infrastructure.Data.Configurations;

public class AccountStatementConfigurations : IEntityTypeConfiguration<AccountStatement>
{
    public void Configure(EntityTypeBuilder<AccountStatement> builder)
    {
       
        builder.Property(t => t.Amount)
          .HasConversion<string>()
           .IsRequired();

        builder.Property(t => t.DateField)
         .HasConversion<string>()
          .IsRequired();

    }
}