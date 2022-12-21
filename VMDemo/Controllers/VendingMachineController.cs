using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using VMDemo.Services;
using VMDemo.Utility;
using static VMDemo.Utility.Constant;

namespace VMDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendingMachineController : ControllerBase
    {
        public readonly ICoinCollectionService _coinCollectionService;
        public VendingMachineController(ICoinCollectionService coinCollectionService)
        {
            _coinCollectionService = coinCollectionService;
        }

        [HttpGet]
        [Route("Insert")]
        public IActionResult InsertCoin(string coin, int num = 0)
        {
            if (string.IsNullOrWhiteSpace(coin))
                return BadRequest(StringConstants.CoinIsNullOrEmpty);

            if (!coin.IsValidCoin() || num <= 0)
                return BadRequest(StringConstants.CheckInput);

            if (coin.ToLower() == Coin.Penny.ToString().ToLower())
                return Ok(StringConstants.InvalidCoin);

            _coinCollectionService.Insert(coin, num);
            int coinValue = _coinCollectionService.GetCoinValue();

            string returnMessage = $"{StringConstants.CoinSuccess} & BALANCE IS: { coinValue.GetCurrencyString() }";
            return Ok(returnMessage);
        }

        [HttpGet]
        [Route("Dispense")]
        public IActionResult DispenseProduct(string prod)
        {
            if (string.IsNullOrWhiteSpace(prod))
                return BadRequest(StringConstants.ProductNullOrEmpty);

            if (!prod.IsValidProduct())
                return BadRequest(StringConstants.CheckInput);

            int price = prod.ParseEnum<Product>().GetPrice();

            int coinValue = _coinCollectionService.GetCoinValue();

            if (coinValue >= price)
            {
                _coinCollectionService.UpdateCoin(price);
                return Ok(StringConstants.ThankYou);
            }

            var returnMessage = $"BALANCE IS: { coinValue.GetCurrencyString() } & PRODUCT PRICE IS: { price.GetCurrencyString() }";
            return Ok(returnMessage);
        }

        [HttpGet]
        [Route("Display")]
        public IActionResult DisplayBalance()
        {
            var returnMesage = string.Empty;

            int coinValue = _coinCollectionService.GetCoinValue();
            returnMesage = (coinValue > 0) ? $"BALANCE IS: {coinValue.GetCurrencyString() }" : $"{StringConstants.InsertCoin} YOUR BALANCE IS: {coinValue.GetCurrencyString()}";

            return Ok(returnMesage);
        }
    }
}
