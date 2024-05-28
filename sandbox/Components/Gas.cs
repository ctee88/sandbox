using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sandbox.Components
{
    public abstract class Gas : Element
    {
        public override int[] UpdateElementPosition(int x, int y, Element element, bool leftOrRight)
        {
            int[] index = new int[2];

            //Directly above
            if (ElementMatrix.IsWithinBounds(x, y - 1) && (ElementMatrix.IsEmptyCell(x, y - 1)))
            {
                //Only accounts for empty cells - might get trapped in liquid and movable solids
                ElementMatrix.elements[x, y] = null;
                ElementMatrix.elements[x, y - 1] = element;

                index[0] = x;
                index[1] = y - 1;

                return index;
            }

            //Above left
            if (ElementMatrix.IsWithinBounds(x - 1, y - 1) && (ElementMatrix.IsEmptyCell(x - 1, y - 1)))
            {
                ElementMatrix.elements[x, y] = null;
                ElementMatrix.elements[x - 1, y - 1] = element;

                index[0] = x - 1;
                index[1] = y - 1;

                return index;
            }

            //Above right
            if (ElementMatrix.IsWithinBounds(x + 1, y - 1) && (ElementMatrix.IsEmptyCell(x + 1, y - 1)))
            {
                ElementMatrix.elements[x, y] = null;
                ElementMatrix.elements[x + 1, y - 1] = element;

                index[0] = x + 1;
                index[1] = y - 1;

                return index;
            }

            //Can't move
            index[0] = x; 
            index[1] = y;
            return index;
        }
    }
}
