using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.EntityLayer.Data
{
    [Table("InfoPayment")]
    public class InfoPayment
    {
        [Key]
        public int ID { get; set; }
        public int OrderID {  get; set; }
        public string TxnRef {  get; set; }
        public string TransactionNo {  get; set; }
        public string UserCreateBy { get; set; }

    }
}
