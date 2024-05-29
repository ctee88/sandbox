using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
        private static Texture2D _wood;
        private static Texture2D _smoke;
        private static Texture2D _cinder;

        //private static string hoveredElementName;
        //Default selection
        private static ElementType selectedElementType = ElementType.Sand;
        public static void LoadTextures(ContentManager content)
        {
            _font = content.Load<SpriteFont>("File");
            _sand = content.Load<Texture2D>("sand");
            _water = content.Load<Texture2D>("water");
            _wood = content.Load<Texture2D>("wood");
            _smoke = content.Load<Texture2D>("smoke");
            _cinder = content.Load<Texture2D>("cinder");
        }

        public static void InitialiseGui()
        {
            //Scale the x,y and width+height with ElementMatrix.size_x / graphics.PreferredBackBufferWidth?
            guiElements.Add(new GuiElement(new Rectangle(2, 1, 6, 6), _sand, ElementType.Sand));
            guiElements.Add(new GuiElement(new Rectangle(10, 1, 6, 6), _water, ElementType.Water));
            guiElements.Add(new GuiElement(new Rectangle(18, 1, 6, 6), _wood, ElementType.Wood));
            guiElements.Add(new GuiElement(new Rectangle(26, 1, 6, 6), _smoke, ElementType.Smoke));
            guiElements.Add(new GuiElement(new Rectangle(34, 1, 6, 6), _cinder, ElementType.Cinder));
        }

        public static ElementType GetSelectedElementName()
        {
            return selectedElementType;
        }
        public static void SelectElement(GraphicsDeviceManager graphics)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.Position.X * ElementMatrix.size_x / graphics.PreferredBackBufferWidth,
                                                _currentMouse.Position.Y * ElementMatrix.size_y / graphics.PreferredBackBufferHeight,
                                                1, 1);

            //hoveredElementName = string.Empty;

            foreach (var guiElement in guiElements)
            {
                //guiElement._isHovering = false;
                //guiElement._isSelected = false;
                if (guiElement._destinationRect.Intersects(mouseRectangle))
                {
                    //guiElement._isHovering = true;
                    //hoveredElementName = guiElement._elementName;

                    if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        selectedElementType = guiElement._elementType;
                        Debug.WriteLine("Intersect test working");
                    }
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            foreach (var guiElement in guiElements)
            {
                var color = Color.White;

                if (guiElement._elementType == selectedElementType)
                {
                    color = Color.Gray;
                    guiElement.DrawElementName(spriteBatch, graphics);
                }

                guiElement.Draw(spriteBatch, color);
            }
        }
    }
}


