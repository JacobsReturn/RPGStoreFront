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
            Print("Here are a list of commands: ", "Cyan");
            foreach (Commands command in commandList)
            {
                if (command.stringCommand != "/help")
                {
                    Print(textSpacing + "- " + command.stringCommand, "White");
                    Print(textSpacing + "  " + command.commandHelp, "Blue");
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
            itemList.Sort((x, y) => x.GetRarity().CompareTo(y.GetRarity()));

            Print($"Here are a list of results for: {arg}.", "Cyan");

            foreach (BaseItem item in itemList)
            {
                string input = item.GetName();

                if (input.ToLower().Contains(arg))
                {
                    item.NicePrint("  - ");
                }

               
            }
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

            Print("Garvalsh \x25BA Welcome to Garvalsh's \x25D8 Weaponry and Armoury! \x25D8", "Cyan");
            Print("Garvalsh \x25BA Here are my current supplies! Hopefully you will buy something traveller!", "Cyan");
            Print(" You currently have: $" + user.balance, "Green");

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
            Print("You currently have: $" + user.balance, "Green");
            Print("Your Inventory:", "White");

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
                    Print("The item " + arg + " does not exist.", "Red");
                }
            }
            else if (user.atStore) Print("You cant inspect nothing, use '/inspect {item name}'.", "Red");
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
            if (!user.atStore) Print("You must be at the store to buy stuff!", "Red");

            if (user.atStore & arg.Length > 0 & arg != this.stringCommand)
            {
                if (shop.inventoryList.Count < 1) Print("You cannot buy an item Galvash doesn't own.", "Red");

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
                    Print("Galvash does not own the item " + arg + ".", "Red");
                }
            }
            else if (user.atStore) Print("You cant buy nothing, use '/buy {item name}', and use /store to find a list of items for purchase.", "Red");
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
            if (!user.atStore) Print("You must be at the store to sell stuff!", "Red");

            bool found = false;
            if (user.atStore & arg.Length >= 0 & arg != this.stringCommand)
            {
                if (user.inventoryList.Count < 1) Print("You cannot sell an item you don't own.", "Red");

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

                if (!found) Print("Galvash does not own the item " + arg + ".", "Red");
            }
            else if (user.atStore) Print("You cannot sell 'nothing', use '/sell {item name}', and use /inventory to find a list of items to sell.", "Red");
        }
    }
}