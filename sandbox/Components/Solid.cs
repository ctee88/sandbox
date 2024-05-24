using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sandbox.Components
{
    public abstract class Solid : Element
    {
        //Hide isFalling = true from base (might be another way to overwrite this)
        public new bool isFalling = false;

        public override int[] UpdateElementPosition(int x, int y, Element element, bool leftOrRight)
        {
            int[] index = new int[2];
            index[0] = x;
            index[1] = y;

            return index;
        }
    }
}
