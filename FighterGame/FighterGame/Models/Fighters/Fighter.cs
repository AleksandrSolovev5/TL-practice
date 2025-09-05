using Fighters.Config;
using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters
{
    public class Fighter : IFighter
    {
        private readonly IRace _race;
        private readonly IClass _class;
        private readonly IWeapon _weapon;
        private readonly IArmor _armor;
        private static readonly Random rand = new();



        public string Name { get; private set; }
        public double CurrentHealth { get; private set; }
        public int MaxHealth => _race.Health + _class.Health;

        public Fighter( string name, IRace race, IClass fighterClass, IWeapon weapon, IArmor armor )
        {
            Name = name;
            _race = race;
            _class = fighterClass;
            _weapon = weapon;
            _armor = armor;
            CurrentHealth = MaxHealth;
        }

        public int CalculateDamage() => _weapon.Damage + _race.Damage + _class.Damage;

        public int CalculateArmor() => _armor.Armor + _race.Armor;

        public void TakeDamage( double damage )
        {
            double newHealth = CurrentHealth - damage;
            if ( newHealth < 0 )
                newHealth = 0;

            CurrentHealth = Math.Round( newHealth, 1 );
        }

        private double CalculateFinalDamage()
        {
            int baseDamage = CalculateDamage();
            double randomMultiplier = 0.9 + rand.NextDouble() * 0.2; // урон +- 10%
            double damage = baseDamage * randomMultiplier;

            if ( rand.NextDouble() < GameConfig.CriticalHitChance )
                damage *= GameConfig.MultiplicatorCriticalDamage;

            return damage;
        }

        public double Attack( IFighter target )
        {
            double finalDamage = CalculateFinalDamage();
            finalDamage = Math.Max( finalDamage - target.CalculateArmor(), 0 );
            finalDamage = Math.Round( finalDamage, 1 );
            target.TakeDamage( finalDamage );
            return finalDamage;
        }
    }
}
