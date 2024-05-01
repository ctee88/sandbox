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
        public Vector2 pos = new Vector2 (0, 0);
        public int velocityX = 0;
        public int velocityY = 0; //0
        public Color color; //null
        public Texture2D texture; //null
        //public bool isFalling = true;

        public abstract void UpdateElementPos(int x, int y, Element element);

    }
}
