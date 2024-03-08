using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sandbox.Components
{
    public abstract class Element
    {
        //Position within the matrix
        public Vector2 pos; //0
        //(x, y) vel of a given element
        public Vector2 velocity; //0
        //Is color required if we are drawing each particle as a texture?
        public Color color; //null
        public Texture2D texture; //null

        //public bool isColliding(int x, int y)
        //{
        //    if (y >= ElementMatrix.size_x)
        //    {
        //        return true;
        //    }
        //    if (x <= -1 || x >= ElementMatrix.size_y)
        //    {
        //        return true;
        //    }
        //    return ElementMatrix.elements[x, y] != null;
        //}

        //Collision logic will be different depending on element type (Movable solid/liquid etc)
        public abstract bool checkCollisions(int x, int y);
    }
}
