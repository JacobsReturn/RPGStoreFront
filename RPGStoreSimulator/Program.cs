﻿using System;
using System.IO;
using System.Text;

namespace RPGStoreSimulator
{
    /// <summary>
    /// Base class for the RPGStoreSimulator.
    /// </summary>
    class Program : Library
    {
        public static Commands[] commandList; /* Creating a command list for all commands (ease of access). */
        public static BaseItem[] itemList = new BaseItem[] { }; /* Creating a list for all created items (ease of access). */

        /* Creating entities */
        public static Player user = new Player();
        public static Store shop = new Store();

        public static BaseItem itemReference = new BaseItem();

        public static string repo = Environment.CurrentDirectory;

        public static string[,] RarityColours = new string[5,2]{
            { "Legendary", "Yellow" }, 
            { "Epic", "Magenta" }, 
            { "Rare", "Cyan" }, 
            { "Uncommon", "DarkGreen" }, 
            { "Common", "Gray" }
        };

        /// <summary>
        /// Creates items for global use that can be accessed by a player and the store.
        /// </summary>
        /// <param name="name">Name of the item. Example: "God Sword".</param>
        /// <param name="description">The description of the item so the user can understand its importance/use.</param>
        /// <param name="cost">How much the user has to pay for it.</param>
        /// <param name="category">The type aka Sword, Spear etc.</param>
        /// <param name="rarity">The rarity as a number, 1 being the best, 5 being the worst.</param>
        /// <param name="stats">An array of possible stats?</param>
        public static void CreateItem(string name, string description, int cost, string category, int rarity, string[] stats)
        {
            BaseItem item;
            if (category == "Sword") item = new Sword();
            else if (category == "Staff") item = new Staff();
            else item = new BaseItem();
            
            item.SetName(name);
            item.SetDescription(description);
            item.SetCost(cost);
            item.SetCategory(category);
            item.SetRarity(rarity);
            item.SetStats(stats);

            Table.Add(itemList, item, out itemList);

            item.Setup();
        }

        /// <summary>
        /// Creates items for global use that can be accessed by a player and the store.
        /// </summary>
        /// <param name="name">Name of the item. Example: "God Sword".</param>
        /// <param name="description">The description of the item so the user can understand its importance/use.</param>
        /// <param name="cost">How much the user has to pay for it.</param>
        /// <param name="category">The type aka Sword, Spear etc.</param>
        /// <param name="rarity">The rarity as a number, 1 being the best, 5 being the worst.</param>
        public static void CreateItem(string name, string description, int cost, string category, int rarity)
        {
            CreateItem(name, description, cost, category, rarity, new string[] { });
        }

        /// <summary>
        /// Grab any item by its name alone from the list of items (itemList).
        /// </summary>
        /// <param name="name">The name of the item (this will be the first item with the result)</param>
        /// <returns>Returns the BaseItem item, the item as its class as a whole.</returns>
        public static BaseItem GetItem(string name) /* Grabbing an item by using its name. */
        {
            foreach (BaseItem item in itemList)
            {
                if(item.GetName() == name)
                {
                    return item;
                }
            }

            return itemReference; /* Yes, a fall back. I don't trust myself. */
        }

        /// <summary>
        /// Saves items for later use in the store.
        /// </summary>
        /// <param name="name">Name of the item. Example: "God Sword".</param>
        /// <param name="description">The description of the item so the user can understand its importance/use.</param>
        /// <param name="cost">How much the user has to pay for it.</param>
        /// <param name="category">The type aka Sword, Spear etc.</param>
        /// <param name="rarity">The rarity as a number, 1 being the best, 5 being the worst.</param>
        /// <param name="stats">An array of possible stats?</param>
        public static void SaveItem(string name, string description, int cost, string category, int rarity, string[] stats)
        {
            string location = repo + @"\items\Item_" + name + @".txt";
            string[] lines = new string[6]
            {          
                $"Name/{name}",
                $"Description/{description}",
                $"Cost/{cost.ToString()}",
                $"Category/{category}",
                $"Rarity/{rarity.ToString()}",
                $"Stats/"
            };

            foreach (string stat in stats)
            {
                lines[5] = lines[5] + $"{stat}";
                if (stat != stats[stats.Length - 1]) lines[5] = lines[5] + $"|";
            }

            File.WriteAllLines(location, lines);
        }

