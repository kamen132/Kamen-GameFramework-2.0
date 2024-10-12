using System;
using System.Collections.Generic;
using System.Reflection;

namespace KamenFramework.Runtime.Tool.UnityExtension
{
    public static partial class UnityExtension
    {
        /// <summary>
        /// 获取枚举名称列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回以字符串为关键字的映射表</returns>
        public static List<string> GetEnumNames<T>()
        {
            Type type = typeof(T);
            if (!type.IsEnum)
            {
                return null;
            }
            List<string> enumNames = new List<string>();
            FieldInfo[] enumFields = type.GetFields();
            int uLength = enumFields.Length;
            for (int i = 0; i < uLength; i++)
            {
                FieldInfo fieldInfo = enumFields[i];
                if (fieldInfo.IsSpecialName)
                {
                    continue;
                }
                enumNames.Add(fieldInfo.Name);
            }
            return enumNames;
        }

        /// <summary>
        /// 获取枚举成员数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>枚举成员数量</returns>
        public static int GetEnumCount<T>()
        {
            int nCount = 0;
            Type type = typeof(T);
            if (type.IsEnum)
            {
                nCount = type.GetFields().Length - 1;
            }
            return nCount;
        }

        /// <summary>
        /// 通过名称获取指定枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strName">枚举字符串形式名称</param>
        /// <returns>通过名称获取指定枚举</returns>
        public static T GetEnumValue<T>(string strName)
        {
            T tValue = default(T);
            Type type = typeof(T);
            if (type.IsEnum)
            {
                FieldInfo[] enumFields = type.GetFields();
                foreach (FieldInfo fi in enumFields)
                {
                    if (fi.IsSpecialName)
                    {
                        continue;
                    }
                    if (fi.Name == strName)
                    {
                        tValue = (T)fi.GetRawConstantValue();
                    }
                }
            }
            return tValue;
        }

        /// <summary>
        /// 通过值获取指定枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nValue">枚举值</param>
        /// <returns>枚举</returns>
        public static T GetEnumValue<T>(int nValue)
        {
            T tValue = default(T);
            Type type = typeof(T);
            if (type.IsEnum)
            {
                FieldInfo[] enumFields = type.GetFields();
                foreach (FieldInfo fi in enumFields)
                {
                    if (fi.IsSpecialName)
                    {
                        continue;
                    }
                    object v = fi.GetRawConstantValue();
                    if (int.Parse(v.ToString()) == nValue)
                    {
                        tValue = (T)v;
                    }
                }
            }
            return tValue;
        }
        
        public static bool IsDefinedEnum<T>(T e)
        {
            return Enum.GetName(typeof(T), e) != null;
        }
    }
}