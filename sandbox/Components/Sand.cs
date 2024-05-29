using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace sandbox.Components
{
    public class Sand : MovableSolid
    {
        public Sand(GraphicsDeviceManager graphics) : base(graphics)
        {
            maxVelY = 2;
            string elementName = typeof(Sand).Name;
            color = ColorConstants.GetElementColor(elementName);
            SetElementTexture(graphics);
        }
    }
}
