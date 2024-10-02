using eShopSolution.WebAPI.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace eShopSolution.WebAPI.CustomPermission
{

    public class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {

        public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }
        public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (!policyName.StartsWith(CustomClaimTypes.Permission, StringComparison.OrdinalIgnoreCase))
                return FallbackPolicyProvider.GetPolicyAsync(policyName);
            var policy = new AuthorizationPolicyBuilder();
            policy.RequireAuthenticatedUser();
            policy.AddRequirements(new PermissionRequirement(policyName));
            return Task.FromResult(policy.Build());
        }
    }
}
