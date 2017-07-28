using System.Collections.Generic;
using UnityEngine;

namespace ConfigurationWindow.DataStorage
{
    public static class OverviewElements
    {
        /// <summary>
        /// The Datastorage to save all string elements.
        /// </summary>
        private static List<string> overviewData;

        /// <summary>
        /// Initialize the List.
        /// </summary>
        public static void Initialize()
        {
            overviewData = new List<string>();
        }

        /// <summary>
        /// Checks if the datastructure is not initialized.
        /// </summary>
        /// <returns>Returns an boolean if it is not initialized.</returns>
        public static bool IsEmpty()
        {
            return overviewData.Count == 0;
        }

        /// <summary>
        /// Insert an string element into the Datastorage.
        /// </summary>
        /// <param name="data">The string which will be saved in the storage.</param>
        public static void InsertElement(string data)
        {
            if (!overviewData.Contains(data))
                overviewData.Add(data);
        }

        /// <summary>
        /// Remove an element from the Datastorage.
        /// </summary>
        /// <param name="i">To remove from an index.</param>
        /// <returns>Returns true if the element was removed succesfully, otherwise false.</returns>
        public static bool RemoveElement(int i)
        {
            if (i > 0)
                return overviewData.Remove(overviewData[i]);
            else
                return false;
        }

        /// <summary>
        /// Removes all elements from the overviewData list.
        /// </summary>
        public static void ClearList()
        {
            overviewData.Clear();
        }

        /// <summary>
        /// To get an element from the Datastorage.
        /// </summary>
        /// <param name="i">The index to show which element to get.</param>
        /// <returns>Returns the string from the Datastorage.</returns>
        public static string GetElement(int i)
        {
            return overviewData[i];
        }

        /// <summary>
        /// Get the length of the Datastorage.
        /// </summary>
        /// <returns>Returns an int to show how large the Datastorage is.</returns>
        public static int Length()
        {
            return overviewData.Count;
        }

        /// <summary>
        /// Print the datastructure in the console.
        /// </summary>
        public static string Print()
        {
            string temp = "";
            foreach(string s in overviewData)
            {
                temp += s + " ";
            }
            return temp;
        }
    }
}
