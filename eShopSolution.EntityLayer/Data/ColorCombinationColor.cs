using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.EntityLayer.Data
{
    [Table("ColorCombinationColor")]
    public class ColorCombinationColor
    {
        [Key]
        public int ID {  get; set; }
        public int ColorCombinationID { get; set; }
        public int ColorID {  get; set; }
        public virtual Colors Colors { get; set; }
        public virtual ColorCombination ColorCombination { get; set; }
    }
}
