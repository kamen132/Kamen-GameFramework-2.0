using System;
using System.Diagnostics;
using KamenFramework.Runtime.Service;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace KamenFramework.Runtime.Tool.Log
{
   public static class KLogger
    {
        public static bool useLog = true;
        [Conditional("DEBUG"), Conditional("UNITY_EDITOR")]
        public static void Log(string log)
        {
            try
            {
                if (useLog)
                {
                    Debug.Log(GetLogFormat(log));
                }
            }
            catch (Exception)
            {
                //do nothing
            }
        }

        [Conditional("DEBUG"), Conditional("UNITY_EDITOR")]
        public static void Log(object message)
        {
            Log(message.ToString());
        }

        [Conditional("DEBUG"), Conditional("UNITY_EDITOR")]
        public static void Log(string format, object arg0)
        {
            Log(Lang.Format(format, arg0));
        }

        [Conditional("DEBUG"), Conditional("UNITY_EDITOR")]
        public static void Log(string format, Color color)
        {
            Log(Lang.Format("<b><color=#{0:X2}{1:X2}{2:X2}>{3}</color></b>\n", (byte) (color.r * 255), (byte) (color.g * 255), (byte) (color.b * 255), format));
        }

        [Conditional("DEBUG"), Conditional("UNITY_EDITOR")]
        public static void Log(string format, object arg0, object arg1)
        {
            Log(Lang.Format(format, arg0, arg1));
        }

        [Conditional("DEBUG"), Conditional("UNITY_EDITOR")]
        public static void Log(string format, object arg0, object arg1, object arg2)
        {
            Log(Lang.Format(format, arg0, arg1, arg2));
        }

        [Conditional("DEBUG"), Conditional("UNITY_EDITOR")]
        public static void Log(string format, params object[] param)
        {
            Log(Lang.Format(format, param));
        }
        public static void LogError(string log)
        {
            if (useLog)
            {
                Debug.LogError(GetLogFormat(log));
            }
        }

        static string GetLogFormat(string log)
        {
            log = $"[{GameHelper.GetGameSampleMillTimeString()}]=>{log}";
            return log;
        }

        public static void LogError(object message)
        {
            LogError(message.ToString());
        }

        public static void LogError(string format, object arg0)
        {
            LogError(Lang.Format(format, arg0));
        }

        public static void LogError(string format, object arg0, object arg1)
        {
            LogError(Lang.Format(format, arg0, arg1));
        }
    }
}