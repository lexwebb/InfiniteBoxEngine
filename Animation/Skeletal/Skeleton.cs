using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine.Skeletal.Animation {
    public class Skeleton {
        Dictionary<String, Bone> bones = new Dictionary<string, Bone>();
        BoneNode primaryNode;
        bool isKinematic;

        public Dictionary<String, Bone> Bones {
            get { return bones; }
            set { bones = value; }
        }

        public BoneNode PrimaryNode {
            get { return primaryNode; }
            set { primaryNode = value; }
        }

        public Skeleton(Vector2 position) {
            this.primaryNode = new BoneNode(null, position, 0);
        }

        public bool AddBone(BoneNode joinTo, String name, float angle, float length) {
            if (bones.ContainsKey(name)) {
                return false;
            }
            else if (bones.ContainsValue(joinTo.Parent)) {
                Bone bone = new Bone(this, joinTo, length, angle);
                bones.Add(name, bone);
                bone.UpdateBone();
                return true;
            }
            else if (joinTo == primaryNode) {
                Bone bone = new Bone(this, joinTo, length, angle);
                bones.Add(name, bone);
                bone.UpdateBone();
                return true;
            }
            else
                return false;
        }
    }
}
