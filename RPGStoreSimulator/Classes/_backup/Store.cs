using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime;

/* File is for Item and Inventory system */

/// <summary>
/// RPGStoreSimulator is for the RPG store and the user, with many inventory, write, saving, storage and other capabilities. Made for an AIE Assessment.
/// </summary>
namespace RPGStoreSimulator
{
    /* Classes */
    /// <summary>
    /// The base for the player/store or any other thing that uses a "inventory" of sorts.
    /// </summary>
    internal class InventoryBase : Program
    {
        public int balance = 0;
        public string name = "";

        public List<BaseItem> inventoryList = new List<BaseItem>(); /* Creating a list for inventory (ease of access) */
        public InventoryBase() { }

        /// <summary>
        /// Add an item to the inventory that is connected to this class.
        /// </summary>
        /// <param name="item">The (BaseItem) item to be added to the inventory.</param>
        /// <param name="amount">How many do you want to add? (this can be as much as you desire).</param>
        public void AddItem(BaseItem item, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                item.usedBy = this.name;
                inventoryList.Add(item);
            }

            Save();
        }

        /// <summary>
        /// Print the inventory is a nice form.
        /// </summary>
        /// <param name="spacing">String spacing between the text and the side. Example: "   " (which is three spaces to the right).</param>
        public void GetInventory(string spacing)
        {
            inventoryList.Sort((x, y) => x.GetCost().CompareTo(y.GetCost()));
            inventoryList.Sort((x, y) => x.GetRarity().CompareTo(y.GetRarity()));

            bool[] rarities = new bool[5]{false, false, false, false, false};

            foreach (BaseItem item in inventoryList)
            {
                if (!rarities[item.GetRarity() - 1])
                {
                    Print($"   {RarityColours[item.GetRarity() - 1, 0]}", RarityColours[item.GetRarity() - 1, 1]);
                    rarities[item.GetRarity() - 1] = true;
                }

                item.NicePrint(spacing, true);
            }
        }

        /// <summary>
        /// Remove the item from the classes inventory.
        /// </summary>
        /// <param name="item">The (BaseItem) item to remove from the current inventory in context.</param>
        public void RemoveItem(BaseItem item) /* Remove an item to the inventory */
        {
            inventoryList.Remove(item);

            Save();
        }

        /// <summary>
        /// Check if the currenct inventory in context has the item.
        /// </summary>
        /// <param name="item">The (BaseItem) item to check if it exists in the inventory list.</param>
        /// <returns></returns>
        public bool HasItem(BaseItem item)
        {
            bool exists = false;

            foreach (BaseItem item2 in inventoryList)
            {
                if (item2 == item)
                {
                    exists = true;
                    break;
                }
            }

            return exists;
        }

        /// <summary>
        /// Load the current context classes data.
        /// </summary>
        public void Load()
        {
            string location = repo + @"\" + this.name + @".txt";
            string[] text = File.ReadAllLines(location);

            if (text.Length > 0) 
            {
                inventoryList.Clear();

                foreach (string line in text)
                {
                    if (text[0] == line) balance = Int32.Parse(text[0]);
                    else
                    {
                        foreach (BaseItem item in itemList)
                        {
                            if (item.GetName() == line)
                            {
                                item.usedBy = name;
                                inventoryList.Add(item);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Save the current context classes data.
        /// </summary>
        public void Save()
        {
            string location = repo + @"\" + this.name + @".txt";
            string[] lines = new string[inventoryList.Count + 1];
            lines[0] = this.balance.ToString();

            int i = 1;
            foreach (BaseItem item in inventoryList)
            {
                lines[i] = item.GetName();
                ++i;
            }

            File.WriteAllLines(location, lines);
        }
    }

    class Player : InventoryBase /* The user */
    {
        public bool atStore = false;

        public Player() 
        {
            balance = 1000;
            name = "Player";
        }

        public bool ItemExists(BaseItem item) /* Does the item exist in the item stack */
        {
            bool exists = false;

            foreach (BaseItem item2 in itemList)
            {
                if (item2 == item)
                {
                    exists = true;
                    break;
                }
            }

            return exists;
        }

        public void BuyItem(BaseItem item) /* Buy an item from the store */
        {
            if (ItemExists(item) & shop.HasItem(item))
            {
                if (balance >= item.Cost)
                {
                    balance -= item.Cost;
                    Print("You purchased: " + item.GetName() + " for $" + item.Cost + ". Your balance is now: $" + balance, ConsoleColor.Green);

                    AddItem(item, 1);

                    shop.RemoveItem(item);
                }
                else
                {
                    Print("You cannot afford the " + item.GetName() + "!", ConsoleColor.Red);
                }
            }
            else
            {
                Print("You cannot buy an item that Garvalsh doesn't own.", ConsoleColor.Red);
            }
        }
        public void SellItem(BaseItem item) /* Sell an item to the store */
        {
            if (ItemExists(item) & user.HasItem(item))
            {
                balance += item.Cost;

                Print("You sold: " + item.GetName() + " for $" + item.Cost + ". Your balance is now: $" + balance, ConsoleColor.Green);

                shop.AddItem(item, 1);

                RemoveItem(item);
            }
            else
            {
                Print("You cannot sell an item you don't own.", ConsoleColor.Red);
            }
        }
    }

    class Store : InventoryBase /* Interactable Store */
    {
        public Store()
        {
            name = "Store";
        }
    }

    /* Items */
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
            string spacing = "  ";

            Print("\x25BA Inspecting Item \x25C4", ConsoleColor.White);
            Print($"[{this.Icon}] {this.GetName()}", ConsoleColor.White);
            Print($"{spacing}Description: {this.GetDescription()}", ConsoleColor.White);
            Print($"{spacing}Type: {this.GetCategory()}", "White");
            Print($"{spacing}Rarity: {RarityColours[this.GetRarity() - 1,0]}", ConsoleColor.White);
            Print($"{spacing}Stats:", ConsoleColor.White);

            foreach (string stat in this.Stats)
            {
                Print($"{spacing} - {stat}", ConsoleColor.White);
            }
        }

        public void NicePrint(string spacing, bool cost)
        {
            string display = this.GetName();
            if (cost)
            {
                display = $"{display} \x25BA ${this.GetCost()} "; // \x is used for symbols <3
            }

            Print($"{spacing}[{this.Icon}] {display}", RarityColours[this.GetRarity() - 1,1]);
        }

        public void NicePrint(string spacing)
        {
            NicePrint(spacing, false);
        }
    }
}