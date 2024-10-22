using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.EntityLayer.Data
{
    [Table("ColorCombination")]
    public class ColorCombination
    {
        public int ID {  get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProductColors> ProductColors { get; set; }
        public virtual ICollection<ColorCombinationColor> ColorCombinationColors { get; set; }
    }
}
