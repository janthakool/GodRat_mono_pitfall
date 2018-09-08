using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace AssThreeG11
{
    class Boss
    {
        // public Animation PlayerAnimation;
        public Texture2D PlayerTexture;
        public Vector2 Position;
        public int Health;
        public bool isActive;
        public float Speed;
        public int Width
        {
            get { return PlayerTexture.Width; }

        }

        public int Height
        {
            get { return PlayerTexture.Height; }

        }

        public void Initialize(Texture2D texture, Vector2 position)
        {
            PlayerTexture = texture;
            Position = position;
            Health = 1000;
            isActive = true;
            Speed = 5.0f;
        }

        public void Update(GameTime gameTime)
        {
            //Position = Position;
            //PlayerTexture.Update(gameTime);

            if (Health < 0)
            {
                isActive = false;
            }

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(PlayerTexture, Position, Color.White);

        }


        public void MoveLeft()
        {
            Position.X = Position.X - Speed;
        }

        public void MoveRight()
        {
            Position.X = Position.X + Speed;
        }
    }
}
