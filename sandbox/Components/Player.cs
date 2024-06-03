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
        //TODO: Figure this shit out --> Do I want this dict to be in this class? or elsewhere. Probably better to place this dict in a class which uses the ElementType enum?
        private static Dictionary<ElementType, Type> elementTypes = new Dictionary<ElementType, Type>()
        {
            { ElementType.Sand, typeof(Sand) },
            { ElementType.Water, typeof(Water) },
            { ElementType.Wood, typeof(Wood) },
            { ElementType.Smoke, typeof(Smoke) },
            { ElementType.Cinder, typeof(Cinder) }
        };

        //This method needs a better name?
        public static void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            spawnTimer += gameTime.ElapsedGameTime.Milliseconds;
            var mouseState = Mouse.GetState();

            ElementType selectedElementName = GuiManager.GetSelectedElementName();

            //TODO: Remove spawnTimer check for Solids so that there are less gaps when spawning Solids
            //TODO: Fix elements spawning behind UI when player clicks an element box
            if (mouseState.LeftButton == ButtonState.Pressed && spawnTimer > 40)
            {
                spawnTimer = 0;
                int mouseRow = mouseState.Position.X * ElementMatrix.size_x / graphics.PreferredBackBufferWidth;
                int mouseCol = mouseState.Position.Y * ElementMatrix.size_y / graphics.PreferredBackBufferHeight;

                int spawnMatrixArea = 4;
                int spawnMatrixSize = spawnMatrixArea / 2;
                for (int i = -spawnMatrixSize; i <= spawnMatrixSize; i++)
                {
                    for (int j = -spawnMatrixSize; j <= spawnMatrixSize; j++)
                    {
                        int row = mouseRow + i;
                        int col = mouseCol + j;

                        if (elementTypes[selectedElementName].IsSubclassOf(typeof(Solid)))
                        {
                            SpawnElement(row, col, selectedElementName, graphics);
                        } else
                        {
                            //Amorphous spawning
                            int randomNum = random.Next(0, 4) < 3 ? 1 : 0;
                            if (randomNum == 0)
                            {
                                SpawnElement(row, col, selectedElementName, graphics);
                            }
                        }
                    }
                }
            }
        }

        public static void SpawnElement(int row, int col, ElementType selectedElementName, GraphicsDeviceManager graphics)
        {
            if (ElementMatrix.IsWithinBounds(row, col)) //&& ElementMatrix.elements[row, col] == null)
            {
                //This will execute every single time a new particle is spawned...
                //Can this be written in a way in which this code is executed once (when the element is selected)
                //And then the element can be spawned normally after without reflection?
                if (elementTypes.ContainsKey(selectedElementName))
                {
                    Type elementType = elementTypes[selectedElementName];
                    Element element = CreateElement(elementType, graphics);
                    ElementMatrix.elements[row, col] = element;
                    element.pos = new Vector2(row, col);
                }
            }
        }

        //TODO: Makes more sense to move this to Element class or the class handling elementTypes
        public static Element CreateElement(Type elementType, GraphicsDeviceManager graphics)
        {
            return (Element)Activator.CreateInstance(elementType, graphics);
        }
    }
}


