using System.Collections.Generic;
using UnityEngine;

namespace KamenFramework
{
    public partial class GameHelper
    {
        public static bool IsPointInPolygon(Vector2 point, List<Vector2> polygon)
        {
            int polygonLength = polygon.Count, i = 0;
            bool inside = false;

            float pointX = point.x, pointY = point.y;
            float startX, startY, endX, endY;

            Vector2 endPoint = polygon[polygonLength - 1];
            endX = endPoint.x;
            endY = endPoint.y;

            while (i < polygonLength)
            {
                startX = endX;
                startY = endY;
                endPoint = polygon[i++];
                endX = endPoint.x;
                endY = endPoint.y;
                inside ^= (endY > pointY ^ startY > pointY) &&
                          ((pointX - endX) < (pointY - endY) * (startX - endX) / (startY - endY));
            }

            return inside;
        }

        public static bool IsPointInPolygon(Vector2 point, Vector2[] polygon)
        {
            int polygonLength = polygon.Length, i = 0;
            bool inside = false;

            float pointX = point.x, pointY = point.y;
            float startX, startY, endX, endY;

            Vector2 endPoint = polygon[polygonLength - 1];
            endX = endPoint.x;
            endY = endPoint.y;

            while (i < polygonLength)
            {
                startX = endX;
                startY = endY;
                endPoint = polygon[i++];
                endX = endPoint.x;
                endY = endPoint.y;
                inside ^= (endY > pointY ^ startY > pointY) &&
                          ((pointX - endX) < (pointY - endY) * (startX - endX) / (startY - endY));
            }

            return inside;
        }

        public static bool RayPlaneIntersection(out Vector3 intersection, Ray ray, Vector3 planeNormal, Vector3 planePoint)
        {
            return LinePlaneIntersection(out intersection, ray.origin, ray.direction, planeNormal, planePoint);
        }

        public static bool LinePlaneIntersection(out Vector3 intersection, Vector3 linePoint, Vector3 lineVec, Vector3 planeNormal, Vector3 planePoint)
        {

            float length;
            float dotNumerator;
            float dotDenominator;
            Vector3 vector;
            intersection = Vector3.zero;

            dotNumerator = Vector3.Dot((planePoint - linePoint), planeNormal);
            dotDenominator = Vector3.Dot(lineVec, planeNormal);

            if (dotDenominator != 0.0f)
            {
                length = dotNumerator / dotDenominator;

                vector = SetVectorLength(lineVec, length);

                intersection = linePoint + vector;

                return true;
            }
            else
            {
                return false;
            }
        }

        public static Vector3 SetVectorLength(Vector3 vector, float size)
        {
            Vector3 vectorNormalized = Vector3.Normalize(vector);

            return vectorNormalized *= size;
        }
    }
}