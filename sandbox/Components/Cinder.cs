using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sandbox.Components
{
    public class Cinder : MovableSolid
    {
        private string elementName = typeof(Cinder).Name;
        public Cinder(GraphicsDeviceManager graphics) : base(graphics)
        {
            lifeSpan = 600 + (int)(600 * new Random().NextDouble());
            lifeRemaining = lifeSpan;
            maxVelY = 1f;
            //Set initial color
            color = ColorConstants.GetElementColor(elementName);
            SetElementTexture(graphics);
        }

        public override void UpdateElementLifeRemaining(int x, int y)
        {
            if (lifeRemaining <= 0)
            {
                ElementMatrix.elements[x, y] = Player.CreateElement(typeof(Smoke), graphics);
            } else
            {
                lifeRemaining = Math.Abs(lifeRemaining - 1);
                UpdateColor();
            }
        }

        public void UpdateColor()
        {
            //Called as LifeRemaining decreases - create fire flickering effect
            color = ColorConstants.GetElementColor(elementName);
        }
    }
}
