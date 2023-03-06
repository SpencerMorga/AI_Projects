using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    internal class Pipe : Sprite
    {
        public Pipe(Texture2D image, Vector2 position, Color color)
            : base(image, position, color)
        {

        }

        public void Update(GameTime gameTime)
        {
            position.X -= 5;
        }

    }
}
