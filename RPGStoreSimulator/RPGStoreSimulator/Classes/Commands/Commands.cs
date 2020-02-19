using System;

namespace RPGStoreSimulator
{
    /// <summary>
    /// Base for all commands.
    /// </summary>
    internal class Commands : Program
    {
        public string stringCommand;
        public string commandHelp;
        public string textSpacing = "  ";

        /// <summary>
        /// Setting up the command.
        /// </summary>
        public Commands()
        {
            this.stringCommand = "/unknown";
            this.commandHelp = "This command has no meaning, sorry.";
        }

        public virtual void Execute(string arg)
        {
            Console.WriteLine("failed_execute");
        }

        public bool CanRun(string command)
        {
            return (command.ToLower() == this.stringCommand.ToLower());
        }

        /// <summary>
        /// Set the command and helper description of the command.
        /// </summary>
        /// <param name="command">The command to type can be anything. Example: "/help".</param>
        /// <param name="help">The message it displays when using the command to help the user.</param>
        public void SetCommand(string command, string help)
        {
            SetCommand(command);
            this.commandHelp = help;
        }
        public void SetCommand(string command) // Easy way to not use multiple args ;)
        {
            this.stringCommand = command;
        }
    }
}
