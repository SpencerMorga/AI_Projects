using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using NeuralNetworks;
using NeuralNetIntro;
using System;

namespace SineWaveVisualizer
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        double[] y_position = new double[628];
        NeuralNetwork net;
        SpriteFont hakop;

        Func<double, double, double> mse = (actual, expected) => Math.Pow(expected - actual, 2);

        Func<double, double, double> mseDeriv = (actual, expected) => -2 * (expected - actual);

        double[][] input = new double[628][];

        double[][] output = new double[628][];

        double error;
        double batchSize = 10; //mine: 1 gmr: 1000 optimal: 10
        double learningRate = 0.002; //mine = 0.002 gmr: 0.0005 optimal: 0.002
        double momentum = 0.4; //mine = 0.4 gmr: 0.01 momentum: 0.4
        Random random = new Random();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 628;
            _graphics.PreferredBackBufferHeight = 628;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()//24446666
        {
            // TODO: Add your initialization logic here
            net = new NeuralNetwork(new ActivationFunction(ActivationFunction.TanH, ActivationFunction.TanH_deriv), new ErrorFunction(mse, mseDeriv), new int[] { 1, 2, 2, 1 }); //mine: 1, 2, 2, 1 gmr: 1, 5, 5, 5, 5, 1
            net.Randomize(random, -1, 1);

            for (int i = 0; i < 628; i++)
            {
                input[i] = new double[] { i / 100.0};
            }

            for (int i = 0; i < 628; i++)
            {
                output[i] = new double[] { 2 };//Math.Sin(i / 100.0)};
            }
            error = 0;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            hakop = Content.Load<SpriteFont>("hakoplosesinfantasyfootball");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            error = net.BatchTrain(input, output, (int)(batchSize), learningRate, momentum);
            for (int i = 0; i < 628; i++)
            {
                y_position[i] = Math.Round(net.Compute(input[i])[0], 3);
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            
            spriteBatch.Begin();

            for (int i = 0; i < 628; i++)
            {
                spriteBatch.DrawPoint(i, 314 + (int)(y_position[i] * -100), Color.Black, 2);
            }
            spriteBatch.DrawString(hakop, error.ToString(), new Vector2(0, 0), Color.Black);

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}