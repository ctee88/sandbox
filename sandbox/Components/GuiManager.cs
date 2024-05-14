using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sandbox.Components
{
    public static class GuiManager
    {
        private static List<GuiElement> guiElements = new List<GuiElement>();
        private static Texture2D sand;
        private static Texture2D water;

        public static void Load(ContentManager content)
        {
            //Need to use these textures somehow in the initialise/draw methods
            sand = content.Load<Texture2D>("sand");
            water = content.Load<Texture2D>("water");
        }

        public static void InitialiseGui()
        {
            guiElements.Add(new GuiElement(new Rectangle(2, 1, 6, 6), sand));
            guiElements.Add(new GuiElement(new Rectangle(10, 1, 6, 6), water));
        }

        public static void Update(GameTime gametime)
        {
            //Handle hovering + clicking logic here + displaying elementName centered in the screen
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var guiElement in guiElements)
            {
                guiElement.Draw(spriteBatch);
            }
        }
    }
}
