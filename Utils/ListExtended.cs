using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Word_Representation.Utils
{
    internal class ListExtended<T>
    {
        public static List<T> Shuffle(List<T> list)
        {
            Random rand = new Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                T v = list[k];
                list[k] = list[n];
                list[n] = v;
            }
            return list;
        }
    }
}
