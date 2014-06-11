using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine.GUI {
    public class KeyPressEventArgs : EventArgs {
        public Keys Key { get; set; }
        public KeyboardState KeyboardState { get; set; }

        public double TimeInMilliseconds { get; set; }
    }
}
