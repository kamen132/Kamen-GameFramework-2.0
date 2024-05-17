using System;

namespace KamenFramework.Runtime.Tool
{
       /// <summary>
    /// 工具类 - 时间 Timer
    /// </summary>
    public partial class GameHelper
    {
        public const int ONE_DAY = 86400;
        public const int ONE_HOUR = 3600;
        public const int ONE_MINUTE = 60;
        private static readonly DateTime StartTime= new DateTime(1970, 1, 1);

        /// <summary>
        /// 2023-03-30 13:23
        /// </summary>
        public static string GetCurGameSampleTimeString()
        {
            return GetGameSampleTimeString(GetLocalTimestampInSec());
        }

        /// <summary>
        /// 2024-03-30 14:55:00:002
        /// </summary>
        public static string GetGameSampleMillTimeString()
        {
            return GetGameSampleDefaultTime(GetLocalTimestampInMilliSec());
        }
        
        /// <summary>
        /// 秒级时间戳
        /// </summary>
        /// <returns></returns>
        public static int GetLocalTimestampInSec()
        { 
            int timestamp = (int)Math.Floor((DateTime.UtcNow - StartTime).TotalSeconds);
            return timestamp;
        }

        /// <summary>
        /// 毫秒级时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetLocalTimestampInMilliSec()
        {
            long timestamp = (long)Math.Floor((DateTime.UtcNow - StartTime).TotalMilliseconds);
            return timestamp;
        }
        
        /// <summary>
        /// 时间戳 转 DateTime
        /// </summary>
        public static DateTime Timestamp2DateTime(int timestamp)
        {
            DateTime time = StartTime.AddSeconds(timestamp);
            return time;
        }

        /// <summary>
        /// 2023-03-30 13:23
        /// </summary>
        public static string GetGameSampleTimeString(int timestamp)
        {
            var dateTime = Timestamp2DateTime(timestamp);
            return GetGameSampleTimeString(dateTime);
        }
        /// <summary>
        /// 2023-03-30 13:23
        /// </summary>
        public static string GetGameSampleTimeString(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm");
        }

        /// <summary>
        /// 03/30
        /// </summary>
        public static string GetGameSampleTimeMMDDString(int timestamp)
        {
            var dateTime = Timestamp2DateTime(timestamp);
            return dateTime.ToString("M/d");
        }
        
        
        /// <summary>
        /// 2024/3/30
        /// </summary>
        public static string GetGameSampleTimeHMDtring(int timestamp)
        {
            var dateTime = Timestamp2DateTime(timestamp);
            return GetGameSampleTimeHMDtring(dateTime);
        }
        
        /// <summary>
        /// 2024/3/30
        /// </summary>
        public static string GetGameSampleTimeHMDtring(DateTime dateTime)
        {
            return dateTime.ToString("yyyy/MM/dd");
        }
        
        /// <summary>
        /// 2024-03-30 14:55:00
        /// </summary>
        public static string GetGameSampleTimeHHMMSSString(int timestamp)
        {
            var dateTime = Timestamp2DateTime(timestamp);
            return GetGameSampleTimeHHMMSSString(dateTime);
        }
        /// <summary>
        /// 2024-03-30 14:55:00
        /// </summary>
        public static string GetGameSampleTimeHHMMSSString(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }


        /// <summary>
        /// 2024-03-30 14:55:00:002
        /// </summary>
        public static string GetGameSampleDefaultTime(long millseconds)
        {
            DateTime dateTime = StartTime.AddMilliseconds(millseconds);
            return dateTime.ToString( "yyyy-MM-dd HH:mm:ss:fff");
        }
    }
}