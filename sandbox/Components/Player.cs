using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sandbox.Components
{
    public class Player
    {
        int spawnTimer = 0;

        public void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            spawnTimer += gameTime.ElapsedGameTime.Milliseconds;
            var mouse_state = Mouse.GetState();
            //TODO: Functionality for selecting element, i.e '1' for sand, '2' for water etc...
            //TODO: Make a simple GUI to select element
            if (mouse_state.LeftButton == ButtonState.Pressed && spawnTimer > 20)
            {
                spawnTimer = 0;
                //Where am I passing GDM.BackBuffer dimensions?
                var x = mouse_state.Position.X * ElementMatrix.size_x / graphics.PreferredBackBufferWidth;
                var y = mouse_state.Position.Y * ElementMatrix.size_y / graphics.PreferredBackBufferHeight;

                if (x >= 0 && x < ElementMatrix.size_x && y >= 0 && y < ElementMatrix.size_y && ElementMatrix.elements[x, y] == null)
                {
                    //Need to get the correct element
                    //For now just instantiate sand - Element -> MovableSolid -> Sand
                    //Will need to implement instantiating the correct element in the future
                    Element sand = new Sand();
                    ElementMatrix.elements[x, y] = sand;

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
