using InfiniteBoxEngine.Abstracts;
using InfiniteBoxEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine.GUI.Controls
{
    public class TextBox : Control
    {
        Typeable typeable;
        Color textColor =  Color.DimGray;
        bool flicker;
        int prevFlickerTime;

        public TextBox(Vector2 position, int width, int height, string text, SpriteFont font, Control alignedTo,  Texture2D image) :
            base(position, width, height, alignedTo, new Dictionary<string, Texture2D> { {"image", image} })
        {
            typeable = new Typeable(text, font, position);
            this.CanBeFocused = true;
        }

        public TextBox(Vector2 position, int width, int height, string text, SpriteFont font, Control alignedTo, Color color) :
            base(position, width, height, alignedTo, new Dictionary<string, Texture2D> { { "image", DrawUtilities.GetColoredImage(color)}})
        {
            typeable = new Typeable(text, font, position);
            this.CanBeFocused = true;
        }

        public override void OnClick(Vector2 pos, MouseButton button)
        {
            for (int i = 0; i < typeable.Text.Length + 1; i++)
            {
                float offset = this.Position.X + typeable.Font.MeasureString(typeable.Text.Substring(0, i)).X;
                if (pos.X < offset)
                {
                    typeable.CursorPosition = i;
                    break;
                }
                    
            }
        }

        public override void DrawMiddleground(SpriteBatch sb, GameTime gt)
        {
            sb.Draw(this.Images["image"], this.GetRelativeRectangle(), Color.White);
        }

        public override void DrawForeground(SpriteBatch sb, GameTime gt)
        {
            sb.DrawString(typeable.Font, typeable.Text, this.GetRelativeDrawOffset(), textColor);           
        }

        public override void DrawHighlights(SpriteBatch sb, GameTime gt)
        {
            if (gt.TotalGameTime.Seconds > prevFlickerTime)
            {
                flicker = !flicker;
                prevFlickerTime = gt.TotalGameTime.Seconds;
            }
                

            if (IsFocused && flicker)
            {
                Vector2 drawVec = new Vector2(this.GetRelativeDrawOffset().X + typeable.Font.MeasureString(typeable.Text.Substring(0, typeable.CursorPosition)).X, this.GetRelativeDrawOffset().Y);

                DrawUtilities.DrawLinePositiveY(sb, drawVec, drawVec + new Vector2(0, -this.Height), 1, Color.White);
            }
        }

        public override void OnFocusedChanged(bool isFocused)
        {
            typeable.Usable = isFocused;
        }

        public Color TextColor
        {
            get { return textColor; }
            set { textColor = value; }
        }

        public string Text
        {
            get { return typeable.Text; }
            set { typeable.Text = value; }
        }
    }
}
