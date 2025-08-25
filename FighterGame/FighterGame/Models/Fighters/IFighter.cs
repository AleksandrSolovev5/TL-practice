using Fighters.Models.Armors;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters
{
    public interface IFighter : IModel
    {
        double CurrentHealth { get; }
        int MaxHealth { get; }

        int CalculateDamage();
        int CalculateArmor();
        void TakeDamage( double damage );
        double Attack( IFighter target );
    }
}
