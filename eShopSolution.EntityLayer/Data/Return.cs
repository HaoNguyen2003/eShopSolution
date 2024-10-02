using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShopSolution.EntityLayer.Data
{
    [Table("Return")]
    public class Return
    {
        [Key]
        public int ReturnID { get; set; }
        public int OrderID { get; set; }
        public string UserID { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Reason { get; set; }

        public virtual Order Order { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual ICollection<ReturnDetail> ReturnDetails { get; set; }

    }
}
