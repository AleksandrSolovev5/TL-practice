using Fighters.Extensions;
using Fighters.Models.Fighters;
using Fighters.Utils;

namespace Fighters
{
    public class GameManager
    {
        private IFighter? Fighter1 { get; set; }
        private IFighter? Fighter2 { get; set; }
        private readonly Random random = new Random();

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
                ConsolePrinter.PrintNoFightersError();
                return false;
            }

            WriteFightersInfo();
            int round = 1;

            (IFighter firstFighter, IFighter secondFighter) = ChooseFirstFighter();

            PlayRounds( firstFighter, secondFighter, round );

            PrintWinner( firstFighter, secondFighter, round );
            return true;
        }

        private void PerformAttack( IFighter attacker, IFighter target )
        {
            double finalDamage = attacker.Attack( target );
            ConsolePrinter.PrintAttack( attacker.Name, target.Name, finalDamage, target.CurrentHealth );
        }

        private void WriteFightersInfo()
        {
            ConsolePrinter.PrintFighterInfoHeader();
            ConsolePrinter.PrintFighterInfo( Fighter1! );
            ConsolePrinter.PrintFighterInfo( Fighter2! );
            Console.WriteLine();
        }

        private (IFighter firstFighter, IFighter secondFighter) ChooseFirstFighter()
        {
            bool firstStarts = random.Next( ChoicesForStart ) == 0;
            IFighter? firstFighter = firstStarts ? Fighter1 : Fighter2;
            IFighter? secondFighter = firstStarts ? Fighter2 : Fighter1;
            ConsolePrinter.PrintFighterStart( firstFighter.Name );
            return (firstFighter, secondFighter);
        }

        private void PlayRounds( IFighter firstFighter, IFighter secondFighter, int round )
        {
            while ( firstFighter.IsAlive() && secondFighter.IsAlive() && round <= MaxRounds )
            {
                ConsolePrinter.PrintRound( round );

                PerformAttack( firstFighter, secondFighter );
                if ( !secondFighter.IsAlive() ) break;

                PerformAttack( secondFighter, firstFighter );

                round++;
            }
        }

        private void PrintWinner( IFighter firstFighter, IFighter secondFighter, int round )
        {
            if ( round > MaxRounds )
            {
                ConsolePrinter.PrintDraw();
                return;
            }
            if ( firstFighter.IsAlive() )
            {
                ConsolePrinter.PrintWinner( firstFighter.Name );
            }
            else
            {
                ConsolePrinter.PrintWinner( secondFighter.Name );
            }
        }
    }
}