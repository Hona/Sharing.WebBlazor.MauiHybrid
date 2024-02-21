using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using MyShop.Shared.Entities;

namespace MyShop.Shared.RBAC;

public static class DependencyInjection
{
    public static void AddUserRolePolicies(this AuthorizationOptions options)
    {
        // Equivalent to nameof(UserRole.xyz)
        var role = Enum.GetNames<UserRole>();
        
        foreach (var item in role)
        {
            options.AddPolicy(item, policy => policy.RequireClaim(ClaimTypes.Role, 
                item, $"[\"{item}\"]")); // Format from Auth0
        }
    }
}