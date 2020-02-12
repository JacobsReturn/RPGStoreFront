using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime;

/* File is for Item and Inventory system */
namespace RPGStoreSimulator
{
    /* Classes */
    internal class InventoryBase : Program /* Inventory so I didnt have to do it in every class */
    {
        public List<BaseItem> inventoryList = new List<BaseItem>(); /* Creating a list for inventory (ease of access) */
        public InventoryBase() { }

        public void AddItem(BaseItem item, int amount) /* Add an item to the inventory */
        {
            for (int i = 0; i < amount; i++)
            {
                inventoryList.Add(item);
                /* Print("You just received, " + item.GetName()); */
            }
        }

        public void GetInventory(string spacing) /* Nicely print the inventory */
        {
            foreach (BaseItem item in inventoryList)
            {
                item.NicePrint(spacing);
            }
        }

        public void RemoveItem(BaseItem item) /* Remove an item to the inventory */
        {
            inventoryList.Remove(item);
        }
    }

    class Player : InventoryBase /* The user */
    {
        public bool buying = false;
        public bool selling = false;
        public int balance = 1000;

        public Player() { }

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
            if (ItemExists(item))
            {
                if (balance >= item.Cost)
                {
                    balance -= item.Cost;
                    Print("You purchased: " + item.GetName() + " for $" + item.Cost + ". Your balance is now: $" + balance, "Red");

                    AddItem(item, 1);
                    buying = false;
                    selling = false;

                    shop.RemoveItem(item);
                }
                else
                {
                    Print("You cannot afford that item!", "White");
                }
            }
        }
        public void SellItem(BaseItem item) /* Sell an item to the store */
        {
            if (ItemExists(item))
            {
                if (balance >= item.Cost)
                {
                    balance += item.Cost;

                    Print("You sold: " + item.GetName() + " for $" + item.Cost + ". Your balance is now: $" + balance, "Red");

                    shop.AddItem(item, 1);

                    buying = false;
                    selling = false;

                    RemoveItem(item);
                }
                else
                {
                    Print("You cannot afford that item!", "White");
                }
            }
        }
    }

    class Store : InventoryBase /* Interactable Store */
    {

    }

    /* Items */
    internal class BaseItem : Program /* Item Base. */
    {
        public string Name;
        public string Description;
        public int Cost;

        public BaseItem() /* Constructor. */
        {
            this.Name = "Unknown";
            this.Description = "Unknown";
            this.Cost = 0;
        }

        public void SetName(string value) /* Setting item name. */
        {
            this.Name = value;
        }

        public void SetDescription(string value) /* Setting item description. */
        {
            this.Description = value;
        }

        public void SetCost(int value) /* Setting item cost. */
        {
            this.Cost = value;
        }

        public string GetName() /* Grabbing items name. */
        {
            return this.Name;
        }

        public string GetDescription() /* Grabbing items description. */
        {
            return this.Description;
        }

        public int GetCost() /* Grabbing items cost. */
        {
            return this.Cost;
        }

        public void NicePrint(string spacing)
        {
            Print(spacing + this.GetName() + ", " + this.GetDescription() + ", $" + this.GetCost(), "White");
        }
    }
}