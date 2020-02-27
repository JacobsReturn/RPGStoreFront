namespace RPGStoreSimulator
{
    class Sword : BaseItem
    {
        public int sharpness = 0; // How much it pierces armour.
        public float crit = 0.1f; // Chance of critting.

        public Sword() { }
        public override void Setup() // Just adding stats cuz why not.
        {
            string[] stats = { };
            Table.Add(GetStats(), $"- Sharpness: {sharpness}", out stats); SetStats(stats);
            Table.Add(GetStats(), $"- Crit: {crit * 100}%", out stats); SetStats(stats);
        }
    }
}