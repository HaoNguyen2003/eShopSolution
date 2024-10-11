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
        [Key]
        public int ID { get; set; }
        public int MenuID {  get; set; }
        public int PermissionID {  get; set; }
        public virtual Permission Permission { get; set; }
        public virtual AspNetMenu AspNetMenu { get; set; } 
        public virtual ICollection<AspNetRoleAccess> AspNetRoleAccesses { get; set; }
    }
}
