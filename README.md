##Description
We want you to design and develop an application for a vendor machine. 
Within this exercise, we will observe your decisions regarding architecture, code clarity, quality and maintainability.
##Requirements
###The customer should be able to insert coins:
To insert coins the customer should enter the value of the coin via standard input.
The machine should consider the coins inserted for change.
To insert multiple coins the customer should provide the values separated by space.
The available coins are 0.01, 0.05, 0.10, 0.25, 0.50 and 1.00
###The customer should be able to retrieve change:
To retrieve change the customer should write to the standard input CHANGE.
The change should be provided as a list of coins.
If there are not enough coins to provide the change the vendor machine should write to the standard output NO_COINS.
If there is no change to be returned the vendor machine should write to the standard output NO_CHANGE.
Consider that the vendor machine contains 10 units of each coin.
###The customer should be able to request a product:
To retrieve a product the customer should write to the standard input the name of the product.
If the product is not available, the vendor machine should write to the standard output NO_PRODUCT
The available products are Coke (cost 1.50), Water (cost: 1.00) and Pastelina (cost: 0.30)
Consider that the vendor machine contains 10 unites of each product.
When the customer request a product, the vendor machine should write to the standard output the name of the product and the amount of money remaining by the customer preceded by an equal.
##Samples
Insert coins and request a product:
Input: 0.50 1.00 Coke
Output: Coke =0
Insert too many value for a product:
Input: 1.00 Pastelina CHANGE
Output:  Pastelina =0.70 0.50 0.10 0.10
When there is no change:
Input: 0.25 0.05 Pastelina CHANGE
Output:  Pastelina =0.00 NO_CHANGE
When requesting multiple products:
Input: 1.00 Pastelina Pastelina Pastelina
Output: Pastelina =0.70 Pastelina =0.40 Pastelina =0.10

##Considerations
Develop this application as a Console application.
The application should be easy to build and deploy
You can use either Visual Studio or Visual Studio Code – and use C#
When the code is modified, you should be able to guarantee that previous functionality is still working as expected
Another developer should be able to quickly get the source code and start coding/testing
Write this application as you would deliver to production, not as a demo
The code must be testable and easy to change
