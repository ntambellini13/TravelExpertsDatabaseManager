using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Purpose: Class for creating bindable lists for binding sources
 * Author: Tawico
 * Date: September 18, 2019
 * 
 * */

namespace TravelExpertsDatabaseManager
{
    /// <summary>
    /// Creates a binding list from any other list that supports BindingSources' Find Method
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FindableBindingList<T>: BindingList<T>
    {
        public FindableBindingList() : base() { } // Use binding list constructor

        public FindableBindingList(List<T> list): base(list) { } // Use binding list constructor

        /// <summary>
        /// Checks if an element in the list has the specified property and key value
        /// </summary>
        /// <param name="prop">Property to check in elements</param>
        /// <param name="key">Value of property desired</param>
        /// <returns>The index where a match is found, -1 if no match</returns>
        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            // Iterates through all list items and see if key matches value of requested property
            for (int i = 0 ; i < Count ; i++)
            {
                T item = this[i];
                if (prop.GetValue(item).ToString().StartsWith(key.ToString()))
                {
                    return i;
                }
            }
            return -1; // Not found
        }
    }
}
