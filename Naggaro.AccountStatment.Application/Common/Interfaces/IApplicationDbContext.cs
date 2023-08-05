using Microsoft.EntityFrameworkCore;
using Naggaro.AccountStatment.Domain.Entities;
using System.Collections.Generic;

namespace Naggaro.AccountStatment.Application.Common.Interfaces;
public interface IApplicationDbContext
{


    public DbSet<Account> Accounts { get; }
    public DbSet<Domain.Entities.AccountStatement> AccountStatments { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}