using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine.Skeletal.Animation {
    public class Bone {
        Skeleton belongsTo;
        BoneNode joinedTo;
        BoneNode rootNode, endNode;
        float length, angle;

        public Bone(Skeleton belongsTo, BoneNode joinTo, float length, float rotation) {
            this.belongsTo = belongsTo;
            this.joinedTo = joinTo;
            this.length = length;
            this.rootNode = new BoneNode(this, joinedTo.Position, rotation);
            this.endNode = new BoneNode(this, new Vector2((float)Math.Sin(angle) * length, (float)Math.Cos(angle) * length), 0);
        }

        public BoneNode JoinedTo {
            get { return joinedTo; }
            set { joinedTo = value; }
        }

        public BoneNode EndNode {
            get { return endNode; }
            set { endNode = value; }
        }

        public BoneNode RootNode {
            get { return rootNode; }
            set { rootNode = value; }
        }

        public float Angle {
            get { return angle; }
            set { angle = value; UpdateBone(); }
        }

        public float Length {
            get { return length; }
            set { length = value; UpdateBone(); }
        }

        public void UpdateBone() {
            this.rootNode.Position = joinedTo.Position;
            this.rootNode.ResolvedRotation = this.rootNode.GetResolvedRotation(this.rootNode.Rotation);
            this.endNode.Position = new Vector2(this.rootNode.Position.X + (float)Math.Sin(rootNode.ResolvedRotation + joinedTo.ResolvedRotation) * length,
                this.rootNode.Position.Y + (float)Math.Cos(rootNode.ResolvedRotation + joinedTo.ResolvedRotation) * length);
            this.EndNode.SetRotationWithoutUpdate(this.rootNode.Rotation);

            foreach (Bone childBone in belongsTo.Bones.Values) {
                if (childBone.JoinedTo == this.endNode)
                    childBone.UpdateBone();
            }
        }

        public void MoveNodeKinematic(BoneNode node, Vector2 position) {

        }
    }
}