        /// <summary>
        /// The main function of the program.
        /// </summary>
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8; // Used for \x character map numbers later on.

            if (!Directory.Exists(repo + @"\items"))
            {
                Directory.CreateDirectory(repo + @"\items");

                /* Adding items */
                SaveItem("Heavens Penetration", "A spear so powerful and light, it can kill any opponent swiftly.", 1800, "Spear", 1,
                    new string[2]
                    {
                    "- Direct Damage: 10000",
                    "- Swing Speed: 0.2s",
                    }
                );
                SaveItem("Hell Daggers", "The dual daggers born from the hell scape itself. Ignites targets on hit.", 1600, "Daggers", 1,
                    new string[4]
                    {
                    "- Direct Damage: 600/per dagger",
                    "- Swing Speed: 0.2s/per dagger",
                    "- Applies <[\x03FE] Ignite> for 13s.",
                    "            \x25B2 Deals 18 magic damage/s.",
                    }
                );
                SaveItem("Frost Staff", "Allows the wielder to shoot powerful ice bolts. The bolts apply frostbite.", 1300, "Magic Staff", 1,
                    new string[5]
                    {
                    "- Direct Damage: 500",
                    "- Splash Damage: 100",
                    "- Projectile Speed: 63u/s.",
                    "- Applies <[\x03FE] Frostbite> for 10s.",
                    "            \x25B2 Deals 35 magic damage/s.",
                    }
                );
                SaveItem("Wooden Sword", "A sturdy piece of wood capable of barely damaging your enemies", 8, "Sword", 5,
                    new string[2]
                    {
                    "- Direct Damage: 3",
                    "- Swing Speed: 1.3s",
                    }
                );
                SaveItem("Slime Skin", "Skin of a slime.", 18, "Item", 5,
                    new string[1]
                    {
                    "- Can be used for crafting, very sticky.",
                    }
                );
                SaveItem("Dragon Scale", "A scale of a dragon.", 1000, "Item", 1,
                    new string[1]
                    {
                    "- Can be used for crafting, fire resistant, stronger then most metals, water resistance, highly durable.",
                    }
                );
                SaveItem("Golden Feather", "The feather of a golden duck.", 630, "Item", 2,
                    new string[1]
                    {
                    "- Pure gold, good for crafting/trade.",
                    }
                );
                SaveItem("Poison Staff", "A staff that can poison the target.", 800, "Magic Staff", 2,
                    new string[5]
                    {
                    "- Direct Damage: 170",
                    "- Splash Damage: 30",
                    "- Projectile Speed: 25u/s.",
                    "- Applies <[\x03FE] Poison> for 160s.",
                    "            \x25B2 Deals 6 pure damage/s.",
                    }
                );
                SaveItem("Dragon Scale Chestplate", "Scale Armour.", 2500, "Chestplate", 1,
                    new string[3]
                    {
                    "- Armour: 500",
                    "- Fire Resistance: 35%",
                    "- Damage Reduction: 15%",
                    }
                );
                SaveItem("Dragon Scale Helmet", "Scale Armour.", 1800, "Helmet", 1,
                    new string[2]
                    {
                    "- Armour: 150",
                    "- Fire Resistance: 15%",
                    }
                );
                SaveItem("Dragon Scale Leggings", "Scale Armour.", 2300, "Leggings", 1,
                    new string[2]
                    {
                    "- Armour: 400",
                    "- Fire Resistance: 35%",
                    }
                );
                SaveItem("Dragon Scale Boots", "Scale Armour.", 800, "Boots", 1,
                    new string[5]
                    {
                    "- Armour: 50",
                    "- Fire Resistance: 15%",
                    "- Speed Bonus: 215%",
                    "- Gains buff <[\x03FE] Lava Walk> for 10s.",
                    "               \x25B2 Allows you to walk on lava.",
                    }
                );
            }

