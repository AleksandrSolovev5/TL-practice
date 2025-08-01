using Fighters.Models.Armors;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Fighters.Models.Classes;

namespace Fighters.Models.Fighters
{
    public class Fighter : IFighter
    {
        private readonly IRace _race;
        private readonly IClass _class;
        private IArmor _armor = new NoArmor();
        private IWeapon _weapon = new Dagger();
        private double _currentHealth;
        private static Random rand = new Random();
        private const int multiplicatorCriticalDamage = 2;
        private const double criticalHitChance = 0.1;

        public string Name { get; private set; }

        public Fighter( string name, IRace race, IClass fighterClass )
        {
            Name = name;
            _race = race;
            _class = fighterClass;
            _currentHealth = GetMaxHealth();
        }

        public double GetCurrentHealth() => _currentHealth;

        public int GetMaxHealth() => _race.Health + _class.Health;

        public int CalculateDamage() => _weapon.Damage + _race.Damage + _class.Damage;

        public int CalculateArmor() => _armor.Armor + _race.Armor;

        public void SetArmor( IArmor armor )
        {
            _armor = armor;
        }

        public void SetWeapon( IWeapon weapon )
        {
            _weapon = weapon;
        }

        public void TakeDamage( double damage )
        {
            double newHealth = _currentHealth - damage;
            if ( newHealth < 0 )
            {
                newHealth = 0;
            }

            _currentHealth = Math.Round( newHealth, 1 );
        }

        public double Attack( IFighter target )
        {
            int baseDamage = CalculateDamage();
            double variation = 0.9 + rand.NextDouble() * 0.2;
            double finalDamage = baseDamage * variation;
            if ( rand.NextDouble() < criticalHitChance )
            {
                finalDamage *= multiplicatorCriticalDamage;
            }
            finalDamage = Math.Max( finalDamage - target.CalculateArmor(), 0 );
            finalDamage = Math.Round( finalDamage, 1 );
            target.TakeDamage( finalDamage );

            return finalDamage;

        }
    }
}