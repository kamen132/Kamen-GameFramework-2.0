using System;
using UnityEngine;

namespace KamenFramework
{
     /// <summary>
    /// 工具类 - 本地缓存 PlayerPrefs
    /// </summary>
    public static partial class GameHelper
    {
        /// <summary>
        /// 密钥
        /// </summary>
        public const string DecryptKey = "Kamen";

        /// <summary>
        /// 清除本地缓存
        /// </summary>
        public static void DeleteAllLocalData()
        {
            PlayerPrefs.DeleteAll();
        }

        /// <summary>
        /// 本地缓存 - string
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SaveStringData(string key, string value)
        {
            string aesKey = AES.AESEncrypt(key, DecryptKey);
            string aesValue = AES.AESEncrypt(value, DecryptKey);
            PlayerPrefs.SetString(aesKey, aesValue);
        }

        /// <summary>
        /// 本地缓存 - int
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetIntData(string key, int value)
        {
            string aesKey = AES.AESEncrypt(key, DecryptKey);
            PlayerPrefs.SetInt(aesKey, value);
        }

        /// <summary>
        /// 本地缓存 - float
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetFloatData(string key, float value)
        {
            string aesKey = AES.AESEncrypt(key, DecryptKey);
            PlayerPrefs.SetFloat(aesKey, value);
        }

        /// <summary>
        /// 本地缓存 - bool
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetBoolData(string key, bool value)
        {
            SetIntData(key, value ? 1 : 0);
        }

        /// <summary>
        /// 本地是否包含某个key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool HasLocalKey(string key)
        {
            string aesKey = AES.AESEncrypt(key, DecryptKey);
            return PlayerPrefs.HasKey(aesKey);
        }

        /// <summary>
        /// 获取本地数据 - string
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetStringLocalData(string key, string defaultValue = "")
        {
            string aesKey = AES.AESEncrypt(key, DecryptKey);
            string saveValue = PlayerPrefs.GetString(aesKey, defaultValue);
            if (string.IsNullOrEmpty(saveValue))
            {
                return defaultValue;
            }

            //解密之后内容
            string enAesValue = AES.AESEncrypt(saveValue, DecryptKey);
            return enAesValue;
        }

        /// <summary>
        /// 获取本地数据 - int
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetIntLocalData(string key, int defaultValue = 0)
        {
            string aesKey = AES.AESEncrypt(key, DecryptKey);
            if (PlayerPrefs.HasKey(aesKey))
            {
                return PlayerPrefs.GetInt(aesKey);
            }
            return defaultValue;
        }

        /// <summary>
        /// 获取本地数据 - float
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float GetFloatLocalData(string key, float defaultValue = 0f)
        {
            string aesKey = AES.AESEncrypt(key, DecryptKey);
            if (PlayerPrefs.HasKey(aesKey))
            {
                return PlayerPrefs.GetFloat(aesKey);
            }
            return defaultValue;
        }

        /// <summary>
        /// 获取本地数据 - bool
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool GetBoolLocalData(string key, bool defaultValue = false)
        {
            int value = GetIntLocalData(key, defaultValue ? 1 : 0);
            return Convert.ToBoolean(value);
        }
    }
}