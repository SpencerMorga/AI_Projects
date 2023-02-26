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
        public double velocity { get; set; } = 0;
        const double acceleration = -9.8;
        public Player(Texture2D image, Vector2 position, Color color)
            : base(image, position, color)
        {

        }

        public void Update(GameTime gameTime)
        {
            position.Y += (float)(gameTime.ElapsedGameTime.TotalSeconds * velocity);

            velocity += gameTime.ElapsedGameTime.TotalSeconds * acceleration;
        }

    }
}
