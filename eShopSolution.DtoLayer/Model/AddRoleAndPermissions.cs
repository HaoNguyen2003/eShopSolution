using eShopSolution.DtoLayer.AddModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.Model
{
    public class AddRoleAndPermissions
    {
        public AddRole role {  get; set; }
        public List<PermissionMenuModel> permissionMenuModels {  get; set; }
    }
}
