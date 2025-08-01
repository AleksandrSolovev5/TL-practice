using Fighters.Models.Armors;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters
{
    public interface IFighter : IModel
    {
        public string Name { get; }
        public double GetCurrentHealth();
        public int GetMaxHealth();
        public int CalculateDamage();
        public int CalculateArmor();

        public void SetArmor( IArmor armor );
        public void SetWeapon( IWeapon weapon );
        public void TakeDamage( double damage );
        double Attack( IFighter target );
    }
}