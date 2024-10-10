using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.EntityLayer.Data
{
    [Table("AspNetRoleMenu")]
    public class AspNetRoleMenu
    {
        [Key]
        public int ID { get; set; }
        public string RoleID { get; set; }
        public virtual AppRole AppRole { get; set; }
        public int MenuID { get; set; }
        public virtual AspNetMenu Menu { get; set; }
        public virtual ICollection<MenuPermission> MenuPermissions { get; set; }
    }
}
