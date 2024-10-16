﻿using Microsoft.AspNetCore.Identity;

namespace eShopSolution.EntityLayer.Data
{
    public class AppRole : IdentityRole
    {
        public string Description { get; set; }
        public virtual ICollection<AspNetRoleAccess> AspNetRoleAccesses { get; set; }

    }
}
