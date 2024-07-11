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
            maxVelY = 2f;
            string elementName = typeof(Sand).Name;
            color = ColorConstants.GetElementColor(elementName);
            SetElementTexture(graphics);
            lifeRemaining = (int)(1000 + 1000 * new Random().NextDouble());
            corrosionDamage = 5;
        }

        //public override void UpdateElementLifeRemaining(int x, int y)
        //{
        //    //Currently can only be damaged by Acid, so can spawn GaseousAcid here for now... Ideally want some check
        //    //So that this functionality can be placed into the GaseousAcid class
        //    if (lifeRemaining <= 0)
        //    {
        //        ElementMatrix.elements[x, y] = Player.CreateElement(typeof(GaseousAcid), graphics);
        //    } else
        //    {
        //        ApplyHeatToNeighbours(x, y);
        //    }
        //}
    }
}
