using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AssThreeG11
{
    class MovingLight
    {
        private Texture2D _texture;
        private float _speed;
        public Vector2 _position;
        public bool isActive;
        int W, H;
        public int Damage;
        public int Health;

        public int _W
        {
            get { return _texture.Width; }

        }

        public int _H
        {
            get { return _texture.Height; }

        }


        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }


        public void Initialize(Texture2D texture, Vector2 position, int speed)
        {
            _position = position;
            _texture = texture;
            _speed = speed;
            isActive = true;
            W = 640;
            H = 960;
            Damage = 1;
            Health = 1;
        }

        public void Update()
        {
            _position.Y = _position.Y + Speed;
            if (_position.Y >= H || Health <=0)
            {
                isActive = false;
            }

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(_texture, _position, Color.White);

        }


    }
}
