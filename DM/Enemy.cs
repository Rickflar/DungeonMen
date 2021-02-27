using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;



namespace DM
{
    class Enemy
    {
        public Texture2D Texture;
        public int Damage;
        public int Speed;
        public int X, Y, startX, startY, type, temp4;
        public int counter;
        public bool is_going;
        List<Texture2D> Eanimations;
        public Enemy(List<Texture2D> anim, int x, int y, int damage, int speed, int Type)
        {
            X = x;
            Y = y;
            startX = 960;
            startY = 540;
            is_going = false;
            type = Type;
            Eanimations = anim;
            Texture = Eanimations[0];
            Speed = speed;
            Damage = damage;
            counter = 0;
            temp4 = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle(X, Y, Texture.Width, Texture.Height), Color.White * 0.5f);
        }

        public void Update(float x, float y)
        {
            if ((x+16) < X)
            {
                X -= Speed;
                if (type == 0)
                {
                    Texture = Eanimations[2];
                }
                else if (type == 1)
                {
                    Texture = Eanimations[6];
                }
                if ((y + 16) < Y)
                {
                    Y -= Speed;
                    if (type == 0)
                    {
                        Texture = Eanimations[3];
                    }
                    else if (type == 1)
                    {
                        Texture = Eanimations[7];
                    }
                }
                else if(Y < (y + 16))
                {
                    Y += Speed;
                    if (type == 0)
                    {
                        Texture = Eanimations[0];
                    }
                    else if (type == 1)
                    {
                        Texture = Eanimations[4];
                    }
                }
            }
            else 
            {
                X += Speed;
                if (type == 0)
                {
                    Texture = Eanimations[1];
                }
                else if (type == 1)
                {
                    Texture = Eanimations[5];
                }
                if ((y + 16) < Y)
                {
                    Y -= Speed;
                    if (type == 0)
                    {
                        Texture = Eanimations[3];
                    }
                    else if (type == 1)
                    {
                        Texture = Eanimations[7];
                    }
                }
                else if (Y < (y + 16))
                {
                    Y += Speed;
                    if (type == 0)
                    {
                        Texture = Eanimations[0];
                    }
                    else if (type == 1)
                    {
                        Texture = Eanimations[4];
                    }
                }
            }
            
        }

        public void Attack(List<Hero> heroes, List<SoundEffect> SoundEffects)
        {
            foreach (Hero h in heroes)
            {
                if (!h.is_god)
                {
                    if ((h.Position.X >= X - Texture.Width / 2) && (h.Position.X <= (X)) && ((h.Position.Y + h.Texture.Height / 2) >= Y) && ((h.Position.Y + h.Texture.Height / 2) <= (Y + Texture.Height)))
                    {
                        if (temp4 != 10)
                        {
                            temp4++;
                        }
                        else
                        {
                            h.Health -= Damage * 2;
                            SoundEffects[3].Play();
                            temp4 = 0;
                        }
                    }
                }
            }
        }
    }
}