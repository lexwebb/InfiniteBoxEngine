using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine.Graphics
{
    public class DrawUtilities
    {
        static Texture2D texture;
        static Texture2D simpleTexture;

        public void DrawCircle(SpriteBatch sb, Vector2 position, int radius, Color color)
        {
            int outerRadius = radius * 2 + 2; // So circle doesn't go out of bounds
            texture = new Texture2D(Engine.GetGraphicsDevice(), outerRadius, outerRadius);

            Color[] data = new Color[outerRadius * outerRadius];

            // Colour the entire texture transparent first.
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.Transparent;

            // Work out the minimum step necessary using trigonometry + sine approximation.
            double angleStep = 1f / radius;

            for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
            {
                int x = (int)Math.Round(radius + radius * Math.Cos(angle));
                int y = (int)Math.Round(radius + radius * Math.Sin(angle));

                data[y * outerRadius + x + 1] = Color.White;
            }

            texture.SetData(data);

            sb.Draw(texture, position - new Vector2(2, 0), color);
        }

        public static void DrawLineNegativeY(SpriteBatch sb, Vector2 origin, Vector2 end, int thickness, Color color)
        {
            Vector2 edge = end - origin;
            float angle = (float)Math.Atan2(-edge.Y, edge.X);

            if (simpleTexture == null)
                simpleTexture = new Texture2D(Engine.GetGraphicsDevice(), 1, 1, false, SurfaceFormat.Color);
            simpleTexture.SetData<Color>(new Color[] { Color.White });
            Rectangle temp = new Rectangle((int)origin.X, (int)-origin.Y, (int)edge.Length(), 1);
            sb.Draw(simpleTexture, temp,
               null, color, angle, new Vector2(0, 0), SpriteEffects.None, 0);
        }

        public static void DrawLinePositiveY(SpriteBatch sb, Vector2 origin, Vector2 end, int thickness, Color color)
        {
            Vector2 edge = end - origin;
            float angle = (float)Math.Atan2(-edge.Y, edge.X);

            if(simpleTexture == null)
                simpleTexture = new Texture2D(Engine.GetGraphicsDevice(), 1, 1, false, SurfaceFormat.Color);
            simpleTexture.SetData<Color>(new Color[] { Color.White });
            Rectangle temp = new Rectangle((int)origin.X, (int)origin.Y, (int)edge.Length(), 1);
            sb.Draw(simpleTexture, temp,
               null, color, angle, new Vector2(0, 0), SpriteEffects.None, 0);
        }

        public static Texture2D GetColoredImage(Color color)
        {
            texture = new Texture2D(Engine.GetGraphicsDevice(), 1, 1);
            texture.SetData(new Color[] { color });
            return texture;
        }

        public static Texture2D GetWhitePixelTexture()
        {
            texture = new Texture2D(Engine.GetGraphicsDevice(), 1, 1);
            texture.SetData(new Color[] { Color.White });
            return texture;
        }

        public static void DrawControlBorder(SpriteBatch sb, Rectangle rectangle, Color color)
        {
            DrawLinePositiveY(sb, new Vector2(rectangle.X, rectangle.Y), new Vector2(rectangle.X + rectangle.Width, rectangle.Y), 1, color);
            DrawLinePositiveY(sb, new Vector2(rectangle.X + rectangle.Width, rectangle.Y), new Vector2(rectangle.X + rectangle.Width, rectangle.Y - rectangle.Height), 1, color);
            DrawLinePositiveY(sb, new Vector2(rectangle.X, rectangle.Y), new Vector2(rectangle.X, rectangle.Y - rectangle.Height), 1, color);
            DrawLinePositiveY(sb, new Vector2(rectangle.X, rectangle.Y + rectangle.Height), new Vector2(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height), 1, color);
        }

        public static void DrawGameObjBorder(SpriteBatch sb, Rectangle rectangle, Color color)
        {
            DrawLineNegativeY(sb, new Vector2(rectangle.X - 1, rectangle.Y), new Vector2(rectangle.X + rectangle.Width + 1, rectangle.Y), 1, color);
            DrawLineNegativeY(sb, new Vector2(rectangle.X + rectangle.Width, rectangle.Y), new Vector2(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height + 1), 1, color);
            DrawLineNegativeY(sb, new Vector2(rectangle.X - 1, rectangle.Y), new Vector2(rectangle.X - 1, rectangle.Y + rectangle.Height + 1), 1, color);
            DrawLineNegativeY(sb, new Vector2(rectangle.X, rectangle.Y + rectangle.Height + 1), new Vector2(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height + 1), 1, color);
        }
    }
}
