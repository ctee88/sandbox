using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sandbox.Components
{
    public class GuiElement
    {
        public Rectangle _destinationRect { get; private set; }
        public Texture2D _texture { get; private set; }
        public String _elementName { get; private set; }
        public bool _isHovering;
        //public bool _isSelected = false;
        public GuiElement(Rectangle rect, Texture2D texture, String elementName)
        {
            _destinationRect = rect;
            _texture = texture;
            _elementName = elementName;
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(_texture, _destinationRect, color);
        }

        public void DrawElementName(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.DrawString(GuiManager._font, _elementName.ToUpper(),
                new Vector2(0, 8),
                Color.White);
        }
    }   
}
