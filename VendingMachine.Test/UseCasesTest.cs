using NUnit.Framework;
using System;
using System.Collections.Generic;
using VendingMachine.Domain;
using VendingMachine.Services;
using VendingMachine.Transition;

namespace VendingMachine.Test
{
    public class UseCasesTest
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

        [Test]
        public void ShouldBeAbleToInsertCoins()
        {
            var input = "0.50 1.00 Coke";
            var output = "Coke =0.00";

            InputTransaction entrada = inputTransformer.GetInput(input);
            var transaction = vm.Request(entrada);
            string saida = outputTransformer.GetOutput(transaction);

            Assert.AreEqual(input, entrada.ToString());
            Assert.AreEqual(output, saida);
        }

        [Test]
        public void ShouldBeAbleToRetrieveChange()
        {
            var input = "1.00 Pastelina CHANGE";
            var output = "Pastelina =0.70 0.50 0.10 0.10";

            InputTransaction entrada = inputTransformer.GetInput(input);
            var transaction = vm.Request(entrada);
            string saida = outputTransformer.GetOutput(transaction);

            Assert.AreEqual(input, entrada.ToString());
            Assert.AreEqual(output, saida);
        }

        [Test]
        public void ShouldBeUpsetAboutProduct()
        {
            var input = "1.00 Guaraná";
            try
            {
                InputTransaction entrada = inputTransformer.GetInput(input);
                var transaction = vm.Request(entrada);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Product is invalid or not available", e.Message);
            }
        }
    }
}