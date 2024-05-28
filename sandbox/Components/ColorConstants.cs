using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace sandbox.Components
{
    public static class ColorConstants
    {
        private static Random random = new Random();
        public static Dictionary<string, List<Color>> colorMap = new Dictionary<string, List<Color>>();

        //Movable Solids
        private static Color SAND_1 = new Color(255, 255, 0);
        private static Color SAND_2 = new Color(236, 214, 22);//(233 / 255f, 252 / 255f, 90 / 255f);
        private static Color SAND_3 = new Color(244, 235, 54);//178 / 255f, 201 / 255f, 6 / 255f);

        //Immovable Solids
        private static Color WOOD_1 = new Color(136, 104, 20);//170/255f, 130/255f, 25/255f);//60 / 255f, 40 / 255f, 20 / 255f);

        //Liquids
        private static Color WATER_1 = new Color(28, 86, 234);
        //private static Color OIL_1 = new Color(55 / 255f, 50 / 255f, 48 / 255f);

        //Gases
        private static Color SMOKE_1 = new Color(86, 89, 88);

        public static void InitialiseElementColors()
        {
            colorMap.Add("Sand", new List<Color> { SAND_1 , SAND_2, SAND_3 });
            colorMap.Add("Water", new List<Color> { WATER_1 });
            colorMap.Add("Wood", new List<Color> { WOOD_1 });
            colorMap.Add("Smoke", new List<Color> { SMOKE_1 });

        }

        //Make another method for single colour elements? Instead of doing this random stuff
        public static Color GetElementColor(string elementName)
        {
            List<Color> colors = colorMap[elementName];
            int randomNum = random.Next(0, colors.Count);

            return colors[randomNum];
        }
    }
}
