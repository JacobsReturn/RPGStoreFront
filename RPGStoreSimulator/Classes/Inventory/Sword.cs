namespace RPGStoreSimulator
{
    class Sword : BaseItem
    {
        public int sharpness = 0; // How much it pierces armour.
        public float crit = 0.1f; // Chance of critting.

        public Sword() { }
        public override void Setup() // Just adding stats cuz why not.
        {
            Table.Add<string>(this.Stats, $"- Sharpness: {sharpness}", out this.Stats);
            Table.Add<string>(this.Stats, $"- Crit: {crit * 100}%", out this.Stats);
        }
    }
}