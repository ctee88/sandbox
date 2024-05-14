using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using System.Globalization;


namespace sandbox.Components
{
    public class ElementMatrix
    {
        public const int size_x = 128;
        public const int size_y = size_x;
        public static Element[,] elements;
        public static float gravity = 0.2f;
        public static Random random = new Random();

        public void CreateElementMatrix()
        {
            elements = new Element[size_x, size_y];

            for (int x = 0; x < size_x; x++)
            {
                for (int y = 0; y < size_y; y++)
                {
                    elements[x, y] = null;
                }
            }
        }

        public void Move()
        {
            for (int x = 0; x < size_x; x++)
            //for (int x = size_x - 1; x >= 0; x--)
            {
                //for (int y = size_y - 1; y >= 0; y--)
                for (int y = 0; y < size_y; y++)
                {
                    Element element = elements[x, y];

                    if (element != null) 
                    {
                        element.CheckIfFalling();

                        if (!element.isFalling)// && element is MovableSolid) This seems wrong, might get caught out in the future... Wtf do I even need this?
                        {
                            continue;
                        }

                        for (int i = 0; i < element.GetUpdateCount(); i++)
                        {
                            bool leftOrRight = random.NextDouble() > 0.5;

                            int[] newIndex = element.UpdateElementPosition(x, y, element, leftOrRight);
                            if (newIndex[0] != x || newIndex[1] != y)
                            {
                                x = newIndex[0];
                                y = newIndex[1];
                            } else
                            {
                                element.ResetElementVelocity();
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void DrawMatrix(Element[,] elements, SpriteBatch spritebatch)
        {
            int depth = 0;
            for (int x = 0; x < size_x; x++)
            //for (int x = size_x - 1; x >= 0; x--)
            {
                //for (int y = size_y - 1; y >= 0; y--)
                for (int y = 0; y < size_y; y++)
                {
                    depth += 1;
                    Element element = elements[x, y];
                    if (element != null)
                    {
                        element.pos = new Vector2(x, y);
                        spritebatch.Draw(element.texture,
                            element.pos,
                            null,
                            Color.White,
                            0,
                            Vector2.Zero,
                            1.0f,
                            SpriteEffects.None,
                            depth * 0.00001f);
                    }
                }
            }
        }
        public static bool IsWithinBounds(int x, int y)
        {
            if (x < 0 || x > size_x - 1 || y < 0 || y > size_y - 1)
            { 
                return false; 
            }
            return true;
        }

        public static bool IsEmptyCell(int x, int y)
        {
            if (elements[x, y] == null)
            {
                return true;
            }
            return false;
        }
    }
}
