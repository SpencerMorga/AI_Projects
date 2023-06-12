using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    internal class Player : Sprite
    {
        bool jump = false;

        public double velocity { get; set; } = 0;
        public int score;
        public bool isDead;
        const double acceleration = .4;
        double currentTime = 0; 
        public Player(Texture2D image, Vector2 position, Color color)
            : base(image, position, color)
        {

        } 

        public void Update(GameTime gameTime, double result)
        {
            velocity += acceleration;

            position.Y += (float)velocity;

            
            if (result > 0) 
            { 
                jump = true;
            }

            if (result < 0  && jump) 
            {
                velocity = -5;
                jump = false;
            }

            //if (jump)
            //{

            //}
        }
    }
}
