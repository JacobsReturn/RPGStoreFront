using System;
using System.Collections.Generic;
using System.Text;

namespace RPGStoreSimulator
{
    /// <summary>
    /// My new library for not-using lists.
    /// </summary>
    internal class Table : Program
    {
        public Table()
        {

        }
         
        /// <summary>
        /// Basically like sorting a list but more complex.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="array">Array to be grabbed.</param>
        /// <param name="comparison">Comparison of array inputs.</param>
        /// <param name="outArray">Output array location</param>
        public static void Sort<T>(T[] array, Comparison<T> comparison, out T[] outArray)
        {
            Array.Sort(array, comparison);

            outArray = array;
        }

        /// <summary>
        /// Basically like sorting adding to a list but more complex.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="array">Array to be grabbed.</param>
        /// <param name="input">To be added to array.</param>
        /// <param name="outArray">Output array location</param>
        public static void Add<T>(T[] array, T input, out T[] outArray)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = input;

            outArray = array;
        }

        /// <summary>
        /// Basically like removing from a list but more complex.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="array">Array to be grabbed.</param>
        /// <param name="input">To be removed from array.</param>
        /// <param name="outArray">Output array location</param>
        public static void Remove<T>(T[] array, T input, out T[] outArray)
        {
            int newLength = array.Length - 1;

            T[] newArray = new T[newLength];

            for (int i = 0; i <= newLength; i++)
            {
                if (!array[i].Equals(input))
                {
                    newArray[newArray.GetUpperBound(0)] = array[i];
                }
            }

            outArray = newArray;
        }

        /// <summary>
        /// Prints the array (ease of access)
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="array">To be printed.</param>
        public static void PrintTable<T>(T[] array)
        {
            foreach (T display in array)
            {
                Print(display.ToString());
            }
        }
    }
}
