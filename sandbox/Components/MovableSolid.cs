using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sandbox.Components
{
    public abstract class MovableSolid : Element
    {
        public override int[] UpdateElementPosition(int x, int y, Element element)
        {
            int[] index = new int[2];
            //Directly below
            if (ElementMatrix.IsWithinBounds(x, y + 1) && ElementMatrix.IsEmptyCell(x, y + 1))
            {
                ElementMatrix.elements[x, y + 1] = element;
                ElementMatrix.elements[x, y] = null;
                index[0] = x;
                index[1] = y + 1;

                return index;
            }

            //Below left
            else if (ElementMatrix.IsWithinBounds(x - 1, y + 1) && ElementMatrix.IsEmptyCell(x - 1, y + 1))
            {
                ElementMatrix.elements[x - 1, y + 1] = element;
                ElementMatrix.elements[x, y] = null;
                index[0] = x - 1;
                index[1] = y + 1;

                return index;
            }

            //Below right
            else if (ElementMatrix.IsWithinBounds(x + 1, y + 1) && ElementMatrix.IsEmptyCell(x + 1, y + 1))
            {
                ElementMatrix.elements[x + 1, y + 1] = element;
                ElementMatrix.elements[x, y] = null;
                index[0] = x + 1;
                index[1] = y + 1;

                return index;
            }

            //Can't move
            index[0] = x;
            index[1] = y;
            return index;

        }
    }
}
