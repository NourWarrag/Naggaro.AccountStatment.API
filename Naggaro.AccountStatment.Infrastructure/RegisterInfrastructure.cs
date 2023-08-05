using Microsoft.AspNetCore.Authentication.Cookies;
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

        services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddSignInManager();
        services.Configure<IdentityOptions>(options =>
        {
            // Default Password settings.
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 4;
            options.Password.RequiredUniqueChars = 0;
        });
        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = "AccountAppCookie";
            options.Cookie.HttpOnly = false;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            options.LoginPath = "/api/auth/Login";
            // ReturnUrlParameter requires 
            //using Microsoft.AspNetCore.Authentication.Cookies;
            options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            options.SlidingExpiration = true;
        });
        services.AddTransient<IIdentityService, IdentityService>();

        services.AddScoped<ApplicationDbContextInitialiser>();

       

         
        return services;
    }
}
