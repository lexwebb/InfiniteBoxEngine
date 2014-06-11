using InfiniteBoxEngine.Abstracts;
using InfiniteBoxEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine.GUI.Controls {
    abstract public class Control : Clickable {
        Dictionary<String, Texture2D> images = new Dictionary<string, Texture2D>();

        bool visible, usable;

        Control alignedTo;
        Alignment alignment;
        float yOffset, xOffset;
        Color backgroundColor, borderColor = Color.White;
        bool showBorder;
        bool screenAligned;
        bool canBeFocus, isFocused;
        float transparency;

        public Control(Vector2 position, int width, int height, Control alignedTo, Dictionary<String, Texture2D> images) :
            base(position, width, height) {
            this.images = images;
            this.Visible = true;
            this.Usable = true;
            this.alignedTo = alignedTo;
            this.transparency = 1;

            RecalculateClickArea();
        }

        public virtual void DrawBackground(SpriteBatch sb, GameTime gt) { }

        public virtual void DrawMiddleground(SpriteBatch sb, GameTime gt) { }

        public virtual void DrawForeground(SpriteBatch sb, GameTime gt) { }

        public virtual void DrawHighlights(SpriteBatch sb, GameTime gt) {
            if (ShowBorder)
                DrawUtilities.DrawControlBorder(sb, this.GetClickableArea(), borderColor);
        }

        public virtual void OnFocusedChanged(bool isFocused) { }

        public Rectangle GetRelativeRectangle() {
            RecalculateClickArea();
            return new Rectangle(GetClickableArea().X, GetClickableArea().Y, Width, Height);
        }

        public Vector2 GetRelativeDrawOffset() {
            Vector2 panelOffset = Vector2.Zero;

            if (alignedTo is Panel) {
                //panelOffset = new Vector2(alignedTo.Width, alignedTo.Height);
                return new Vector2(alignedTo.Position.X + XOffset, alignedTo.Position.Y + YOffset);
            }

            if (alignedTo == null)
                return Vector2.Zero + panelOffset;
            else {
                switch (alignment) {
                    case Alignment.Above:
                        return new Vector2(alignedTo.Position.X + XOffset, alignedTo.Position.Y + YOffset - Height);
                    case Alignment.Below:
                        return new Vector2(alignedTo.Position.X + XOffset, alignedTo.Position.Y + YOffset + alignedTo.Height - panelOffset.Y);
                    case Alignment.Right:
                        return new Vector2(alignedTo.Position.X + XOffset + alignedTo.Width - panelOffset.X, alignedTo.Position.Y + YOffset);
                    case Alignment.Left:
                        return new Vector2(alignedTo.Position.X + XOffset - Width, alignedTo.Position.Y + YOffset);
                    case Alignment.AboveRight:
                        return new Vector2(alignedTo.Position.X + XOffset + alignedTo.Width - panelOffset.X, alignedTo.Position.Y + YOffset - Height);
                    case Alignment.AboveLeft:
                        return new Vector2(alignedTo.Position.X + XOffset - Width, alignedTo.Position.Y + YOffset - Height);
                    case Alignment.BelowRight:
                        return new Vector2(alignedTo.Position.X + XOffset + alignedTo.Width - alignedTo.XOffset, alignedTo.Position.Y + YOffset + alignedTo.Height - panelOffset.Y);
                    case Alignment.BelowLeft:
                        return new Vector2(alignedTo.Position.X + XOffset - Width, alignedTo.Position.Y + YOffset + alignedTo.Height - panelOffset.Y);
                    default:
                        return Vector2.Zero + panelOffset;
                }
            }
        }

        public void RecalculateClickArea() {
            if (alignedTo != null)
                Position = GetRelativeDrawOffset();
            else if (alignment != null) {
                int width = Engine.GetGraphicsDevice().Viewport.Width, height = Engine.GetGraphicsDevice().Viewport.Height;

                switch (alignment) {
                    case Alignment.Above:
                        Position = new Vector2(width / 2 + XOffset, YOffset); break;
                    case Alignment.Below:
                        Position = new Vector2(width / 2 + XOffset, height - Height + YOffset); break;
                    case Alignment.Right:
                        Position = new Vector2(width - Width + XOffset, height / 2 + YOffset); break;
                    case Alignment.Left:
                        Position = new Vector2(XOffset, height / 2 + YOffset); break;
                    case Alignment.AboveRight:
                        Position = new Vector2(width - Width + XOffset, YOffset); break;
                    case Alignment.AboveLeft:
                        Position = new Vector2(XOffset, YOffset); break;
                    case Alignment.BelowRight:
                        Position = new Vector2(width - Width + XOffset, height - Height + YOffset); break;
                    case Alignment.BelowLeft:
                        Position = new Vector2(XOffset, height - Height + YOffset); break;
                    case Alignment.Center:
                        Position = new Vector2(width / 2 + XOffset, height / 2 + YOffset); break;
                    default:
                        Position = Position; break;
                }
            }
        }

        public bool Usable {
            get { return usable; }
            set { usable = value; }
        }

        public bool Visible {
            get { return visible; }
            set { visible = value; }
        }

        public Control AlignedTo {
            get { return alignedTo; }
            set { alignedTo = value; }
        }

        public Alignment Alignment {
            get { return alignment; }
            set { alignment = value; }
        }

        public float XOffset {
            get { return xOffset; }
            set { xOffset = value; }
        }

        public float YOffset {
            get { return yOffset; }
            set { yOffset = value; }
        }

        public Color BackgroundColor {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }

        public bool ShowBorder {
            get { return showBorder; }
            set { showBorder = value; }
        }

        public Color BorderColor {
            get { return borderColor; }
            set { borderColor = value; }
        }

        public float Transparency {
            get { return transparency; }
            set { transparency = value; }
        }

        public Dictionary<String, Texture2D> Images {
            get { return images; }
            set { images = value; }
        }

        public bool ScreenAligned {
            get { return screenAligned; }
            set { screenAligned = value; }
        }

        public bool CanBeFocused {
            get { return canBeFocus; }
            set { canBeFocus = value; }
        }

        public bool IsFocused {
            get { return isFocused; }
            set { isFocused = value; OnFocusedChanged(value); }
        }
    }
}
