using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine.GUI.Controls
{
    public class Checkbox : Control
    {
        Texture2D currentImage;
        bool isChecked;

        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; }
        }

        public Checkbox(String name, Vector2 position, int width, int height, Control alignedTo, Texture2D uncheckedImage, Texture2D checkedImage) :
            base(position, width, height, alignedTo, new Dictionary<string,Texture2D> {{"uncheckedImage", uncheckedImage}, {"checkedImage", checkedImage}})
        {
            this.currentImage = Images["uncheckedImage"];
        }

        public override void OnClick(Vector2 pos, MouseButton button)
        {
            if (button == MouseButton.Left)
            {
                if (isChecked)
                {
                    this.isChecked = false;
                    this.currentImage = Images["uncheckedImage"];
                }
                else
                {
                    this.isChecked = true;
                    this.currentImage = Images["checkedImage"];
                }
            }
        }

        public override void OnHold(Vector2 pos, MouseButton button)
        {
            
        }

        public override void OnHover(Vector2 pos)
        {
            
        }

        public override void OnRelease(Vector2 pos, MouseButton button)
        {
            
        }

        public override void OnUnHover(Vector2 pos)
        {
            
        }

        public override void DrawBackground(SpriteBatch sb, GameTime gt)
        {

        }

        public override void DrawMiddleground(SpriteBatch sb, GameTime gt)
        {
            sb.Draw(currentImage, GetRelativeRectangle(), Color.White);
        }

        public override void DrawForeground(SpriteBatch sb, GameTime gt)
        {
            
        }
    }
}
