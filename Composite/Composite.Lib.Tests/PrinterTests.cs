using System;
using System.Collections.Generic;
using Xunit;

namespace Composite.Lib.Tests
{
    public class PrinterTests
    {
        [Fact]
        public void PrintShoppingCart_CartIsEmpty_ReturnsEmptyStringList()
        {
            var shoppingCart = new List<IPrintableProduct>();
            var output = Printer.PrintShoppingCart(shoppingCart);

            Assert.Empty(output);
        }

        [Fact]
        public void PrintShoppingCart_CartContains2Products_Returns2Strings()
        {
            var shoppingCart = new List<IPrintableProduct>
            {
                new Product(),
                new Product()
            };
            var output = Printer.PrintShoppingCart(shoppingCart);

            Assert.Equal(2, output.Count);
        }

        [Fact]
        public void PrintShoppingCart_CartContainsProduct_ReturnedLineContainsProductPropertiesAndTotalPrice()
        {
            var shoppingCart = new List<IPrintableProduct>
            {
                new Product()
                {
                    Quantity = 3,
                    Name = "Parrot",
                    SingleItemPrice = 300.0M
                }
            };

            var output = Printer.PrintShoppingCart(shoppingCart);

            const string expectedOutput = "3\tParrot\t300.00\t900.00";
            Assert.Equal(expectedOutput, output[0]);
        }

        [Fact]
        public void PrintShoppingCart_CartContainsProductGroup_ReturnedLineContainsGroupHeader()
        {
            var shoppingCart = new List<IPrintableProduct>()
            {
                new DiscountedProducts()
                {
                    Name = "Pet Food",
                    DiscountRate = 0.3
                }
            };

            var output = Printer.PrintShoppingCart(shoppingCart);

            const string expectedOutput = "\tPet Food (Discount: 30 %)\t";
            Assert.Equal(expectedOutput, output[0]);
        }
    }
}
