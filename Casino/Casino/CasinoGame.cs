using Casino.Utils;

namespace Casino
{
    public class CasinoGame
    {
        private int _balance;
        private readonly Random _rnd = new Random();
        private const int Multiplier = 1;
        private const int MaxDiceValue = 20;
        private const int WinThreshold = 18;
        private const int RollModulo = 17;

        public int Balance => _balance;

        public CasinoGame( int startingBalance )
        {
            _balance = startingBalance;
        }

        public bool CanBet( int bet )
        {
            return bet > 0 && bet <= _balance;
        }

        public record RoundResult( int Roll, int WinAmount, bool IsWin );

        public RoundResult PlayRound( int bet )
        {
            int roll = RollDice();
            int winAmount = 0;
            bool isWin = false;

            if ( roll >= WinThreshold )
            {
                winAmount = CalculateWin( bet, roll );
                _balance += winAmount;
                isWin = true;
            }
            else
            {
                _balance -= bet;
            }

            return new RoundResult( roll, winAmount, isWin );
        }

        private int RollDice() => _rnd.Next( 1, MaxDiceValue + 1 );

        private int CalculateWin( int bet, int roll )
        {
            int remainder = roll % RollModulo;
            int multiplierEffect = Multiplier * remainder;
            int totalMultiplier = 1 + multiplierEffect;
            int result = bet * totalMultiplier;
            return result;
        }
        
        public static void HandlePlay( CasinoGame game )
        {
            ConsolePrinter.PrintRequestBet();
            int bet = GetBet( game );

            RoundResult result = game.PlayRound( bet );
            ConsolePrinter.PrintRoll( result.Roll );

            if ( result.IsWin )
                ConsolePrinter.PrintWin( result.WinAmount, game.Balance );
            else
                ConsolePrinter.PrintLose( bet, game.Balance );
        }

        private static int GetBet( CasinoGame game )
        {
            while ( true )
            {
                string? inputLine = Console.ReadLine();
                if ( !Program.TryParseInt( inputLine, out int bet ) )
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
    }
}
