using Microsoft.Xna.Framework;
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
        
        protected MovableSolid(GraphicsDeviceManager graphics) : base(graphics) { }
        //Future Elements may need a similar method - can move this into Element class if required
        private bool IsSubmerged(int x, int y)
        {
            //TODO: Change this approach/Fix the check. Not working as intended as this method currently doesn't account
            //for when sand is falling in groups and adjacent to each other when sinking in water.

            //Check if the current element is surrounded by water on the sides. Can be changed to liquid in future
            //depending on properties such as mass/type of liquid e.g. salt (can dissolve) or oil etc...
            return (ElementMatrix.IsWithinBounds(x - 1, y) && ElementMatrix.elements[x - 1, y] is Liquid) ||
                   (ElementMatrix.IsWithinBounds(x + 1, y) && ElementMatrix.elements[x + 1, y] is Liquid);
        }

        public override int[] UpdateElementPosition(int x, int y, Element element, bool leftOrRight)
        {
            int[] index = new int[2];

            //Directly below
            if (ElementMatrix.IsWithinBounds(x, y + 1) && (ElementMatrix.CanMoveThrough(x, y + 1) || ElementMatrix.elements[x, y + 1] is Liquid))
            {
                ElementMatrix.elements[x, y] = ElementMatrix.elements[x, y + 1];
                ElementMatrix.elements[x, y + 1] = element;

                index[0] = x;
                index[1] = y + 1;

                if (IsSubmerged(x, y + 1))
                {
                    element.velY *= 0.1f;
                }
                return index;
            }

            //Below left
            else if (ElementMatrix.IsWithinBounds(x - 1, y + 1) && (ElementMatrix.CanMoveThrough(x - 1, y + 1) || ElementMatrix.elements[x - 1, y + 1] is Liquid))
            {
                ElementMatrix.elements[x, y] = ElementMatrix.elements[x - 1, y + 1];
                ElementMatrix.elements[x - 1, y + 1] = element;

                index[0] = x - 1;
                index[1] = y + 1;

                if (IsSubmerged(x - 1, y + 1))
                {
                    element.velY *= 0.1f;
                }
                return index;
            }

            //Below right
            else if (ElementMatrix.IsWithinBounds(x + 1, y + 1) && (ElementMatrix.CanMoveThrough(x + 1, y + 1) || ElementMatrix.elements[x + 1, y + 1] is Liquid))
            {
                ElementMatrix.elements[x, y] = ElementMatrix.elements[x + 1, y + 1];
                ElementMatrix.elements[x + 1, y + 1] = element;

                index[0] = x + 1;
                index[1] = y + 1;

                if (IsSubmerged(x + 1, y + 1))
                {
                    element.velY *= 0.1f;
                }
                return index;
            }

            //Can't move
            index[0] = x;
            index[1] = y;
            return index;

        }
    }
}
