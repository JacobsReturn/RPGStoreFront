using System;

namespace RPGStoreSimulator
{
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
                Table.Sort(itemList, (x, y) => x.GetRarity().CompareTo(y.GetRarity()), out itemList);

                Print($"Here are a list of results for: {arg}.", ConsoleColor.Cyan);

                foreach (BaseItem item in itemList)
                {
                    string input = item.GetName();
                    bool check = input.Contains(arg);

                    if (check)
                    {
                        item.NicePrint("  - ");
                    }
                }
            }
            else Print("You cant search nothing, use '/search {item name}'.", ConsoleColor.Red);
        }
    }
}
