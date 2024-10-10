using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.EntityLayer.Data
{
    [Table("AspNetMenu")]
    public class AspNetMenu
    {
        [Key]
        public int ID {  get; set; }
        public string Name { get; set; }
        public string? icon { get; set; }
        public string? title { get; set; }
        public string? URL {  get; set; }
    }
}
