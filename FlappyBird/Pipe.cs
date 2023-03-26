using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    internal class Pipe : Sprite
    {
        Texture2D imageR;

        public Pipe(Texture2D image, Vector2 position, Color color, Texture2D ImageR)
            : base(image, position, color)
        {
            imageR = ImageR;
        }

       

        public void Update(GameTime gameTime)
        {
            position.X -= 5;
        }
        
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(imageR, position, sourceRectangle, color, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);

            int uprightStart = imageR.Height - (int)Math.Abs(position.Y) + 120;
            
            sb.Draw(image, new Vector2(position.X, uprightStart), sourceRectangle, color, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

    }

    /*
     * 
     * generate reverse pipe from (screen width, [0 to -50])
     * length of image - abs(y) + gap space = start of upright pipe
     */
}
