using System;
using System.IO;

namespace RPGStoreSimulator
{
    /// <summary>
    /// The base for the player/store or any other thing that uses a "inventory" of sorts.
    /// </summary>
    internal class InventoryBase : Program
    {
        public int balance = 0;
        public string name = "";

        public BaseItem[] inventoryList = new BaseItem[] { }; /* Creating a list for inventory (ease of access) */
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
                Table.Add(inventoryList, item, out inventoryList);
            }

            Save();
        }

        /// <summary>
        /// Print the inventory is a nice form.
        /// </summary>
        /// <param name="spacing">String spacing between the text and the side. Example: "   " (which is three spaces to the right).</param>
        public void GetInventory(string spacing)
        {
            Table.Sort(inventoryList, (x, y) => x.GetCategory().CompareTo(y.GetCategory()), out inventoryList);
            Table.Sort(inventoryList, (x, y) => x.GetCost().CompareTo(y.GetCost()), out inventoryList);
            Table.Sort(inventoryList, (x, y) => x.GetRarity().CompareTo(y.GetRarity()), out inventoryList);

            bool[] rarities = new bool[5] { false, false, false, false, false };

            foreach (BaseItem item in inventoryList)
            {
                if (!rarities[item.GetRarity() - 1])
                {
                    Space();
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
            Table.Remove(inventoryList, item, out inventoryList);

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
                Array.Clear(inventoryList, 0, inventoryList.Length);

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
                                Table.Add(inventoryList, item, out inventoryList);
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
            string[] lines = new string[inventoryList.Length + 1];
            lines[0] = this.balance.ToString();

            int i = 1;
            foreach (BaseItem item in inventoryList)
            {
                if (item != null)
                {
                    lines[i] = item.GetName();
                    ++i;
                }
            }

            File.WriteAllLines(location, lines);
        }
    }
}
