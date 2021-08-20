using System.Collections.Generic;
using System.Linq;

namespace Composite.Lib
{
    public static class Printer
    {
        public static List<string> PrintShoppingCart(List<Product> shoppingCart)
        {
            var printedProducts = shoppingCart.Select(product => "").ToList();
            return printedProducts;
        }
    }
}
