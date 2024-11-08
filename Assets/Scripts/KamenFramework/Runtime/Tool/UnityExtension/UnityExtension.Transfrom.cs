using UnityEngine;

namespace KamenFramework.Runtime.Tool.UnityExtension
{
    public static partial class UnityExtension
    {
        public static void SetScale(this RectTransform rt, float x, float y, float z)
        {
            rt.localScale = new Vector3(x, y, z);
        }
        public static void SetScale(this RectTransform rt, float width, float height)
        {
            rt.localScale = new Vector2(width, height);
        }
        public static void SetScale(this RectTransform rt, Vector2 value)
        {
            rt.localScale = value;
        }
        public static void SetScale(this RectTransform rt, Vector3 value)
        {
            rt.localScale = value;
        }
        
        public static void SetSize(this GameObject obj, Vector2 size)
        {
            RectTransform rt = obj.GetComponent<RectTransform>();
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
        }
        
        public static void OffsetY(this Transform trans, float offsetY)
        {
            if (trans != null)
            {
                var localPos = trans.localPosition;
                localPos.y = offsetY;
                trans.localPosition = localPos;
            }
        }
        public static Vector2 ToXz(this Vector3 thiz)
        {
            return new Vector2(thiz.x, thiz.z);
        }
        
        public static void OffsetX(this Transform trans, float offsetX)
        {
            if (trans != null)
            {
                var localPos = trans.localPosition;
                localPos.x = offsetX;
                trans.localPosition = localPos;
            }
        }
        
        public static void ResetTransform(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }
}