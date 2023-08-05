using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Naggaro.AccountStatment.Application.Common.Interfaces;
using Naggaro.AccountStatment.Infrastructure.Data;
using Naggaro.AccountStatment.Infrastructure.Identity;

namespace Naggaro.AccountStatment.Infrastructure;
public static class RegisterInfrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseJet( "accountsdb.accdb",
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddIdentity < ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddSignInManager();
        services.AddTransient<IIdentityService, IdentityService>();


        services.AddAuthentication().AddCookie(cfg =>
        {
            cfg.ExpireTimeSpan= TimeSpan.FromMinutes(5);
            cfg.SlidingExpiration = true;
        });

        services.AddAuthorization();
        return services;
    }
}
