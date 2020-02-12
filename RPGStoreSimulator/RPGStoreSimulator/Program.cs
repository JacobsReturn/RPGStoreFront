using System;
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
            /*
            foreach (BaseItem item in itemList)
            {
                item.NicePrint("");
            }
            */

            /* Giving the user and the shop class an item to have in their inventory */
            user.AddItem(GetItem("Wooden Sword"), 35);
            shop.AddItem(GetItem("God Spear"), 1);

            /* Creating a list for all of our commands (ease of access). */
            commandList = new List<Commands>()
            {
                new HelpCommand(),
                new StoreCommand(),
                new InventoryCommand()
            };

            while (true) /* Creating our tick */
            {
                Think();
            }
        }

        static void Think() /* Something that is run every tick */
        {
            string textSpacing = "  ";
            bool found = false;

            foreach (Commands command in commandList)
            {
                if (command.CanRun(Console.ReadLine()))
                {
                    command.Execute();
                }
            }

            if (false)
            {
                switch (Console.ReadLine().ToLower()) /* Command looper */
                {
                    case "/store":
                        Print("Garvalsh > Welcome to Garvalsh's Weaponry and Armour!", "Cyan");
                        Print("Garvalsh > Here are my current supplies! Hopefully you will buy something traveller!", "Cyan");
                        Print(" You currently have: $" + user.balance, "Green");

                        shop.GetInventory(textSpacing + "- ");

                        found = true;

                        break;
                    case "/buy":
                        if (user.buying)
                        {
                            Print("You are already buying something! Type 'cancel' to stop buying something.", "White");
                        }
                        else
                        {
                            user.selling = false;
                            user.buying = true;

                            Print("Type the name of the item you would like to purchase from Garvalsh.", "White");
                        }

                        found = true;

                        break;
                    case "/sell":
                        if (user.selling)
                        {
                            Print("You are already selling something! Type 'cancel' to stop selling something.", "White");
                        }
                        else
                        {
                            user.buying = false;
                            user.selling = true;

                            Print("Type the name of the item you would like to sell to Garvalsh.", "White");
                        }

                        found = true;

                        break;
                    case "cancel":
                        user.buying = false;
                        user.selling = false;
                        Print("You are no longer about to buy/sell anything.", "White");

                        break;
                    case "/inventory":
                        Print("You currently have: $" + user.balance, "Green");
                        Print("Your Inventory:", "White");

                        user.GetInventory(textSpacing + "- ");

                        found = true;

                        break;
                    case "/help":
                        Print("Here are a list of commands: ", "White");
                        Print(textSpacing + "- /store (takes you to the store)", "White");
                        Print(textSpacing + "- /buy (allows you to select an item to purchase)", "White");
                        Print(textSpacing + "- /inventory (allows you to view your inventory)", "White");
                        Print(textSpacing + "- /sell (allows you to sell your items)", "White");

                        found = true;

                        break;
                }

                if (!found)
                {
                    if (user.buying)
                    {
                        foreach (BaseItem item in shop.inventoryList)
                        {
                            string itemName = item.GetName();
                            if (Console.ReadLine() == itemName & !found)
                            {
                                user.BuyItem(item);
                                found = true;

                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else if (user.selling)
                    {
                        foreach (BaseItem item in user.inventoryList)
                        {
                            string itemName = item.GetName();
                            if (Console.ReadLine() == itemName & !found)
                            {
                                user.SellItem(item);
                                found = true;

                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
