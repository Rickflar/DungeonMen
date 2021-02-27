using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;


namespace DM
{
    class Hero
    {
        private AnimatedSprite animatedSprite;
        public Vector2 Position;
        public Texture2D Texture;
        public int Health,MaxHealth;
        public int Dash,MaxDash;
        public int Speed;
        public int Score;
        public int counter;
        public int temp;
        public int keys;
        public bool is_facing_down;
        public bool is_facing_up;
        public bool is_facing_left;
        public bool is_facing_right;
        public bool playerone;
        public bool is_died, is_played, is_god, gk, sk, ck, is_collected;
        public string Name;
        public int dashmultiplier, multiplier;
        public List<Texture2D> Panimations = new List<Texture2D>();
        public List<SoundEffect> Effects = new List<SoundEffect>();
        public Random random;
        public int gametype;
        public void Initialize(Texture2D texture, Vector2 pos, List<Texture2D> animations, List<SoundEffect> effects, string name, int health, int dash)
        {
            random = new Random();
            is_died = false;
            Position = pos;
            Health = health;
            MaxHealth = 100;
            Dash = dash;
            MaxDash = dash;
            Name = name;
            counter = 0;
            temp = 0;
            keys = 0;
            Texture = texture;
            Effects = effects;
            Speed = 3;
            gk = false;
            sk = false;
            ck = false;
            animatedSprite = new AnimatedSprite(texture, 1, 4);
            Panimations = animations;
            is_facing_down = true;
            is_facing_left = false;
            is_facing_up = false;
            is_facing_right = false;
            is_played = false;
            is_god = false;
            is_collected = false;
            multiplier = 1;
            dashmultiplier = 5;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 Pos, int mode, Effect effect)
        {
            animatedSprite.Draw(Texture,spriteBatch, Position, mode, effect);
        }
        public void Update(List<Potion> HealthPotions, List<Potion> KillList, ref int BadP)
        {
            if (!is_died)
            {
                KeyboardHandler();
                PickPotion(HealthPotions, KillList, ref Health, ref Dash, ref BadP);
                if (Dash > MaxDash)
                {
                    Dash = MaxDash;
                }
                if (Health > MaxHealth)
                {
                    Health = MaxHealth;
                }
                if (Health < 0)
                {
                    Health = 0;
                }
                if (is_god)
                {
                    counter++;
                    if (counter == 600)
                    {
                        is_god = false;
                        counter = 0;
                    }
                }
                animatedSprite.Update();
                
            }
            else
            {
                Texture = Panimations[12];

                if (!is_played)
                {
                    Effects[0].Play();
                    is_played = true;
                }
                animatedSprite.Update();
            }
        }
        public void KeyboardHandler()
        {
            KeyboardState state = Keyboard.GetState();
            GamePadCapabilities capabilities = GamePad.GetCapabilities(
                                               PlayerIndex.One);
            GamePadState state1 = GamePad.GetState(PlayerIndex.One);
            
            if (playerone ? ((gametype == 0|| gametype == 2|| gametype == 4)? state.IsKeyDown(Keys.W) : state1.ThumbSticks.Left.Y > 0.2f) : ((gametype == 0 || gametype == 2 )) ? state1.ThumbSticks.Left.Y > 0.2f :state.IsKeyDown(Keys.Up))
            {
                is_facing_up = true;
                is_facing_down = false;
                is_facing_left = false;
                is_facing_right = false;
                if (playerone ? ((gametype == 0 || gametype == 2 || gametype == 4) ? state.IsKeyDown(Keys.LeftShift): state1.IsButtonDown(Buttons.A)) : ((gametype == 0 || gametype == 2 )) ? state1.IsButtonDown(Buttons.A) : state.IsKeyDown(Keys.Space))
                {
                    if ((Dash != 0)||(is_god))
                    {
                        temp++;
                        if (temp % 5 == 0)
                        {
                            Effects[2].Play();
                        }
                        if (!is_god)
                        {                         
                                Dash--;
                        }
                        Position.Y -= dashmultiplier * Speed;

                    }
                    else
                    {
                        this.Position.Y -= multiplier * Speed;
                    }
                    if (Position.Y <= -10)
                    {
                        Position.Y = -10;
                    }
                }
                else
                {
                    Position.Y -= multiplier * Speed;
                    if (Position.Y <= -10)
                    {
                        Position.Y = -10;
                    }
                }
                if (is_facing_up)
                {
                        Texture = Panimations[4];
                }

            }
            else if (playerone ? state.IsKeyUp(Keys.W) : state.IsKeyUp(Keys.Up))
            {
                if (is_facing_up)
                {
                      Texture = Panimations[3];
                }
            }
            if (playerone ? ((gametype == 0 || gametype == 2 || gametype == 4) ? state.IsKeyDown(Keys.S) : state1.ThumbSticks.Left.Y < -0.2f) : ((gametype == 0 || gametype == 2 )) ? state1.ThumbSticks.Left.Y < -0.2f : state.IsKeyDown(Keys.Down))

            {
                is_facing_up = false;
                is_facing_down = true;
                is_facing_left = false;
                is_facing_right = false;
                if (playerone ? ((gametype == 0 || gametype == 2 || gametype == 4) ? state.IsKeyDown(Keys.LeftShift) : state1.IsButtonDown(Buttons.A)) : ((gametype == 0 || gametype == 2 )) ? state1.IsButtonDown(Buttons.A) : state.IsKeyDown(Keys.Space))
                {
                    if ((Dash != 0)|| (is_god))
                    {
                        temp++;
                        if (temp % 5 == 0)
                        {
                            Effects[2].Play();
                        }
                        if (!is_god)
                        {
                            Dash--;
                        }
                        Position.Y += dashmultiplier * Speed;
                    }
                    else
                    {
                        Position.Y += multiplier * Speed;
                    }
                    if ((Position.Y >= 675) && ((Position.X <= 840) || (Position.X >= 1010)))
                    {
                        Position.Y = 675;
                    }
                    else if ((Position.Y >= 775) && ((Position.X >= 840) && (Position.X <= 1010)))
                    {
                        Position.Y = 775;
                    }
                }
                else
                {
                    Position.Y += multiplier * Speed;
                    if ((Position.Y >= 675) && ((Position.X <= 830) || (Position.X >= 1010)))
                    {
                        Position.Y = 675;
                    }
                    else if ((Position.Y >= 775) && ((Position.X >= 830) && (Position.X <= 1010)))
                    {
                        Position.Y = 775;
                    }
                }
                if (is_facing_down)
                {
                        Texture = Panimations[1];
                }
            }
            else if (playerone ? state.IsKeyUp(Keys.S) : state.IsKeyUp(Keys.Down))

            {
                if (is_facing_down)
                {
                        Texture = Panimations[0];
                }
            }
            if (playerone ? ((gametype == 0 || gametype == 2 || gametype == 4) ? state.IsKeyDown(Keys.A): state1.ThumbSticks.Left.X < -0.2f) : ((gametype == 0 || gametype == 2)) ? state1.ThumbSticks.Left.X < -0.2f : state.IsKeyDown(Keys.Left))
            {
                is_facing_up = false;
                is_facing_down = false;
                is_facing_left = true;
                is_facing_right = false;
                if (playerone ? ((gametype == 0 || gametype == 2 || gametype == 4) ? state.IsKeyDown(Keys.LeftShift) : state1.IsButtonDown(Buttons.A)) : ((gametype == 0 || gametype == 2)) ? state1.IsButtonDown(Buttons.A) : state.IsKeyDown(Keys.Space))
                {
                    if ((Dash != 0)|| (is_god))
                    {
                        temp++;
                        if (temp % 5 == 0)
                        {
                            Effects[2].Play();
                        }
                        if (!is_god)
                        {
                            Dash--;
                        }
                        Position.X -= dashmultiplier * Speed;
                    }
                    else
                    {
                        Position.X -= multiplier * Speed;
                    }
                    if (Position.X <= 55)
                    {
                        Position.X = 55;
                    }
                    else if ((Position.Y >= 678) && (Position.X <= 845))
                    {
                        Position.X = 845;
                    }
                    else if ((Position.Y >= 678) && (Position.X >= 1000))
                    {
                        Position.X = 1000;
                    }
                }
                else
                {
                    Position.X -= multiplier * Speed;
                    if (Position.X <= 55)
                    {
                        Position.X = 55;
                    }
                    else if ((Position.Y >= 678) && (Position.X <= 845))
                    {
                        Position.X = 845;
                    }
                    else if ((Position.Y >= 678) && (Position.X >= 1000))
                    {
                        Position.X = 1000;
                    }
                }
                if (is_facing_left)
                {
                        Texture = Panimations[7];
                }

            }
            else if (playerone ? state.IsKeyUp(Keys.A) : state.IsKeyUp(Keys.Left))

            {
                if (is_facing_left)
                {
                        Texture = Panimations[6];
                }
            }
            if (playerone ? ((gametype == 0 || gametype == 2 || gametype == 4) ? state.IsKeyDown(Keys.D): state1.ThumbSticks.Left.X > 0.2f) : ((gametype == 0 || gametype == 2 )) ? state1.ThumbSticks.Left.X > 0.2f : state.IsKeyDown(Keys.Right))
            {
                is_facing_up = false;
                is_facing_down = false;
                is_facing_left = false;
                is_facing_right = true;
                if (playerone ? ((gametype == 0 || gametype == 2 || gametype == 4) ? state.IsKeyDown(Keys.LeftShift) : state1.IsButtonDown(Buttons.A)) : ((gametype == 0 || gametype == 2)) ? state1.IsButtonDown(Buttons.A) : state.IsKeyDown(Keys.Space))
                {
                    if ((Dash != 0)|| (is_god))
                    {
                        temp++;
                        if (temp % 5 == 0)
                        {
                            Effects[2].Play();
                        }
                        if (!is_god)
                        {
                            Dash--;
                        }
                        Position.X += dashmultiplier * Speed;
                    }
                    else
                    {
                        Position.X += multiplier * Speed;
                    }
                    if (Position.X >= 1800)
                    {
                        Position.X = 1800;
                    }
                    else if ((Position.Y >= 678) && (Position.X <= 845))
                    {
                        Position.X = 845;
                    }
                    else if ((Position.Y >= 678) && (Position.X >= 1000))
                    {
                        Position.X = 1000;
                    }
                }
                else
                {
                    Position.X += multiplier * Speed;
                    if (Position.X >= 1800)
                    {
                        Position.X = 1800;
                    }
                    else if ((Position.Y >= 678) && (Position.X <= 845))
                    {
                        Position.X = 845;
                    }
                    else if ((Position.Y >= 678) && (Position.X >= 1000))
                    {
                        Position.X = 1000;
                    }
                }
                if (is_facing_right)
                {
                        Texture = Panimations[10];
                }

            }
            else if (playerone ? state.IsKeyUp(Keys.D) : state.IsKeyUp(Keys.Right))
            {
                if (is_facing_right)
                {
                        Texture = Panimations[9];
                }
            }
        }
        public int PickPotion(List<Potion> Potions, List<Potion> KillList, ref int Data, ref int Dash, ref int BadP)
        {
            foreach (Potion p in Potions.ToArray())
            {
                if ((Position.X + p.Texture.Width/2 >= p.X) && (Position.X + p.Texture.Width/2 <= (p.X + p.Texture.Width)) && ((Position.Y + p.Texture.Height / 2) >= p.Y) && ((Position.Y + p.Texture.Height / 2) <= (p.Y + p.Texture.Height)))
                {
                    KillList.Add(p);
                    Potions.Remove(p);
                    if (p.Type == 1)
                    {
                        Health += 10;

                    }
                    else if (p.Type == 2)
                    {
                        Dash += 20;
                    }
                    else if (p.Type == 3)
                    {
                        BadP ++;
                    }
                    else if(p.Type == 4)
                    {
                        is_god = true;
                    }
                    else if (p.Type == 5)
                    {
                        Health = 2;
                    }
                    else if (p.Type == 6)
                    {
                        MaxHealth = MaxHealth + 20;
                        MaxDash = MaxHealth;
                        Health = MaxHealth;
                        Dash = MaxDash;
                    }
                    if (p.Type == 7)
                    {
                        ck = true;
                        Score += 1000;
                        keys++;
                    }
                    if(p.Type == 8)
                    {
                        sk = true;
                        Score += 2000;
                        keys++;
                    }
                    if (p.Type == 9)
                    {
                        gk = true;
                        Score += 3000;
                        keys++;
                    }
                    if (ck && sk && gk)
                    {
                        is_collected = true;
                    }
                    Score += 10;
                    if (p.Type != 6)
                    {
                        Effects[1].Play();
                    }
                    else
                    {
                        Effects[4].Play();
                    }

                }
            }
            RemovingPotions(KillList);
           
            return Data;
        }
        public void RemovingPotions(List<Potion> KillList)
        {
            foreach (Potion p in KillList.ToArray())
            {
                KillList.Remove(p);
            }
        }
    }
}