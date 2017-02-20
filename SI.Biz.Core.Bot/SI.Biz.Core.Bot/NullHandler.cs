using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI.Biz.Core.Bot
{
    public static class Empty<T> where T : new()
    {
        public static List<T> DefaultList()
        {
            return new List<T>();
        }

        public static T Default()
        {
            return new T();
        }
    }
}
