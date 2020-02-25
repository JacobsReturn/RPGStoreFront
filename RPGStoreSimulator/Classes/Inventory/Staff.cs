namespace RPGStoreSimulator
{
    class Staff : BaseItem
    {
        public string magicType = "Light";
        public float crit = 0.1f;

        public Staff() { }
        public override void Setup() // Just adding stats cuz why not.
        {
            Table.Add<string>(this.Stats, $"- Magic Type: {magicType}", out this.Stats);
            Table.Add<string>(this.Stats, $"- Crit: {crit * 100}%", out this.Stats);
        }
    }
}
