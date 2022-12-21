using Microsoft.AspNetCore.Mvc;
using System;
using VMDemo.Controllers;
using VMDemo.Services;
using VMDemo.Utility;
using Xunit;
using static VMDemo.Utility.Constant;

namespace VMTests
{
    public class VendingMachineTest
    {
        VendingMachineController _controller;
        ICoinCollectionService _coinCollectionService;
        IMemoryCacheService _memoryCacheService;
        public VendingMachineTest()
        {
            _memoryCacheService = new MemoryCacheService();
            _coinCollectionService = new CoinCollectionService(_memoryCacheService);
            _controller = new VendingMachineController(_coinCollectionService);
        }

        [Fact]
        public void InsertCoin_ReturnsBadRequest_When_CoinIsNULL()
        {
            //Arrange
            var coin = string.Empty;
            int num = 4;

            //Act
            var badRequest = _controller.InsertCoin(coin, num) as BadRequestObjectResult;

            //Assert
            Assert.IsType<BadRequestObjectResult>(badRequest);
            Assert.Equal(StringConstants.CoinIsNullOrEmpty, badRequest.Value);
        }

        [Fact]
        public void InsertCoin_ReturnsBadRequest_When_CoinIsInvalid()
        {
            //Arrange
            var coin = "xyz";
            int num = 4;

            //Act
            var badRequest = _controller.InsertCoin(coin, num) as BadRequestObjectResult;

            //Assert
            Assert.IsType<BadRequestObjectResult>(badRequest);
            Assert.Equal(StringConstants.CheckInput, badRequest.Value);
        }

        [Fact]
        public void InsertCoin_ReturnsBadRequest_When_NumberIsInvalid()
        {
            //Arrange
            var coin = Coin.Quarter.ToString();
            int num = -1;

            //Act
            var badRequest = _controller.InsertCoin(coin, num) as BadRequestObjectResult;

            //Assert
            Assert.IsType<BadRequestObjectResult>(badRequest);
            Assert.Equal(StringConstants.CheckInput, badRequest.Value);
        }

        [Fact]
        public void InsertCoin_ReturnsOk_When_CoinIsQuarter()
        {
            //Arrange
            var coin = Coin.Quarter.ToString();
            int num = 4;

            //Act
            var okResult = _controller.InsertCoin(coin, num) as OkObjectResult;
            int coinValue = _coinCollectionService.GetCoinValue();
            string returnMessage = $"{StringConstants.CoinSuccess} & BALANCE IS: { coinValue.GetCurrencyString() }";

            //Assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.Equal(returnMessage, okResult.Value);
        }

        [Fact]
        public void InsertCoin_ReturnsOk_When_CoinIsDime()
        {
            //Arrange
            var coin = Coin.Dime.ToString();
            int num = 2;

            //Act
            var okResult = _controller.InsertCoin(coin, num) as OkObjectResult;
            int coinValue = _coinCollectionService.GetCoinValue();
            string returnMessage = $"{StringConstants.CoinSuccess} & BALANCE IS: { coinValue.GetCurrencyString() }";

            //Assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.Equal(returnMessage, okResult.Value);
        }

        [Fact]
        public void InsertCoin_ReturnsOk_When_CoinIsNickel()
        {
            //Arrange
            var coin = Coin.Nickel.ToString();
            int num = 5;

            //Act
            var okResult = _controller.InsertCoin(coin, num) as OkObjectResult;
            int coinValue = _coinCollectionService.GetCoinValue();
            string returnMessage = $"{StringConstants.CoinSuccess} & BALANCE IS: { coinValue.GetCurrencyString() }";

            //Assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.Equal(returnMessage, okResult.Value);
        }

        [Fact]
        public void InsetrCoin_RetunInvalidCoinMessage_When_CoinIsPenny()
        {
            //Arrange
            var coin = Coin.Penny.ToString();
            int num = 1;

            //Act
            var okResult = _controller.InsertCoin(coin, num) as OkObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.Equal(StringConstants.InvalidCoin, okResult.Value);
        }

