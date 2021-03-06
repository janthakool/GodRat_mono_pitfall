﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AssThreeG11


{
    class Bullet
    {
        public Texture2D Texture;
        public Vector2 Position;
        public bool isActive;
        public int Dramage;
        private int H;
        public float Speed;

        public void Intialize(Texture2D texture, Vector2 position, int height)
        {
            Texture = texture;
            Position = position;
            isActive = true;
            Dramage = 2;
            Speed = 20f;
            H = height;
        }

        public void Update()
        {
            Position.Y -= Speed;

            if (Position.Y < 0)
            {
                isActive = false;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Texture, Position, Color.White);
        }
    }
}
