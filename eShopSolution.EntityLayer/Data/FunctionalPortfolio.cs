using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.EntityLayer.Data
{
    [Table("FunctionalPortfolio")]
    public class FunctionalPortfolio
    {
        [Key]
        public string FunctionalPortfolioID { get; set; }
        public string FunctionalPortfolioName { get; set; }
        public string ImageUrl { get; set; }
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
    }
}
