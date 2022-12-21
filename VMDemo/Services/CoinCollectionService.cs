using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using VMDemo.Utility;
using static VMDemo.Utility.Constant;

namespace VMDemo.Services
{
    public interface ICoinCollectionService
    {
        void Insert(string coin, int num);
        int GetCoinValue();
        public int Count(Coin coin);
        void UpdateCoin(int amount);
    }
    public class CoinCollectionService: ICoinCollectionService
    {
        public IMemoryCacheService _memoryCacheService;
        public CoinCollectionService(IMemoryCacheService memoryCacheService)
        {
            _memoryCacheService = memoryCacheService;
        }
        public void Insert(string coin, int num)
        {
            if (_memoryCacheService.IsExists(coin))
            {
                var value = _memoryCacheService.GetCacheItem(coin);
                num += (int)value;
                var cacheItem = new CacheItem(coin.ToString(), num);
                _memoryCacheService.SetCacheItem(cacheItem);
            }
            else
            {
                var cacheItem = new CacheItem(coin.ToString(), num);
                _memoryCacheService.Insert(cacheItem);
            }
        }
        
        public int GetCoinValue()
        {
            int result = 0;
            int quarterValue = Count(Coin.Quarter) * Coin.Quarter.GetValue();
            int dimeValue = Count(Coin.Dime) * Coin.Dime.GetValue();
            int nickelValue = Count(Coin.Nickel) * Coin.Nickel.GetValue();
            result = quarterValue + dimeValue + nickelValue;
            return result;
        }

        public int Count(Coin coin)
        {
            int count = 0;
            if (_memoryCacheService.IsExists(coin.ToString()))
            {
                count = (int)_memoryCacheService.GetCacheItem(coin.ToString());
            }
            return count;
        }

        public void UpdateCoin(int amount)
        {
            int numQuartersDispensed = DispenseCoinCalculation(Coin.Quarter, amount);
            amount -= numQuartersDispensed * Coin.Quarter.GetValue();

            int numDimesDispensed = DispenseCoinCalculation(Coin.Dime, amount);
            amount -= numDimesDispensed * Coin.Dime.GetValue();

            int numNickelsDispensed = DispenseCoinCalculation(Coin.Nickel, amount);
            amount -= numNickelsDispensed * Coin.Nickel.GetValue();
        }

        private int DispenseCoinCalculation(Coin coin, int amount)
        {
            if (amount == 0) return 0;

            int numDispensed = Math.Min(amount / coin.GetValue(), Count(coin));
            int count = Count(coin);
            if(count > 0)
            {
                var minValue = Math.Min(numDispensed, count);
                count -= minValue;
                if(count == 0)
                {
                    _memoryCacheService.Remove(coin.ToString());
                }
                else
                {
                    var cacheItem = new CacheItem(coin.ToString(), count);
                    _memoryCacheService.SetCacheItem(cacheItem);
                }                
            }
            return numDispensed;
        }
    }
}
