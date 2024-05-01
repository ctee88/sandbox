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
        public override void UpdateElementPos(int x, int y, Element element)
        {
            //y += element.velocityY + ElementMatrix.gravity;

            //Directly below
            if (ElementMatrix.IsWithinBounds(x, y + 1) && ElementMatrix.IsEmptyCell(x, y + 1))
            {
                ElementMatrix.elements[x, y + 1] = element;
                ElementMatrix.elements[x, y] = null;
            }
            //Below left
            else if (ElementMatrix.IsWithinBounds(x - 1, y + 1) && ElementMatrix.IsEmptyCell(x - 1, y + 1))
            {
                ElementMatrix.elements[x - 1, y + 1] = element;
                ElementMatrix.elements[x, y] = null;
            }
            //Below right
            else if (ElementMatrix.IsWithinBounds(x + 1, y + 1) && ElementMatrix.IsEmptyCell(x + 1, y + 1))
            {
                ElementMatrix.elements[x + 1, y + 1] = element;
                ElementMatrix.elements[x, y] = null;
            }
            //Can't move
            //else
            //{
            //    element.velocityY = 0;
            //}
        }
    }
}
