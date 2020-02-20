using System;

namespace RPGStoreSimulator
{
    /// <summary>
    /// Help command.
    /// </summary>
    class HelpCommand : Commands
    {
        public HelpCommand()
        {
            this.SetCommand("/help", "To get some help.");
        }

        public override void Execute(string arg)
        {
            Print("Here are a list of commands: ", ConsoleColor.Cyan);
            foreach (Commands command in commandList)
            {
                if (command.stringCommand != "/help")
                {
                    Print(textSpacing + "- " + command.stringCommand, ConsoleColor.White);
                    Print(textSpacing + "  " + command.commandHelp, ConsoleColor.Blue);
                    Space();
                }
            }
        }
    }
}
