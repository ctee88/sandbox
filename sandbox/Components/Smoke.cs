using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sandbox.Components
{
    public class Smoke : Gas
    {
        public Smoke(GraphicsDeviceManager graphics) : base(graphics)
        {
            lifeSpan = 400 + (int)(400 * new Random().NextDouble());
            lifeRemaining = lifeSpan;
            maxVelY = 0.2f;
            elementName = typeof(Smoke).Name;
            color = ColorConstants.GetElementColor(elementName);
            SetElementTexture(graphics);
        }

        public override void UpdateElementLifeRemaining(int x, int y)
        {
            if (lifeRemaining <= 0)
            {
                ElementMatrix.Kill(x, y);
            }
            else
            {
                lifeRemaining = Math.Abs(lifeRemaining - 1);
                UpdateColor();
            }
        }

        public void UpdateColor()
        {
            float percent = (float)lifeRemaining / lifeSpan;
            color = new Color(color.R, color.G, color.B, (byte)(255.0f * percent));
        }
        public override bool Corrode()
        {
            return false;
        }
    }
}
