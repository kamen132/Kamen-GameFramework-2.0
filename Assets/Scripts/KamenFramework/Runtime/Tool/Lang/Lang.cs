using System;


namespace KamenFramework.Runtime.Service
{
    public class Lang
    {
        /// <summary>
        /// 字符串拼接
        /// </summary>
        /// <param name="format"></param>
        /// <param name="paramArray"></param>
        /// <returns></returns>
        public static string Format(string format, params object[] paramArray)
        {
            try
            {
                return string.Format(format, paramArray);
            }
            catch (Exception ex)
            {
                KLogger.LogError($"string format error : {ex}");
            }

            return format;
        }
    }
}