using InfiniteBoxEngine.GUI.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine.GUI.Menu {
    abstract public class Menu : GUIManager {
        public bool Active { get; set; }

        public LinkedList<Control> controlOrder = new LinkedList<Control>();

        public void DrawMenu(SpriteBatch sb, GameTime gt) {
            if (Active)
                base.DrawGUIMiddleground(sb, gt);
        }

        public void Resize() {

        }

        public void UpdateMenu(ControlManager controlManager) {
            if (Active)
                base.UpdateGUI(controlManager);
        }

        public void RegisterOrderedControl(Control control, LinkedListNode<Control> node, bool after) {
            if (controlOrder.Count == 0)
                controlOrder.AddFirst(control);
            else {
                if (after)
                    controlOrder.AddAfter(node, control);
                else
                    controlOrder.AddBefore(node, control);
            }

            Controls.Add("", control);
        }
    }
}
