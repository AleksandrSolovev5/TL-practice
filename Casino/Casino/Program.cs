using Casino.Utils;

namespace Casino
{
    public class Program
    {
        public static void Main()
        {
            ConsolePrinter.PrintGameName();
            int balance = ReadInitialBalance();
            CasinoGame game = new CasinoGame( balance );
            Operation operation = Operation.Unknown;

            while ( operation != Operation.Exit )
            {
                ConsolePrinter.PrintMenu();
                try
                {
                    operation = ReadOperation();

                    switch ( operation )
                    {
                        case Operation.Unknown:
                            throw new ArgumentOutOfRangeException();

                        case Operation.Play:
                            operation = HandlePlayOperation( game );
                            break;

                        case Operation.CheckBalance:
                            ConsolePrinter.PrintCurrentBalance( game.Balance );
                            break;

                        case Operation.Exit:
                            ConsolePrinter.PrintThankYou();
                            break;
                    }
                }
                catch ( ArgumentOutOfRangeException )
                {
                    ConsolePrinter.PrintInvalidSelection();
                }
            }
        }

        private static Operation ReadOperation()
        {
            string? inputLine = Console.ReadLine();
            if ( !TryParseInt( inputLine, out int choice ) )
                return Operation.Unknown;

            if ( choice < 1 || choice > 3 )
                return Operation.Unknown;

            return ( Operation )choice;
        }

        public static bool TryParseInt( string? inputLine, out int value )
        {
            return int.TryParse( inputLine, out value );
        }

        private static Operation HandlePlayOperation( CasinoGame game )
        {
            CasinoGame.HandlePlay( game );

            if ( game.Balance == 0 )
            {
                ConsolePrinter.PrintGameOver();
                return Operation.Exit;
            }
            return Operation.Play;
        }

        private static int ReadInitialBalance()
        {
            while ( true )
            {
                ConsolePrinter.PrintRequestBalance();
                string? balanceInput = Console.ReadLine();

                if ( !TryParseInt( balanceInput, out int balance ) || balance <= 0 )
                {
                    ConsolePrinter.PrintInvalidBalance();
                    continue;
                }

                return balance;
            }
        }
    }
}
