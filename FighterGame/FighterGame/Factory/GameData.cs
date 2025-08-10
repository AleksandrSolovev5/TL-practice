using Fighters.Models.Races;
using Fighters.Models.Classes;
using Fighters.Models.Weapons;
using Fighters.Models.Armors;

namespace Fighters.Factory
{
    public static class GameData
    {
        public static IReadOnlyList<IRace> AvailableRaces =
        [
            new Human(),
            new Goblin(),
            new Giant()
        ];

        public static IReadOnlyList<IClass> AvailableClasses =
        [
            new Gladiator(),
            new Knight()
        ];

        public static IReadOnlyList<IWeapon> AvailableWeapons =
        [
            new Spear(),
            new Dagger(),
            new IronSword()
        ];

        public static IReadOnlyList<IArmor> AvailableArmors =
        [
            new NoArmor(),
            new LeatherArmor(),
            new Chainmail()
        ];
    }
}