using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine.Animation
{
    /// <summary>
    /// Used for storing an objects position and rotation in radians.
    /// </summary>
    public struct Translation
    {
        Vector2 position;
        float rotation;

        public Translation(Vector2 position, float rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }

        /// <summary>
        /// Position of the object.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }        

        /// <summary>
        /// Roation of the object.
        /// </summary>
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
    }
}
