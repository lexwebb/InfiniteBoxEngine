using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine.GUI.Controls {
    public class Panel : Control {
        public Panel(Vector2 position, int width, int height, Control alignedTo, Texture2D image, Color backgroundColor) :
            base(position, width, height, alignedTo, new Dictionary<string, Texture2D> { { "image", image } }) {
            this.BackgroundColor = backgroundColor;

            if (image == null) {
                Texture2D temp = new Texture2D(Engine.GetGraphicsDevice(), 1, 1);
                temp.SetData(new Color[] { Color.White });
                Images["image"] = temp;
            }
        }

        public override void OnRelease(Vector2 postion, MouseButton button) {

        }

        public override void OnClick(Vector2 pos, MouseButton button) {

        }

        public override void OnHold(Vector2 pos, MouseButton button) {

        }

        public override void OnHover(Vector2 pos) {

        }

        public override void OnUnHover(Vector2 pos) {

        }

        public override void DrawMiddleground(SpriteBatch sb, GameTime gt) {

        }

        public override void DrawBackground(SpriteBatch sb, GameTime gt) {
            if (Visible)
                sb.Draw(Images["image"], GetRelativeRectangle(), BackgroundColor * Transparency);
        }

        public override void DrawForeground(SpriteBatch sb, GameTime gt) {

        }
    }
}
