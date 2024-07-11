using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sandbox.Components
{
    public class FlammableGas : Gas
    {
        public FlammableGas(GraphicsDeviceManager graphics) : base(graphics)
        {
            elementName = typeof(FlammableGas).Name;
            color = ColorConstants.GetElementColor(elementName);
            SetElementTexture(graphics);
            lifeSpan = 800 + (int)(800 * new Random().NextDouble());
            lifeRemaining = lifeSpan;
            maxVelY = 0.2f;
            isFlammable = true;
            heatResistance = 10;
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
            } //else
            //{
            //    lifeRemaining = Math.Abs(lifeRemaining - 1);
            //}
        }
    }
}
