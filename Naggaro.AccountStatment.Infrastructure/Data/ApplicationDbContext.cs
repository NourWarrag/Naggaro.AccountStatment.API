using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Naggaro.AccountStatment.Application.Common.Interfaces;
using Naggaro.AccountStatment.Domain.Entities;
using Naggaro.AccountStatment.Infrastructure.Identity;
using System.Reflection;

namespace Naggaro.AccountStatment.Infrastructure.Data;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Domain.Entities.AccountStatement> AccountStatments => Set<Domain.Entities.AccountStatement>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}