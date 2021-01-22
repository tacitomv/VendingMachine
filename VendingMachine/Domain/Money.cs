using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace VendingMachine.Domain
{
    /// <summary>
    /// Do all the calculeira
    /// </summary>
    public class Money
    {
        public Money()
        {
            Coins = new List<Coin>();
        }

        public Money(List<Coin> coins) : this()
        {
            AddCoins(coins);
        }

        private List<Coin> Coins { get; set; }
        public decimal Total { get; set; } = 0.00m;

        public bool AddCoins(int quantity, decimal value)
        {
            bool bulk = false;
            if (quantity <= 0 || value <= 0) return bulk;

            for (int i = 0; i < quantity; i++)
                bulk = AddCoin(value);

            return bulk;
        }

        public bool AddCoins(List<Coin> coins)
        {
            bool bulk = false;
            foreach (var coin in coins)
                bulk = AddCoin(coin);
            return bulk;
        }

        public bool AddCoin(decimal value)
        {
            if (value <= 0) return false;
            return AddCoin(new Coin(value));
        }

        public bool AddCoin(Coin coin)
        {
            Coins.Add(coin);
            Total += coin.Value;
            return true;
        }

        public bool RemoveCoin(decimal value)
        {
            if (value <= 0) return false;
            var coin = Coins.Find(c => c.Value == value);
            if (coin == null) return false;
            Coins.Remove(coin);
            Total -= value;
            return true;
        }

        public override string ToString() => Coins.Count <= 0 ? "0" :
                Coins.Select(c => c.Value.ToString(new CultureInfo("en-US"))).Aggregate((a, b) => a + " " + b);

        internal Money Calculate(Money newCoins, decimal productValue)
        {
            decimal change = newCoins.Total - productValue;
            List<Coin> result = new List<Coin>();
            if (change != 0 && Total > 0 && Total >= change)
            {
                if (change == newCoins.Total)
                    return new Money(result);

                if (change > newCoins.Total)
                    throw new ArgumentException("Insuficient money for the product");

                AddCoins(newCoins.Coins);

                decimal valueRemoved = 0;
                while (Total > 0 && change > 0)
                {
                    if (Total >= Coin.PossibleCoinValues[5] && change >= Coin.PossibleCoinValues[5])
                    {
                        valueRemoved = RemoveMoney(result, 5);
                    }
                    else if (Total >= Coin.PossibleCoinValues[4] && change >= Coin.PossibleCoinValues[4])
                    {
                        valueRemoved = RemoveMoney(result, 4);
                    }
                    else if (Total >= Coin.PossibleCoinValues[3] && change >= Coin.PossibleCoinValues[3])
                    {
                        valueRemoved = RemoveMoney(result, 3);
                    }
                    else if (Total >= Coin.PossibleCoinValues[2] && change >= Coin.PossibleCoinValues[2])
                    {
                        valueRemoved = RemoveMoney(result, 2);
                    }
                    else if (Total >= Coin.PossibleCoinValues[1] && change >= Coin.PossibleCoinValues[1])
                    {
                        valueRemoved = RemoveMoney(result, 1);
                    }
                    else if (Total >= Coin.PossibleCoinValues[0] && change >= Coin.PossibleCoinValues[0])
                    {
                        valueRemoved = RemoveMoney(result, 0);
                    }
                    change -= valueRemoved;
                }
            }
            return new Money(result);
        }

        private decimal RemoveMoney(List<Coin> result, int index)
        {
            decimal valueRemoved;
            RemoveCoin(Coin.PossibleCoinValues[index]);
            result.Add(new Coin(Coin.PossibleCoinValues[index]));
            valueRemoved = Coin.PossibleCoinValues[index];
            return valueRemoved;
        }
    }
}
