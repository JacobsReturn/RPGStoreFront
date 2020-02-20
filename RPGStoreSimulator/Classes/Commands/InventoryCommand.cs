using System;

namespace RPGStoreSimulator
{
    /// <summary>
    /// Inventory Command.
    /// </summary>
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
}
