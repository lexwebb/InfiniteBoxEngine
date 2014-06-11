using InfiniteBoxEngine.GUI;
using InfiniteBoxEngine.GUI.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine
{
    public class GUIManager
    {
        Dictionary<String, Control> controls = new Dictionary<string, Control>();

        #region properties
        public Dictionary<String, Control> Controls
        {
            get { return controls; }
            set { controls = value; }
        }
        #endregion

        public void RegisterControl(String name, Control control)
        {
            this.controls.Add(name, control);
        }

        public void DrawGUIBackground(SpriteBatch sb, GameTime gt)
        {
            foreach (Control control in controls.Values)
                if (control.Visible)
                    control.DrawBackground(sb, gt);
        }

        public void DrawGUIMiddleground(SpriteBatch sb, GameTime gt)
        {
            foreach (Control control in controls.Values)
                if (control.Visible)
                    control.DrawMiddleground(sb, gt);
        }

        public void DrawGUIForeground(SpriteBatch sb, GameTime gt)
        {
            foreach (Control control in controls.Values)
                if (control.Visible)
                    control.DrawForeground(sb, gt);
        }

        public void DrawGUIHighlights(SpriteBatch sb, GameTime gt)
        {
            foreach (Control control in controls.Values)
                if (control.Visible)
                    control.DrawHighlights(sb, gt);
        }

        public void ClearControls()
        {
            this.controls.Clear();
        }

        public void UpdateGUI(ControlManager controlManager)
        {
            foreach (Control control in controls.Values)
            {               
                if (control.Usable)
                {
                    if (controlManager.IsLeftClicked)
                    {
                        if (!control.GetRelativeRectangle().Contains(new Point((int)controlManager.MousePosition.X, (int)controlManager.MousePosition.Y)))
                        {
                            control.IsFocused = false;
                        }
                    }
                    if (control.GetRelativeRectangle().Contains(new Point((int)controlManager.MousePosition.X, (int)controlManager.MousePosition.Y)))
                    {
                        control.WasHovering = true;

                            //Left
                        if (controlManager.IsLeftClicked)
                        {
                            Engine.ControlManager.IsControlFocused = false;
                            control.OnActionCore(controlManager.MousePosition, MouseButton.Left, ButtonAction.OnClick);
                            if (control.CanBeFocused)
                            {
                                control.IsFocused = true;
                                Engine.ControlManager.IsControlFocused = true;
                            }
                        }
                        else if (controlManager.IsLeftHeld)
                        {
                            control.OnActionCore(controlManager.MousePosition, MouseButton.Left, ButtonAction.OnHold);
                        }
                        else if (controlManager.IsLeftReleased)
                        {
                            control.OnActionCore(controlManager.MousePosition, MouseButton.Left, ButtonAction.OnRelease);
                        }
                            //Right
                        else if (controlManager.IsRightClicked)
                        {
                            control.OnActionCore(controlManager.MousePosition, MouseButton.Right, ButtonAction.OnClick);
                        }
                        else if (controlManager.IsRightHeld)
                        {
                            control.OnActionCore(controlManager.MousePosition, MouseButton.Right, ButtonAction.OnHold);
                        }
                        else if (controlManager.IsRightReleased)
                        {
                            control.OnActionCore(controlManager.MousePosition, MouseButton.Right, ButtonAction.OnRelease);
                        }
                            //Middle
                        else if (controlManager.IsMiddleClicked)
                        {
                            control.OnActionCore(controlManager.MousePosition, MouseButton.Middle, ButtonAction.OnClick);
                        }
                        else if (controlManager.IsMiddleHeld)
                        {
                            control.OnActionCore(controlManager.MousePosition, MouseButton.Middle, ButtonAction.OnHold);
                        }
                        else if (controlManager.IsMiddleReleased)
                        {
                            control.OnActionCore(controlManager.MousePosition, MouseButton.Middle, ButtonAction.OnRelease);
                        }
                        else
                        {
                            control.OnActionCore(controlManager.MousePosition, MouseButton.Left, ButtonAction.OnHover);
                        }
                    }
                    else if (control.WasHovering)
                    {
                        control.OnActionCore(controlManager.MousePosition, MouseButton.Left, ButtonAction.OnUnHover);
                    }
                }
            }
        }
    }
}
