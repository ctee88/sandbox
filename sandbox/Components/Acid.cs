using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sandbox.Components
{
    public class Acid : Liquid
    {
        public Acid(GraphicsDeviceManager graphics) : base(graphics)
        {
            lifeSpan = 1200 + (int)(1200 * new Random().NextDouble());
            lifeRemaining = lifeSpan;
            maxVelY = 2f;
            elementName = typeof(Acid).Name;
            color = ColorConstants.GetElementColor(elementName);
            SetElementTexture(graphics);
        }

        public override void ActOnNeighbour(int ElementX, int ElementY)
        {
            for (int i = ElementX - 1; i <= ElementX + 1; i++)
            {
                for (int j = ElementY - 1; j <= ElementY + 1; j++)
                {
                    if (ElementMatrix.IsWithinBounds(i, j) && !ElementMatrix.IsEmptyCell(i, j))
                    {
                        Element neighbour = ElementMatrix.elements[i, j];
                        bool isCorrodible = neighbour.Corrode();
                        if (isCorrodible)
                        {
                            neighbour.UpdateElementLifeRemaining(i, j);
                            if (neighbour.lifeRemaining <= 0)
                            {
                                ElementMatrix.Kill(i, j);
                                ElementMatrix.elements[i, j] = Player.CreateElement(typeof(FlammableGas), graphics);
                            }
                        }
                    }
                }
            }
        }
    }
}
