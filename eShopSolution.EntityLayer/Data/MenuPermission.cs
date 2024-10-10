using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.EntityLayer.Data
{
    [Table("MenuPermission")]
    public class MenuPermission
    {
        public int RoleMenuID {  get; set; }
        public int PermissionID {  get; set; }
        public virtual Permission Permission { get; set; }
        public virtual AspNetRoleMenu RoleMenu { get; set; }
    }
}
