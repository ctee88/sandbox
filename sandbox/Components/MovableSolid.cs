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
        public override int[] UpdateElementPosition(int x, int y, Element element, bool leftOrRight)
        {
            int[] index = new int[2];

            //Issue here is that I need to swap the positions when a MovableSolid meets a Liquid but the Liquid is not instantiated in this context
            //Currently makes the previous cell empty (null) and if the next cell was water, it's overwritten as a result

            //Directly below
            if (ElementMatrix.IsWithinBounds(x, y + 1) && (ElementMatrix.IsEmptyCell(x, y + 1) || ElementMatrix.elements[x, y + 1] is Water))
            {
                if (ElementMatrix.elements[x, y + 1] is Water)
                {
                    ElementMatrix.elements[x, y] = ElementMatrix.elements[x, y + 1];
                    ElementMatrix.elements[x, y + 1] = element;
                } else 
                {
                    ElementMatrix.elements[x, y] = null;
                    ElementMatrix.elements[x, y + 1] = element;
                }
                index[0] = x;
                index[1] = y + 1;
                return index;
            }

            //Below left
            else if (ElementMatrix.IsWithinBounds(x - 1, y + 1) && (ElementMatrix.IsEmptyCell(x - 1, y + 1) || ElementMatrix.elements[x - 1, y + 1] is Water))
            {
                if (ElementMatrix.elements[x - 1, y + 1] is Water)
                {
                    ElementMatrix.elements[x, y] = ElementMatrix.elements[x - 1, y + 1];
                    ElementMatrix.elements[x - 1, y + 1] = element;
                } else
                {
                    ElementMatrix.elements[x, y] = null;
                    ElementMatrix.elements[x - 1, y + 1] = element;
                }
                index[0] = x - 1;
                index[1] = y + 1;
                return index;
            }

            //Below right
            else if (ElementMatrix.IsWithinBounds(x + 1, y + 1) && (ElementMatrix.IsEmptyCell(x + 1, y + 1) || ElementMatrix.elements[x + 1, y + 1] is Water))
            {
                if (ElementMatrix.elements[x + 1, y + 1] is Water)
                {
                    ElementMatrix.elements[x, y] = ElementMatrix.elements[x + 1, y + 1];
                    ElementMatrix.elements[x + 1, y + 1] = element;
                } else
                {
                    ElementMatrix.elements[x, y] = null;
                    ElementMatrix.elements[x + 1, y + 1] = element;

                }
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
