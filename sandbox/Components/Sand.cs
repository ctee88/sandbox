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
        public Sand() 
        {
            //Different vels and other forces specific to sand later on in development will be
            //constructed within this class (same for other elements)
            //isGaseous = false;
            maxVelY = 4;
            string elementName = typeof(Sand).Name;
            color = ColorConstants.GetElementColor(elementName);
        }
    }
}
