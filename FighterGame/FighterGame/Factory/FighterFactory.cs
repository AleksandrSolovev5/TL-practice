using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Classes;
using Fighters.Models.Weapons;
using Fighters.Models.Armors;
using Fighters.Models;
using Fighters.Factory;

namespace Fighters
{
    public static class FighterFactory
    {
        public static IFighter CreateFighter()
        {
            string name = GetName();
            IRace race = SelectSingleFromList( "race", GameData.AvailableRaces );
            IClass fighterClass = SelectSingleFromList( "class", GameData.AvailableClasses );
            IWeapon weapon = SelectSingleFromList( "weapon", GameData.AvailableWeapons );
            IArmor armor = SelectSingleFromList( "armor", GameData.AvailableArmors );

            IFighter fighter = new Fighter( name, race, fighterClass );
            fighter.SetWeapon( weapon );
            fighter.SetArmor( armor );
            return fighter;
        }
        private static string GetName()
        {
            while ( true )
            {
                Console.WriteLine( "Enter the fighter's name:" );
                string? name = Console.ReadLine();
                if ( string.IsNullOrWhiteSpace( name ) )
                {
                    Console.WriteLine( "Invalid name. Please try again." );
                    continue;
                }
                return name;
            }
        }
        private static T SelectSingleFromList<T>( string category, IReadOnlyList<T> options ) where T : IModel
        {
            Console.WriteLine( $"""
                       Select a {category}:
                       {string.Join( "\n", options.Select( ( model, index ) => $"{index + 1}. {model.Name}" ) )}
                       """ );

            while ( true )
            {
                string? input = Console.ReadLine();

                if ( int.TryParse( input, out int choice ) )
                {
                    if ( choice >= 1 && choice <= options.Count )
                    {
                        return options[ choice - 1 ];
                    }
                }

                Console.WriteLine( $"Invalid input. Please try again." );
            }
        }
    }
}