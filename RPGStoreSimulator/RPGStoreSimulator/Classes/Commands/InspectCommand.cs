using System;

namespace RPGStoreSimulator
{
    /// <summary>
    /// Inspection command.
    /// </summary>
    class InspectCommand : Commands
    {
        public InspectCommand()
        {
            this.SetCommand("/inspect", "Used to insect any item in the game. (Usage: /inspect God Spear)");
        }

        public override void Execute(string arg)
        {
            if (arg.Length > 0 & arg != this.stringCommand)
            {
                bool found = false;
                foreach (BaseItem item in itemList)
                {
                    string itemName = item.GetName();
                    if (arg == itemName)
                    {
                        item.PrintInfo();
                        found = true;

                        break;
                    }
                }

                if (!found)
                {
                    Print("The item " + arg + " does not exist.", ConsoleColor.Red);
                }
            }
            else Print("You cant inspect nothing, use '/inspect {item name}'.", ConsoleColor.Red);
        }
    }
}
