using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.RequestModel;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.BusinessLayer.Abstract
{
    public interface ICustomCache<E>
    {
        T Get<T>(E key);
        void Set<T>(E key, T value, TimeSpan? absoluteExpireTime = null, TimeSpan? slidingExpiration = null, CacheItemPriority priority = CacheItemPriority.Normal);
        IDictionary<E, object> GetAllCaches();
        void Remove(E key);
        void Clear();
        string GenerateCacheKey(FilterModel model, int page, int limit);
        string GenerateCacheKeyProduct(DetailProductReq detailProductReq);
    }
}
