using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Naggaro.AccountStatment.Application.Common.Interfaces;

namespace Naggaro.AccountStatment.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
  

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }
    public async Task<bool> SignInAsync(string userName, string password)
    {
        var user = await _userManager.Users.FirstAsync(u => u.UserName == userName);
        var result = await _signInManager.PasswordSignInAsync(user, password, lockoutOnFailure: false, isPersistent: true);

        return result.Succeeded;

    }
    public async Task SignOutAsync()
    {  
        await _signInManager.SignOutAsync();
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

}
