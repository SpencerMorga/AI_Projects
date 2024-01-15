using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace FlappyBird
{
    internal class Pipe : Sprite
    {
        Texture2D imageR;
        int bStart = 0;

        public Pipe(Texture2D image, Vector2 position, Color color, Texture2D ImageR)
            : base(image, position, color)
        {
            imageR = ImageR;
        }

        public Rectangle hitbox
        {
            get
            {
                if (sourceRectangle == null)
                {
                    return new Rectangle((int)position.X, (int)position.Y, (int)(image.Width * Scale.X), (int)(image.Height * Scale.Y));
                }

                return new Rectangle((int)position.X, (int)position.Y, (int)(sourceRectangle.Value.Width * Scale.X), (int)(sourceRectangle.Value.Height * Scale.Y));
            }
        }




        public Rectangle hitboxR
        {
            get
            {
                if (sourceRectangle == null)
                {
                    return new Rectangle((int)position.X, (imageR.Height - (int)Math.Abs(position.Y) + 120), (int)(imageR.Width * Scale.X), (int)(imageR.Height * Scale.Y));
                }

                return new Rectangle((int)position.X, (int)position.Y, (int)(sourceRectangle.Value.Width * Scale.X), (int)(sourceRectangle.Value.Height * Scale.Y));
            }
        }

        public void Update(GameTime gameTime)
        {
            position.X -= 5;
        }
        
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(imageR, position, sourceRectangle, color, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);

            bStart = imageR.Height - (int)Math.Abs(position.Y) + 120;
            
            sb.Draw(image, new Vector2(position.X, bStart), sourceRectangle, color, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

        public (double, double) getGapCenter()
        {
            // tpipe_y + tpipe_height + ((bpipe_y - (tpipe_y + tpipe_height)) / 2)
            // tpipe_x + (tpipe_width / 2)
            return (position.X + (image.Width / 2.0), position.Y + image.Height + ((bStart - (position.Y + image.Height)) / 2.0));
        }

    }

    /*
     * 
     * generate reverse pipe from (screen width, [0 to -50])
     * length of image - abs(y) + gap space = start of upright pipe
     */
}
