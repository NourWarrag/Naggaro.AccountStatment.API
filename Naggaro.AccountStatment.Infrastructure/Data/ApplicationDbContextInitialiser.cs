using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Naggaro.AccountStatment.Domain.Constants;
using Naggaro.AccountStatment.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naggaro.AccountStatment.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole(Roles.Admin);
        var userRole = new IdentityRole(Roles.User);

        if ((await _roleManager.Roles.Where(r => r.Name == administratorRole.Name).FirstOrDefaultAsync())== null)
        {
            await _roleManager.CreateAsync(administratorRole);
        }
        if ((await _roleManager.Roles.Where(r => r.Name == userRole.Name).FirstOrDefaultAsync()) == null)
        {
            await _roleManager.CreateAsync(userRole);
        }
  
        // Default users
        var administrator = new ApplicationUser { UserName = "admin", Email = "admin@localhost" };

        if (_userManager.Users.Where(u => u.UserName == administrator.UserName).FirstOrDefault() == null)
        {
            var re=   await _userManager.CreateAsync(administrator, "admin");
            
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
                
            }
        }
        var user = new ApplicationUser { UserName = "user", Email = "user@localhost" };

        if ((await _userManager.Users.Where(u => u.UserName == user.UserName).FirstOrDefaultAsync()) == null)
        {
            await _userManager.CreateAsync(user, "user");
            
            if (!string.IsNullOrWhiteSpace(userRole.Name))
            {
                await _userManager.AddToRolesAsync(user, new[] { userRole.Name });
               
            }
        }

        // Default data
        // Seed, if necessary
        //if (!_context.Accounts.Any())
        //{
        //    _context.Accounts.Add(new Domain.Entities.Account
        //    {
        //        AccountType = "",
        //        AccountNumber = " ",
        //        AccountStatments =
        //        {
        //            new Domain.Entities.AccountStatement { Amount =1233 },

        //        }
        //    }) ;

        //    await _context.SaveChangesAsync();
        //}
    }
}
