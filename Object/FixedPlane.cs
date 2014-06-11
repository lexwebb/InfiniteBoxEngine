using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine.Object {
    public class FixedPlane : GameObject {
        public FixedPlane(World world, String name, Vector2 start, Vector2 end) :
            base(world, name, start, end) {
            this.Drawable = false;
            this.Body.CollisionCategories = Category.All;
        }
    }
}
