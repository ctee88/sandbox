using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sandbox.Components
{
    public abstract class Solid : Element
    {
        protected Solid(GraphicsDeviceManager graphics) : base(graphics) 
        {
            isFalling = false;
        }
        public override int[] UpdateElementPosition(int x, int y, Element element, bool leftOrRight)
        {
            int[] index = new int[2];
            index[0] = x;
            index[1] = y;

            return index;
        }
    }
}
