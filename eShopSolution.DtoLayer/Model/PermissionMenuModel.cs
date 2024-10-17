using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.Model
{
    public class PermissionMenuModel
    {
        public int ID { get; set; }
        public int MenuID { get; set; }
        public int PermissionID { get; set; }
        public string FunctionName {  get; set; }
    }
}
