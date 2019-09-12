using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsDatabaseManager
{
    public class FindableBindingList<T>: BindingList<T>
    {
        public FindableBindingList() : base() { }

        public FindableBindingList(List<T> list): base(list) { }

        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            // Simple iteration:
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
