using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sandbox.Components
{
    public class Oil : Liquid
    {
        public Oil(GraphicsDeviceManager graphics) : base(graphics)
        {
            maxVelY = 1f;
            elementName = typeof(Oil).Name;
            color = ColorConstants.GetElementColor(elementName);
            SetElementTexture(graphics);
            lifeRemaining = 2000 + (int)(2000 * new Random().NextDouble());
            isFlammable = true;
            heatResistance = 5;
            heatDamage = 1;
        }

        public override void UpdateElementLifeRemaining(int x, int y)
        {
            if (burning)
            {
                ApplyHeatToNeighbours(x, y);
            }

            if (lifeRemaining <= 0)
            {
                ElementMatrix.Kill(x, y);
            }
        }
        public override bool Corrode()
        {
            return false;

        }
    }
}
