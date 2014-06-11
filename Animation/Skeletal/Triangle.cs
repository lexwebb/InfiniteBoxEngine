using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine.Animation.Skeletal
{
    /// <summary>
    /// Triangle structure created to deal with resolving all edge lengths and angles given a certain input.
    /// </summary>
    struct Triangle
    {
        float sideA, sideB, sideC;
        float angleA, angleB, angleC;

        /// <summary>
        /// Creates a triangle.
        /// <remarks> May not be fully resolved if enough not information is passed.</remarks>
        /// </summary>
        /// <param name="sideA">One side of the triangle, adjacent to AngleA and AngleB.</param>
        /// <param name="sideB">One side of the triangle, adjacent to AngleB and AngleC.</param>
        /// <param name="sideC">One side of the triangle, adjacent to AngleC and AngleA.</param>
        /// <param name="angleA">One angle of the triangle, between SideC and SideA.</param>
        /// <param name="angleB">One angle of the triangle, between SideA and SideB.</param>
        /// <param name="angleC">One angle of the triangle, between SideB and SideC.</param>
        public Triangle(float sideA, float sideB, float sideC, float angleA, float angleB, float angleC)
        {
            this.sideA = sideA;
            this.sideB = sideB;
            this.sideC = sideC;
            this.angleA = angleA;
            this.angleB = angleB;
            this.angleC = angleC;
            ResolveTriangle();
        }

        /// <summary>
        /// Creates a triangle.
        /// <remarks> May not be fully resolved if enough not information is passed.</remarks>
        /// </summary>
        /// <param name="sideA">One side of the triangle, adjacent to AngleA and AngleB.s</param>
        /// <param name="sideB">One side of the triangle, adjacent to AngleB and AngleC.</param>
        /// <param name="sideC">One side of the triangle, adjacent to AngleC and AngleA.</param>
        public Triangle(float sideA, float sideB, float sideC)
        {
            this.sideA = sideA;
            this.sideB = sideB;
            this.sideC = sideC;
            this.angleA = 0;
            this.angleB = 0;
            this.angleC = 0;
            ResolveTriangle();
        }

        public Triangle(Vector2 pointA, Vector2 pointB, Vector2 pointC)
        {
            sideA = (float)Math.Sqrt(Math.Pow(pointB.X - pointA.X, 2) + Math.Pow(pointB.Y - pointA.Y, 2));
            sideB = (float)Math.Sqrt(Math.Pow(pointC.X - pointB.X, 2) + Math.Pow(pointC.Y - pointB.Y, 2));
            sideC = (float)Math.Sqrt(Math.Pow(pointA.X - pointC.X, 2) + Math.Pow(pointA.Y - pointC.Y, 2));
            angleA = 0;
            angleB = 0;
            angleC = 0;
            ResolveTriangle();
        }

        public void ResolveTriangle()
        {
            if (sideA != 0 && sideB != 0 && sideC != 0)
            {
                angleA = (float)Math.Acos((Sqr(sideB) + Sqr(sideC) - Sqr(sideA)) / (2 * sideB * sideC));
                angleB = (float)Math.Acos((Sqr(sideC) + Sqr(sideA) - Sqr(sideB)) / (2 * sideC * sideA));
                angleC = 180 - angleA - angleB;
            }
        }

        private double Sqr(double a)
        {
            return Math.Pow(a, 2);
        }

        public float SideA
        {
            get { return sideA; }
            set { sideA = value; }
        }

        public float SideB
        {
            get { return sideB; }
            set { sideB = value; }
        }

        public float SideC
        {
            get { return sideC; }
            set { sideC = value; }
        }

        public float AngleA
        {
            get { return angleA; }
            set { angleA = value; }
        }

        public float AngleB
        {
            get { return angleB; }
            set { angleB = value; }
        }

        public float AngleC
        {
            get { return angleC; }
            set { angleC = value; }
        }
    }
}
