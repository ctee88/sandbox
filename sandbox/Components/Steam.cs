using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sandbox.Components
{
    public class Steam : Gas
    {
        public static Random random = new Random();
        public Steam(GraphicsDeviceManager graphics) : base(graphics) 
        {
            lifeSpan = 600 + (int)(600 * new Random().NextDouble());
            lifeRemaining = lifeSpan;
            maxVelY = 0.2f;
            elementName = typeof(Steam).Name;
            color = ColorConstants.GetElementColor(elementName);
            SetElementTexture(graphics);
        }

        public override void UpdateElementLifeRemaining(int x, int y)
        {
            if (lifeRemaining <= 0)
            {
                int randomNum = random.Next(0, 4) <= 2 ? 1 : 0;
                if (randomNum == 1)
                {
                    ElementMatrix.Kill(x, y);
                } else
                {
                    ElementMatrix.elements[x, y] = Player.CreateElement(typeof(Water), graphics);
                }
            }
            lifeRemaining = Math.Abs(lifeRemaining - 1);
        }
    }
}
