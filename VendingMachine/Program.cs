using System;
using VendingMachine.Domain;
using VendingMachine.Services;

namespace VendingMachine
{
    public class Program
    {
        static void Main(string[] args)
        {
            Machine vm = Machine.BuildAVendingMachine();
            InputTransformer inputTransformer = new InputTransformer();
            OutputTransformer outputTransformer = new OutputTransformer();

            Console.Clear();
            Console.WriteLine("Welcome to the Vending Machine V0.1!");
            Console.WriteLine("(Sorry, no menus. You need a manual to operate this thing)");

            string command = string.Empty;

            do
            {
                try
                {
                    command = Console.ReadLine();

                    var input = inputTransformer.GetInput(command);
                    var output = vm.Request(input);
                    Console.WriteLine(outputTransformer.GetOutput(output));
                }
                catch(ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }
            while (!string.IsNullOrEmpty(command));
        }
    }
}
