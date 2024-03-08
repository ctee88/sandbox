using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace sandbox.Components
{
    public class ElementMatrix
    {
        public const int size_x = 256;
        public const int size_y = size_x;
        public static Element[,] elements;
        
        public void Draw(Element[,] elements, SpriteBatch spritebatch)
        {
            int depth = 0;
            for (int x = 0; x < size_x; x++)
            {
                for (int y = 0 ; y < size_y; y++)
                {
                    depth += 1;
                    Element element = elements[x, y];
                    if (element != null)
                    {
                        element.pos = new Vector2(x, y);

                        spritebatch.Draw(element.texture,
                            element.pos,
                            new Rectangle(0, 0, 1, 1),
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
        
        public Element[,] createElementMatrix()
        {
            elements = new Element[size_x, size_y];

            for (int x = 0; x < size_x; x++)
            {
                for (int y = 0; y < size_y; y++)
                {
                    elements[x, y] = null;
                }
            }

            return elements;
        }


    }
}
