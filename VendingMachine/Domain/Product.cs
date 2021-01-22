using System.Collections.Generic;
using System.Globalization;

namespace VendingMachine.Domain
{
    public class Product
    {
        public static readonly List<Product> PossibleProducts = new List<Product>(){
            new Product("Coke", 1.50m),
            new Product("Water", 1.00m),
            new Product("Pastelina", 0.30m)
        };

        public Product(string name, decimal value)
        {
            Name = name;
            Value = value;
        }
        public string Name { get; set; }
        public decimal Value { get; set; }

        internal static bool TryParse(string entry, out Product product)
        {
            foreach (var productName in PossibleProducts)
                if (productName.Name == entry)
                {
                    product = (Product)productName.MemberwiseClone();
                    return true;
                }
            product = null;
            return false;
        }

        public static IEnumerable<Product> Generate(int quantity, Product product)
        {
            List<Product> products = new List<Product>();
            for (int i = 0; i < quantity; i++)
                products.Add((Product)product.MemberwiseClone());

            return products;
        }

        public override string ToString()
        {
            return string.Format(new CultureInfo("en-US"), "{0} ={1}", Name, Value);
        }
    }
}
