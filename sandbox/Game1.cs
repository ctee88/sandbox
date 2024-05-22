using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using sandbox.Components;

namespace sandbox
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ElementMatrix _elementMatrix = new ElementMatrix();
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(8.333); //60 frames/sec = 16.667ms pre frame

            _graphics.PreferredBackBufferHeight = GameManager.screenHeight;
            _graphics.PreferredBackBufferWidth = GameManager.screenWidth;
            _graphics.ApplyChanges(); 
        }

        protected override void Initialize()
        {
            _elementMatrix.CreateElementMatrix();
            ColorConstants.InitialiseElementColors();
            //GuiManager.InitialiseGui();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            GuiManager.LoadTextures(Content);
            GuiManager.InitialiseGui();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Clear the screen
            if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                _elementMatrix.CreateElementMatrix();
            }

            //GuiManager.Update(gameTime, _graphics);
            GuiManager.SelectElement(gameTime, _graphics);
            Player.Update(gameTime, _graphics);
            _elementMatrix.Move();
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            SpriteBatch targetBatch = new SpriteBatch(GraphicsDevice);
            RenderTarget2D target = new RenderTarget2D(GraphicsDevice, ElementMatrix.size_x, ElementMatrix.size_y);
            GraphicsDevice.SetRenderTarget(target);

            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            _elementMatrix.DrawMatrix(ElementMatrix.elements, _spriteBatch);
            GuiManager.Draw(_spriteBatch, _graphics);

            _spriteBatch.End();

            // Set rendering back to the back buffer.
            GraphicsDevice.SetRenderTarget(null);

            // Render target to back buffer.
            targetBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            targetBatch.Draw(target, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
            targetBatch.End();

            base.Draw(gameTime);
        }
    }
}