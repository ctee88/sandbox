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
        private static Dictionary<string, List<Color>> colorMap = new Dictionary<string, List<Color>>();

        //Movable Solids
        private static Color SAND_1 = new Color(255, 255, 0);
        private static Color SAND_2 = new Color(236, 214, 22);//(233 / 255f, 252 / 255f, 90 / 255f);
        private static Color SAND_3 = new Color(244, 235, 54);//178 / 255f, 201 / 255f, 6 / 255f);

        private static Color CINDER_1 = new Color(242, 104, 67); //Blood Red (197, 77, 29);
        private static Color CINDER_2 = new Color(236, 135, 20);
        private static Color CINDER_3 = new Color(255, 255, 0);

        //Immovable Solids
        private static Color WOOD_1 = new Color(136, 104, 20);
        private static Color WOOD_2 = new Color(170, 130, 25);
        private static Color WOOD_3 = new Color(108, 72, 36);

        //Liquids
        private static Color WATER_1 = new Color(28, 86, 234);
        //private static Color OIL_1 = new Color(55 / 255f, 50 / 255f, 48 / 255f);
        //private static Color LAVA_1 = new Color(248, 128, 8); //new Color(248, 136, 8);

        //Gases
        private static Color SMOKE_1 = new Color(119, 123, 122);//new Color(102, 106, 105);//new Color(86, 89, 88);

        private static Color STEAM_1 = new Color(201, 202, 202);
        public static void InitialiseElementColors()
        {
            colorMap.Add("Sand", new List<Color> { SAND_1 , SAND_2, SAND_3 });
            colorMap.Add("Water", new List<Color> { WATER_1 });
            colorMap.Add("Wood", new List<Color> { WOOD_1, WOOD_2, WOOD_3 });
            colorMap.Add("Smoke", new List<Color> { SMOKE_1 });
            colorMap.Add("Cinder", new List<Color> { CINDER_1, CINDER_2, CINDER_3 });
            colorMap.Add("Steam", new List<Color> { STEAM_1 });
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
