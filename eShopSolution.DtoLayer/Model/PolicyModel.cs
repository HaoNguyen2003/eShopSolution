using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.Model
{
    public class PolicyModel
    {
        public int RoleAccessID { get; set; }
        public PermissionModel permission { get; set; }
        public MenuModel menu { get; set; }

    }
}
