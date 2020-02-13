using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime;

namespace RPGStoreSimulator
{
    /// <summary>
    /// Base class for the RPGStoreSimulator.
    /// </summary>
    class Program
    {
        public static List<Commands> commandList; /* Creating a command list for all commands (ease of access). */
        public static List<BaseItem> itemList = new List<BaseItem>(); /* Creating a list for all created items (ease of access). */

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
        /// Printing text in colour and a simplier version of Console.WriteLine to ice it off.
        /// </summary>
        /// <param name="str">The text to be printed.</param>
        /// <param name="col">The colour in text form. (Example: ConsoleColor.Green).</param>
        public static void Print(string str, ConsoleColor col)
        {
            Console.ForegroundColor = col;
            Console.WriteLine(str);

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Printing text in colour and a simplier version of Console.WriteLine to ice it off.
        /// </summary>
        /// <param name="str">The text to be printed.</param>
        /// <param name="col">The colour in text form. Example: "Green".</param>
        public static void Print(string str, string col)
        {
            Print(str, (ConsoleColor)Enum.Parse(typeof(ConsoleColor), col));
        }

        /// <summary>
        /// Creates items for global use that can be accessed by a player and the store.
        /// </summary>
        /// <param name="name">Name of the item. Example: "God Sword".</param>
        /// <param name="description">The description of the item so the user can understand its importance/use.</param>
        /// <param name="cost">How much the user has to pay for it.</param>
        public static void CreateItem(string name, string description, int cost, string category, int rarity, string[] stats)
        {
            BaseItem item = new BaseItem();
            item.SetName(name);
            item.SetDescription(description);
            item.SetCost(cost);
            item.SetCategory(category);
            item.SetRarity(rarity);
            item.SetStats(stats);

            itemList.Add(item);
        }

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

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8; // Used for \x character map numbers later on.

            /* Adding items */
            CreateItem("Heavens Penetration", "The spear of the ages, something so powerful a mere mortal would crumble in fear.", 1800, "Spear", 1);
            CreateItem("Hell Daggers", "The daggers born from hell itself.", 1600, "Daggers", 1);
            CreateItem("Frost Staff", "Allows the wielder to shoot powerful ice bolts.", 1300, "Magic Staff", 1);
            CreateItem("Wooden Sword", "A sturdy piece of wood capable of barely damaging your enemies", 8, "Sword", 5);
            CreateItem("Slime Skin", "Skin of a slime.", 18, "Item", 5);
            CreateItem("Dragon Scale", "A scale of a dragon.", 1000, "Item", 1);
            CreateItem("Golden Feather", "The feather of a golden duck.", 630, "Item", 2);
            CreateItem("Poison Staff", "A staff that can poison the target.", 800, "Magic Staff", 2);
            CreateItem("Dragon Scale Chestplate", "Scale Armour.", 2500, "Chestplate", 1);
            CreateItem("Dragon Scale Helmet", "Scale Armour.", 1800, "Helmet", 1);
            CreateItem("Dragon Scale Leggings", "Scale Armour.", 2300, "Leggings", 1);
            CreateItem("Dragon Scale Boots", "Scale Armour.", 800, "Boots", 1);

            Print("Type /help for more information on the commands.", ConsoleColor.White);

            /* Checking if files exist */
            if (!File.Exists(repo + @"\" + user.name + @".txt") & !File.Exists(repo + @"\" + shop.name + @".txt"))
            {
                /* Giving the user and the shop class an item to have in their inventory */
                user.AddItem(GetItem("Wooden Sword"), 3);
                user.AddItem(GetItem("Dragon Scale"), 1);
                user.AddItem(GetItem("Slime Skin"), 4);

                foreach (BaseItem item in itemList)
                {
                    shop.AddItem(GetItem(item.GetName()), item.GetRarity());
                }
            }
            else
            {
                /* Loading all items from files */
                user.Load();
                shop.Load();
            }

            /* Creating a list for all of our commands (ease of access). */
            commandList = new List<Commands>()
            {
                new HelpCommand(),
                new StoreCommand(),
                new InventoryCommand(),
                new BuyCommand(),
                new SellCommand(),
                new InspectCommand(),
                new SearchCommand(),
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
