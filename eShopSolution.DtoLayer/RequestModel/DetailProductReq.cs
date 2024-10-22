using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.RequestModel
{
    public class DetailProductReq
    {
        public string Where {  get; set; }
        public int ProductID { get; set; }
        public int ProductColorID {  get; set; }
    }
}
