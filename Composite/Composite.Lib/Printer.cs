using System.Collections.Generic;
using System.Linq;

namespace Composite.Lib
{
    public static class Printer
    {
        public static List<string> PrintShoppingCart(List<Product> shoppingCart)
        {
            var printedProducts = shoppingCart.Select(product =>
            {
                string productLine = "";

                if (product is DiscountedProducts)
                {
                    var discountedProducts = (DiscountedProducts)product;
                    productLine = $"\t" +
                                  $"{discountedProducts.Name} (Discount: {discountedProducts.DiscountRate:0 %})" +
                                  $"\t" +
                                  $"";
                }
                else
                {
                    productLine = $"{product.Quantity}\t" +
                                  $"{product.Name}\t" +
                                  $"{product.SingleItemPrice:0.00}\t" +
                                  $"{product.Quantity * product.SingleItemPrice:0.00}";
                }

                return productLine;
            }).ToList();
            return printedProducts;
        }
    }
}
