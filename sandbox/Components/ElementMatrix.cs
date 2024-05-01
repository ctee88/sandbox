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


namespace sandbox.Components
{
    public class ElementMatrix
    {
        public const int size_x = 256;
        public const int size_y = size_x;
        public static Element[,] elements;
        public static int gravity = 1;

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
            //for (int x = 0; x < size_x; x++)
            for (int x = size_x - 1; x >= 0; x--)
            {
                //for (int y = 0; y < size_y; y++)
                for (int y = size_y - 1; y >= 0; y--)
                {
                    Element element = elements[x, y];
                    if (element != null) 
                    {   
                        element.UpdateElementPos(x, y, element);
                    }
                }
            }
        }

        public static void DrawMatrix(Element[,] elements, SpriteBatch spritebatch)
        {
            int depth = 0;
            for (int x = 0; x < size_x; x++)
            {
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
        //Confine elements within the screen
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
