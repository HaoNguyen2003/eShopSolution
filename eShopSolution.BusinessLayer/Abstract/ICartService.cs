using eShopSolution.DataLayer.Abstract;
using eShopSolution.DtoLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.BusinessLayer.Abstract
{
    public interface ICartService
    {
        public Task<DetailProduct> UpdateDetailProductByProductIDAndColorID(DetailCart cart);
    }
}
