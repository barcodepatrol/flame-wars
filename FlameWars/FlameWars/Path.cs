using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlameWars
{
    public class Path
    {
        // Instance variables
        private Vector2 pos;
        private Rectangle boundaries;
        private Color tint;
        private SpaceType space;

        // Properties
        public SpaceType Space { get { this.space} }
        public Vector2 Position { get { } set { } }
        public Rectangle Bounds { get { } set { } }
        public int X { get { } set { } }
        public int Y { get { } set { } }
        public Color Tint { get { } set { } }

        // Constructor
        Path() { }

        // Method
        public void Trigger()
        {
            Switch(space) {
            Resource:
                break;
            Empty:
                break;
            }
        }
    }
}
