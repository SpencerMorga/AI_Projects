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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            KeyboardState ks = Keyboard.GetState();
            pipespan += gameTime.ElapsedGameTime;
            if (pipespan > TimeSpan.FromMilliseconds(1000))
            {
                pipes.Add(new Pipe(pipetexture, new Vector2(GraphicsDevice.Viewport.Width - 10, 50), Color.White));
                pipespan = TimeSpan.Zero;
            }
            player.Update(gameTime, ks);
            foreach (Pipe pipe in pipes)
            {
                pipe.Update(gameTime);
            }
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