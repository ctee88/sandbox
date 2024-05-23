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
        public ElementType _elementType { get; private set; }
        //public bool _isHovering;
        //public bool _isSelected = false;
        public GuiElement(Rectangle rect, Texture2D texture, ElementType elementType)
        {
            _destinationRect = rect;
            _texture = texture;
            _elementType = elementType;
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(_texture, _destinationRect, color);
        }

        public void DrawElementName(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.DrawString(
                GuiManager._font,
                _elementType.ToString().ToUpper(),
                new Vector2(0, 8),
                Color.White,
                0,
                Vector2.Zero,
                1f,
                SpriteEffects.None,
                0.0001f);
        }
    }   
}
