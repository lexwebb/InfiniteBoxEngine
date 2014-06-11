using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine.Abstracts
{
    /// <summary>
    /// Allows objects tro store a string name, removes duplicate code.
    /// </summary>
    abstract public class Nameable
    {
        String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
