using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.RequestModel;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.BusinessLayer.Service
{
    public class CustomCache<E> : ICustomCache<E>
    {
        private readonly IMemoryCache _memoryCache;
        private readonly HashSet<E> _keys = new HashSet<E>();
        public CustomCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public void Clear()
        {
            if (_memoryCache is MemoryCache concreteMemoryCache)
            {
                concreteMemoryCache.Clear();
            }
            _keys.Clear();
        }

        public T Get<T>(E key)
        {
            _memoryCache.TryGetValue(key, out T value);
            return value;
        }

        public IDictionary<E, object> GetAllCaches()
        {
            var allItems = new Dictionary<E, object>();
            foreach (var key in _keys.ToList())
            {
                if (_memoryCache.TryGetValue(key, out object value))
                {
                    allItems[key] = value;
                }
                else
                {
                    _keys.Remove(key);
                }
            }
            return allItems;
        }
        public void Remove(E key)
        {
            _memoryCache.Remove(key);
            _keys.Remove(key);
        }

        public void Set<T>(E key, T value, TimeSpan? absoluteExpireTime = null, TimeSpan? slidingExpiration = null, CacheItemPriority priority = CacheItemPriority.Normal)
        {
            var _options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTime,
                SlidingExpiration = slidingExpiration,
                Priority = priority
            };
            _memoryCache.Set(key, value, _options);
            _keys.Add(key);
        }

        public string GenerateCacheKeyProduct(DetailProductReq detailProductReq)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"Where:{detailProductReq.Where}");
            keyBuilder.Append($"ProductID:{detailProductReq.ProductID}");
            keyBuilder.Append($"ProductColorID:{detailProductReq.ProductColorID}");
            return keyBuilder.ToString();
        }
        public string GenerateCacheKey(FilterModel model, int page, int limit)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"TextSearch:{model.TextSearch}|");
            keyBuilder.Append($"BrandIDs:{string.Join(",", model.ListBrandID)}|");
            keyBuilder.Append($"CategoryIDs:{string.Join(",", model.ListCategoryID)}|");
            keyBuilder.Append($"ColorIDs:{string.Join(",", model.ListColorID)}|");
            keyBuilder.Append($"GenderIDs:{string.Join(",", model.ListGenderID)}|");
            keyBuilder.Append($"SizeIDs:{string.Join(",", model.ListSizeID)}|");
            keyBuilder.Append($"SortByPrice:{model.SortByPrice}|");
            keyBuilder.Append($"Page:{page}|Limit:{limit}");
            return keyBuilder.ToString();
        }
        public void RemoveCacheIfKeyContains(string searchString)
        {
            var keysToRemove = new List<E>();
            foreach (var key in _keys)
            {
                if (key.ToString().Contains(searchString))
                {
                    keysToRemove.Add(key);
                }
            }
            foreach (var key in keysToRemove)
            {
                _keys.Remove(key);
                _memoryCache.Remove(key);
            }
        }
    }
}
