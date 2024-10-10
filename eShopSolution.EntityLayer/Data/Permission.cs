using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.EntityLayer.Data
{
    [Table("Permission")]
    public class Permission
    {
        public int Id { get; set; }
        public string PermissionName { get; set; }
    }
}
