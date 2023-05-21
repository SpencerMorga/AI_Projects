using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetworks;

namespace FlappyBird
{
    internal class FlappyBirdGame
    {
        GraphicsDevice graphics;
        Player player;
        List<Pipe> pipes = new List<Pipe>();
        Texture2D pipetexture;
        Texture2D pipeRtexture;
        TimeSpan pipespan;
        NeuralNetwork net;
        bool isDead;
        Pipe nearestPipe;
        
        public FlappyBirdGame(Player Player, List<Pipe> Pipes, Texture2D Pipe, Texture2D PipeR, TimeSpan Pipespan, GraphicsDevice graphics, NeuralNetwork net)
        {
            player = Player;
            pipes = Pipes;
            pipetexture = Pipe;
            pipeRtexture = PipeR;
            pipespan = Pipespan;
            this.graphics = graphics;
            this.net = net;
            nearestPipe = Pipes[0];
        }

        
        public void Update(GameTime gameTime)
        {
            if (isDead)
            {
                return;
            }

            Random random = new Random();
            pipespan += gameTime.ElapsedGameTime;

            if (pipespan > TimeSpan.FromMilliseconds(2000))
            {
                pipes.Add(new Pipe(pipetexture, new Vector2(graphics.Viewport.Width, -random.Next(1, 150)), Color.White, pipeRtexture));
                pipespan = TimeSpan.Zero;

            }

            if (player.position.Y > graphics.Viewport.Height || player.position.Y < 0) isDead = true;

            foreach (Pipe pipe in pipes)
            {
                if (player.hitbox.Intersects(pipe.hitbox) || player.hitbox.Intersects(pipe.hitboxR)
                {
                    isDead = true;
                }

                if (getPipeDist(pipe) < getPipeDist(nearestPipe))
                {
                    nearestPipe = pipe;
                }
                pipe.Update(gameTime);
            }//KEEP TRACK OF NEAREST PIPE, compute
            player.Update(gameTime, net.Compute(new double[] {(nearestPipe.getGapCenter().Item1), (nearestPipe.getGapCenter().Item2)})[0]);
            
        }

        public double getPipeDist(Pipe pipe)
        {
            return Math.Sqrt(Math.Pow(pipe.getGapCenter().Item1 - player.position.X, 2) + Math.Pow(pipe.getGapCenter().Item2 - player.position.Y, 2));
        }
    }
}
