using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.Model
{
    public class ProductReviewModel
    {
        public int ID { get; set; }
        public string UserID {  get; set; }
        public int ProductID {  get; set; }
        public int DetailOrderID {  get; set; }
        public DateTime CreateTime { get; set; }
        public int Rating {  get; set; }
        public string Review { get; set; } = string.Empty;

    }
}
