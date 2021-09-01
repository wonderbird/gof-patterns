using System.Collections.Generic;

namespace Composite.Logic
{
    public class DiscountedProducts : IPrintableProduct
    {
        public double DiscountRate { get; set; }

        public string Name { get; set; }

        public List<string> PrintShoppingCartEntries()
        {
            var productLine = "\t" +
                              $"{Name} (Discount: {DiscountRate:0 %})" +
                              "\t" +
                              "";

            return new List<string> { productLine };
        }
    }
}