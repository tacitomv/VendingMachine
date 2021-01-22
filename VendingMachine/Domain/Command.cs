namespace VendingMachine.Domain
{
    public class Command
    {
        public static readonly string[] PossibleCommandNames = { "CHANGE", "NO_CHANGE", "NO_COINS", "NO_MONEY", "NO_PRODUCT" };

        public Command() { }

        internal static bool TryParse(string entry, out string command)
        {
            foreach (var commandName in PossibleCommandNames)
                if (commandName == entry)
                {
                    command = commandName;
                    return true;
                }
            command = string.Empty;
            return false;
        }
    }
}
