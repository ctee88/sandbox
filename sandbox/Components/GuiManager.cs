using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sandbox.Components
{
    public static class GuiManager
    {
        //Mouse fields - maybe abstract this to separate class and have Player use the same fields?
        private static MouseState _currentMouse;
        private static MouseState _previousMouse;
        public static SpriteFont _font;

        private static List<GuiElement> guiElements = new List<GuiElement>();
        private static Texture2D _sand;
        private static Texture2D _water;

        //Default selection
        private static string selectedElementName = "Sand";
        public static void LoadTextures(ContentManager content)
        {
            _font = content.Load<SpriteFont>("File");
            _sand = content.Load<Texture2D>("sand");
            _water = content.Load<Texture2D>("water");
        }

        public static void InitialiseGui()
        {
            //Scale the x,y and width+height with ElementMatrix.size_x / graphics.PreferredBackBufferWidth?
            guiElements.Add(new GuiElement(new Rectangle(2, 1, 6, 6), _sand, "Sand"));
            guiElements.Add(new GuiElement(new Rectangle(10, 1, 6, 6), _water, "Water"));
        }

        public static string GetSelectedElementName()
        {
            return selectedElementName;
        }
        public static void SelectElement(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.Position.X * ElementMatrix.size_x / graphics.PreferredBackBufferWidth,
                                                _currentMouse.Position.Y * ElementMatrix.size_y / graphics.PreferredBackBufferHeight,
                                                1, 1);

            //If no element selected: Keep checking all guiElements for an intersection with the mouse
            //If element selected (eg sand): GUI should keep track of selectedelement (state/_isSelected/_isActive flag??) so that the player
            //can only spawn this element UNTIL another element is selected from the GUI

            foreach (var guiElement in guiElements)
            {
                guiElement._isHovering = false;
                //guiElement._isSelected = false;
                if (guiElement._destinationRect.Intersects(mouseRectangle))
                {
                    guiElement._isHovering = true;
                    if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        //guiElement._isSelected = true;
                        selectedElementName = guiElement._elementName;

                        Debug.WriteLine("Intersect test working");
                        //return guiElement._elementName;
                    }
                }
            }
            //return "Test string";
        }

        public static void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            foreach (var guiElement in guiElements)
            {
                var color = Color.White;

                if (guiElement._isHovering) //|| _isSelected? to keep an element grey once selected
                {
                    color = Color.Gray;
                    guiElement.Draw(spriteBatch, color);
                    guiElement.DrawElementName(spriteBatch, graphics);
                }
                else 
                { 
                    guiElement.Draw(spriteBatch, color); 
                }
                
            }
        }
    }
}

//Click event - what happens when the player clicks a box?
//1) Box should turn grey (or highlight different colour) to represent selected element - maybe handled in Draw()
//2) ElementName should be displayed (x-centered, y-slightly under element boxes) showing the current selected element
//3) Player should only be able to spawn the current selected element - will need to pass this element
//      to Player.Update() so that the correct element is instantiated and spawned in the matrix (this method no longer void)
//Other notes - Is gametime a required paremeter here?
//            - Player should not be able to spawn elements when clicking a box, maybe handle this in Player()

