using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using static VMDemo.Utility.Constant;

namespace VMDemo.Services
{
    public interface IMemoryCacheService
    {
        void Insert(CacheItem cacheItem);
        void SetCacheItem(CacheItem cacheItem);
        bool IsExists(string coin);
        Object GetCacheItem(string key);
        void Remove(string key);
    }
    public class MemoryCacheService: IMemoryCacheService
    {
        public string CacheName { get; set; } = "vendingmachine";
        public MemoryCache Cache { get; set; }
        public CacheItemPolicy CacheItemPloicy { get; set; }
        public MemoryCacheService()
        {
            Cache = new MemoryCache(CacheName);
            CacheItemPloicy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)
            };
        }       
        public Object GetCacheItem(string key)
        {
            var item = Cache.GetCacheItem(key);
            return item.Value;
        }
        public void Insert(CacheItem cacheItem)
        {
            Cache.Add(cacheItem, CacheItemPloicy);
        } 
        public bool IsExists(string coin)
        {
            return Cache.Contains(coin);
        }
        public void SetCacheItem(CacheItem cacheItem)
        {
            Cache.Set(cacheItem, CacheItemPloicy);
        }
        public void Remove(string key)
        {
            Cache.Remove(key);
        }
    }
}
