using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace sandbox
{
    public static class Storage
    {   
        //Mixture of Element, GameManager, ElementMatrix fields
        public static GraphicsDeviceManager GDM;
        public static ContentManager CM;
        public static SpriteBatch SB;
        public static Game1 game;
        //public static Texture2D texture;
        public static Rectangle drawRec = new Rectangle(0, 0, 1, 1);
        public static Particle[,] particles;
    }

    public class Particle
    {
        public int x_velocity, y_velocity;
        //public Color color;
        public Texture2D texture;
    }

    public class Game1 : Game
    {
        public const int size_x = 256;
        public const int size_y = size_x;
        public int timer = 0;
        public Game1()
        {
            Storage.GDM = new GraphicsDeviceManager(this);
            Storage.GDM.GraphicsProfile = GraphicsProfile.HiDef;
            Storage.CM = Content;
            Storage.game = this;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(8.333); //60 frames/sec = 16.667ms pre frame
        }

        protected override void Initialize()
        {
            Storage.GDM.PreferredBackBufferWidth = 800;
            Storage.GDM.PreferredBackBufferHeight = 800;
            Storage.GDM.ApplyChanges();
            Storage.particles = new Particle[size_x, size_y];
            //This is the grid? [ Xi, Yi ] where i is the specific cell? 
            //n cells/particles where n is the size of x?
            //This initialises all the cells to be empty initially
            for (int x = 0; x < size_x; x++)
            //rows
            {
                for (int y = 0; y < size_y; y++)
                //columns
                {
                    //cell [row_x, column_y]
                    Storage.particles[x, y] = null;
                }
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Storage.SB = new SpriteBatch(GraphicsDevice);

            //List of sand colors
            //var sandColors = new List<Color> { Color.Yellow, Color.YellowGreen, Color.GreenYellow };
            //var random = new Random();
            //int index = random.Next(sandColors.Count);

            //if (Storage.texture == null)
            //{
            //    // Texture used for drawing, will be scaled later.
            //    Storage.texture = new Texture2D(Storage.GDM.GraphicsDevice, 1, 1);
                
            //    Storage.texture.SetData<Color>(new Color[] { sandColors[index] });
            //    //Storage.texture.SetData<Color>(new Color[] { Color.Yellow });

            //}

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            for (int x = 0; x < size_x; x++)
            {
                for (int y = 0; y < size_y; y++)
                {
                    //Iterate through the grid, assign particle var at given (x, y) in particles grid
                    //Check whether it is empty (null)
                    var particle = Storage.particles[x, y];
                    if (particle != null)
                    {
                        particle.y_velocity += 1;
                        if (MathF.Abs(particle.y_velocity) > 1)
                        {
                            particle.y_velocity = 1 * Math.Sign(particle.y_velocity);
                        }

                        particle.x_velocity += 0;
                        if (MathF.Abs(particle.x_velocity) > 1)
                        {
                            particle.x_velocity = 1 * Math.Sign(particle.x_velocity);
                        }

                        int next_x = x + particle.x_velocity;
                        int next_y = y + particle.y_velocity;
                        if (!find_collision(next_x, next_y))
                        {
                            // Go there.
                        }
                        //What if x - 1 and x+1 are always empty? need some random implementation?
                        else if (!find_collision(next_x - 1, next_y))
                        {
                            next_x -= 1;
                        }
                        else if (!find_collision(next_x + 1, next_y))
                        {
                            next_x += 1;
                        }
                        // Simulate water. Move side to side if you can.

                        //else if (!find_collision(next_x - 1, y))
                        //{
                        //    next_x -= 1;
                        //    next_y = y;
                        //    particle.y_velocity = 0;
                        //}
                        //else if (!find_collision(next_x + 1, y))
                        //{
                        //    next_x += 1;
                        //    next_y = y;
                        //    particle.y_velocity = 0;
                        //}

                        else
                        {
                            // Can't move.
                            next_x = x;
                            particle.x_velocity = 0;

                            next_y = y;
                            particle.y_velocity = 0;
                        }
                        //Move particle to updated (x, y), set previous cell as empty
                        Storage.particles[x, y] = null;
                        x = next_x;
                        y = next_y;
                        Storage.particles[x, y] = particle;
                    }
                }
            }

            // Delay how fast you can create particles.
            timer += gameTime.ElapsedGameTime.Milliseconds;
            var mouse_state = Mouse.GetState();
            if (mouse_state.LeftButton == ButtonState.Pressed && timer > 1) //20
            {
                timer = 0;
                var x = mouse_state.Position.X * size_x / Storage.GDM.PreferredBackBufferWidth;
                var y = mouse_state.Position.Y * size_y / Storage.GDM.PreferredBackBufferHeight;

                //Only create a particle in this grid location if nothing is currently there.
                //Will need some helper methods in Element class? To generate the correct element and get
                //the correct color
                if (x >= 0 && x < size_x && y >= 0 && y < size_y && Storage.particles[x, y] == null)
                {
                    Particle particle = new Particle();
                    Storage.particles[x, y] = particle;

                    //Generate random shade of yellow for sand
                    //Separate class to manage color constants
                    //+ random functionatlity for color determination for respective elements
                    var sandColors = new List<Color> { 
                        new Color(255/255f, 255/255f, 0/255f), 
                        new Color(178/255f, 201/255f, 6/255f), 
                        new Color(233/255f, 252/255f, 90/255f)
                        };
                    var random = new Random();
                    int index = random.Next(sandColors.Count);

                    if (particle.texture == null)
                    {
                        // Texture used for drawing, will be scaled later.
                        particle.texture = new Texture2D(Storage.GDM.GraphicsDevice, 1, 1);

                        particle.texture.SetData<Color>(new Color[] { sandColors[index] });
                        //Storage.texture.SetData<Color>(new Color[] { Color.Yellow });

                    }

                }
            }

            base.Update(gameTime);
        }

        bool find_collision(int x, int y)
        {
            if (y >= size_x)
            {
                return true;
            }
            if (x <= -1 || x >= size_y)
            {
                return true;
            }
            return Storage.particles[x, y] != null;
        }

        protected override void Draw(GameTime gameTime)
        {
            SpriteBatch targetBatch = new SpriteBatch(GraphicsDevice);
            RenderTarget2D target = new RenderTarget2D(GraphicsDevice, size_x, size_y);
            GraphicsDevice.SetRenderTarget(target);

            GraphicsDevice.Clear(Color.Black);

            // Storage.SB.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            Storage.SB.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

            int depth = 0;
            for (int x = 0; x < size_x; x++)
            {
                for (int y = 0; y < size_y; y++)
                {
                    depth += 1;
                    Particle particle = Storage.particles[x, y];
                    if (particle != null)
                    {
                        Vector2 pos = new Vector2(x, y);
                        // Draw each particle as a sprite.
                        Storage.SB.Draw(particle.texture,
                            pos,
                            Storage.drawRec,
                            Color.White,
                            0,
                            Vector2.Zero,
                            1.0f, // Scale
                            SpriteEffects.None,
                            depth * 0.00001f);
                    }
                }
            }

            Storage.SB.End();

            // Set rendering back to the back buffer.
            GraphicsDevice.SetRenderTarget(null);

            // Render target to back buffer.
            targetBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            // Scale target from 64x64 to 512x512.
            targetBatch.Draw(target, new Rectangle(0, 0, Storage.GDM.PreferredBackBufferWidth, Storage.GDM.PreferredBackBufferHeight), Color.White);
            targetBatch.End();

            base.Draw(gameTime);
        }
    }
}