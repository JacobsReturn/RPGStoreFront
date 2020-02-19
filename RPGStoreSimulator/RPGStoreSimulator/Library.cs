using System;

namespace RPGStoreSimulator // Set to current namespace.
{
    class Library
    {
        /// <summary>
        /// Printing text in colour and a simplier version of Console.WriteLine to ice it off.
        /// </summary>
        /// <param name="str">The text to be printed.</param>
        /// <param name="col">The colour in text form. (Example: ConsoleColor.Green).</param>
        public static void Print(string str, ConsoleColor col)
        {
            Console.ForegroundColor = col;

            if (str.Length == 1 & !int.TryParse(str, out int _))
            {
                Console.Write(str);
            }
            else
            {
                Console.WriteLine(str);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Printing text in colour and a simplier version of Console.WriteLine to ice it off.
        /// </summary>
        /// <param name="str">The text to be printed.</param>
        /// <param name="col">The colour in text form. Example: "Green".</param>
        public static void Print(string str, string col)
        {
            Print(str, (ConsoleColor)Enum.Parse(typeof(ConsoleColor), col));
        }

        /// <summary>
        /// Printing text in colour and a simplier version of Console.WriteLine to ice it off.
        /// </summary>
        /// <param name="input">The text to be printed.</param>
        /// <param name="col">The colour in text form. Example: "Green".</param>
        public static void Print(int input, string col)
        {
            Print(input.ToString(), (ConsoleColor)Enum.Parse(typeof(ConsoleColor), col));
        }

        /// <summary>
        /// Printing text in colour and a simplier version of Console.WriteLine to ice it off.
        /// </summary>
        /// <param name="str">The text to be printed.</param>
        public static void Print(string str)
        {
            Print(str, ConsoleColor.White);
        }

        /// <summary>
        /// Printing text in colour and a simplier version of Console.WriteLine to ice it off.
        /// </summary>
        /// <param name="input">The text to be printed.</param>
        public static void Print(int input)
        {
            Print(input.ToString(), ConsoleColor.White);
        }

        /// <summary>
        /// Im lazy aight.
        /// </summary>
        public static void Space()
        {
            Console.WriteLine("");
        }
    }
}
