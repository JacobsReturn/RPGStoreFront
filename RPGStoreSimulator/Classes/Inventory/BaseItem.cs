using System;

namespace RPGStoreSimulator
{
    /// <summary>
    /// For creating items.
    /// </summary>
    internal class BaseItem : Program /* Item Base. */
    {
        private string Name;
        private string Description;
        private int Cost;
        private string Category;
        private int Rarity;
        private string[] Stats = new string[] { };
        private string Icon;

        public string usedBy;

        /// <summary>
        /// Constructor.
        /// </summary>
        public BaseItem()
        {
            this.Name = "Unknown";
            this.Description = "Unknown";
            this.Cost = 0;
            this.Category = "Sword";
            this.Rarity = 4;
            this.Icon = "";
            this.usedBy = "0";
        }

        /// <summary>
        /// Virtual Setup for later use.
        /// </summary>
        public virtual void Setup() { }

        /// <summary>
        /// Setting the name of an item.
        /// </summary>
        /// <param name="value">The string name to set.</param>
        public void SetName(string value) { this.Name = value; }

        /// <summary>
        /// Setting item description.
        /// </summary>
        /// <param name="value">The string description to set.</param>
        public void SetDescription(string value) { this.Description = value; }

        /// <summary>
        /// Setting the cost of the item.
        /// </summary>
        /// <param name="value">The int amount to be set as a buying price.</param>
        public void SetCost(int value) { this.Cost = value; }

        /// <summary>
        /// Setting item rarity.
        /// </summary>
        /// <param name="value">Rarity number (1-5</param>
        public void SetRarity(int value) { this.Rarity = value; }

        /// <summary>
        /// Setting the items category.
        /// </summary>
        /// <param name="value">String category for the item (Sword, Staff, etc)</param>
        public void SetCategory(string value)
        {
            this.Category = value;

            switch (value)
            {
                case "Sword": Icon = "\x2666"; break;
                case "Spear": Icon = "\x2660"; break;
                case "Daggers": Icon = "\x2663"; break;
                case "Magic Staff": Icon = "\x2663"; break;
                case "Helmet": Icon = "\x263C"; break;
                case "Chestplate": Icon = "\x263C"; break;
                case "Leggings": Icon = "\x263C"; break;
                case "Boots": Icon = "\x263C"; break;
                case "Item": Icon = "\x25CA"; break;
            }
        }

        /// <summary>
        /// Setting an items stats.
        /// </summary>
        /// <param name="values">String array setting the stats, unlimited amount.</param>
        public void SetStats(string[] values) { this.Stats = values; }

        /// <summary>
        /// Grabbing the name of the item.
        /// </summary>
        /// <returns>Returns the items name.</returns>
        public string GetName() { return this.Name; }

        /// <summary>
        /// Grabbing the description of the item.
        /// </summary>
        /// <returns>Returns the items description.</returns>
        public string GetDescription() { return this.Description; }

        /// <summary>
        /// Grabbing the cost of the item.
        /// </summary>
        /// <returns>Returns the items cost.</returns>
        public int GetCost() { return this.Cost; }

        /// <summary>
        /// Grabbing the category of the item.
        /// </summary>
        /// <returns>The category/type like staff, sword, etc.</returns>
        public string GetCategory() { return this.Category; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetRarity() { return this.Rarity; }

        /// <summary>
        /// Grabbing the stats of the item.
        /// </summary>
        /// <returns>Returns the stats of the item.</returns>
        public string[] GetStats() { return this.Stats; }

        /// <summary>
        /// Used to print the full info, category, stats, etc.
        /// </summary>
        public void PrintInfo()
        {
            string spacing = "   ";

            Print("\x25BA Inspecting Item \x25C4", ConsoleColor.Cyan);
            Print($"[{this.Icon}] {this.GetName()}", RarityColours[this.GetRarity() - 1, 1]);
            Print($"{spacing}Description: {this.GetDescription()}", ConsoleColor.White);
            Space();

            Print($"{spacing}Type: {this.GetCategory()}", ConsoleColor.Red);
            Print($"{spacing}Rarity: {RarityColours[this.GetRarity() - 1, 0]}", RarityColours[this.GetRarity() - 1, 1]);

            Space();
            Print($"{spacing}More Info [{this.Stats.Length}]:", ConsoleColor.White);

            foreach (string stat in this.Stats)
            {
                Print($"{spacing}     {stat}", ConsoleColor.Blue);
            }
        }

        /// <summary>
        /// Nicely prints the item.
        /// </summary>
        /// <param name="spacing">Add spacing like " ".</param>
        /// <param name="cost">A boolean if you want to use the cost or not.</param>
        public void NicePrint(string spacing, bool cost)
        {
            string display = this.GetName();
            if (cost)
            {
                display = $"{display} \x25BA ${this.GetCost()} "; // \x is used for symbols <3
            }

            Print($"{spacing}[{this.Icon}] {display}", RarityColours[this.GetRarity() - 1, 1]);
        }

        /// <summary>
        /// Nicely prints the item.
        /// </summary>
        /// <param name="spacing">Add spacing like " ".</param>
        public void NicePrint(string spacing)
        {
            NicePrint(spacing, false);
        }
    }
}
