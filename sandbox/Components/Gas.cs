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
                //ElementMatrix.elements[x, y] = null;
                ElementMatrix.elements[x, y] = ElementMatrix.elements[x, y - 1];
                ElementMatrix.elements[x, y - 1] = element;

                index[0] = x;
                index[1] = y - 1;

                return index;
            }

            //Above left
            else if (ElementMatrix.IsWithinBounds(x - 1, y - 1) && (ElementMatrix.IsEmptyCell(x - 1, y - 1)))
            {
                //ElementMatrix.elements[x, y] = null;
                ElementMatrix.elements[x, y] = ElementMatrix.elements[x - 1, y - 1];
                ElementMatrix.elements[x - 1, y - 1] = element;

                index[0] = x - 1;
                index[1] = y - 1;

                return index;
            }

            //Above right
            else if (ElementMatrix.IsWithinBounds(x + 1, y - 1) && (ElementMatrix.IsEmptyCell(x + 1, y - 1)))
            {
                //ElementMatrix.elements[x, y] = null;
                ElementMatrix.elements[x, y] = ElementMatrix.elements[x + 1, y - 1];
                ElementMatrix.elements[x + 1, y - 1] = element;

                index[0] = x + 1;
                index[1] = y - 1;

                return index;
            }

            //Check these after the 3 cells below are occupied
            //There is 100% a way to rewrite these if statements, this method is getting quite ugly 
            //as there is a lot of repeated code
            //Left and Right both empty and within bounds
            else if ((ElementMatrix.IsWithinBounds(x - 1, y) && ElementMatrix.CanMoveThrough(x - 1, y)) &&
                (ElementMatrix.IsWithinBounds(x + 1, y) && ElementMatrix.CanMoveThrough(x + 1, y)))
            {
                if (leftOrRight == true)
                {
                    ElementMatrix.elements[x, y] = ElementMatrix.elements[x - 1, y];
                    ElementMatrix.elements[x - 1, y] = element;
                    //ElementMatrix.elements[x, y] = null;
                    index[0] = x - 1;
                    index[1] = y;

                    return index;
                }
                else //Is it better to be explicit here? leftOrRight == false
                {
                    ElementMatrix.elements[x, y] = ElementMatrix.elements[x + 1, y];
                    ElementMatrix.elements[x + 1, y] = element;
                    //ElementMatrix.elements[x, y] = null;
                    index[0] = x + 1;
                    index[1] = y;

                    return index;
                }
            }
            //Left
            else if (ElementMatrix.IsWithinBounds(x - 1, y) && ElementMatrix.CanMoveThrough(x - 1, y))
            {
                ElementMatrix.elements[x, y] = ElementMatrix.elements[x - 1, y];
                ElementMatrix.elements[x - 1, y] = element;
                //ElementMatrix.elements[x, y] = null;
                index[0] = x - 1;
                index[1] = y;

                return index;
            }

            //Right
            else if (ElementMatrix.IsWithinBounds(x + 1, y) && ElementMatrix.CanMoveThrough(x + 1, y))
            {
                ElementMatrix.elements[x, y] = ElementMatrix.elements[x + 1, y];
                ElementMatrix.elements[x + 1, y] = element;
                //ElementMatrix.elements[x, y] = null;
                index[0] = x + 1;
                index[1] = y;

                return index;
            }

            //Can't move
            index[0] = x; 
            index[1] = y;
            return index;
        }
    }
}
