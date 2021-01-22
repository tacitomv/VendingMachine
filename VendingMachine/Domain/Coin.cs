using System;

namespace VendingMachine.Domain
{
    public class Coin
    {
        public static readonly decimal[] PossibleCoinValues = { 0.01m, 0.05m, 0.10m, 0.25m, 0.50m, 1.00m };
        public Coin(decimal value)
        {
            if (IsValid(value))
                Value = value;
            else
                throw new ArgumentException("Not a possible Coin value.");
        }

        private bool IsValid(decimal value)
        {
            foreach (var coinValue in PossibleCoinValues)
                if (coinValue == value) return true;
            return false;
        }

        public decimal Value { get; set; }
    }
}
