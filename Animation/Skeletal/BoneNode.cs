using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine.Skeletal.Animation
{
    public class BoneNode
    {
        float rotation, resolvedRotation;
        float antiClockConstraint, clockConstraint;
        Bone parent;
        Vector2 position;

        public BoneNode(Bone parent, Vector2 position, float rotation)
        {
            this.parent = parent;
            this.position = position;
            this.rotation = rotation;
        }

        public Bone Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; if(parent != null) parent.UpdateBone(); }
        }

        public float ResolvedRotation
        {
            get { return resolvedRotation; }
            set { resolvedRotation = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float AntiClockConstraint
        {
            get { return antiClockConstraint; }
            set { antiClockConstraint = value; }
        }

        public float ClockConstraint
        {
            get { return clockConstraint; }
            set { clockConstraint = value; }
        }

        public void SetRotationWithoutUpdate(float rotation)
        {
            this.rotation = rotation;
        }

        public float GetResolvedRotation(float offset)
        {
            if (parent != null)
                return this.rotation + parent.JoinedTo.GetResolvedRotation(this.rotation);
            else
                return offset;
        }
    }
}
