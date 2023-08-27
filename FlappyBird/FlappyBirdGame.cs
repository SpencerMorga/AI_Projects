using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using NeuralNetIntro;
using NeuralNetworks;

namespace FlappyBird
{
    internal class FlappyBirdGame
    {
        //5/28/23: next time incorporate genetic learning into this class
        GraphicsDevice graphics;


        Player[] players;
        List<Pipe> pipes = new List<Pipe>();

        Texture2D pipetexture;
        Texture2D pipeRtexture;
        TimeSpan pipespan;
        int speed = 1; 
        bool isDown = false;
        bool isDown2 = false;

        KeyboardState prevks;

        bool shouldIncScore = false;
        bool allDead = true;

        Pipe nearestPipe;
        Random random = new Random();

        NeuralNetwork[] nets;

        GeneticLearning geneticLearning;
        public FlappyBirdGame(int playersCount, ActivationFunction activation, int[] neuronsPerLayer, Texture2D image, Vector2 position, Color color, Texture2D Pipe, Texture2D PipeR, GraphicsDevice graphics)
        {
            players = new Player[playersCount];
            for (int i = 0; i < playersCount; i++)
            {
                players[i] = new Player(image, position, color);
            }

            geneticLearning = new GeneticLearning(0.1, random);
            nets = new NeuralNetwork[playersCount];
            for (int i = 0;i < playersCount; i++)
            {
                nets[i] = new NeuralNetwork(activation, neuronsPerLayer);
            }

            pipes = new List<Pipe>();
            pipetexture = Pipe;
            pipeRtexture = PipeR;
        
            this.graphics = graphics;

            pipes.Add(new Pipe(pipetexture, new Vector2(graphics.Viewport.Width -200, -random.Next(1, 150)), Color.White, pipeRtexture));
            pipespan = TimeSpan.Zero;
            nearestPipe = pipes[0];
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            
            

            if (ks.JustPressed(prevks, Keys.Up))
            {
                isDown = true;
                speed++;
            }

            if (ks.JustPressed(prevks, Keys.Down))
            {
                isDown2 = true;
                speed--;
            }
           

            for (int k = 0; k < speed; k++)
            {
                pipespan += gameTime.ElapsedGameTime;

                if (pipespan > TimeSpan.FromMilliseconds(2000))
                {
                    pipes.Add(new Pipe(pipetexture, new Vector2(graphics.Viewport.Width, -random.Next(1, 150)), Color.White, pipeRtexture));
                    pipespan = TimeSpan.Zero;
                    shouldIncScore = true;
                }
                allDead = true;

                for (int i = 0; i < players.Length; i++)
                {
                    if (players[i].position.Y > graphics.Viewport.Height || players[i].position.Y < 0)
                    {
                        players[i].isDead = true;
                    }

                    if (shouldIncScore)
                    {
                        players[i].score++;
                    }

                    foreach (Pipe pipe in pipes)
                    {

                        if (players[i].hitbox.Intersects(pipe.hitbox) || players[i].hitbox.Intersects(pipe.hitboxR))
                        {
                            players[i].isDead = true;
                        }
                    }

                    if (!players[i].isDead)
                    {
                        players[i].Update(gameTime, nets[i].Compute(new double[] { (nearestPipe.getGapCenter().Item1 - players[i].position.X), (nearestPipe.getGapCenter().Item2 - players[i].position.Y) })[0]);
                        allDead = false;
                    }


                }

                if (allDead)
                {
                    Reset();
                }

                foreach (Pipe pipe in pipes)
                {
                    if (getPipeDist(pipe) < getPipeDist(nearestPipe))
                    {
                        nearestPipe = pipe;
                    }
                    pipe.Update(gameTime);
                }
            }

            prevks = ks;
            
        }

        public void Reset()
        {
            allDead = false;
            
            (NeuralNetwork, int)[] population = new (NeuralNetwork, int)[players.Length];
            for (int i = 0; i < population.Length; i++)
            {
                population[i] = (nets[i], players[i].score);
            }

            geneticLearning.Train(population);

            pipes.Clear();

            foreach (Player bird in players)
            {
                bird.isDead = false;
                bird.score = 0;
                bird.position = new Vector2(50, 10);
                bird.velocity = 0;
            }

            pipes.Add(new Pipe(pipetexture, new Vector2(graphics.Viewport.Width - 200, -random.Next(1, 150)), Color.White, pipeRtexture));
            pipespan = TimeSpan.Zero;
            nearestPipe = pipes[0];
        }

        public double getPipeDist(Pipe pipe)
        {
            return Math.Sqrt(Math.Pow(pipe.getGapCenter().Item1 - players[0].position.X, 2) + Math.Pow(pipe.getGapCenter().Item2 - players[0].position.Y, 2));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            graphics.Clear(Color.Chartreuse);

            spriteBatch.Begin();

            for (int i = 0; i < players.Length; i++)
            {
                if (!players[i].isDead)
                {
                   players[i].Draw(spriteBatch);
                }
            }
                
            foreach (Pipe pipe in pipes)
            {
                pipe.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
