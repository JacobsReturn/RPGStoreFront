using System;
using System.IO;

namespace RPGStoreSimulator
{
    /// <summary>
    /// Custom item creation command.
    /// </summary>
    class CustomCommand : Commands
    {
        public CustomCommand()
        {
            this.SetCommand("/admin_create", "Creates items that save to a data file for use later (will be added to the store).");
        }

        public override void Execute(string arg)
        {
            string[] args = arg.Split(',');

            if (arg != this.stringCommand & args.Length >= 5)
            {
                Print($"Trying to create custom: {args[0]}.", ConsoleColor.Cyan);

                bool canCost = Int32.TryParse(args[2], out int cost);
                bool canRarity = Int32.TryParse(args[4], out int rarity);

                if (canCost & canRarity)
                {
                    string _stats = "";
                    if (args.Length > 5)
                    {
                        _stats = args[5];
                    }

                    CreateItem(args[0], args[1], cost, args[3], rarity, _stats.Split('|'));

                    shop.AddItem(GetItem(args[0]), Int32.Parse(args[4]));

                    string location = repo + @"\items\Item_" + args[0] + @".txt";
                    string[] lines = new string[6]
                    {
                        $"Name/{args[0]}",
                        $"Description/{args[1]}", 
                        $"Cost/{cost.ToString()}", 
                        $"Category/{args[3]}", 
                        $"Rarity/{rarity.ToString()}", 
                        $"Stats/{_stats}"
                    };

                    File.WriteAllLines(location, lines);

                    Print($"Success. Item has been created and sent to the store.", ConsoleColor.Cyan);
                }
                else
                {
                    if (!canCost) Print($"Error > Failed to create item. The cost is not a number.", ConsoleColor.Red);
                    if (!canRarity) Print($"Error > Failed to create item. The rarity is not a number.", ConsoleColor.Red);
                }
            }
            else Print("You cant create nothing, use '/admin_create {item name},{description},{cost},{type Sword, Spear, etc},{rarity 1-5},{stats example: - Inflicts Poison|- Damage: 50}'.", ConsoleColor.Red);
        }
    }
}
