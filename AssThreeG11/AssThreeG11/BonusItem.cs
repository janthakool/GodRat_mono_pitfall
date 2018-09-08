using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AssThreeG11
{
    class BonusItem
    {
        public Texture2D BNTexture;
        public Vector2 _Position;
        public int Health;
        public bool isActive;
        public float Speed;
        int H, W;
        public int BWidth
        {
            get { return BNTexture.Width; }

        }

        public int BHeight
        {
            get { return BNTexture.Height; }

        }

        public void Initialize(Texture2D texture, Vector2 position, int speed)
        {
            BNTexture = texture;
            _Position = position;
            Health = 1;
            isActive = true;
            Speed = speed;
            W = 640;
            H = 960;
        }

        public void Update()
        {
            _Position.Y = _Position.Y + 5;

            if (_Position.Y >= H)
            {
                isActive = false;
            }
            //PlayerTexture.Update(gameTime);

            //if (Health <= 0)
            //{
            //    isActive = false;
            //}

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(BNTexture, _Position, Color.White);

        }
    }
}
