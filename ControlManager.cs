using InfiniteBoxEngine.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine
{
    /// <summary>
    /// The ControlManager is where all game control is calculated and retrieved from. Includes custom control patterns extended from MouseState and KeyboardState. 
    /// </summary>
    public class ControlManager
    {
        MouseState mouseState;
        KeyboardState keyboardState;
        Vector2 mousePosition;

        bool isLeftClicked, isLeftHeld, isLeftReleased;
        int leftMouseCounter;
        bool isRightClicked, isRightHeld, isRightReleased;
        int rightMouseCounter;
        bool isMiddleClicked, isMiddleHeld, isMiddleReleased;
        int middleMouseCounter;
        int scrollValue, oldScrollValue, scrollAmount;

        bool isControlFocused;

        public event EventHandler<KeyPressEventArgs> KeyPressEvent;

        /// <summary>
        /// Initializes the ControlManager, sets the initial states.
        /// </summary>
        /// <param name="mState"></param>
        /// <param name="kState"></param>
        public ControlManager(MouseState mState, KeyboardState kState)
        {
            this.oldScrollValue = mState.ScrollWheelValue;
            KeyPressEvent += nullMethod;
        }

        /// <summary>
        /// Updates the keyboard state for external use.
        /// </summary>
        /// <param name="state"></param>
        public void UpdateKeyboardState(KeyboardState state)
        {
            this.keyboardState = state;

            foreach(Keys key in GetPressedKeys())
            {
                KeyPressEventArgs args = new KeyPressEventArgs();
                args.Key = key;
                args.KeyboardState = state;
                args.TimeInMilliseconds = Engine.GetGameTime().TotalMilliseconds;
                KeyPressEvent(this, args);
            }
            
        }

        private void nullMethod(object sender, KeyPressEventArgs args) { }

        /// <summary>
        /// Gets a list of keys current pressed.
        /// </summary>
        /// <returns>List of keys.</returns>
        public Keys[] GetPressedKeys()
        {
            return keyboardState.GetPressedKeys();
        }

        /// <summary>
        /// Get the state of a key, will return up if a control is currently focused.
        /// </summary>
        /// <param name="key">The key in question.</param>
        /// <returns>The state of the key.</returns>
        public KeyState GetKeyState(Keys key)
        {
            if (!isControlFocused)
                if (keyboardState.IsKeyUp(key))
                    return KeyState.Up;
                else
                    return KeyState.Down;
            else
                return KeyState.Up;
        }

        /// <summary>
        /// Gets whether a key is currently being pressed, will return false if a control is currently focused.
        /// </summary>
        /// <param name="key">The key in question.</param>
        /// <returns>True if the key is down.</returns>
        public bool IsKeyDown(Keys key){
            if (!isControlFocused)
                return keyboardState.IsKeyDown(key);
            else
                return false;
        }

        /// <summary>
        /// Gets whether a key is currently not being pressed, will return true if a control is currently focused.
        /// </summary>
        /// <param name="key">The key in question.</param>
        /// <returns>True if the key is up.</returns>
        public bool IsKeyUp(Keys key)
        {
            if (!isControlFocused)
                return keyboardState.IsKeyUp(key);
            else
                return true;
        }
            
        /// <summary>
        /// Updates mouse boolean Feilds stored in the ControlManager.
        /// </summary>
        /// <param name="state"></param>
        public void UpdateMouseState(MouseState state)
        {
            this.mouseState = state;
            this.mousePosition = new Vector2(state.Position.X, state.Position.Y);
            this.scrollValue = state.ScrollWheelValue;
            this.scrollAmount = scrollValue - oldScrollValue;
            this.oldScrollValue = state.ScrollWheelValue;

            //------Left-Button--------
            if (mouseState.LeftButton == ButtonState.Pressed)
                leftMouseCounter++;
            else if (mouseState.LeftButton == ButtonState.Released && leftMouseCounter > 0)
            {
                this.isLeftReleased = true;
                leftMouseCounter = 0;
            }
            else
            {
                leftMouseCounter = 0;
                this.isLeftReleased = false;
            }

            if (leftMouseCounter == 1)
            {
                this.isLeftClicked = true;
                Console.Out.WriteLine(mousePosition);
            }
            else if (leftMouseCounter > 1 && isLeftClicked == true)
                this.isLeftClicked = false;
            else if (leftMouseCounter > 5)
            {
                this.isLeftClicked = false;
                this.isLeftHeld = true;
            }
            else
            {
                this.isLeftClicked = false;
                this.isLeftHeld = false;
            }
            //--------------------------

            //------Right-Button--------
            if (mouseState.RightButton == ButtonState.Pressed)
                rightMouseCounter++;
            else if (mouseState.RightButton == ButtonState.Released && rightMouseCounter > 0)
                this.isRightReleased = true;
            else
            {
                rightMouseCounter = 0;
                this.isRightReleased = false;
            }

            if (rightMouseCounter == 1)
                this.isRightClicked = true;
            else if (rightMouseCounter > 1 && isRightClicked == true)
                this.isRightClicked = false;
            else if (rightMouseCounter > 5)
            {
                this.isRightClicked = false;
                this.isRightHeld = true;
            }
            else
            {
                this.isRightClicked = false;
                this.isRightHeld = false;
            }
            //--------------------------

            //------Middle-Button--------
            if (mouseState.MiddleButton == ButtonState.Pressed)
                rightMouseCounter++;
            else if (mouseState.MiddleButton == ButtonState.Released && middleMouseCounter > 0)
                this.isMiddleReleased = true;
            else
            {
                middleMouseCounter = 0;
                this.isMiddleReleased = false;
            }

            if (middleMouseCounter == 1)
                this.isMiddleClicked = true;
            else if (middleMouseCounter > 1 && isMiddleClicked == true)
                this.isMiddleClicked = false;
            else if (middleMouseCounter > 5)
            {
                this.isMiddleClicked = false;
                this.isMiddleHeld = true;
            }
            else
            {
                this.isMiddleClicked = false;
                this.isMiddleHeld = false;
            }
        }

        public Vector2 MousePosition
        {
            get { return mousePosition; }
        }

        public bool IsLeftHeld
        {
            get { return isLeftHeld; }
        }

        public bool IsLeftClicked
        {
            get { return isLeftClicked; }
        }

        public bool IsLeftReleased
        {
            get { return isLeftReleased;  }
        }

        public bool IsRightHeld
        {
            get { return isRightHeld; }
        }

        public bool IsRightClicked
        {
            get { return isRightClicked; }
        }

        public bool IsRightReleased
        {
            get { return isRightReleased; }
        }

        public bool IsMiddleHeld
        {
            get { return isMiddleHeld; }
        }

        public bool IsMiddleClicked
        {
            get { return isMiddleClicked; }
        }

        public bool IsMiddleReleased
        {
            get { return isMiddleReleased;  }
        }

        public int ScrollAmount
        {
            get { return scrollAmount; }
        }

        public int ScrollValue
        {
            get { return scrollValue; }
        }

        public bool IsControlFocused
        {
            get { return isControlFocused; }
            set { isControlFocused = value; }
        }
    }
}
