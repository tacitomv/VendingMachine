using NUnit.Framework;
using System;
using VendingMachine.Domain;

namespace VendingMachine.Test
{
    public class CoinTest
    {
        [TestCase(new object[] { "0.01", true })]
        [TestCase(new object[] { "0.05", true })]
        [TestCase(new object[] { "0.10", true })]
        [TestCase(new object[] { "0.25", true })]
        [TestCase(new object[] { "0.50", true })]
        [TestCase(new object[] { "0.76", false })]

        public void CreateCoin(decimal value, bool assert)
        {
            try
            {
                Coin coin = new Coin(value);

                Assert.AreEqual(true, assert);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual(false, assert);
            }
        }
    }
}