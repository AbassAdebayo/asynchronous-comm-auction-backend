using System.Security.Claims;
using IdentityModel;
using IdentityService.Data;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IdentityService;

public class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
        context.Database.Migrate();

        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        if (userMgr.Users.Any()) return;

        var janet = userMgr.FindByNameAsync("janet").Result;
        if (janet == null)
        {
            janet = new ApplicationUser
            {
                UserName = "janet",
                Email = "Janetfat@gmail.com",
                EmailConfirmed = true,
            };
            var result = userMgr.CreateAsync(janet, "Pass123$_").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(janet, new Claim[]
            {
                new(JwtClaimTypes.Name, "Janet Fat"),
            }).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            Log.Debug("Janet created");
        }
        else
        {
            Log.Debug("Janet already exists");
        }

        var fred = userMgr.FindByNameAsync("fred").Result;
        if (fred == null)
        {
            fred = new ApplicationUser
            {
                UserName = "fred",
                Email = "Fred@gmail.com",
                EmailConfirmed = true
            };
            var result = userMgr.CreateAsync(fred, "Pass123$_").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(fred, new Claim[]
            {
                new(JwtClaimTypes.Name, "Fred Joe"),
            }).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            Log.Debug("fred created");
        }
        else
        {
            Log.Debug("fred already exists");
        }

        var tom = userMgr.FindByNameAsync("tom").Result;
        if (tom == null)
        {
            tom = new ApplicationUser
            {
                UserName = "tom",
                Email = "TomSmith@email.com",
                EmailConfirmed = true,
            };
            var result = userMgr.CreateAsync(tom, "Pass123$_").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(tom, new Claim[]
            {
                new(JwtClaimTypes.Name, "Tom Smith"),
            }).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            Log.Debug("tom created");
        }
        else
        {
            Log.Debug("tom already exists");
        }

        var andrew = userMgr.FindByNameAsync("andrew").Result;
        if (andrew == null)
        {
            andrew = new ApplicationUser
            {
                UserName = "andrew",
                Email = "andrewgunn31@gmail.com",
                EmailConfirmed = true
            };
            var result = userMgr.CreateAsync(andrew, "Pass123$_").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(andrew, new Claim[]
            {
                new(JwtClaimTypes.Name, "Andrew Gunn"),
                new(JwtClaimTypes.Email, "andrewgunn31@gmail.com"),
            }).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            Log.Debug("andrew created");
        }
        else
        {
            Log.Debug("andrew already exists");
        }
    }
}