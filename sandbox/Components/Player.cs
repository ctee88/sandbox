using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Security.Cryptography;

namespace sandbox.Components
{
    static class Player
    {
        private static int spawnTimer = 0;
        private static Random random = new Random();
        //Do I want this dict to be in this class? or elsewhere. Probably better to create an enum in the Element class, this reflection approach seems a bit dumb
        private static Dictionary<ElementType, Type> elementTypes = new Dictionary<ElementType, Type>()
        {
            { ElementType.Sand, typeof(Sand) },
            { ElementType.Water, typeof(Water) }
        };

        //This method needs a better name?
        public static void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            spawnTimer += gameTime.ElapsedGameTime.Milliseconds;
            var mouseState = Mouse.GetState();

            ElementType selectedElementName = GuiManager.GetSelectedElementName();

            if (mouseState.LeftButton == ButtonState.Pressed && spawnTimer > 60)
            {
                spawnTimer = 0;
                int mouseRow = mouseState.Position.X * ElementMatrix.size_x / graphics.PreferredBackBufferWidth;
                int mouseCol = mouseState.Position.Y * ElementMatrix.size_y / graphics.PreferredBackBufferHeight;

                int spawnMatrixArea = 10;
                int spawnMatrixSize = spawnMatrixArea / 2;
                for (int i = -spawnMatrixSize; i <= spawnMatrixSize; i++)
                {
                    for (int j = -spawnMatrixSize; j <= spawnMatrixSize; j++)
                    {
                        int row = mouseRow + i;
                        int col = mouseCol + j;

                        //Amorphous spawning
                        int randomNum = random.Next(0, 4) < 3 ? 1 : 0;
                        if (randomNum == 0)
                        {
                            if (ElementMatrix.IsWithinBounds(row, col) && ElementMatrix.elements[row, col] == null)
                            {
                                //This will execute every single time a new particle is spawned...
                                //Can this be written in a way in which this code is executed once (when the element is selected)
                                //And then the element can be spawned normally after without reflection?
                                if (elementTypes.ContainsKey(selectedElementName))
                                {
                                    Type elementType = elementTypes[selectedElementName];
                                    Element element = (Element)Activator.CreateInstance(elementType);
                                    ElementMatrix.elements[row, col] = element;
                                    element.pos = new Vector2(row, col);

                                    if (element.texture == null)
                                    {
                                        element.texture = new Texture2D(graphics.GraphicsDevice, 1, 1);
                                        element.texture.SetData<Color>(new Color[] { element.color });
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}


