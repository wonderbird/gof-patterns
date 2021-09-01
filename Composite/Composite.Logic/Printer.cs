using System.Collections.Generic;
using System.Linq;

namespace Composite.Logic
{
    public static class Printer
    {
        public static List<string> PrintShoppingCart(List<IPrintableProduct> shoppingCart)
        {
            var printedProducts = shoppingCart.SelectMany(printable => printable.PrintShoppingCartEntries()).ToList();
            return printedProducts;
        }
    }
}