﻿using eShopSolution.WebAPI.Permission;
using Microsoft.AspNetCore.Authorization;

namespace eShopSolution.WebAPI.CustomPermission
{
    public class PermissionAuthorizationHandler //: AuthorizationHandler<PermissionRequirement>
    {
       /* protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
             PermissionRequirement requirement)
        {
            if ((context.User.Identity != null && !context.User.Identity.IsAuthenticated) || context.User.Identity == null)
            {
                context.Fail();
                return;
            }

            var permissions = context.User.Claims.ToList();
            if (permissions.Count == 0)
            {
                context.Fail();
                return;
            }

            if (permissions.Any(x => x.Type == PermissionHandler.Permission
                                     && x.Value == requirement.Permission
                                     && x.Issuer == "LOCAL AUTHORITY"))
            {
                context.Succeed(requirement);
                return;
            }
            context.Fail();*/
        }
    }
