using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace KamenFramework.Runtime.Tool.UnityExtension
{
    public static partial class UnityExtension
    {
        /// <summary>
        /// 按列拆分列表
        /// </summary>
        public static List<T[]> ListSplitByColumn<T>(this List<T> list, int column)
        {
            int listCount = list.Count;
            int rowCount = Mathf.CeilToInt(listCount / (float) column);

            List<T[]> resultList = new List<T[]>(rowCount);

            for (int i = 0; i < listCount; i += column)
            {
                T[] row = new T[column];

                for (int c = 0; c < column; c++)
                {
                    int index = i + c;

                    if (index >= listCount)
                    {
                        break;
                    }

                    row[c] = list[index];
                }

                resultList.Add(row);
            }

            return resultList;
        }

        /// <summary>
        /// 元素按照指定字符进行拼接
        /// </summary>
        public static string ListToStringWithSplit<T>(this IReadOnlyList<T> list, char splitChar)
        {
            if (list.Count == 0)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();
            var count = list.Count;
            for (int i = 0; i < count; i++)
            {
                T data = list[i];
                sb.Append(data);
                if (i != count - 1)
                {
                    sb.Append(splitChar);
                }
            }

            return sb.ToString();
        }

        public static bool IsEmpty<T>(this IList<T> objects)
        {
            if (objects == null)
                return true;

            for (int i = objects.Count - 1; i >= 0; i--)
            {
                if (!objects[i].IsNull())
                    return false;
            }

            return true;
        }
    }
}