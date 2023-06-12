using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NeuralNetIntro;
using System;
using System.Collections.Generic;

namespace FlappyBird
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        
        FlappyBirdGame birdGames;
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
            birdGames = new FlappyBirdGame(100, new ActivationFunction(ActivationFunction.TanH, ActivationFunction.TanH_deriv), new int[] { 2, 2, 1 }, Content.Load<Texture2D>("turtle"), new Vector2(50, 10), Color.White, pipetexture, pipeRtexture, GraphicsDevice);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
           // Texture2Dplayer = new Player(Content.Load<Texture2D>("turtle"), new Vector2(50, 10), Color.White);
            pipetexture = Content.Load<Texture2D>("pipe");
            pipeRtexture = Content.Load<Texture2D>("pipeR");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            birdGames.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            birdGames.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}