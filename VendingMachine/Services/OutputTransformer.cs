using System.Linq;
using VendingMachine.Domain;
using VendingMachine.Transition;

namespace VendingMachine.Services
{
    public class OutputTransformer
    {
        public string GetOutput(OutputTransaction saida)
        {
            string result = string.Empty;
            if (saida.Command == Command.PossibleCommandNames[0] && saida.Money.Total > 0)
                result = saida.Money.ToString();
            else if (saida.Command == Command.PossibleCommandNames[0] && saida.Money.Total == 0)
                result = Command.PossibleCommandNames[1];
            else
                result = saida.Command;

            string products = string.Empty;
            if (saida.Products.Count > 0)
                products = saida.Products?.Select(s => s.ToString()).Aggregate((a, b) => a + " " + b);

            return string.Format("{0} {1}", products, result).Trim();
        }
    }
}
