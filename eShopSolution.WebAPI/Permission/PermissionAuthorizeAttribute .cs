using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using System.Security.Claims;

namespace eShopSolution.WebAPI.Permission
{
    public class PermissionAuthorizeAttribute : AuthorizeAttribute
    {
        public string Permission { get; }

        public PermissionAuthorizeAttribute(string permission)
        {
            Permission = permission;
        }
    }
}
