using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Naggaro.AccountStatment.Domain.Entities;

namespace Naggaro.AccountStatment.Infrastructure.Data.Configurations;
public class AccountConfigurations : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.Property(i => i.AccountType)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(i => i.AccountNumber)
           .HasMaxLength(200)
           .IsRequired();

        builder.HasMany(i => i.AccountStatments).WithOne().HasForeignKey(i => i.AccountID);

    
    }
}
