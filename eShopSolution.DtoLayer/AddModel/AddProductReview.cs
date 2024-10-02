using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.AddModel
{
    public class AddProductReview
    {
        public int ProductID { get; set; }
        public int DetailOrderID { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; } = string.Empty;
    }
}
