using Fighters.Models.Fighters;
using Fighters.Utils;
using Fighters.Config;

namespace Fighters
{
    public class GameManager
    {
        private readonly List<IFighter> fighters = [];

        public bool CanStartFight()
        {
            if ( fighters.Count < GameConfig.MinFighters )
            {
                ConsolePrinter.PrintNoFightersError();
                return false;
            }
            return true;
        }

        public bool IsMaxFightersReached()
        {
            if ( fighters.Count >= GameConfig.MaxFighters )
            {
                ConsolePrinter.PrintMaxFightersReached( GameConfig.MaxFighters );
                return true;
            }
            return false;
        }

        public void AddFighter( IFighter fighter )
        {
            if ( fighters.Count >= GameConfig.MaxFighters )
            {
                ConsolePrinter.PrintMaxFightersReached( GameConfig.MaxFighters );
                return;
            }

            fighters.Add( fighter );
            ConsolePrinter.PrintFighterAdded();
        }


        public void StartFight()
        {
            WriteFightersInfo();
            int round = 1;

            while ( fighters.Count( f => f.IsAlive ) > 1 && round <= GameConfig.MaxRounds )
            {
                ConsolePrinter.PrintRound( round );

                for ( int i = 0; i < fighters.Count; i++ )
                {
                    IFighter attacker = fighters[ i ];
                    if ( !attacker.IsAlive )
                    {
                        continue;
                    }

                    IFighter? target = GetNextAliveFighter( i );
                    if ( target != null )
                        PerformAttack( attacker, target );
                }

                round++;
            }

            PrintWinner( round );
        }

        private IFighter? GetNextAliveFighter( int currentIndex )
        {
            int nextIndex = ( currentIndex + 1 ) % fighters.Count;
            while ( nextIndex != currentIndex )
            {
                if ( fighters[ nextIndex ].IsAlive )
                    return fighters[ nextIndex ];

                nextIndex = ( nextIndex + 1 ) % fighters.Count;
            }
            return null;
        }

        private void PerformAttack( IFighter attacker, IFighter target )
        {
            double finalDamage = attacker.Attack( target );
            ConsolePrinter.PrintAttack( attacker.Name, target.Name, finalDamage, target.CurrentHealth );

            if ( !target.IsAlive )
                ConsolePrinter.PrintFighterDeath( target.Name );
        }

        private void WriteFightersInfo()
        {
            ConsolePrinter.PrintFighterInfoHeader();
            foreach ( IFighter fighter in fighters )
                ConsolePrinter.PrintFighterInfo( fighter );

            Console.WriteLine();
        }

        private void PrintWinner( int round )
        {
            if ( round > GameConfig.MaxRounds )
            {
                ConsolePrinter.PrintDraw();
                return;
            }

            IFighter? winner = fighters.FirstOrDefault( f => f.IsAlive );
            if ( winner != null )
                ConsolePrinter.PrintWinner( winner.Name );
        }
    }
}
