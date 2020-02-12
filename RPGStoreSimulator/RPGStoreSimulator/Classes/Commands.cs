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
        public string textSpacing = "  ";

        public Commands()
        {
            this.stringCommand = "/unknown";
        }

        public virtual void Execute(string arg)
        {
            Console.WriteLine("failed_execute");
        }

        public bool CanRun(string command)
        {
            return (command.ToLower() == this.stringCommand.ToLower());
        }

        public void SetCommand(string command)
        {
            this.stringCommand = command;
        }
    }

    class HelpCommand : Commands
    {
        public HelpCommand()
        {
            this.SetCommand("/help");
        }

        public override void Execute(string arg)
        {
            Print("Here are a list of commands: ", "White");
            Print(textSpacing + "- /store (takes you to the store)", "White");
            Print(textSpacing + "- /buy (allows you to select an item to purchase)", "White");
            Print(textSpacing + "- /inventory (allows you to view your inventory)", "White");
            Print(textSpacing + "- /sell (allows you to sell your items)", "White");
        }
    }

    class StoreCommand : Commands
    {
        public StoreCommand()
        {
            this.SetCommand("/store");
        }

        public override void Execute(string arg)
        {
            user.atStore = true;

            Print("Garvalsh > Welcome to Garvalsh's Weaponry and Armour!", "Cyan");
            Print("Garvalsh > Here are my current supplies! Hopefully you will buy something traveller!", "Cyan");
            Print(" You currently have: $" + user.balance, "Green");

            shop.GetInventory(textSpacing + "- ");
        }
    }

    class InventoryCommand : Commands
    {
        public InventoryCommand()
        {
            this.SetCommand("/inventory");
        }

        public override void Execute(string arg)
        {
            Print("You currently have: $" + user.balance, "Green");
            Print("Your Inventory:", "White");

            user.GetInventory(textSpacing + "- ");
        }
    }

    class BuyCommand : Commands
    {
        public BuyCommand()
        {
            this.SetCommand("/buy");
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
            this.SetCommand("/sell");
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