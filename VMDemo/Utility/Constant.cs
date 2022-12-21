using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMDemo.Utility
{
    public class Constant
    {
        public enum Coin
        {
            Penny,
            Nickel,
            Dime,
            Quarter
        };

        public enum Product
        {
            Cola,
            Candy,
            Chips
        };

        public class StringConstants
        {
            public const string InsertCoin = "PLEASE INSERT COIN!";
            public const string CoinSuccess = "COIN INSERTED SUCCESSFULLY!";
            public const string InvalidCoin = "INVALID COIN. PLEASE INSERT VALID COIN!";
            public const string ThankYou = "THANK YOU!";
            public const string CoinIsNullOrEmpty = "COIN VALUE SHOULD NOT BE NULL OR EMPTY!";
            public const string CheckInput = "PLEASE CHECK YOUR INPUTS!";
            public const string ProductNullOrEmpty = "PRODUCT VALUE SHOULD NOT BE NULL OR EMPTY";
        }
    }
}
