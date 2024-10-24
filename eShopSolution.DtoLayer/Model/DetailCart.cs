using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.Model
{
    public class DetailCart
    {
        public int ProductID { get; set; }
        public int ProductColorID { get; set; }
        public int SizeID { get; set; }
        public int Quantity { get; set; }
    }
}
