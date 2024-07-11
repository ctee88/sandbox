using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace sandbox.Components
{
    //TODO: Clean up Element Child classes - LifeSpan only needed for Smoke or other elements where the colour changes over their lifetimes
    public abstract class Element
    {
        public Color color;
        public Texture2D texture;
        public string elementName;
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
        public int heatResistance; //Buffer before an Element starts burning
        public int heatDamage; //Amount of damage an Element takes from heat
        public int corrosionDamage;

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

        public virtual void UpdateElementLifeRemaining(int x, int y) { }
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
        //Rename/redesign to apply all possible effects to neighbours?
        //Make separate method to get neighbours (as the neighbours needs to be used in multiple methods)
        // - This method needs to return a list of neighbours
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

                        if (neighbour is Water) 
                        {
                            //TODO: - Currently kills burning Element - so deletes burning wood... Need to fix this but not sure how?
                            //      - Cooling functionality isn't that realistic
                            //      - Need to redesign Element, ElementMatrix, Player, Water and Cinder
                            //          - This controls Water's behaviour when interacting with Cinder, shouldn't this be in the Water class?
                            //      - Liquids need to put out fire but the gas which is produced from these interactions will be dependent on the liquid
                            ElementMatrix.Kill(ElementX, ElementY);
                            ElementMatrix.elements[i, j] = Player.CreateElement(typeof(Steam), graphics);
                        }
                    }
                }
            }
        }
        public virtual void ActOnNeighbour(int ElementX, int ElementY) { }

        public virtual bool Corrode()
        {
            lifeRemaining -= corrosionDamage;
            return true;
        }
        //Combine Apply methods into one. Apply the effects based on current element and neighbour?
        //public void ApplyCoolingToNeighbours(int ElementX, int ElementY)
        //{
        //    for (int i = ElementX - 1; i <= ElementX + 1; i++)
        //    {
        //        for (int j = ElementY - 1; j <= ElementY + 1; j++)
        //        {
        //            if (ElementMatrix.IsWithinBounds(i, j) && !ElementMatrix.IsEmptyCell(i, j))
        //            {
        //                Element neighbour = ElementMatrix.elements[i, j];
        //                if (neighbour is Cinder)
        //                {
        //                    ElementMatrix.elements[i, j] = Player.CreateElement(typeof(Steam), graphics);
        //                }
        //                else if (neighbour.burning)
        //                {
        //                    burning = false;
        //                    neighbour.color = ColorConstants.GetElementColor(neighbour.elementName);
        //                    neighbour.SetElementTexture(graphics);
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
