﻿using Microsoft.Xna.Framework;
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
            maxVelY = 2;
            string elementName = typeof(Water).Name;
            color = ColorConstants.GetElementColor(elementName);
            SetElementTexture(graphics);
        }
    }
}
