using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    internal class Pipe
    { 
        public Texture2D image;
        public Texture2D imageR;
        public Vector2 position;
        public Color color;
        public Vector2 Scale;
        public Rectangle? sourceRectangle;
       
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

        public Pipe(Texture2D image, Texture2D imageR, Vector2 position, Color color)
        {
            this.image = image;
            this.imageR = imageR;
            this.position = position;
            this.color = color;
            Scale = Vector2.One;

        }   
         
        
         
        public void Update(GameTime gameTime)
        {
            position.X -= 6;
        }
        
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(imageR, position, null, color, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);

            int uprightStart = imageR.Height - (int)Math.Abs(position.Y) + 120;
            
            sb.Draw(image, new Vector2(position.X, uprightStart), null, color, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

    }

}
