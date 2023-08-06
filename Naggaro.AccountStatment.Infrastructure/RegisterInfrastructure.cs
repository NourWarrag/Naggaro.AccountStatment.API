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
                options.UseJetOleDb("accountsdb.accdb",
                    b =>
                    b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                );

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
            options.Events.OnRedirectToLogin = (ctx) =>
            {
                if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                    ctx.Response.StatusCode = 401;
                else
                    ctx.Response.Redirect(ctx.RedirectUri);
                return Task.CompletedTask;
            };

            options.SlidingExpiration = true;
        });
        services.AddTransient<IIdentityService, IdentityService>();

        services.AddScoped<ApplicationDbContextInitialiser>();


        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });
        return services;
    }
}
