using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sandbox.Components
{
    public abstract class MovableSolid : Element
    {
        //move() or update()?
        public void move()
        {
            for (int x = 0; x < ElementMatrix.size_x; x++)
            {
                for (int y = 0; y < ElementMatrix.size_y; y++)
                {
                    var element = ElementMatrix.elements[x, y];
                    if (element != null)
                    {
                        element.velocity.Y += 1;
                        if (MathF.Abs(element.velocity.Y) > 1)
                        {
                            element.velocity.Y = 1 * Math.Sign(element.velocity.Y);
                        }

                        element.velocity.X += 0;
                        if (MathF.Abs(element.velocity.X) > 1)
                        {
                            element.velocity.X = 1 * Math.Sign(element.velocity.X);
                        }

                        int next_x = x + (int)element.velocity.X;
                        int next_y = y + (int)element.velocity.Y;

                        if (!checkCollisions(next_x, next_y))
                        {

                        }
                        else if (!checkCollisions(next_x - 1, next_y))
                        {
                            next_x -= 1;
                        }
                        else if (!checkCollisions(next_x + 1, next_y))
                        {
                            next_x += 1;
                        }
                        else
                        {
                            next_x = x;
                            element.velocity.X = 0;

                            next_y = y;
                            element.velocity.Y = 0;
                        }
                        ElementMatrix.elements[x, y] = null;
                        x = next_x;
                        y = next_y;
                        ElementMatrix.elements[x, y] = element;
                    }
                }
            }
        }

        public override bool checkCollisions(int x, int y)
        {
            if (y >= ElementMatrix.size_x)
            {
                return true;
            }
            if (x <= -1 || x >= ElementMatrix.size_y)
            {
                return true;
            }
            return ElementMatrix.elements[x, y] != null;
        }
    }
}
