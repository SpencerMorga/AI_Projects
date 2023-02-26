
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlappyBird
{
    public class Sprite
    {
        public Texture2D image;
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

        public Sprite(Texture2D image, Vector2 position, Color color)
        {
            this.image = image;
            this.position = position;
            this.color = color;
            Scale = Vector2.One;
        }
        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(image, position, sourceRectangle, color, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
    }
}