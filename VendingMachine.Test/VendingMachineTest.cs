using NUnit.Framework;
using System;
using System.Collections.Generic;
using VendingMachine.Domain;
using VendingMachine.Services;
using VendingMachine.Transition;

namespace VendingMachine.Test
{
    public class VendingMachineTest
    {
        private Machine vm;
        private InputTransformer inputTransformer;
        private OutputTransformer outputTransformer;

        [SetUp]
        public void Setup()
        {
            vm = Machine.BuildAVendingMachine();

            inputTransformer = new InputTransformer();
            outputTransformer = new OutputTransformer();
        }

        [TestCase(new object[] { "0.50 1.00 Coke", "Coke =0.00", 29 })]
        [TestCase(new object[] { "1.00 Pastelina CHANGE", "Pastelina =0.70 0.50 0.10 0.10", 29 })]
        [TestCase(new object[] { "0.25 0.05 Pastelina CHANGE", "Pastelina =0.00 NO_CHANGE", 29 })]
        [TestCase(new object[] { "1.00 Pastelina Pastelina Pastelina", "Pastelina =0.70 Pastelina =0.40 Pastelina =0.10", 27 })]
        public void InsertCoinsAndRequestAProduct(string input, string output, int productsQuantity)
        {
            InputTransaction entrada = inputTransformer.GetInput(input);
            var transaction = vm.Request(entrada);
            string saida = outputTransformer.GetOutput(transaction);

            Assert.AreEqual(input, entrada.ToString());
            Assert.AreEqual(output, saida);
            Assert.AreEqual(productsQuantity, vm.GetProductsQuantity());
        }
    }
}