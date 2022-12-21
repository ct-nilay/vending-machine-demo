# Problem Statement #

You need to build a vending machine which will accept money, and dispense products. 

## Features ##

**Accept Coins**

_As a vendor_
_I want a vending machine that accepts coins So that I can collect money from the customer_

The vending machine will accept valid coins - nickels(1/20)$, dimes(1/10)$ and quarters(1/4)$ and reject invalid one - pennies(1/100)$. When a valid coin is inserted the amount of the coin will be added to the current amount and the display will be updated. When there are no coins inserted, the machine displays INSERT COIN. Rejected coins are placed in the coin return.

**NOTE:** The temptation here will be to create Coin objects that know their value. However, this is not how a real vending machine works. Instead, it identifies coins by their weight and size and then assigns a value to what was inserted. You will need to do something similar. This can be simulated using strings, constants, enums, symbols, or something of that nature.

**Select Product**

_As a vendor_
_I want customers to select products So that I can give them an incentive to put money in the machine_

There are three products: cola for $1.00, chips for $0.50, and candy for $0.65. When the respective button is pressed and enough money has been inserted, the product is dispensed and the machine displays THANK YOU. If the display is checked again, it will display INSERT COIN and the current amount will be set to $0.00. If there is not enough money inserted then the machine displays PRICE and the price of the item and subsequent checks of the display will display either INSERT COIN or the current amount as appropriate.


## Overview  ##

The project is build on .Net 5 framework. The web APIs are built to implement the problem statements. The Unit Testing has been done using xUnit.
It has One VendingMachine controller which is responsible for the following operations.
1. DisplayBalance
2. InsertCoin
3. DispenseProduct

## How to Run ##

As its a web API project so it doesn't has any UI part. You can simply run the application and try to access API usrls through the browser or you can use the postman. For the demo purpose, the authentication has not been implemented so you can access the APIs easily.

The following are the urls by which the API can be aaccessed.

1. To get the available balance and display balance

    `http://localhost:41548/api/vendingmachine/Display`
    
2. To insert the coin, you have to pass two parameters. One for - the type of coin (Quarter, Dime or Nickel) and the second for - how many numbers of the coin.

    `http://localhost:41548/api/vendingmachine/Insert?coin=Quarter&num=2`
    
3. To dispnese the product, you have to pass one parameter that which product (Cola, Candy, Chips) you want to dispense.

    `http://localhost:41548/api/vendingmachine/Dispense?prod=Chips`
