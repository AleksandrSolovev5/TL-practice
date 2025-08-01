namespace Fighters.Models.Classes
{
    public interface IClass : IModel
    {
        public int Damage { get; }
        public int Health { get; }
    }
}
