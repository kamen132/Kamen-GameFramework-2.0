using UnityEngine;

namespace KamenFramework.Runtime.Tool.UnityExtension
{
    public partial class UnityExtension
    {
        public static Plane HorizontalPlane { get; private set; } = new Plane(Vector3.up, Vector3.zero);

        public static Vector3 GetCameraInteractWithGroundPoint(this Camera camera
            , Plane ground
            , Vector3 viewportPosition)
        {
            if (camera != null)
            {
                Ray ray = camera.ViewportPointToRay(viewportPosition);

                var origin = ray.origin;
                origin.y = Mathf.Max(0.1f, origin.y);
                ray.origin = origin;
                
                if (ground.Raycast(ray, out var hit))
                {
                    return ray.origin + hit * ray.direction;
                }
            }

            return Vector3.zero;
        }

        public static Vector3 GetHorizontalGroundPoint(this Camera camera, float x, float y)
        {
            return camera.GetCameraInteractWithGroundPoint(HorizontalPlane, new Vector3(x, y, 0));
        }

        public static bool GetGroundPosition(Ray ray, Plane ground, out Vector3 position)
        {
            position = Vector3.zero;
            float enter;
            if (ground.Raycast(ray, out enter))
            {
                position = ray.GetPoint(enter);
                return true;
            }
            return false;
        }
    }
}