using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine.Graphics
{
    public class GeometryHelper
    {
        public static float AreaOfTriangle(Vector2 pointA, Vector2 pointB, Vector2 pointC)
        {
            float a = Vector2.Distance(pointA, pointB);
            float b = Vector2.Distance(pointB, pointC);
            float c = Vector2.Distance(pointC, pointA);

            float s = (a + b + c) / 2;
            return (float)Math.Sqrt(s*(s - a)*(s - b)*(s - c));
        }

        public static bool IsPointInRectangle(Vector2 target, Vector2 pointA, Vector2 pointB, Vector2 pointC, Vector2 pointD)
        {
            float recArea = Vector2.Distance(pointA, pointB) * Vector2.Distance(pointD, pointA);

            float a = AreaOfTriangle(pointA, pointB, target);
            float b = AreaOfTriangle(pointB, pointC, target);
            float c = AreaOfTriangle(pointC, pointD, target);
            float d = AreaOfTriangle(pointD, pointA, target);

            float triAreas = a + b + c + d;

            if (triAreas > recArea)
                return false;
            else
                return true;
        }

        public static Vector2 RotateVectorAboutPoint(Vector2 vector, Vector2 rotateAround, float angle)
        {
            double x = ((vector.X - rotateAround.X) * Math.Cos(angle)) - ((rotateAround.Y - vector.Y) * Math.Sin(angle)) + rotateAround.X;
            double y = ((rotateAround.Y - vector.Y) * Math.Cos(angle)) - ((vector.X - rotateAround.X) * Math.Sin(angle)) + rotateAround.Y;

            return new Vector2((float)x, (float)y);
        }
    }
}
