using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShopSolution.EntityLayer.Data
{
    [Table("ReturnDetail")]
    public class ReturnDetail
    {
        [Key]
        public int ReturnDetailID { get; set; }
        public int ReturnID { get; set; }
        public int DetailOrderID { get; set; }
        public int Quantity { get; set; }
        public string Reason { get; set; }
        public virtual Return Return { get; set; }
        public virtual DetailOrder DetailOrder { get; set; }

    }
}
