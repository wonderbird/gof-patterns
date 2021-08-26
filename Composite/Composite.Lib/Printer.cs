using System.Collections.Generic;
using System.Linq;

namespace Composite.Lib
{
    public static class Printer
    {
        public static List<string> PrintShoppingCart(List<Product> shoppingCart)
        {
            var printedProducts = shoppingCart.Select(
                product => $"{product.Quantity}\t{product.Name}\t{product.SingleItemPrice}").ToList();
            return printedProducts;
        }
    }
}
