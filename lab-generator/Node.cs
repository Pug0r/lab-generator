using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_generator
{
    internal class Node
    {
        // 0 - no edge, 1 removable edge, 2 permanent edge
        public int Up { get; set; }
        public int Right { get; set; }
        public int Down { get; set; }
        public int Left { get; set; }
        public bool Visited { get; set; }

        public Node() { Up = 1; Right = 1; Down = 1; Left = 1; Visited = false; }
    }
}
