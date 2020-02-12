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

        /// <summary>
        /// Printing text in colour and a simplier version of Console.WriteLine to ice it off.
        /// </summary>
        /// <param name="str">The text to be printed.</param>
        /// <param name="col">The colour in text form. Example: "Green".</param>
        public static void Print(string str, string col)
        {
            Type type = typeof(ConsoleColor);

            Console.ForegroundColor = (ConsoleColor)Enum.Parse(type, col);
            Console.WriteLine(str);

            Console.ResetColor();
        }

        /// <summary>
        /// Creates items for global use that can be accessed by a player and the store.
        /// </summary>
        /// <param name="name">Name of the item. Example: "God Sword".</param>
        /// <param name="description">The description of the item so the user can understand its importance/use.</param>
        /// <param name="cost">How much the user has to pay for it.</param>
        public static void CreateItem(string name, string description, int cost)
        {
            BaseItem item = new BaseItem();
            item.SetName(name);
            item.SetDescription(description);
            item.SetCost(cost);

            itemList.Add(item);
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
            /* Adding items */
            CreateItem("God Spear", "The spear of the ages, something so powerful a mere mortal would crumble in fear", 1100);
            CreateItem("Wooden Sword", "A sturdy piece of wood capable of barely damaging your enemies", 85);

            Print("Type /help for more information on the commands.", "White");

            /* Checking if files exist */
            if (!File.Exists(@"C:\Users\s200503\source\repos\RPGStoreSimulator" + @"\" + user.name + ".txt") & !File.Exists(@"C:\Users\s200503\source\repos\RPGStoreSimulator" + @"\" + shop.name + ".txt"))
            {
                /* Giving the user and the shop class an item to have in their inventory */
                user.AddItem(GetItem("Wooden Sword"), 3);
                shop.AddItem(GetItem("God Spear"), 1);
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
