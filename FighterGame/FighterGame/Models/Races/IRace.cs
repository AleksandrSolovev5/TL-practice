namespace Fighters.Models.Races
{
    public interface IRace : IModel
    {
        public int Damage { get; }
        public int Health { get; }
        public int Armor { get; }
    }
}