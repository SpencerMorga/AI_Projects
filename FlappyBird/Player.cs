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
        const double acceleration = .3;
        double currentTime = 0; 
        public Player(Texture2D image, Vector2 position, Color color)
            : base(image, position, color)
        {

        } 

        public void Update(GameTime gameTime, KeyboardState keyboard)
        {
            velocity += acceleration;
            //if (gameTime.ElapsedGameTime.TotalMilliseconds - currentTime > 0.0001)
            //{
            //    velocity += acceleration;
            //    currentTime = gameTime.ElapsedGameTime.TotalMilliseconds;
            //}
            position.Y += (float)velocity;

            
            if (keyboard.IsKeyDown(Keys.Space)) 
            { 
                jump = true;
            }

            if (keyboard.IsKeyUp(Keys.Space)  && jump) 
            {
                velocity = -8;
                jump = false;
            }
        }
    }
}
