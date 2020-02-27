using System;

namespace RPGStoreSimulator
{
    /// <summary>
    /// Player (the user).
    /// </summary>
    class Player : InventoryBase /* The user */
    {
        public bool atStore = false;

        public Player()
        {
            balance = 1000;
            name = "Player";
        }

        public bool ItemExists(BaseItem item) /* Does the item exist in the item stack */
        {
            bool exists = false;

            foreach (BaseItem item2 in itemList)
            {
                if (item2 == item)
                {
                    exists = true;
                    break;
                }
            }

            return exists;
        }

        public void BuyItem(BaseItem item) /* Buy an item from the store */
        {
            if (ItemExists(item) & shop.HasItem(item))
            {
                int cost = item.GetCost();
                if (balance >= cost)
                {
                    balance -= cost;
                    Print("You purchased: " + item.GetName() + " for $" + cost + ". Your balance is now: $" + balance, ConsoleColor.Green);

                    AddItem(item, 1);

                    shop.RemoveItem(item);
                }
                else
                {
                    Print("You cannot afford the " + item.GetName() + "!", ConsoleColor.Red);
                }
            }
            else
            {
                Print("You cannot buy an item that Garvalsh doesn't own.", ConsoleColor.Red);
            }
        }
        public void SellItem(BaseItem item) /* Sell an item to the store */
        {
            if (ItemExists(item) & user.HasItem(item))
            {
                int cost = item.GetCost();
                balance += cost;

                Print("You sold: " + item.GetName() + " for $" + cost + ". Your balance is now: $" + balance, ConsoleColor.Green);

                shop.AddItem(item, 1);

                RemoveItem(item);
            }
            else
            {
                Print("You cannot sell an item you don't own.", ConsoleColor.Red);
            }
        }
    }
}
