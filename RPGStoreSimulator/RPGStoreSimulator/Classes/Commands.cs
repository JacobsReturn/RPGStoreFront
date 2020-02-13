using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime;

namespace RPGStoreSimulator
{
    internal class Commands : Program
    {
        public string stringCommand;
        public string commandHelp;
        public string textSpacing = "  ";

        public Commands()
        {
            this.stringCommand = "/unknown";
            this.commandHelp = "This command has no meaning, sorry.";
        }

        public virtual void Execute(string arg)
        {
            Console.WriteLine("failed_execute");
        }

        public bool CanRun(string command)
        {
            return (command.ToLower() == this.stringCommand.ToLower());
        }

        /// <summary>
        /// Set the command and helper description of the command.
        /// </summary>
        /// <param name="command">The command to type can be anything. Example: "/help".</param>
        /// <param name="help">The message it displays when using the command to help the user.</param>
        public void SetCommand(string command, string help)
        {
            SetCommand(command);
            this.commandHelp = help;
        }
        public void SetCommand(string command) // Easy way to not use multiple args ;)
        {
            this.stringCommand = command;
        }
    }

    class HelpCommand : Commands
    {
        public HelpCommand()
        {
            this.SetCommand("/help", "To get some help.");
        }

        public override void Execute(string arg)
        {
            Print("Here are a list of commands: ", ConsoleColor.Cyan);
            foreach (Commands command in commandList)
            {
                if (command.stringCommand != "/help")
                {
                    Print(textSpacing + "- " + command.stringCommand, ConsoleColor.White);
                    Print(textSpacing + "  " + command.commandHelp, ConsoleColor.Blue);
                    Console.WriteLine("");
                }
            }
        }
    }

    /// <summary>
    /// Search bar for console.
    /// </summary>
    class SearchCommand : Commands
    {
        public SearchCommand()
        {
            this.SetCommand("/search", "To search for items.");
        }

        public override void Execute(string arg)
        {
            if (arg.Length > 0 & arg != this.stringCommand)
            {
                itemList.Sort((x, y) => x.GetRarity().CompareTo(y.GetRarity()));

                Print($"Here are a list of results for: {arg}.", ConsoleColor.Cyan);

                foreach (BaseItem item in itemList)
                {
                    string input = item.GetName();

                    if (input.ToLower().Contains(arg))
                    {
                        item.NicePrint("  - ");
                    }


                }
            }
            else Print("You cant search nothing, use '/search {item name}'.", ConsoleColor.Red);
        }
    }

    class CustomCommand : Commands
    {
        public CustomCommand()
        {
            this.SetCommand("/admin_create", "Creates items that save to a data file for use later (will be added to the store).");
        }

        public override void Execute(string arg)
        {
            string[] args = arg.Split(',');

            if (args.Length > 2 & arg != this.stringCommand)
            {
                Print($"Trying to create custom: {args[0]}.", ConsoleColor.Cyan);
                Print(arg, ConsoleColor.White);
                Print(args[1], ConsoleColor.White);
            }
            else Print("You cant create nothing, use '/admin_create {item name},{description},{cost},{type},{rarity}'.", ConsoleColor.Red);
        }
    }

    class StoreCommand : Commands
    {
        public StoreCommand()
        {
            this.SetCommand("/store", "Used to enter Garvalsh's store, and to display the list of items available for purchase.");
        }

        public override void Execute(string arg)
        {
            user.atStore = true;

            Print("Garvalsh \x25BA Welcome to Garvalsh's \x25D8 Weaponry and Armoury! \x25D8", ConsoleColor.Cyan);
            Print("Garvalsh \x25BA Here are my current supplies! Hopefully you will buy something traveller!", ConsoleColor.Cyan);
            Print(" You currently have: $" + user.balance, ConsoleColor.Green);

            shop.GetInventory(textSpacing + "- ");
        }
    }

    class InventoryCommand : Commands
    {
        public InventoryCommand()
        {
            this.SetCommand("/inventory", "Used to open and view your current inventory.");
        }

        public override void Execute(string arg)
        {
            Print("You currently have: $" + user.balance, ConsoleColor.Green);
            Print("Your Inventory:", ConsoleColor.White);

            user.GetInventory(textSpacing + "- ");
        }
    }

    class InspectCommand : Commands
    {
        public InspectCommand()
        {
            this.SetCommand("/inspect", "Used to insect any item in the game. (Usage: /inspect God Spear)");
        }

        public override void Execute(string arg)
        {
            if (arg.Length > 0 & arg != this.stringCommand)
            {
                bool found = false;
                foreach (BaseItem item in shop.inventoryList)
                {
                    string itemName = item.GetName();
                    if (arg == itemName)
                    {
                        item.PrintInfo();
                        found = true;

                        break;
                    }
                }

                if (!found)
                {
                    Print("The item " + arg + " does not exist.", ConsoleColor.Red);
                }
            }
            else Print("You cant inspect nothing, use '/inspect {item name}'.", ConsoleColor.Red);
        }
    }

    class BuyCommand : Commands
    {
        public BuyCommand()
        {
            this.SetCommand("/buy", "Used to buy an item from Garvalsh's store. (Usage: /buy God Spear)");
        }

        public override void Execute(string arg)
        {
            if (!user.atStore) Print("You must be at the store to buy stuff!", ConsoleColor.Red);

            if (user.atStore & arg.Length > 0 & arg != this.stringCommand)
            {
                if (shop.inventoryList.Count < 1) Print("You cannot buy an item Galvash doesn't own.", ConsoleColor.Red);

                bool found = false;
                foreach (BaseItem item in shop.inventoryList)
                {
                    string itemName = item.GetName();
                    if (arg == itemName)
                    {
                        user.BuyItem(item);
                        found = true;

                        break;
                    }
                }

                if (!found)
                {
                    Print("Galvash does not own the item " + arg + ".", ConsoleColor.Red);
                }
            }
            else if (user.atStore) Print("You cant buy nothing, use '/buy {item name}', and use /store to find a list of items for purchase.", ConsoleColor.Red);
        }
    }

    class SellCommand : Commands
    {
        public SellCommand()
        {
            this.SetCommand("/sell", "Used to sell an item from your inventory to Garvalsh. (Example: /sell Wooden Sword)");
        }

        public override void Execute(string arg)
        {
            if (!user.atStore) Print("You must be at the store to sell stuff!", ConsoleColor.Red);

            bool found = false;
            if (user.atStore & arg.Length >= 0 & arg != this.stringCommand)
            {
                if (user.inventoryList.Count < 1) Print("You cannot sell an item you don't own.", ConsoleColor.Red);

                foreach (BaseItem item in user.inventoryList)
                {
                    string itemName = item.GetName();
                    if (arg == itemName)
                    {
                        user.SellItem(item);
                        found = true;

                        break;
                    }
                }

                if (!found) Print("Galvash does not own the item " + arg + ".", ConsoleColor.Red);
            }
            else if (user.atStore) Print("You cannot sell 'nothing', use '/sell {item name}', and use /inventory to find a list of items to sell.", ConsoleColor.Red);
        }
    }
}