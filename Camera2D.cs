using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine {
    public class Camera2D : GameComponent {
        private Vector2 _position;
        protected float _viewportHeight;
        protected float _viewportWidth;
        private float _scale, targetScale;

        public Camera2D(Game game)
            : base(game) {
            Origin = Vector2.Zero;
            TargetScale = 1;
            Rotation = 0;
            Position = Vector2.Zero;
        }

        #region Properties

        public Vector2 Position {
            get { return _position; }
            set { _position = value; }
        }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        public float CurrentScale { get { return _scale; } }
        public float TargetScale { get { return targetScale; } set { if (value < 0.1f) targetScale = 0.1f; else targetScale = value; } }

        public Vector2 ScreenCenter { get; protected set; }
        public Matrix Transform { get; set; }
        public Vector2 Focus { get; set; }
        public float MoveSpeed { get; set; }

        #endregion

        /// <summary>
        /// Called when the GameComponent needs to be initialized. 
        /// </summary>
        public override void Initialize() {
            _viewportWidth = Game.GraphicsDevice.Viewport.Width;
            _viewportHeight = Game.GraphicsDevice.Viewport.Height;

            ScreenCenter = new Vector2(_viewportWidth / 2, _viewportHeight / 2);
            TargetScale = 1;
            MoveSpeed = 2f;

            base.Initialize();
        }

        public override void Update(GameTime gameTime) {
            // Create the Transform used by any
            // spritebatch process
            Transform = Matrix.Identity *
                        Matrix.CreateTranslation(-Position.X, -Position.Y, 0) *
                        Matrix.CreateRotationZ(Rotation) *
                        Matrix.CreateTranslation(Origin.X, Origin.Y, 0) *
                        Matrix.CreateScale(new Vector3(CurrentScale, CurrentScale, CurrentScale));



            // Move the Camera to the position that it needs to go
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _position.X += (Focus.X - Position.X) * MoveSpeed * delta;
            _position.Y += (Focus.Y - Position.Y) * MoveSpeed * delta;
            _scale += (targetScale - _scale) * 4f * delta;

            Origin = ScreenCenter / CurrentScale;

            //GetRelativeWorldMousePos(Vector2.Zero);
            base.Update(gameTime);
        }

        /// <summary>
        /// Determines whether the target is in view given the specified position.
        /// This can be used to increase performance by not drawing objects
        /// directly in the viewport
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="texture">The texture.</param>
        /// <returns>
        ///     <c>true</c> if [is in view] [the specified position]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInView(Vector2 position, Texture2D texture) {
            // If the object is not within the horizontal bounds of the screen

            if ((position.X + texture.Width) < (Position.X - Origin.X) || (position.X) > (Position.X + Origin.X))
                return false;

            // If the object is not within the vertical bounds of the screen
            if ((position.Y + texture.Height) < (Position.Y - Origin.Y) || (position.Y) > (Position.Y + Origin.Y))
                return false;

            // In View
            return true;
        }

        public Vector2 GetRelativeWorldMousePos(Vector2 mousePos) {
            Vector2 vec = Vector2.Transform(mousePos, Matrix.Invert(Transform));
            return new Vector2(vec.X, -vec.Y);
        }
    }
}
