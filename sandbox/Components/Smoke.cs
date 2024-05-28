using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sandbox.Components
{
    public class Smoke : Gas
    {
        public Smoke() 
        {
            //isGaseous = true;
            maxVelY = 0.2f;
            string elementName = typeof(Smoke).Name;
            color = ColorConstants.GetElementColor(elementName);
        }
    }
}
