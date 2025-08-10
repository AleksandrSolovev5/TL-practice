namespace Fighters.Models.Races
{
    public class Goblin : IRace
    {
        public string Name => "Goblin";
        public int Damage => 3;
        public int Health => 80;
        public int Armor => 5;
    }
}
