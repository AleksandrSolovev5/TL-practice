using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Classes;
using Fighters.Models.Weapons;
using Fighters.Models.Armors;
using Fighters.Models;
using Fighters.Factory;
using Fighters.Utils;

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

            IFighter fighter = new Fighter( name, race, fighterClass, weapon, armor );
            return fighter;
        }

        private static string GetName()
        {
            while ( true )
            {
                ConsolePrinter.PrintEnterName();
                string? name = Console.ReadLine();
                if ( string.IsNullOrWhiteSpace( name ) )
                {
                    ConsolePrinter.PrintInvalidName();
                    continue;
                }
                return name;
            }
        }

        private static T SelectSingleFromList<T>( string category, IReadOnlyList<T> options ) where T : IModel
        {
            ConsolePrinter.PrintOptionsList( category, options );

            while ( true )
            {
                string? input = Console.ReadLine();

                if ( !int.TryParse( input, out int choice ) )
                {
                    ConsolePrinter.PrintInvalidNumberInput();
                    continue;
                }

                if ( choice < 1 || choice > options.Count )
                {
                    ConsolePrinter.PrintNumberOutOfRange();
                    continue;
                }

                return options[ choice - 1 ];
            }
        }
    }
}
