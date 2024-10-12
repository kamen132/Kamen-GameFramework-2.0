using System.Collections.Generic;
using UnityEngine;

namespace KamenFramework.Runtime.Tool.UnityExtension
{
    public static partial class UnityExtension
    {
        public static int ToInt(this string value)
        {
            int ret = 0;
            if (int.TryParse(value, out ret))
            {
                return ret;
            }

            KLogger.LogError("int parse error");
            return 0;
        }
        
        public static int ToInt(this char value)
        {
            if (!int.TryParse(value.ToString(), out var ret))
            {
                return 0;
            }
            KLogger.LogError("int parse error");
            return ret;
        }

        public static float ToFloat(this string value)
        {
            float ret = 0;
            if (float.TryParse(value, out ret))
            {
                return ret;
            }

            KLogger.LogError("float parse error");
            return 0;
        }

        public static long ToLong(this string value)
        {
            long ret = 0;
            if (long.TryParse(value, out ret))
            {
                return ret;
            }

            KLogger.LogError("long parse error");
            return 0;
        }

        public static bool IsNull(this object obj)
        {
            if (obj is Object)
                return obj.Equals(null);

            return obj == null;
        }

        public static void SumDictionary<T1, T2>(this IDictionary<T1, T2> dest, IDictionary<T1, T2> source)
        {
            foreach (T1 key in (IEnumerable<T1>) source.Keys)
            {
                dest[key] = source[key];
            }
        }
    }
}