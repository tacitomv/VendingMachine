using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Domain;
using VendingMachine.Transition;

namespace VendingMachine.Domain
{
    /// <summary>
    /// The Vending Machine
    /// </summary>
    public class Machine
    {
        private List<Product> Products { get; set; }
        private Money Money { get; set; }

        public Machine(List<Product> products, Money money)
        {
            Products = products;
            Money = money;
        }

        public OutputTransaction Request(InputTransaction input)
        {
            var output = new OutputTransaction(input);

            if (!HasStock(input.Products))
            {
                output.Command = Command.PossibleCommandNames[4];
                return output;
            }
            
            //valor de entrada é igual ao total dos produtos
            if (input.Money.Total == input.ProductsTotal())
            {
                decimal rest = input.Money.Total;
                foreach (var prod in input.Products)
                {
                    var product = Products.FirstOrDefault(f => prod.Name == f.Name);
                    Products.Remove(product);
                    rest -= product.Value;
                    product.Value = rest;
                    output.Products.Add(product);
                }
                output.Money = new Money();
            }
            else
            {
                foreach (var prod in input.Products)
                {
                    var product = Products.FirstOrDefault(f => prod.Name == f.Name);

                    if (product != null && output.Money.Total >= product.Value)
                    {
                        if (!HasChange(input.Money, product.Value))
                        {
                            output.Command = Command.PossibleCommandNames[1];
                            break;
                        }
                        //calculate change
                        Products.Remove(product);
                        output.Money = Money.Calculate(output.Money, product.Value);
                        product.Value = output.Money.Total;
                        output.Products.Add(product);
                    }
                    else if (product == null)
                    {
                        output.Command = Command.PossibleCommandNames[4];
                        break;
                    }
                    else if (output.Money.Total == 0 || output.Money.Total <= product.Value)
                    {
                        output.Command = Command.PossibleCommandNames[3];
                        break;
                    }
                }
            }

            return output;
        }

        private bool HasChange(Money money, decimal value) => Money.Total >= money.Total - value;

        private bool HasStock(List<Product> products)
        {
            var result = Products.Where(w => products.Any(a => a.Name == w.Name));
            return result.Count() >= products.Count;
        }

        public int GetProductsQuantity() => Products.Count;

        public decimal GetTotal() => Money.Total;

        public static Machine BuildAVendingMachine()
        {
            List<Product> products = new List<Product>();
            products.AddRange(Product.Generate(10, Product.PossibleProducts[0]));
            products.AddRange(Product.Generate(10, Product.PossibleProducts[1]));
            products.AddRange(Product.Generate(10, Product.PossibleProducts[2]));

            Money money = new Money();
            money.AddCoins(10, 0.01m);
            money.AddCoins(10, 0.05m);
            money.AddCoins(10, 0.1m);
            money.AddCoins(10, 0.25m);
            money.AddCoins(10, 0.5m);
            money.AddCoins(10, 1m);

            return new Machine(products, money);
        }
    }
}
