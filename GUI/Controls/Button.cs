using InfiniteBoxEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine.GUI.Controls {
    public class Button : Control {
        Texture2D currentImage;
        String text;
        SpriteFont font;
        Color textColor = Color.Black;
        Color color, heldColor, hoverColor, currentColor = Color.White;

        public Button(String name, String text, Vector2 position, int width, int height, Control alignedTo, Texture2D image, Texture2D hoverImage, Texture2D heldImage) :
            base(position, width, height, alignedTo,
                new Dictionary<string, Texture2D> { 
                {"image",image}, 
                {"hoverImage", hoverImage}, 
                {"heldImage", heldImage}}) {
            this.currentImage = Images["image"];
            this.font = EngineContentManager.GetXNAContent().Load<SpriteFont>("Visitor");
            this.text = text;
        }

        public Button(String name, String text, Vector2 position, int width, int height, Control alignedTo, Color color, Color hoverColor, Color heldColor) :
            base(position, width, height, alignedTo,
               new Dictionary<string, Texture2D> { 
                { "image", DrawUtilities.GetWhitePixelTexture() }, 
                { "hoverImage", DrawUtilities.GetWhitePixelTexture() }, 
                { "heldImage", DrawUtilities.GetWhitePixelTexture() } }) {
            this.currentImage = Images["image"];
            this.font = EngineContentManager.GetXNAContent().Load<SpriteFont>("Visitor");
            this.text = text;
            this.color = color;
            this.heldColor = heldColor;
            this.hoverColor = hoverColor;
            this.currentColor = color;
        }

        public override void OnClick(Vector2 position, MouseButton button) {
            this.currentImage = Images["heldImage"];
            this.currentColor = heldColor;
        }

        public override void OnRelease(Vector2 position, MouseButton button) {
            this.currentImage = Images["image"];
            this.currentColor = color;
        }

        public override void OnHover(Vector2 position) {
            this.currentImage = Images["hoverImage"];
            this.currentColor = hoverColor;
        }

        public override void OnUnHover(Vector2 pos) {
            this.currentImage = Images["image"];
            this.currentColor = color;
        }

        public override void OnHold(Vector2 pos, MouseButton button) {

        }

        public override void DrawBackground(SpriteBatch sb, GameTime gt) {

        }

        public override void DrawMiddleground(SpriteBatch sb, GameTime gt) {
            sb.Draw(currentImage, GetRelativeRectangle(), currentColor * Transparency);
        }

        public override void DrawForeground(SpriteBatch sb, GameTime gt) {
            sb.DrawString(font, text,
                new Vector2(this.GetRelativeDrawOffset().X + this.Width / 2 - font.MeasureString(text).X / 2,
                    this.GetRelativeDrawOffset().Y + this.Height / 2 - font.MeasureString(text).Y / 2), textColor);
        }

        public string Text {
            get { return text; }
            set { text = value; }
        }

        public Color TextColor {
            get { return textColor; }
            set { textColor = value; }
        }

        public SpriteFont Font {
            get { return font; }
            set { font = value; }
        }
    }
}
