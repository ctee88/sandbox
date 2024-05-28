using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sandbox.Components
{
    public class Water : Liquid
    {
        public Water() 
        {
            //isGaseous = false;
            maxVelY = 3;
            string elementName = typeof(Water).Name;
            color = ColorConstants.GetElementColor(elementName);
        }
    }
}
