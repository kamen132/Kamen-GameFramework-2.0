using System.Collections.Generic;

namespace KamenFramework.Runtime.Tool.UnityExtension
{
    public static partial class UnityExtension
    {
        public static List<float> SplitToFloatList(this string str, char separator = '|')
        {
            var list = new List<float>();

            if (string.IsNullOrEmpty(str))
                return list;

            var strArray = str.Split(separator);
            for (var i = 0; i < strArray.Length; ++i) list.Add(float.Parse(strArray[i]));

            return list;
        }
        
        public static List<int> SplitToIntList(this string str, char separator = '|')
        {
            var list = new List<int>();

            if (string.IsNullOrEmpty(str))
                return list;

            var strArray = str.Split(separator);
            for (var i = 0; i < strArray.Length; ++i) list.Add(int.Parse(strArray[i]));

            return list;
        }

        public static List<string> SplitToStringList(this string str, char separator = '|')
        {
            var list = new List<string>();

            if (string.IsNullOrEmpty(str))
                return list;

            var strArray = str.Split(separator);
            for (var i = 0; i < strArray.Length; ++i) list.Add(strArray[i]);

            return list;
        }
        
        public static float[] SplitToFloatAry(this string str, char separator = '|')
        {
            if (string.IsNullOrEmpty(str))
                return new float[0];

            var strArray = str.Split(separator);
            float[] fValues=new float[strArray.Length];
            for (var i = 0; i < strArray.Length; ++i)
            {
                fValues[i]=float.Parse(strArray[i]);
            }

            return fValues;
        }
    }
}