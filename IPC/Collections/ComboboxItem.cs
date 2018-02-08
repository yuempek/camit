using System;
using System.Collections.Generic;
using System.Text;

namespace IPC.Collections
{
    public class ComboboxItem<T>
    {
        private Type valueType = typeof(T);
        public string name = "";
        public T value;

        public ComboboxItem()
        {
        }

        public ComboboxItem(string name, T value)
        {
            this.name = name;
            this.value = value;
        }

        override public string ToString()
        {
            return this.name;
        }
    }
}
