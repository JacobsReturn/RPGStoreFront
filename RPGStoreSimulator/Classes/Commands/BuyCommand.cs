using System;

namespace RPGStoreSimulator
{
    /// <summary>
    /// Buy command.
    /// </summary>
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
                if (shop.inventoryList.Length < 1) Print("You cannot buy an item Galvash doesn't own.", ConsoleColor.Red);

                bool found = false;
                foreach (BaseItem item in shop.inventoryList)
                {
                    string itemName = item.GetName();
                    if (arg == itemName & !found)
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
}
