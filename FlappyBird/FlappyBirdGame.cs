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
        SpriteBatch spriteBatch;

        Player[] players;
        List<Pipe> pipes = new List<Pipe>();

        Texture2D pipetexture;
        Texture2D pipeRtexture;
        TimeSpan pipespan;

        bool shouldIncScore = false;
        bool allDead = true;

        Pipe nearestPipe;
        Random random = new Random();

        NeuralNetwork[] nets;

        GeneticLearning geneticLearning;
        public FlappyBirdGame(int playersCount, ActivationFunction activation, ErrorFunction errorFunc, int[] neuronsPerLayer, Texture2D image, Vector2 position, Color color, Texture2D Pipe, Texture2D PipeR, GraphicsDevice graphics)
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
                nets[i] = new NeuralNetwork(activation, errorFunc, neuronsPerLayer);
            }

            pipes = new List<Pipe>();
            pipetexture = Pipe;
            pipeRtexture = PipeR;
        
            this.graphics = graphics;

            pipes.Add(new Pipe(pipetexture, new Vector2(graphics.Viewport.Width, -random.Next(1, 150)), Color.White, pipeRtexture));
            pipespan = TimeSpan.Zero;
            nearestPipe = pipes[0];
        }

        public void Update(GameTime gameTime)
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

            foreach (Pipe pipe in pipes)
            {
                if (getPipeDist(pipe) < getPipeDist(nearestPipe))
                {
                    nearestPipe = pipe;
                }
                pipe.Update(gameTime);
            }
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
            }
        }

        public double getPipeDist(Pipe pipe)
        {
            return Math.Sqrt(Math.Pow(pipe.getGapCenter().Item1 - players[0].position.X, 2) + Math.Pow(pipe.getGapCenter().Item2 - players[0].position.Y, 2));
        }

        public void Draw(GameTime gameTime)
        {
            graphics.Clear(Color.CornflowerBlue);

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
