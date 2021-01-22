using System.Collections.Generic;
using VendingMachine.Domain;

namespace VendingMachine.Transition
{
    public abstract class Transaction
    {
        public Transaction()
        {
            Money = new Money();
            Products = new List<Product>();
        }
        public Money Money { get; set; }
        public List<Product> Products { get; set; }
        public string Command { get; set; }

        internal void AddMoney(Coin coin) => Money.AddCoin(coin);

        internal void AddProduct(Product product) => Products.Add(product);

        internal void AddCommand(string command) => Command = command;
    }
}