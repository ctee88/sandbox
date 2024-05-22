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

        //Immovable Solids
        private static Color SAND_1 = new Color(255 / 255f, 255 / 255f, 0 / 255f);
        private static Color SAND_2 = new Color(233 / 255f, 252 / 255f, 90 / 255f);
        private static Color SAND_3 = new Color(178 / 255f, 201 / 255f, 6 / 255f);

        //Liquids
        private static Color WATER_1 = new Color(28 / 255f, 86 / 255f, 234 / 255f);

        public static void InitialiseElementColors()
        {
            colorMap.Add("Sand", new List<Color> { SAND_1 , SAND_2, SAND_3 });
            colorMap.Add("Water", new List<Color> { WATER_1 });
        }

        public static Color GetElementColor(string elementName)
        {
            List<Color> colors = colorMap[elementName];
            int randomNum = random.Next(0, colors.Count);

            return colors[randomNum];
        }
    }
}
