
using System.Collections.Generic;
using UnityEngine;

namespace KamenFramework
{
    public partial class GameHelper
    {
        public static GameObject GetRoot()
        {
            return GameObject.Find("KamenApp");
        }
        public static void AttachTo(GameObject go, GameObject parent)
        {
            if (parent == null)
            {
                KLogger.LogError("AttachTo parent is null");
                return;
            }

            if (go == null)
            {
                KLogger.LogError("AttachTo go is null");
                return;
            }

            var goRect = go.GetComponent<RectTransform>();
            var goTransform = go.transform;

            var offsetMin = Vector2.zero;
            var offsetMax = Vector2.zero;
            var anchorMax = Vector2.zero;
            var anchorMin = Vector2.zero;

            var transform = goTransform.transform;
            var lscale = transform.localScale;
            var lpos = transform.localPosition;
            var lrotation = transform.localRotation;

            if (goRect != null)
            {
                offsetMin = goRect.offsetMin;
                offsetMax = goRect.offsetMax;
                anchorMin = goRect.anchorMin;
                anchorMax = goRect.anchorMax;
            }

            go.transform.SetParent(parent.transform, true);

            goTransform.localScale = lscale;
            goTransform.localRotation = lrotation;
            goTransform.localPosition = lpos;

            if (goRect != null)
            {
                goRect.offsetMin = offsetMin;
                goRect.offsetMax = offsetMax;
                goRect.anchorMin = anchorMin;
                goRect.anchorMax = anchorMax;
            }
        }
    }
}