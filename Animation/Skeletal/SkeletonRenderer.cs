using InfiniteBoxEngine.Graphics;
using InfiniteBoxEngine.Skeletal.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine.Animation.Skeletal
{
    public class SkeletonRenderer
    {
        Skeleton skeleton;
        Texture2D nodeTexture = EngineContentManager.GetXNAContent().Load<Texture2D>("Node");
        Texture2D boneTexture = EngineContentManager.GetXNAContent().Load<Texture2D>("Bone");

        public SkeletonRenderer(Skeleton skeleton)
        {
            this.skeleton = skeleton;
        }

        public void Draw(SpriteBatch sb)
        {
            //Draw PrimaryNode
            DrawNode(sb, skeleton.PrimaryNode.Position, skeleton.PrimaryNode.Rotation, Color.Pink);

            //Draw Bones
            foreach (Bone bone in skeleton.Bones.Values)
            {
                DrawNode(sb, bone.RootNode.Position, bone.RootNode.ResolvedRotation, Color.Blue);
                DrawBone(sb, bone.RootNode.Position, bone.EndNode.Position, bone.Length, bone.RootNode.ResolvedRotation, Color.LightGray);
                DrawNode(sb, bone.EndNode.Position, bone.RootNode.ResolvedRotation, Color.Cyan);
            }
            
        }

        public void DrawNode(SpriteBatch sb, Vector2 centerPosition, float rotation, Color color)
        {
            sb.Draw(nodeTexture, centerPosition * new Vector2(1, -1), null, color, rotation, new Vector2(nodeTexture.Width / 2, nodeTexture.Height / 2), 1f, SpriteEffects.None, 0f);
        }

        public void DrawBone(SpriteBatch sb, Vector2 originPosition, Vector2 endPosition, float length, float rotation, Color color)
        {
            //Center Line
            DrawUtilities.DrawLineNegativeY(sb, originPosition, endPosition, 1, color);
            //Left line
            Vector2 pos = originPosition - new Vector2(10, 0);
            Vector2 pos2;
            pos2.X = (float)Math.Cos(-rotation) * (pos.X - originPosition.X) - (float)Math.Sin(-rotation) * (pos.Y - originPosition.Y) + originPosition.X;
            pos2.Y = (float)Math.Sin(-rotation) * (pos.X - originPosition.X) + (float)Math.Cos(-rotation) * (pos.Y - originPosition.Y) + originPosition.Y;
            DrawUtilities.DrawLineNegativeY(sb, pos2, endPosition, 1, color);
            //Right line
            pos = originPosition + new Vector2(10, 0);
            pos2.X = (float)Math.Cos(-rotation) * (pos.X - originPosition.X) - (float)Math.Sin(-rotation) * (pos.Y - originPosition.Y) + originPosition.X;
            pos2.Y = (float)Math.Sin(-rotation) * (pos.X - originPosition.X) + (float)Math.Cos(-rotation) * (pos.Y - originPosition.Y) + originPosition.Y;
            DrawUtilities.DrawLineNegativeY(sb, pos2, endPosition, 1, color);
        }
    }
}
