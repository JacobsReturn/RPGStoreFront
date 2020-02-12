using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime;

namespace RPGStoreSimulator
{
    class Program
    {
        public static List<Commands> commandList; /* Creating a command list for all commands (ease of access). */
        public static List<BaseItem> itemList = new List<BaseItem>(); /* Creating a list for all created items (ease of access). */

        /* Creating entities */
        public static Player user = new Player();
        public static Store shop = new Store();

        public static BaseItem itemReference = new BaseItem();
        public static void Print(string str, string col) /* Printing text in colour and a simplier version of Console.WriteLine to ice it off. */
        {
            Type type = typeof(ConsoleColor);

            Console.ForegroundColor = (ConsoleColor)Enum.Parse(type, col);
            Console.WriteLine(str);

            Console.ResetColor();
        }

        static void CreateItem(string name, string description, int cost) /* Creating items, easier then just having a bunch of code everywhere for each item. */
        {
            BaseItem item = new BaseItem();
            item.SetName(name);
            item.SetDescription(description);
            item.SetCost(cost);

            itemList.Add(item);
        }

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

        static void Think() /* Something that is run every tick */
        {
            string stringReadLine = Console.ReadLine();

            foreach (Commands command in commandList)
            {
                if (command.stringCommand.Length <= stringReadLine.Length)
                {
                    string subCommand = stringReadLine.Substring(0, command.stringCommand.Length);

                    if (command.CanRun(subCommand)) /* Checking if the command is the same */
                    {
                        string subArg = subCommand;

                        if (stringReadLine.Length > command.stringCommand.Length)
                        {
                            subArg = stringReadLine.Substring(command.stringCommand.Length + 1, stringReadLine.Length - command.stringCommand.Length - 1); 
                        }

                        command.Execute(subArg);
                    }
                }
            }
        }
    }
}
