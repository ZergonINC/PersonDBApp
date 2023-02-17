using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PersonDBApp
{
    internal class ConsoleWriter<T>
    {
        public ConsoleWriter(List<T> collection)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var o in collection)
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    Console.Write($"{properties[i].GetValue(o)}  ");
                }
                Console.WriteLine();
            }
        }
    }
}