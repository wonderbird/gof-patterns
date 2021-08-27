using System.Collections.Generic;

namespace Composite.Lib
{
    public class Product : IPrintableProduct
    {
        public int Quantity { get; set; }
        public string Name { get; set; }
        public decimal SingleItemPrice { get; set; }

        public List<string> PrintShoppingCartEntries()
        {
            var productLine = $"{Quantity}\t" +
                              $"{Name}\t" +
                              $"{SingleItemPrice:0.00}\t" +
                              $"{Quantity * SingleItemPrice:0.00}";

            return new List<string> { productLine };
        }
    }
}