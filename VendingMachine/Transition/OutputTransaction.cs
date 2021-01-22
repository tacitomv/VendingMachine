namespace VendingMachine.Transition
{
    public class OutputTransaction : Transaction
    {
        public OutputTransaction(InputTransaction entrada) : base()
        {
            Money = entrada.Money;
            Command = entrada.Command;
        }

        public override string ToString()
        {
            return $"{Money} {Products} ";
        }
    }
}
