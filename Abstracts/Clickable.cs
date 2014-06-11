using InfiniteBoxEngine.GUI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine.Abstracts
{
    /// <summary>
    /// Base object for UI objects, contains methods fired for specific click events.
    /// </summary>
    abstract public class Clickable
    {
        Rectangle clickArea;
        bool wasHovering = false;
        List<MouseListener> listeners = new List<MouseListener>();

        /// <summary>
        /// Constructs clickable object and sets clickable area rectangle.
        /// </summary>
        /// <param name="position">objects clickable position relative to draw coords.</param>
        /// <param name="width">clickable objects width.</param>
        /// <param name="height">clickable objects height.</param>
        public Clickable(Vector2 position, int width, int height)
        {
            clickArea = new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        /// <summary>
        /// Adds a method to be fired when a mouse function is used.
        /// </summary>
        /// <param name="Method">Method to be called.</param>
        /// <param name="button">Mouse button used.</param>
        /// <param name="action">Mouse action performed.</param>
        public void RegisterListener(Action<Vector2> Method, MouseButton button, ButtonAction action)
        {
            foreach (MouseListener listener in listeners)
            {
                if (listener.Button == button)
                {
                    if (listener.Action == action)
                    {
                        listener.AddListener(Method);
                        return;
                    }        
                }               
            }

            MouseListener newListener = new MouseListener(action, button);
            newListener.AddListener(Method);
            listeners.Add(newListener);
        }

        /// <summary>
        /// Calls all methods attatched to listeners for the correct button and action.
        /// </summary>
        /// <param name="pos">Mouse position.</param>
        /// <param name="button">Mouse button pressed.</param>
        /// <param name="action">Mouse button action.</param>
        public void OnActionMethod(Vector2 pos, MouseButton button, ButtonAction action)
        {
            foreach (MouseListener listener in listeners)
            {
                if (listener.Button == button)
                {
                    if (listener.Action == action)
                    {
                        foreach(Action<Vector2> method in listener.GetListeners()){
                            method(pos);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Called to initiate a mouse button pressed action.
        /// </summary>
        /// <param name="pos">Mouse position.</param>
        /// <param name="button">Mouse button pressed.</param>
        /// <param name="action">Mouse button action.</param>
        public void OnActionCore(Vector2 pos, MouseButton button, ButtonAction action)
        {
            this.OnActionMethod(pos, button, action);
            switch (action)
            {
                case ButtonAction.OnClick:
                    this.OnClick(pos, button);
                    break;
                case ButtonAction.OnHold:
                    this.OnHold(pos, button);
                    break;
                case ButtonAction.OnHover:
                    this.OnHover(pos);
                    break;
                case ButtonAction.OnRelease:
                    this.OnRelease(pos, button);
                    break;
                case ButtonAction.OnUnHover:
                    this.OnUnHover(pos);
                    break;
            }
        }
        
        /// <summary>
        /// Called on mouse click on the objects area.
        /// </summary>
        /// <param name="pos">Mouse position.</param>
        /// <param name="button">Mouse button clicked.</param>
        public virtual void OnClick(Vector2 pos, MouseButton button) { }

        /// <summary>
        /// Called on mouse hover on the objects area.
        /// </summary>
        /// <param name="pos">Mouse position.</param>
        public virtual void OnHover(Vector2 pos) { }

        /// <summary>
        /// Called when mouse moves out of the objects area.
        /// </summary>
        /// <param name="pos">Mouse position.</param>
        public virtual void OnUnHover(Vector2 pos) { }

        /// <summary>
        /// Called when mouse button is held down for more than one game tick on the object area.
        /// </summary>
        /// <param name="pos">Mouse position.</param>
        /// <param name="button">Mouse button pressed.</param>
        public virtual void OnHold(Vector2 pos, MouseButton button) { }

        /// <summary>
        /// Held when mouse button is released from a hold on the object area.
        /// </summary>
        /// <param name="pos">Mouse position</param>
        /// <param name="button">Mouse button released.</param>
        public virtual void OnRelease(Vector2 pos, MouseButton button) { }

        /// <summary>
        /// Gets the objects clickable area.
        /// </summary>
        /// <returns>Objects area.</returns>
        public Rectangle GetClickableArea() { return clickArea; }

        public Vector2 Position
        {
            get { return new Vector2(clickArea.X, clickArea.Y); }
            set { clickArea.X = (int)Math.Floor(value.X); clickArea.Y = (int)Math.Floor(value.Y);}
        }

        public int Height
        {
            get { return clickArea.Height; }
            set { clickArea.Height = value; }
        }

        public int Width
        {
            get { return clickArea.Width; }
            set { clickArea.Width = value; }
        }

        /// <summary>
        /// Gets if the mouse was hovering on the object last game tick.
        /// </summary>
        public bool WasHovering
        {
            get { return wasHovering; }
            set { wasHovering = value; }
        }
    }
}
