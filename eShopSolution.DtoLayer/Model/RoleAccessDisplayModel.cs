using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.Model
{
    public class RoleAccessDisplayModel
    {
        public int RoleAccessID { get; set; }
        public PermissionModel permissions { get; set; }
    }
}
