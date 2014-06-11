using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine.GUI.Controls
{
    public class Label : Control
    {
        SpriteFont font;
        string text;
        Color textColor;

        public Label(string text, SpriteFont font, Vector2 position, int width, int height, Control alignedTo) :
            base(position, width, height, alignedTo, null)
        {
            this.font = font;
            this.text = text;
            this.textColor = Color.White;
        }

        public override void DrawForeground(Microsoft.Xna.Framework.Graphics.SpriteBatch sb, GameTime gt)
        {
            sb.DrawString(font, text, this.Position, textColor);
        }

        public SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public Color TextColor
        {
            get { return textColor; }
            set { textColor = value; }
        }
    }
}
