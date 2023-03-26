using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace FlappyBird
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Player player;
        List<Pipe> pipes = new List<Pipe>();
        Texture2D pipetexture;
        Texture2D pipeRtexture;
        TimeSpan pipespan;
        double currentTime = 0;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
           // Texture2D
            player = new Player(Content.Load<Texture2D>("turtle"), new Vector2(50, 10), Color.White);
            pipetexture = Content.Load<Texture2D>("pipe");
            pipeRtexture = Content.Load<Texture2D>("pipeR");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            KeyboardState ks = Keyboard.GetState();
            Random random = new Random();

            pipespan += gameTime.ElapsedGameTime;

            if (pipespan > TimeSpan.FromMilliseconds(2000))
            {
                pipes.Add(new Pipe(pipetexture, new Vector2(GraphicsDevice.Viewport.Width, -random.Next(1, 150)), Color.White, pipeRtexture));
                pipespan = TimeSpan.Zero;
                
            }

            

            foreach (Pipe pipe in pipes)
            {
                if (player.hitbox.Intersects(pipe.hitbox)) 
                {
                    break;
                }
                pipe.Update(gameTime);
            }
            player.Update(gameTime, ks);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            player.Draw(_spriteBatch);
            foreach (Pipe pipe in pipes)
            {
                pipe.Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}