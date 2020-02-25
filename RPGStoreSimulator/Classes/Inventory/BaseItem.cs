using System;

namespace RPGStoreSimulator
{
    /// <summary>
    /// For creating items.
    /// </summary>
    internal class BaseItem : Program /* Item Base. */
    {
        public string Name;
        public string Description;
        public int Cost;
        public string Category;
        public int Rarity;
        public string[] Stats = new string[] { };
        public string Icon;
        public string usedBy;

        public BaseItem() /* Constructor. */
        {
            this.Name = "Unknown";
            this.Description = "Unknown";
            this.Cost = 0;
            this.Category = "Sword";
            this.Rarity = 4;
            this.Icon = "";
            this.usedBy = "0";
        }

        public virtual void Setup() { } // For later use.

        public void SetName(string value) /* Setting item name. */
        {
            this.Name = value;
        }

        public void SetDescription(string value) /* Setting item description. */
        {
            this.Description = value;
        }

        public void SetCost(int value) /* Setting item cost. */
        {
            this.Cost = value;
        }

        public void SetRarity(int value) /* Setting item rarity. */
        {
            this.Rarity = value;
        }

        public void SetCategory(string value) /* Setting item category. */
        {
            this.Category = value;

            if (value == "Sword")
            {
                this.Icon = "\x2666";
            }
            else if (value == "Spear")
            {
                this.Icon = "\x2660";
            }
            else if (value == "Daggers")
            {
                this.Icon = "\x2663";
            }
            else if (value == "Magic Staff")
            {
                this.Icon = "\x2736";
            }
            else if (value == "Helmet")
            {
                this.Icon = "\x263C";
            }
            else if (value == "Chestplate")
            {
                this.Icon = "\x263C";
            }
            else if (value == "Leggings")
            {
                this.Icon = "\x263C";
            }
            else if (value == "Boots")
            {
                this.Icon = "\x263C";
            }
            else if (value == "Item")
            {
                this.Icon = "\x25CA";
            }
        }

        public void SetStats(string[] values) /* Setting item rarity. */
        {
            this.Stats = values;
        }

        public string GetName() /* Grabbing items name. */
        {
            return this.Name;
        }

        public string GetDescription() /* Grabbing items description. */
        {
            return this.Description;
        }

        public int GetCost() /* Grabbing items cost. */
        {
            return this.Cost;
        }

        public string GetCategory() /* Get the type aka Staff, Sword etc. */
        {
            return this.Category;
        }

        public int GetRarity() /* Get the rarity of the item. */
        {
            return this.Rarity;
        }

        public string[] GetStats() /* Get the stats of the item. */
        {
            return this.Stats;
        }

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

        public void NicePrint(string spacing, bool cost)
        {
            string display = this.GetName();
            if (cost)
            {
                display = $"{display} \x25BA ${this.GetCost()} "; // \x is used for symbols <3
            }

            Print($"{spacing}[{this.Icon}] {display}", RarityColours[this.GetRarity() - 1, 1]);
        }

        public void NicePrint(string spacing)
        {
            NicePrint(spacing, false);
        }
    }
}
