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
        private static readonly Random rand = new Random();

        private const int MultiplicatorCriticalDamage = 2;
        private const double CriticalHitChance = 0.1;

        public string Name { get; private set; }
        public double CurrentHealth { get; private set; }
        public int MaxHealth => _race.Health + _class.Health;
        public IArmor Armor { get; set; } = new NoArmor();
        public IWeapon Weapon { get; set; } = new Dagger();

        public Fighter( string name, IRace race, IClass fighterClass )
        {
            Name = name;
            _race = race;
            _class = fighterClass;
            CurrentHealth = MaxHealth;
        }

        public int CalculateDamage() => Weapon.Damage + _race.Damage + _class.Damage;

        public int CalculateArmor() => Armor.Armor + _race.Armor;

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

            if ( rand.NextDouble() < CriticalHitChance )
                damage *= MultiplicatorCriticalDamage;

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
