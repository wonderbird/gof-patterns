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
            var shoppingCart = new List<Product>();
            var output = Printer.PrintShoppingCart(shoppingCart);

            Assert.Empty(output);
        }

        [Fact]
        public void PrintShoppingCart_CartContains2Products_Returns2Strings()
        {
            var shoppingCart = new List<Product>
            {
                new(),
                new()
            };
            var output = Printer.PrintShoppingCart(shoppingCart);

            Assert.Equal(2, output.Count);
        }

        [Fact]
        public void PrintShoppingCart_CartContainsProduct_ReturnedLineContainsProductProperties()
        {
            var shoppingCart = new List<Product>()
            {
                new()
                {
                    Quantity = 3,
                    Name = "Parrot",
                    SingleItemPrice = 300.0M
                }
            };
            
            var output = Printer.PrintShoppingCart(shoppingCart);

            assertLineContainsProductProperties(shoppingCart[0], output[0]);
        }

        [Fact]
        public void PrintShoppingCart_CartContainsProduct_ReturnedLineContainsTotalPrice()
        {
            var quantity = 3;
            var singleItemPrice = 300.0M;
            var expectedTotalPrice = quantity * singleItemPrice;

            var shoppingCart = new List<Product>()
            {
                new()
                {
                    Quantity = quantity,
                    Name = "Parrot",
                    SingleItemPrice = singleItemPrice
                }
            };

            var output = Printer.PrintShoppingCart(shoppingCart);

            AssertValueAtIndexEquals(3, expectedTotalPrice, output[0]);
        }

        private void assertLineContainsProductProperties(Product product, string printedLine)
        {
            AssertValueAtIndexEquals(0, product.Quantity, printedLine);
            AssertValueAtIndexEquals(1, product.Name, printedLine);
            AssertValueAtIndexEquals(2, product.SingleItemPrice, printedLine);
        }

        private static void AssertValueAtIndexEquals<T>(int index, T expected, string printedLine)
            where T : IConvertible
        {
            var propertyValueStrings = printedLine.Split('\t');

            var valueString = propertyValueStrings[index];
            var actual = Convert.ChangeType(valueString, typeof(T));
            Assert.Equal(expected, actual);
        }
    }
}
