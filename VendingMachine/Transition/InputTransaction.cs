using System;
using System.Globalization;
using System.Linq;

namespace VendingMachine.Transition
{
    public class InputTransaction : Transaction
    {
        public override string ToString()
        {
            string products = Products.Select(s => s.Name).Aggregate((a, b) => a + " " + b);
            return string.Format(new CultureInfo("en-US"), "{0} {1} {2}", Money, products, Command).Trim();
        }

        internal decimal ProductsTotal()
        {
            return Products.Select(p => p.Value).Sum();
        }
    }
}
