using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetro
{
    public static class Extensions
    {
        private static Random rng = new Random();

        public static type Random<type>(this List<type> list)
        {
            if (list.Count == 0) return default(type);
            return list[Extensions.rng.Next(list.Count)];
        }
        public static type Random<type>(this HashSet<type> list)
        {
            return list.ToList().Random();
        }
    }
}
