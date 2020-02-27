namespace RPGStoreSimulator
{
    class Staff : BaseItem
    {
        public string magicType = "Light";
        public float crit = 0.1f;

        public Staff() { }
        public override void Setup() // Just adding stats cuz why not.
        {
            string[] stats = { };
            Table.Add(GetStats(), $"- Magic Type: {magicType}", out stats); SetStats(stats);
            Table.Add(GetStats(), $"- Crit: {crit * 100}%", out stats); SetStats(stats);
        }
    }
}
