namespace Casino
{
    public class CasinoGame
    {
        private int _balance;
        private readonly Random _rnd = new Random();
        private const int Multiplier = 1;

        public int Balance => _balance;

        public CasinoGame( int startingBalance )
        {
            _balance = startingBalance;
        }

        public bool CanBet( int bet )
        {
            return bet > 0 && bet <= _balance;
        }

        public bool PlayRound( int bet, out int roll, out int winAmount )
        {
            roll = RollDice();
            winAmount = 0;

            if ( roll >= 18 )
            {
                winAmount = CalculateWin( bet, roll );
                _balance += winAmount;
                return true;
            }

            _balance -= bet;
            return false;
        }

        private int RollDice() => _rnd.Next( 1, 21 );

        private int CalculateWin( int bet, int roll )
        {
            return bet * ( 1 + ( Multiplier * ( roll % 17 ) ) );
        }
    }
}
