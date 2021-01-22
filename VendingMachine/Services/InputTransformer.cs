using System;
using System.Globalization;
using VendingMachine.Domain;
using VendingMachine.Transition;

namespace VendingMachine.Services
{
    public class InputTransformer
    {
        /// <summary>
        /// Any treatment logic needed from the input to the domain objects.
        /// </summary>
        /// <param name="input">Direct input from the console or nay future input.</param>
        /// <returns>A valid Transaction with the cleaned-up input.</returns>
        /// <throws>When the input is null or empty</throws>
        public InputTransaction GetInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Input string cannot be null");

            var inputs = input.Trim().Split(' ');

            decimal value = 0;
            Product product = null;
            string command = string.Empty;
            InputTransaction trans = new InputTransaction();
            foreach (var entry in inputs)
            {
                if (decimal.TryParse(entry, NumberStyles.Any, new CultureInfo("en-US"), out value))
                {
                    trans.AddMoney(new Coin(value));
                }
                else if (Product.TryParse(entry, out product))
                {
                    trans.AddProduct(product);
                }
                else if (Command.TryParse(entry, out command))
                {
                    trans.AddCommand(command);
                }
            }

            return trans;
        }
    }
}
