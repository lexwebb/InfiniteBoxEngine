using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine.GUI {
    public struct MouseListener {
        ButtonAction action;
        MouseButton button;

        List<Action<Vector2>> methods;

        public MouseListener(ButtonAction action, MouseButton button) {
            this.action = action;
            this.button = button;
            this.methods = new List<Action<Vector2>>();
        }

        public void AddListener(Action<Vector2> method) {
            this.methods.Add(method);
        }

        public List<Action<Vector2>> GetListeners() {
            return methods;
        }

        public ButtonAction Action {
            get { return action; }
            set { action = value; }
        }

        public MouseButton Button {
            get { return button; }
            set { button = value; }
        }
    }
}
