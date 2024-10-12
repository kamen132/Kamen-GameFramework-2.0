using System.Globalization;
using KamenFramework.Runtime.Service;
using UnityEngine;

namespace KamenFramework
{
    public partial class GameHelper
    {
        public static Color ColorRed = Color.red;
        public static Color ColorGreen = Color.green;
        public static Color ColorBlue = Color.blue;
        
        /// <summary>
        /// 十六进制色码转颜色
        /// </summary>
        public static Color HexToColor(string hexStr)
        {
            if(string.IsNullOrEmpty(hexStr))
            {
                return Color.black;
            }

            int value = 0;
            if(!int.TryParse(hexStr, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out value))
            {
                return Color.black;
            }

            return RGBToColor(value);
        }

        /// <summary>
        /// rgb值转为颜色
        /// </summary>
        public static Color RGBToColor(int rgb, float alpha = 1f)
        {
            int b = (0xFF & rgb);
            int g = (0xFF & (rgb >> 8));
            int r = (0xFF & (rgb >> 16));
            return new Color(r / 255f, g / 255f, b / 255f, alpha);
        }

        /// <summary>
        /// 颜色转十六进制色码
        /// </summary>
        public static string ColorToHex(Color color)
        {
            int r = Mathf.RoundToInt(color.r * 255f);
            int g = Mathf.RoundToInt(color.g * 255f);
            int b = Mathf.RoundToInt(color.b * 255f);
            return Lang.Format("{0:X2}{1:X2}{2:X2}", r, g, b);
        }
    }
}