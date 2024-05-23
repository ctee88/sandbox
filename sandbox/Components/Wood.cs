using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sandbox.Components
{
    public class Wood : Solid
    {
        public Wood() 
        {
            string elementName = typeof(Wood).Name;
            color = ColorConstants.GetElementColor(elementName);
        }
    }
}
