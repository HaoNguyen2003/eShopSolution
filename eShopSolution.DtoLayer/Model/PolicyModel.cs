using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.Model
{
    public class PolicyModel
    {
        public MenuModel menu { get; set; }
        public List<RoleAccessDisplayModel> roleAccessDisplayModels { get; set; }
    }
}
