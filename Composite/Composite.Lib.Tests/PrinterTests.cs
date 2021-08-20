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
        public void PrintShoppingCart_CartContainsProducts_ReturnsOneStringPerProduct()
        {
            var shoppingCart = new List<Product>()
            {
                new(),
                new()
            };
            var output = Printer.PrintShoppingCart(shoppingCart);

            Assert.Equal(2, output.Count);
        }
    }
}
