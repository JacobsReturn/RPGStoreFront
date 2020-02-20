using System;

namespace RPGStoreSimulator
{
    /// <summary>
    /// Store command.
    /// </summary>
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
}
