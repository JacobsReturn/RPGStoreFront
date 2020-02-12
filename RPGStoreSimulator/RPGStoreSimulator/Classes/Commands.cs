using System;
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

        public virtual void Execute(string[] arg)
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

        public override void Execute(string[] arg)
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

        public override void Execute(string[] arg)
        {
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

        public override void Execute(string[] arg)
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

        public override void Execute(string[] args)
        {
            if (args.Length > 1)
            {
                foreach (BaseItem item in shop.inventoryList)
                {
                    string itemName = item.GetName();
                    if (args[1] == itemName)
                    {
                        user.BuyItem(item);

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
