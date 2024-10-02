using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.EntityLayer.Data
{
    public class RoleClaim : IdentityRoleClaim<string>
    {
        public string FunctionalPortfolioID {  get; set; }
        public virtual FunctionalPortfolio FunctionalPortfolio {  get; set; }
        
    }
}
