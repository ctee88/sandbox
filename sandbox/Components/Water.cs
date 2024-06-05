using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sandbox.Components
{
    public class Water : Liquid
    {
        public Water(GraphicsDeviceManager graphics) : base(graphics)
        {
            maxVelY = 2f;
            elementName = typeof(Water).Name;
            color = ColorConstants.GetElementColor(elementName);
            SetElementTexture(graphics);
            //lifeRemaining = 1;
            //isFlammable = true;
            //heatResistance = 0;
            //heatDamage = 1;
        }
    }
}
