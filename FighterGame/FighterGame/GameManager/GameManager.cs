using Fighters.Models.Fighters;
using Fighters.Extensions;

namespace Fighters
{
    public class GameManager
    {
        private IFighter? Fighter1 { get; set; }
        private IFighter? Fighter2 { get; set; }
        private Random random = new Random();
        private const int MaxRounds = 100;

        public void SetFighters( IFighter fighter1, IFighter fighter2 )
        {
            Fighter1 = fighter1;
            Fighter2 = fighter2;
        }

        public void StartFight()
        {
            if ( Fighter1 == null || Fighter2 == null )
            {
                Console.WriteLine( "Two fighters are required to start a fight." );
                return;
            }

            WriteInfo();
            int round = 1;

            // случайный выбор, кто первый начнёт
            bool firstStarts = random.Next( 2 ) == 0;
            IFighter firstFighter = firstStarts ? Fighter1 : Fighter2;
            IFighter secondFighter = firstStarts ? Fighter2 : Fighter1;
            Console.WriteLine( $"{firstFighter.Name} starts the fight!!" );

            while ( firstFighter.IsAlive() && secondFighter.IsAlive() && round <= MaxRounds )
            {
                Console.WriteLine( $"\n=== Round {round} ===" );

                PerformAttack( firstFighter, secondFighter );
                if ( !secondFighter.IsAlive() ) break;

                PerformAttack( secondFighter, firstFighter );

                round++;
            }

            if ( round > MaxRounds )
            {
                Console.WriteLine( "Draw! Round limit exceeded." );
                Environment.Exit( 0 );
            }

            if ( Fighter1.IsAlive() )
            {
                Console.WriteLine( $"\n{Fighter1.Name} wins the fight!" );
            }
            else
            {
                Console.WriteLine( $"\n{Fighter2.Name} wins the fight!" );
            }
            Environment.Exit( 0 );
        }

        private void PerformAttack( IFighter attacker, IFighter target )
        {
            double finalDamage = attacker.Attack( target );
            Console.WriteLine( $"{attacker.Name} attacks {target.Name} with {finalDamage} damage, {target.Name}'s health: {target.GetCurrentHealth()}" );
        }
        private void WriteInfo()
        {
            Console.WriteLine( "\n=== Fighter Information ===" );
            Console.WriteLine( $"{Fighter1.Name} - Base damage: {Fighter1.CalculateDamage()}, Health: {Fighter1.GetMaxHealth()}, Armor: {Fighter1.CalculateArmor()}" );
            Console.WriteLine( $"{Fighter2.Name} - Base damage: {Fighter2.CalculateDamage()}, Health: {Fighter2.GetMaxHealth()}, Armor: {Fighter2.CalculateArmor()}" );
            Console.WriteLine();
        }
    }
}