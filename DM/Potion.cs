using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DM
{
    class Potion
    {
        public int X, Y;
        public Texture2D Texture;
        public int Type;
        public Potion(Texture2D texture,int x, int y, int type)
        {
            Texture = texture;
            X = x;
            Y = y;
            Type = type;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle(X, Y, Texture.Width, Texture.Height), Color.White);
        }

        public void Update()
        {
           
        }
    }
}