namespace Fighters.Models.Races
{
    public class Giant : IRace
    {
        public string Name => "Giant";
        public int Damage => 2;
        public int Health => 150;
        public int Armor => 0;
    }
}
