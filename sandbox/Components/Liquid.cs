using Microsoft.Xna.Framework;

namespace sandbox.Components
{
    public abstract class Liquid : Element
    {
        //TODO: Liquids need density property and must swap positions based on their density
        //public static int density;
        protected Liquid(GraphicsDeviceManager graphics) : base(graphics) { }
        public override int[] UpdateElementPosition(int x, int y, Element element, bool leftOrRight)
        {
            int[] index = new int[2];
            //Directly below
            if (ElementMatrix.IsWithinBounds(x, y + 1) && (ElementMatrix.CanMoveThrough(x, y + 1) ||
                ElementMatrix.elements[x, y + 1] is Liquid && (ElementMatrix.elements[x, y + 1].density < ElementMatrix.elements[x, y].density)))
            {
                
                ElementMatrix.elements[x, y] = ElementMatrix.elements[x, y + 1];
                ElementMatrix.elements[x, y + 1] = element;
                index[0] = x;
                index[1] = y + 1;

                return index;
            }

            ////Below left
            //else if (ElementMatrix.IsWithinBounds(x - 1, y + 1) && ElementMatrix.CanMoveThrough(x - 1, y + 1))
            //{
            //    ElementMatrix.elements[x, y] = ElementMatrix.elements[x - 1, y + 1];
            //    ElementMatrix.elements[x - 1, y + 1] = element;
            //    index[0] = x - 1;
            //    index[1] = y + 1;

            //    return index;
            //}

            ////Below right
            //else if (ElementMatrix.IsWithinBounds(x + 1, y + 1) && ElementMatrix.CanMoveThrough(x + 1, y + 1))
            //{
            //    ElementMatrix.elements[x, y] = ElementMatrix.elements[x + 1, y + 1];
            //    ElementMatrix.elements[x + 1, y + 1] = element;
            //    index[0] = x + 1;
            //    index[1] = y + 1;

            //    return index;
            //}

            //Check these after the 3 cells below are occupied
            //There is 100% a way to rewrite these if statements, this method is getting quite ugly 
            //as there is a lot of repeated code
            //Left and Right both empty and within bounds
            else if ((ElementMatrix.IsWithinBounds(x - 1, y) && (ElementMatrix.CanMoveThrough(x - 1, y) ||
                ElementMatrix.elements[x - 1, y] is Liquid && (ElementMatrix.elements[x - 1, y].density < ElementMatrix.elements[x, y].density))) &&
                (ElementMatrix.IsWithinBounds(x + 1, y) && (ElementMatrix.CanMoveThrough(x + 1, y) ||
                ElementMatrix.elements[x + 1, y] is Liquid && (ElementMatrix.elements[x + 1, y].density < ElementMatrix.elements[x, y].density))))
            {
                if (leftOrRight == true)
                {
                    ElementMatrix.elements[x, y] = ElementMatrix.elements[x - 1, y];
                    ElementMatrix.elements[x - 1, y] = element;
                    index[0] = x - 1;
                    index[1] = y;

                    return index;
                }
                else //Is it better to be explicit here? leftOrRight == false
                {
                    ElementMatrix.elements[x, y] = ElementMatrix.elements[x + 1, y];
                    ElementMatrix.elements[x + 1, y] = element;
                    index[0] = x + 1;
                    index[1] = y;

                    return index;
                }
            }
            //Left
            else if (ElementMatrix.IsWithinBounds(x - 1, y) && (ElementMatrix.CanMoveThrough(x - 1, y) ||
                ElementMatrix.elements[x - 1, y] is Liquid && (ElementMatrix.elements[x - 1, y].density < ElementMatrix.elements[x, y].density)))
            {
                ElementMatrix.elements[x, y] = ElementMatrix.elements[x - 1, y];
                ElementMatrix.elements[x - 1, y] = element;
                index[0] = x - 1;
                index[1] = y;

                return index;
            }

            //Right
            else if (ElementMatrix.IsWithinBounds(x + 1, y) && (ElementMatrix.CanMoveThrough(x + 1, y) ||
                ElementMatrix.elements[x + 1, y] is Liquid && (ElementMatrix.elements[x + 1, y].density < ElementMatrix.elements[x, y].density)))
            {
                ElementMatrix.elements[x, y] = ElementMatrix.elements[x + 1, y];
                ElementMatrix.elements[x + 1, y] = element;
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