            Print("Type /help for more information on the commands.", ConsoleColor.White);

            string[] files = Directory.GetFiles(repo + @"\items\", "*.txt");

            bool storeLoaded = true;
            foreach (string file in files)
            {
                if (file.Contains("Item_"))
                {
                    string[] text = File.ReadAllLines(file);

                    //using(StreamReader read = new StreamReader(file))
                    //{
                    //    for (bool finished = false; finished != true;)
                    //    {
                    //        if (read.ReadLine() == null)
                    //        {
                    //            read.Close();
                    //            read.Dispose();
                    //            read.DiscardBufferedData();

                    //            finished = true;
                    //        }
                    //        else
                    //        {
                    //            Table.Add(text, read.ReadLine(), out text);
                    //        }
                    //    }
                    //}

                    if (text.Length > 0)
                    {
                        string _name = "Unknown";
                        string _description = "";
                        int _cost = 1;
                        string _category = "Sword";
                        int _rarity = 1;
                        string[] _stats = { };

                        foreach (string line in text)
                        {
                            string[] inputs = line.Split('/');

                            if (inputs.Length > 1)
                            {
                                switch (inputs[0])
                                {
                                    case "Name":
                                        _name = inputs[1];
                                        break;
                                    case "Description":
                                        _description = inputs[1];
                                        break;
                                    case "Cost":
                                        if(Int32.TryParse(inputs[1], out int cost))
                                        {
                                            _cost = cost;
                                        }
                                        
                                        break;
                                    case "Category":
                                        _category = inputs[1];
                                        break;
                                    case "Rarity":
                                        if (Int32.TryParse(inputs[1], out int rarity))
                                        {
                                            _rarity = rarity;
                                        }

                                        break;
                                    case "Stats":
                                        if (inputs[1].Split('|').Length > 0) _stats = inputs[1].Split('|');
                                        break;
                                }
                            }
                        }
                        
                        CreateItem(_name, _description, _cost, _category, _rarity, _stats);

                        if (!File.Exists(repo + @"\" + shop.name + @".txt")) storeLoaded = false;

                        if (!storeLoaded)
                        {
                            shop.AddItem(GetItem(_name), _rarity);
                        }
                    }
                }
            }

            /* Checking if files exist */
            if (!File.Exists(repo + @"\" + user.name + @".txt"))
            {
                /* Giving the user and the shop class an item to have in their inventory */
                user.AddItem(GetItem("Wooden Sword"), 3);
                user.AddItem(GetItem("Dragon Scale"), 1);
                user.AddItem(GetItem("Slime Skin"), 4);
            }
            else user.Load();

            if (storeLoaded) shop.Load();

            /* Creating a list for all of our commands (ease of access). */
            commandList = new Commands[]
            {
                new HelpCommand(),
                new StoreCommand(),
                new InventoryCommand(),
                new BuyCommand(),
                new SellCommand(),
                new InspectCommand(),
                new SearchCommand(),
                new CustomCommand(),
            };

            while (true) /* Creating our tick */
            {
                Think();
            }
        }

        /// <summary>
        /// Called every time the while loop restarts.
        /// </summary>
        static void Think()
        {
            string stringReadLine = Console.ReadLine();

            foreach (Commands command in commandList)
            {
                if (command.stringCommand.Length <= stringReadLine.Length) /* Making sure the command itself is below the length of the current text */
                {
                    string subCommand = stringReadLine.Substring(0, command.stringCommand.Length); /* Culling unwanted parts of the command turning it into arguments */

                    if (command.CanRun(subCommand)) /* Checking if the command is the same */
                    {
                        string subArg = subCommand;

                        if (stringReadLine.Length > command.stringCommand.Length) /* Checking for arguments */
                        {
                            subArg = stringReadLine.Substring(command.stringCommand.Length + 1, stringReadLine.Length - command.stringCommand.Length - 1); 
                        }

                        command.Execute(subArg); /* Running attached function with possible argument */
                    }
                }
            }
        }
    }
}
