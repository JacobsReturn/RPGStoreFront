using System;

namespace RPGStoreSimulator
{
    /// <summary>
    /// Sell command.
    /// </summary>
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
                if (user.inventoryList.Length < 1) Print("You cannot sell an item you don't own.", ConsoleColor.Red);

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
