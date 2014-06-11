using InfiniteBoxEngine.Skeletal.Animation;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine.Animation.Skeletal
{
    /// <summary>
    /// Contains methods used to solve Inverse kinematic functions.
    /// </summary>
    public class KinematicSolver
    {
        /// <summary>
        /// Updates rotation of bones given so that the end BoneNode of bone two will be at the target location.
        /// </summary>
        /// <param name="one">Bone one.</param>
        /// <param name="two">Bone two.</param>
        /// <param name="endTarget">Target location of the edn BoneNode of bone two.</param>
        public static void ResolveTwoBoneJoint(Bone one, Bone two, Vector2 endTarget)
        {
            Triangle boneTriangle = new Triangle(one.Length, two.Length,
                (float)Math.Sqrt(Math.Pow(endTarget.X - one.RootNode.Position.X, 2) + Math.Pow(endTarget.Y - one.RootNode.Position.Y, 2)));

            one.EndNode.SetRotationWithoutUpdate(180 - boneTriangle.AngleB);

            float zeroPointRotation = one.RootNode.GetResolvedRotation(0);
            Vector2 vec = new Vector2(one.RootNode.Position.X + (float)Math.Sin(zeroPointRotation) * 10,
                one.RootNode.Position.Y + (float)Math.Cos(zeroPointRotation) * 10);

            Triangle offsetTriangle = new Triangle(one.RootNode.Position, vec, endTarget);
            one.RootNode.SetRotationWithoutUpdate(boneTriangle.AngleA - offsetTriangle.AngleA);
            two.UpdateBone();
        }
    }
}
