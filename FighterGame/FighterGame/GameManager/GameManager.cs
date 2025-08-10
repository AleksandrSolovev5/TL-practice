using Fighters.Models.Fighters;
using Fighters.Extensions;
using Fighters.Utils;

namespace Fighters
{
    public class GameManager
    {
        private IFighter? Fighter1 { get; set; }
        private IFighter? Fighter2 { get; set; }
        private Random random = new Random();
        private const int MaxRounds = 100;
        private const int ChoicesForStart = 2;

        public void SetFighters( IFighter fighter1, IFighter fighter2 )
        {
            Fighter1 = fighter1;
            Fighter2 = fighter2;
        }

        public bool StartFight()
        {
            if ( Fighter1 == null || Fighter2 == null )
            {
                ConsolePrinter.PrintMessage( "Two fighters are required to start a fight." );
                return false;
            }

            WriteInfo();
            int round = 1;

            // случайный выбор, кто первый начнёт
            bool firstStarts = random.Next( ChoicesForStart ) == 0;
            IFighter firstFighter = firstStarts ? Fighter1 : Fighter2;
            IFighter secondFighter = firstStarts ? Fighter2 : Fighter1;
            ConsolePrinter.PrintFighterStart( firstFighter.Name );

            while ( firstFighter.IsAlive() && secondFighter.IsAlive() && round <= MaxRounds )
            {
                ConsolePrinter.PrintRound( round );

                PerformAttack( firstFighter, secondFighter );
                if ( !secondFighter.IsAlive() ) break;

                PerformAttack( secondFighter, firstFighter );

                round++;
            }

            if ( round > MaxRounds )
            {
                ConsolePrinter.PrintDraw();
                return true;
            }

            if ( Fighter1.IsAlive() )
            {
                ConsolePrinter.PrintWinner( Fighter1.Name );
            }
            else
            {
                ConsolePrinter.PrintWinner( Fighter2.Name );
            }
            return true;
        }

        private void PerformAttack( IFighter attacker, IFighter target )
        {
            double finalDamage = attacker.Attack( target );
            ConsolePrinter.PrintAttack( attacker.Name, target.Name, finalDamage, target.CurrentHealth );
        }
        private void WriteInfo()
        {
            ConsolePrinter.PrintFighterInfoHeader();
            ConsolePrinter.PrintFighterInfo( Fighter1! );
            ConsolePrinter.PrintFighterInfo( Fighter2! );
            Console.WriteLine();
        }
    }
}