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
        //Do I want this dict to be in this class? or elsewhere
        private static Dictionary<string, Type> elementTypes = new Dictionary<string, Type>()
        {
            { "Sand", typeof(Sand) },
            { "Water", typeof(Water) }
        };

        //This method needs a better name?
        public static void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            spawnTimer += gameTime.ElapsedGameTime.Milliseconds;
            var mouseState = Mouse.GetState();

            string selectedElementName = GuiManager.GetSelectedElementName();

            if (mouseState.LeftButton == ButtonState.Pressed && spawnTimer > 60)
            {
                spawnTimer = 0;
                int mouseRow = mouseState.Position.X * ElementMatrix.size_x / graphics.PreferredBackBufferWidth;
                int mouseCol = mouseState.Position.Y * ElementMatrix.size_y / graphics.PreferredBackBufferHeight;

                int spawnMatrixArea = 5;
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






                                    //Need to get the correct element
                                //For now just instantiate sand - Element -> MovableSolid -> Sand
                                //Will need to implement instantiating the correct element in the future
                                //Element sand = new Sand();
                                //ElementMatrix.elements[row, col] = sand;
                                //sand.pos = new Vector2(row, col);

                                //if (sand.texture == null)
                                //{
                                //    sand.texture = new Texture2D(graphics.GraphicsDevice, 1, 1);

                                //    sand.texture.SetData<Color>(new Color[] { sand.color });
                                //}

                                //Element water = new Water();
                                //ElementMatrix.elements[row, col] = water;
                                //water.pos = new Vector2(row, col);

                                //if (water.texture == null)
                                //{
                                //    water.texture = new Texture2D(graphics.GraphicsDevice, 1, 1);

                                //    water.texture.SetData<Color>(new Color[] { water.color });
                                //}

                            }
                        }
                    }
                }
            }
        }
    }
}


