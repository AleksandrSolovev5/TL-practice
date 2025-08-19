using Fighters.Models.Armors;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters
{
    public interface IFighter : IModel
    {
        double CurrentHealth { get; }
        int MaxHealth { get; }

        IArmor Armor { get; set; }
        IWeapon Weapon { get; set; }

        int CalculateDamage();
        int CalculateArmor();
        void TakeDamage( double damage );
        double Attack( IFighter target );
    }
}
