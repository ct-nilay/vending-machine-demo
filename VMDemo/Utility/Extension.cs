using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static VMDemo.Utility.Constant;

namespace VMDemo.Utility
{
    public static class Extension
    {
        public static int GetValue(this Coin coin)
        {
            int value = 0;

            switch (coin)
            {                
                case Coin.Penny:
                    value = 1;
                    break;
                case Coin.Nickel:
                    value = 5;
                    break;
                case Coin.Dime:
                    value = 10;
                    break;
                case Coin.Quarter:
                    value = 25;
                    break;
                default:
                    break;
            }
            return value;
        }

        public static int GetPrice(this Product prod)
        {
            int price = 0;
            switch (prod)
            {
                case Product.Cola:
                    price = 100;
                    break;
                case Product.Candy:
                    price = 65;
                    break;
                case Product.Chips:
                    price = 50;
                    break;
                default:
                    break;
            }
            return price;
        }

        public static string GetCurrencyString(this int amount)
        {
            return string.Format("{0:C}", amount / 100.0);
        }

        public static bool IsValidCoin(this string coin)
        {           
            bool isValid = Enum.GetNames(typeof(Coin))
                .Any(x => x.ToString().Contains(coin, StringComparison.OrdinalIgnoreCase));
            return isValid;
        }

        public static bool IsValidProduct(this string prod)
        {
            bool isValid = Enum.GetNames(typeof(Product))
                .Any(x => x.ToString().Contains(prod, StringComparison.OrdinalIgnoreCase));
            return isValid;
        }

        public static T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
