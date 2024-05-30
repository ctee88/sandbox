using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace sandbox.Components
{
    public abstract class Element
    {
        public Color color; 
        public Texture2D texture;
        public GraphicsDeviceManager graphics;

        public Vector2 pos = new Vector2(0, 0);
        //public float velX = 0; don't think velX will be used
        public float velY = 0f;
        public float maxVelY; 
        public bool isFalling = true;
        public int lifeSpan;
        public int lifeRemaining;
        
        protected Element(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
        }

        //TODO: Figure out how to set the Color and Texture in the Element constructor (using the ElementName/ElementType??)
        //Ideally, want this set in this base class instead of all the child classes
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

        //MovableSolids and Liquids should move through gases
        //public bool CanMoveThrough(int x, int y)
        //{
        //    return ((ElementMatrix.IsEmptyCell(x, y)) || (ElementMatrix.elements[x, y] is Gas));
        //}
    }
}
