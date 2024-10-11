using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.EntityLayer.Data
{
    [Table("AspNetRoleAccess")]
    public class AspNetRoleAccess
    {
        [Key]
        public int ID { get; set; }
        public string RoleID { get; set; }
        public int MenuPermissionID {  get; set; }
        public virtual AppRole AppRole { get; set; }
        public virtual MenuPermission MenuPermission { get; set; }
    }
}
