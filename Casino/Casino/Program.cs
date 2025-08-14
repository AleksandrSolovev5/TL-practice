using Casino.Utils;

namespace Casino
{
    public class Program
    {
        public static void Main()
        {
            ConsolePrinter.PrintGameName();

            ConsolePrinter.PrintRequestBalance();
            string balanceInput = Console.ReadLine();
            if ( !int.TryParse( balanceInput, out int balance ) || balance <= 0 )
            {
                ConsolePrinter.PrintInvalidBalance();
                return;
            }

            CasinoGame game = new CasinoGame( balance );

            Operation? operation = null;

            while ( operation != Operation.Exit )
            {
                ConsolePrinter.PrintMenu();
                operation = ReadOperation();
                if ( operation == null )
                {
                    ConsolePrinter.PrintInvalidSelection();
                    continue;
                }

                switch ( operation.Value )
                {
                    case Operation.Play:
                        HandlePlay( game );
                        if ( game.Balance == 0 )
                        {
                            ConsolePrinter.PrintGameOver();
                            return;
                        }
                        break;

                    case Operation.CheckBalance:
                        ConsolePrinter.PrintCurrentBalance( game.Balance );
                        break;

                    case Operation.Exit:
                        ConsolePrinter.PrintThankYou();
                        break;
                }
            }
        }

        private static void HandlePlay( CasinoGame game )
        {
            ConsolePrinter.PrintRequestBet();
            int bet = GetBet( game );

            bool win = game.PlayRound( bet, out int roll, out int winAmount );
            ConsolePrinter.PrintRoll( roll );

            if ( win )
                ConsolePrinter.PrintWin( winAmount, game.Balance );
            else
                ConsolePrinter.PrintLose( bet, game.Balance );
        }

        private static int GetBet( CasinoGame game )
        {
            while ( true )
            {
                string inputLine = Console.ReadLine();
                if ( !int.TryParse( inputLine, out int bet ) )
                {
                    ConsolePrinter.PrintInvalidBet( game.Balance );
                    continue;
                }

                if ( !game.CanBet( bet ) )
                {
                    if ( bet > game.Balance )
                        ConsolePrinter.PrintBetExceedsBalance( game.Balance );
                    else
                        ConsolePrinter.PrintInvalidBet( game.Balance );

                    continue;
                }

                return bet;
            }
        }

        private static Operation? ReadOperation()
        {
            string inputLine = Console.ReadLine();
            if ( !int.TryParse( inputLine, out int choice ) )
                return null;

            if ( choice < 1 || choice > 3 )
                return null;

            return ( Operation )choice;
        }

    }
}
