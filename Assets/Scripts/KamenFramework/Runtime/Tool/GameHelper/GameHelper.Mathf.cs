using UnityEngine;

namespace KamenFramework
{
    public partial class GameHelper
    {
        public static float ConvertVector2ToAngle(Vector2 v2)
        {
            return Mathf.Atan2(v2.y, v2.x) * 57.29578f;
        }
        
        public static Vector2 DirectionIn2D(Vector3 from, Vector3 to)
        {
            Vector2 vector = new Vector2(from.x, from.y);
            return (new Vector2(to.x, to.y) - vector).normalized;
        }

        public static Vector3 ConvertV2ToV3(Vector2 v2)
        {
            return new Vector3(v2.x, 0f, v2.y);
        }

        public static Vector2 ConvertV3ToV2(Vector3 v3)
        {
            return new Vector2(v3.x, v3.z);
        }
    }
}