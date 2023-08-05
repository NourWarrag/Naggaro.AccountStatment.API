using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Naggaro.AccountStatment.Domain.Entities;

namespace Naggaro.AccountStatment.Infrastructure.Data.Configurations;

public class AccountStatementConfigurations : IEntityTypeConfiguration<AccountStatement>
{
    public void Configure(EntityTypeBuilder<AccountStatement> builder)
    {

        builder.Property(t => t.Amount)
          .HasConversion<string>()
          .HasColumnType("Text")
           .IsRequired();

        builder.Property(t => t.DateField)
         .HasConversion<string>()
         .HasColumnType("Text")
          .IsRequired();

    }
}