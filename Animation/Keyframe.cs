using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine.Animation
{
    public class Keyframe
    {
        Vector2 position;
        float rotation;
        int frameTime;
        Texture2D texture;

        public Keyframe(int frameTime, Vector2 position, float rotation)
        {
            this.frameTime = frameTime;
            this.position = position;
            this.rotation = rotation;
        }

        public Keyframe(int frameTime, Vector2 position, float rotation, Texture2D texture)
        {
            this.frameTime = frameTime;
            this.position = position;
            this.rotation = rotation;
            this.texture = texture;
        }

        public int FrameTime
        {
            get { return frameTime; }
            set { frameTime = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }    

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
    }
}
