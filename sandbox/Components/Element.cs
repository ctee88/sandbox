using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics;

namespace sandbox.Components
{
    public abstract class Element
    {
        public Color color; 
        public Texture2D texture;
        public GraphicsDeviceManager graphics;

        public Vector2 pos = new Vector2(0, 0);
        //public float velX = 0; will need velX for more realistic falling physics
        public float velY = 0f;
        public float maxVelY; 
        public bool isFalling = true;
        public int lifeSpan;
        public int lifeRemaining;
        public bool burning = false;
        public bool isIgnited = false;
        public bool isFlammable;
        public int heatResistance;
        public int heatDamage;
        
        protected Element(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
        }

        //TODO: Figure out how to set the Color and Texture in the Element constructor (using the ElementName/ElementType??)
        //Ideally, want this set in this base class instead of all the child classes
        //Currently setting Color from child class Name, then setting Texture from this value.
        public void SetElementTexture(GraphicsDeviceManager graphics)
        {
            texture = new Texture2D(graphics.GraphicsDevice, 1, 1);
            texture.SetData<Color>(new Color[] { color });
        }
        public abstract int[] UpdateElementPosition(int x, int y, Element element, bool leftOrRight);

        public virtual void UpdateElementLifeRemaining(int x, int y)
        {
            //Do nothing, unless an Element has behaviour involving lifespan (eg smoke, fire)
            //Seems hacky... what am I doing??
        }
        public void UpdateElementVelocity()
        {
            float newVelY = velY + ElementMatrix.gravity;

            if (Math.Abs(newVelY) > maxVelY)
            {
                newVelY = Math.Sign(newVelY) * maxVelY;
            }
            velY = newVelY;
        }

        public void ResetElementVelocity()
        {
            velY = 0;
        }

        public int GetUpdateCount()
        {
            float abs = Math.Abs(velY);
            int floored = (int)Math.Floor(abs);
            float mod = abs - floored;

            return floored + (new Random().NextDouble() < mod ? 1 : 0);
        }
        public void CheckIfFalling() 
        {
            UpdateElementVelocity();
            isFalling = velY != 0;
        }

        public void ReceiveHeat()
        {
            heatResistance -= heatDamage;
            CheckIfBurning();
        }

        public void CheckIfBurning()
        {
            if (heatResistance <= 0 && !isIgnited)
            {
                burning = true;
                isIgnited = true;
                //Needs Cinder color initially to set texture to burning when a particle starts burning
                GetIgnitedColor();
                SetElementTexture(graphics);
            }
            if (burning)
            {
                lifeRemaining -= heatDamage;
                //Keep cycling Cinder colors
                GetIgnitedColor();
            }
        }
        //TODO: Maybe check color before setting so that the new color is not the same as the old?
        public void GetIgnitedColor()
        {
            color = ColorConstants.GetElementColor("Cinder");
        }

        //What if we use an instance of element (Cinder) to apply heat instead of Wood.heatDamage?
        public void ApplyHeatToNeighbours(int ElementX, int ElementY)
        {
            for (int i = ElementX - 1; i <= ElementX + 1; i++)
            {
                for (int j = ElementY - 1; j <= ElementY + 1; j++)
                {
                    if (ElementMatrix.IsWithinBounds(i, j) && !ElementMatrix.IsEmptyCell(i, j))
                    {
                        Element neighbour = ElementMatrix.elements[i, j];
                        if (neighbour.isFlammable)
                        {
                            neighbour.ReceiveHeat();
                        }
                    }
                }
            }
        }
    }
}
