using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Domain;
using VendingMachine.Services;

namespace VendingMachine.Test
{
    class VendingMachineEdgeCasesTest
    {
        private Machine vm;
        private InputTransformer inputTransformer;
        private OutputTransformer outputTransformer;

        [SetUp]
        public void Setup()
        {
            List<Product> products = new List<Product>();
            products.AddRange(Product.Generate(1, Product.PossibleProducts[0]));
            products.AddRange(Product.Generate(2, Product.PossibleProducts[1]));
            products.AddRange(Product.Generate(3, Product.PossibleProducts[2]));

            Money money = new Money();
            money.AddCoins(3, 0.01m);

            vm = new Machine(products, money);

            inputTransformer = new InputTransformer();
            outputTransformer = new OutputTransformer();
        }

        [TestCase(new object[] { "0.01 Coke", "NO_MONEY" })]
        [TestCase(new object[] { "1.00 1.00 Coke Coke", "NO_PRODUCT" })]
        [TestCase(new object[] { "1.00 1.00 1.00 Coke Coke", "NO_PRODUCT" })]
        [TestCase(new object[] { "0.50 1.00 Coke Coke Coke Coke", "NO_PRODUCT" })]
        [TestCase(new object[] { "0.50 0.10 Pastelina Pastelina", "Pastelina =0.30 Pastelina =0.00" })]
        [TestCase(new object[] { "0.10 0.10 0.10 1.00 Pastelina Water", "Pastelina =1.00 Water =0.00" })]
        [TestCase(new object[] { "0.50 0.10 Pastelina Pastelina CHANGE", "Pastelina =0.30 Pastelina =0.00 NO_CHANGE" })]
        [TestCase(new object[] { "1.00 Pastelina", "NO_CHANGE" })]
        public void InsertCoinsAndRequestProducts(string input, string output)
        {
            var entrada = inputTransformer.GetInput(input);
            var transaction = vm.Request(entrada);
            string saida = outputTransformer.GetOutput(transaction);

            Assert.AreEqual(input, entrada.ToString());
            Assert.AreEqual(output, saida);
        }
    }
}
