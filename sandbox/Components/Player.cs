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

        public static void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            spawnTimer += gameTime.ElapsedGameTime.Milliseconds;
            var mouseState = Mouse.GetState();
            //TODO: Functionality for selecting element, i.e '1' for sand, '2' for water etc...
            //TODO: Make a simple GUI to select element
            //Will need an InputManager
            if (mouseState.LeftButton == ButtonState.Pressed && spawnTimer > 40)
            {
                spawnTimer = 0;
                int mouseRow = mouseState.Position.X * ElementMatrix.size_x / graphics.PreferredBackBufferWidth;
                int mouseCol = mouseState.Position.Y * ElementMatrix.size_y / graphics.PreferredBackBufferHeight;

                int spawnMatrixArea = 8;
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
                                //Need to get the correct element
                                //For now just instantiate sand - Element -> MovableSolid -> Sand
                                //Will need to implement instantiating the correct element in the future
                                Element sand = new Sand();
                                ElementMatrix.elements[row, col] = sand;
                                sand.pos = new Vector2(row, col);

                                if (sand.texture == null)
                                {
                                    sand.texture = new Texture2D(graphics.GraphicsDevice, 1, 1);

                                    sand.texture.SetData<Color>(new Color[] { sand.color });
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}