        [Fact]
        public void DisplayBalance_ReturnsMessage_When_NoCoinsInserted()
        {
            //Arrange            

            //Act
            var okResult = _controller.DisplayBalance() as OkObjectResult;
            int coinValue = _coinCollectionService.GetCoinValue();
            var returnMessage = $"{StringConstants.InsertCoin} YOUR BALANCE IS: {coinValue.GetCurrencyString()}";

            //Assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.Equal(returnMessage, okResult.Value);
        }

        [Fact]
        public void DisplayBalance_ReturnsOk_When_BalanceIsAvailable()
        {
            //Arrange            
            var coin = Coin.Quarter.ToString();
            int num = 4;

            //Act
            _controller.InsertCoin(coin, num);
            int coinValue = _coinCollectionService.GetCoinValue();
            var okResult = _controller.DisplayBalance() as OkObjectResult;
            var returnMessage = $"BALANCE IS: {coinValue.GetCurrencyString()}";

            //Assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.Equal(returnMessage, okResult.Value);
        }

        [Fact]
        public void DispenseProduct_ReturnsBadRequest_When_ProductIsNULLOrEmpty()
        {
            //Arrange            
            var prod = string.Empty;

            //Act
            var badRequest = _controller.DispenseProduct(prod) as BadRequestObjectResult;

            //Assert
            Assert.IsType<BadRequestObjectResult>(badRequest);
            Assert.Equal(StringConstants.ProductNullOrEmpty, badRequest.Value);
        }

        [Fact]
        public void DispenseProduct_ReturnsBadRequest_When_ProductIsInValid()
        {
            //Arrange            
            var prod = "xyz";

            //Act
            var badRequest = _controller.DispenseProduct(prod) as BadRequestObjectResult;

            //Assert
            Assert.IsType<BadRequestObjectResult>(badRequest);
            Assert.Equal(StringConstants.CheckInput, badRequest.Value);
        }

        [Fact]
        public void DispenseProduct_ReturnsOk_When_ColaDespensedSuccessfully()
        {
            //Arrange            
            var prod = Product.Cola.ToString();
            var coin = Coin.Quarter.ToString();
            int num = 4;

            //Act
            _controller.InsertCoin(coin, num);
            var okResult = _controller.DispenseProduct(prod) as OkObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.Equal(StringConstants.ThankYou, okResult.Value);
        }

        [Fact]
        public void DispenseProduct_ReturnsOk_When_CandyDespensedSuccessfully()
        {
            //Arrange            
            var prod = Product.Candy.ToString();
            var coin = Coin.Quarter.ToString();
            int num = 4;

            //Act
            _controller.InsertCoin(coin, num);
            var okResult = _controller.DispenseProduct(prod) as OkObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.Equal(StringConstants.ThankYou, okResult.Value);
        }

        [Fact]
        public void DispenseProduct_ReturnsOk_When_ChipsDespensedSuccessfully()
        {
            //Arrange            
            var prod = Product.Chips.ToString();
            var coin = Coin.Quarter.ToString();
            int num = 4;

            //Act
            _controller.InsertCoin(coin, num);
            var okResult = _controller.DispenseProduct(prod) as OkObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.Equal(StringConstants.ThankYou, okResult.Value);
        }

        [Fact]
        public void DispenseProduct_ReturnsPriceAndBalance_When_BalanceIsLow()
        {
            //Arrange            
            var prod = Product.Cola.ToString();

            //Act
            var okResult = _controller.DispenseProduct(prod) as OkObjectResult;
            int coinValue = _coinCollectionService.GetCoinValue();

            var returnMessage = $"BALANCE IS: { coinValue.GetCurrencyString() } & PRODUCT PRICE IS: {prod.ParseEnum<Product>().GetPrice().GetCurrencyString()}";

            //Assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.Equal(returnMessage, okResult.Value);
        }
    }
}
