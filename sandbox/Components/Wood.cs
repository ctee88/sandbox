using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace sandbox.Components
{
    public class Wood : Solid
    {
        public static Random random = new Random();
        public Wood(GraphicsDeviceManager graphics) : base(graphics)
        {
            string elementName = typeof(Wood).Name;
            color = ColorConstants.GetElementColor(elementName);
            SetElementTexture(graphics);
            lifeRemaining = 300 + (int)(300 * new Random().NextDouble());
            isFlammable = true;
            heatResistance = 200;
            heatDamage = 2;
        }

        public override void UpdateElementLifeRemaining(int x, int y)
        {
            if (burning)
            {
                ApplyHeatToNeighbours(x, y);
            }

            //Only heatDamage can affect lifeRemaining at the moment, will need to change this logic for
            //properties like corrosionDamage for Acid in the future.
            if (lifeRemaining <= 0)
            {
                int randomNum = random.Next(0, 10) < 8 ? 1 : 0;
                if (randomNum == 1)
                {
                    ElementMatrix.elements[x, y] = Player.CreateElement(typeof(Smoke), graphics);
                }
                else
                {
                    //Could this be a Wood particle with fire effects? Remove the fire effects on cooling
                    //On cooling - turns back into a Wood particle (with no fire effect).
                    ElementMatrix.elements[x, y] = Player.CreateElement(typeof(Cinder), graphics);
                }
            }
        }
    }
}
