using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Content;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;

namespace DM
{
    enum GameState
    {
        LogoScreen,
        MainMenu,
        Gameplay,
        Options,
        Controls,
        History,
        EndOfGame,
    }
   
public class Game1 : Game
    {
        GameState _state;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D Back1, Back2,Screen, Icon1,Icon2;
        private List<Texture2D> P1animations = new List<Texture2D>();
        private List<Texture2D> P2animations = new List<Texture2D>();
        private List<Texture2D> Eanimations = new List<Texture2D>();
        private List<Texture2D> Icons = new List<Texture2D>();
        private List<Texture2D> PTextures = new List<Texture2D>();
        private List<Potion> Potions = new List<Potion>();
        private List<Potion> KillList = new List<Potion>();
        private List<Enemy> Enemies = new List<Enemy>();
        private List<Enemy> KillList2 = new List<Enemy>();
        private List<Hero> heroes = new List<Hero>();
        private List<Song> Music = new List<Song>();
        private List<Char> Name = new List<Char>();
        private List<SoundEffect> SoundEffects = new List<SoundEffect>();
        private Vector2 PlayerPosition;
        private int temp3, temp4, pausecount, gametype, temp5, players, mode, phrase, map, tSeconds, timer, steps, badp, gk, sk, ck, shift_right, shift_down,keytimer, score,best, switcher, switchercount, difficulty, switcher2, switcher3,switcher4, switcher5, volume, volume2;
        private double secs, secsRounded;
        private float time, splashAlpha;
        private SpriteFont font,mmfont,cfont;
        private string temp, temp1,name1,name2,alphabet, modetype;
        public bool deleted, m1,m2,m3, drawSplashScreen, pause, keys, eater, select, entered, saving, b, rus, modes,darktheme;
        Random random;
        Color color;

        public static Texture2D lightMask,lightMask2,lightMask3;
        public List <Effect> effects = new List<Effect>();
        RenderTarget2D lightsTarget;
        RenderTarget2D mainTarget;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            rus = false;
            modes = true;
            darktheme = true;
            graphics.ToggleFullScreen();
            color = Color.White;
            volume = 50;
            volume2 = 50;
            players = 2;
            gametype = 4;
        }
        protected override void Initialize()
        {
            if (File.Exists("Records.txt"))
            {
                name2 = File.ReadAllText("Records.txt");
                string[] temp = name2.Split(' ');
                name2 = temp[0];
                best = Convert.ToInt32(temp[1]);
            }
            modetype = "";
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
            var pp = GraphicsDevice.PresentationParameters;
            lightsTarget = new RenderTarget2D(
            GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);
            mainTarget = new RenderTarget2D(
            GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);

            switcher = -1;
            switcher2 = -1;
            switcher3 = -1;
            switcher4 = -1;
            switcher5 = -1;
            difficulty = 0;
            switchercount = 0;
            _state = GameState.LogoScreen;
            Window.Title = "The Dungeonman | v1.96";
            for(int i = 0; i < 6; i++)
            {
                Name.Add('A');
            }
            alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            name1 = "AAAAAA";
            
            keytimer = 0;
            m1 = false;
            m2 = false;
            m3 = false;
            shift_right = 0;
            shift_down = 0;
            gk = 0;
            sk = 0;
            ck = 0;
            pause = false;
            keys = false;
            eater = false;
            select = false;
            entered = false;
            saving = false;
            b = false;
            timer = 0;
            steps = 0;
            badp = 0;
            time = 0;
            deleted = false;
            
            for (int i = 0; i < 2; i++)
            {
                heroes.Add(new Hero());
            }
            heroes[0].playerone = true;
            heroes[1].playerone = false;
            random = new Random();
            map = random.Next(0, 2);
            phrase = random.Next(0, 5);
            if (modes)
            {
                mode = random.Next(0, 6);
            }
            else
            {
                mode = 0;
            }
            temp3 = 0;
            temp4 = 0;
            temp5 = 0;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Back1 = Content.Load<Texture2D>("Backs/Back1");
            Back2 = Content.Load<Texture2D>("Backs/Back2");
            Screen = Content.Load<Texture2D>("Backs/Screen");
            lightMask = Content.Load<Texture2D>("lightmask");
            lightMask2 = Content.Load<Texture2D>("lightmask2");
            lightMask3 = Content.Load<Texture2D>("lightmask3");
            effects.Add(Content.Load<Effect>("light"));
            effects.Add(Content.Load<Effect>("grayscale"));
            effects.Add(Content.Load<Effect>("rainbow"));
            PTextures.Add(Content.Load<Texture2D>("Potion/Health"));
            PTextures.Add(Content.Load<Texture2D>("Potion/Mana"));
            PTextures.Add(Content.Load<Texture2D>("Potion/Bad"));
            PTextures.Add(Content.Load<Texture2D>("Potion/God"));
            PTextures.Add(Content.Load<Texture2D>("Potion/Poison"));
            PTextures.Add(Content.Load<Texture2D>("Potion/1up"));
            PTextures.Add(Content.Load<Texture2D>("Potion/CopperKey"));
            PTextures.Add(Content.Load<Texture2D>("Potion/SilverKey"));
            PTextures.Add(Content.Load<Texture2D>("Potion/GoldKey"));
            Icon1 = Content.Load<Texture2D>("Icons/Icon");
            Icon2 = Content.Load<Texture2D>("Icons/Icon2");
            Icons.Add(Content.Load<Texture2D>("Icons/HealthIcon"));
            Icons.Add(Content.Load<Texture2D>("Icons/DashIcon"));
            Icons.Add(Content.Load<Texture2D>("Icons/KeyIcon"));
            Icons.Add(Content.Load<Texture2D>("Icons/PickIcon"));
            Icons.Add(Content.Load<Texture2D>("Icons/ControlIcon"));
            Icons.Add(Content.Load<Texture2D>("Icons/ControlIcon2"));
            Icons.Add(Content.Load<Texture2D>("Icons/ControlPS4"));
            Music.Add(Content.Load<Song>("Music/1"));
            Music.Add(Content.Load<Song>("Music/2"));
            Music.Add(Content.Load<Song>("Music/3"));
            SoundEffects.Add(Content.Load<SoundEffect>("Effects/HeroDeath"));
            SoundEffects.Add(Content.Load<SoundEffect>("Effects/PotionPickUp2"));
            SoundEffects.Add(Content.Load<SoundEffect>("Effects/Dash"));
            SoundEffects.Add(Content.Load<SoundEffect>("Effects/Hit"));
            SoundEffects.Add(Content.Load<SoundEffect>("Effects/PotionPickUp"));
            P1animations.Add(Content.Load<Texture2D>("Hero/1"));
            P1animations.Add(Content.Load<Texture2D>("Hero/2"));
            P1animations.Add(Content.Load<Texture2D>("Hero/3"));
            P1animations.Add(Content.Load<Texture2D>("Hero/4"));
            P1animations.Add(Content.Load<Texture2D>("Hero/5"));
            P1animations.Add(Content.Load<Texture2D>("Hero/6"));
            P1animations.Add(Content.Load<Texture2D>("Hero/7"));
            P1animations.Add(Content.Load<Texture2D>("Hero/8"));
            P1animations.Add(Content.Load<Texture2D>("Hero/9"));
            P1animations.Add(Content.Load<Texture2D>("Hero/10"));
            P1animations.Add(Content.Load<Texture2D>("Hero/11"));
            P1animations.Add(Content.Load<Texture2D>("Hero/12"));
            P1animations.Add(Content.Load<Texture2D>("Hero/13"));
            P2animations.Add(Content.Load<Texture2D>("Hero2/1"));
            P2animations.Add(Content.Load<Texture2D>("Hero2/2"));
            P2animations.Add(Content.Load<Texture2D>("Hero2/3"));
            P2animations.Add(Content.Load<Texture2D>("Hero2/4"));
            P2animations.Add(Content.Load<Texture2D>("Hero2/5"));
            P2animations.Add(Content.Load<Texture2D>("Hero2/6"));
            P2animations.Add(Content.Load<Texture2D>("Hero2/7"));
            P2animations.Add(Content.Load<Texture2D>("Hero2/8"));
            P2animations.Add(Content.Load<Texture2D>("Hero2/9"));
            P2animations.Add(Content.Load<Texture2D>("Hero2/10"));
            P2animations.Add(Content.Load<Texture2D>("Hero2/11"));
            P2animations.Add(Content.Load<Texture2D>("Hero2/12"));
            P2animations.Add(Content.Load<Texture2D>("Hero2/13"));
            Eanimations.Add(Content.Load<Texture2D>("Enemy/1"));
            Eanimations.Add(Content.Load<Texture2D>("Enemy/2"));
            Eanimations.Add(Content.Load<Texture2D>("Enemy/3"));
            Eanimations.Add(Content.Load<Texture2D>("Enemy/4"));
            Eanimations.Add(Content.Load<Texture2D>("Enemy/5"));
            Eanimations.Add(Content.Load<Texture2D>("Enemy/6"));
            Eanimations.Add(Content.Load<Texture2D>("Enemy/7"));
            Eanimations.Add(Content.Load<Texture2D>("Enemy/8"));
            PlayerPosition = new Vector2(930, 350);
            int temp = 1;
            int thealth, tdash;
            if (mode == 3)
            {
                thealth = 2;
                tdash = 200;
            }
            else
            {
                thealth = 100;
                tdash = 100;
            }
            foreach (Hero h in heroes)
            {
                h.Initialize(Content.Load<Texture2D>(h.playerone ? "Hero/1" : "Hero2/1"), new Vector2(random.Next(100, 1800), random.Next(50, 600)), h.playerone ? P1animations : P2animations, SoundEffects, string.Concat("P",temp.ToString()), thealth, tdash);
                temp++;
            }
            font = Content.Load<SpriteFont>("Font");
            mmfont = Content.Load<SpriteFont>("MainMenuFont");
            cfont = Content.Load<SpriteFont>("ControlFont");
        }
        private void UpdateGameplay(GameTime gameTime)
        {
            foreach(Hero h in heroes)
            {
                h.gametype = gametype;
            }
            if (!pause)
            {
                if (!m2)
                {
                    MediaPlayer.Play(Music[1]);
                    MediaPlayer.IsRepeating = true;
                    m2 = true;
                    m1 = false;
                    m3 = false;
                }
                time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if ((players == 1) && (!deleted))
                {
                    deleted = true;
                    heroes.RemoveAt(1);
                }
                EnemyAttack(heroes, SoundEffects);
                temp3++;
                if (mode != 2)
                {
                    if (temp3 % (difficulty == 0 ? 1200 : difficulty == 1 ? 1500 : 2000) == 0)
                    {
                        CreatePotion(Potions, PTextures[1], 2);
                        if (mode != 3)
                        {
                            CreatePotion(Potions, PTextures[0], 1);
                        }
                    }
                }
                if (temp3 % (difficulty == 0 ? 300 : difficulty == 1 ? 250 : 150) == 0)
                {
                    CreateEnemy(0);
                    CreateEnemy(0);
                    if(!eater && (temp3 % (difficulty == 0 ? 1500 : difficulty == 1 ? 1000 : 500) == 0))
                    {
                        eater = true;
                        CreateEnemy(1);
                    }
                    if (!heroes[0].is_died)
                    {
                        heroes[0].Score += 100;
                    }
                    if (players == 2)
                    {
                        if (!heroes[1].is_died)
                        {
                            heroes[1].Score += 100;
                        }
                    }
                }
                if (mode != 2)
                {
                    if (temp3 % (difficulty == 0 ? 1500 : difficulty == 1 ? 2000 : 2500) == 0)
                    {
                        CreatePotion(Potions, PTextures[4], 5);
                    }
                    if (temp3 % (difficulty == 0 ? 1800 : difficulty == 1 ? 2700 : 3600) == 0)
                    {
                        CreatePotion(Potions, PTextures[2], 3);
                    }
                    if (temp3 % (difficulty == 0 ? 3900 : difficulty == 1 ? 3600 : 4200) == 0)
                    {
                        CreatePotion(Potions, PTextures[3], 4);
                    }
                }
                if (mode != 3)
                {
                    if (temp3 % (difficulty == 0 ? 3300 : difficulty == 1 ? 4200 : 4800) == 0)
                    {
                        CreatePotion(Potions, PTextures[5], 6);
                    }
                }
                if (players == 2)
                {
                    if(heroes[0].Score>= 3000 || heroes[1].Score >= 3000)
                    {
                        keys = true;
                    }
                }
                else
                {
                    if (heroes[0].Score >= 3000)
                    {
                        keys = true;
                    }
                }
                if (keys)
                {
                    if (temp5 % (difficulty == 0 ? 4500 : difficulty == 1 ? 5000 : 5500) == 0)
                    {
                        if (ck < 1)
                        {
                            CreatePotion(Potions, PTextures[6], 7);
                            ck++;
                        }
                        else if (ck > 0 && sk <1)
                        {
                            CreatePotion(Potions, PTextures[7], 8);
                            sk++;
                        }
                        else if (sk > 0 && gk < 1)
                        {
                            CreatePotion(Potions, PTextures[8], 9);
                            gk++;
                        }
                    }
                    temp5++;
                }
                if (players == 2)
                {
                    if (heroes[0].Score >= 3000 && heroes[0].ck && heroes[0].sk && heroes[0].gk)
                    {
                        heroes[0].is_collected = true;
                        _state = GameState.EndOfGame;
                    }
                    else if(heroes[1].Score >= 3000 && heroes[1].ck && heroes[1].sk && heroes[1].gk)
                    {
                        heroes[1].is_collected = true;
                        _state = GameState.EndOfGame;
                    }
                }
                else
                {
                    if (heroes[0].Score >= 3000 && heroes[0].ck && heroes[0].sk && heroes[0].gk)
                    {
                        heroes[0].is_collected = true;
                        _state = GameState.EndOfGame;
                    }
                }
                if (badp > 0)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        CreateEnemy(0);
                        badp = 0;
                    }
                }
                foreach (Potion p in Potions)
                {
                    p.Update();
                }
                foreach (Enemy e in Enemies)
                {
                    if (e.type == 0)
                    {
                        double temp1, temp2;
                        if (players == 2)
                        {
                            temp1 = Math.Sqrt(Math.Pow((heroes[0].Position.X - e.X), 2) + Math.Pow((heroes[0].Position.Y - e.Y), 2));
                            temp2 = Math.Sqrt(Math.Pow((heroes[1].Position.X - e.X), 2) + Math.Pow((heroes[1].Position.Y - e.Y), 2));
                            if (temp1 > temp2)
                            {
                                if (temp2 < 400)
                                {
                                    if (!heroes[1].is_died)
                                    {
                                        e.Update(heroes[1].Position.X, heroes[1].Position.Y);
                                    }
                                    else
                                    {
                                        e.Update(heroes[0].Position.X, heroes[0].Position.Y);
                                    }
                                }
                                else
                                {
                                    if (!e.is_going)
                                    {
                                        e.startX = random.Next(100, 1800);
                                        e.startY = random.Next(50, 600);
                                        e.is_going = true;
                                    }
                                    if ((Math.Abs(e.X - e.startX) <= e.Texture.Width) && (Math.Abs(e.Y - e.startY) <= e.Texture.Height))
                                    {
                                        e.is_going = false;
                                    }
                                    e.Update(e.startX, e.startY);
                                }
                            }
                            else
                            {
                                if (temp1 < 400)
                                {
                                    if (!heroes[0].is_died)
                                    {
                                        e.Update(heroes[0].Position.X, heroes[0].Position.Y);
                                    }
                                    else
                                    {
                                        e.Update(heroes[1].Position.X, heroes[1].Position.Y);
                                    }
                                }
                                else
                                {
                                    if (!e.is_going)
                                    {
                                        e.startX = random.Next(100, 1800);
                                        e.startY = random.Next(50, 600);
                                        e.is_going = true;
                                    }
                                    if ((Math.Abs(e.X - e.startX) <= e.Texture.Width) && (Math.Abs(e.Y - e.startY) <= e.Texture.Height))
                                    {
                                        e.is_going = false;
                                    }
                                    e.Update(e.startX, e.startY);
                                }
                            }

                        }
                        else
                        {
                            temp1 = Math.Sqrt(Math.Pow((heroes[0].Position.X - e.X), 2) + Math.Pow((heroes[0].Position.Y - e.Y), 2));
                            if (temp1 < 400)
                            {
                                e.Update(heroes[0].Position.X, heroes[0].Position.Y);
                            }
                            else
                            {
                                if (!e.is_going)
                                {
                                    e.startX = random.Next(100, 1800);
                                    e.startY = random.Next(50, 600);
                                    e.is_going = true;
                                }
                                if ((Math.Abs(e.X - e.startX) <= e.Texture.Width) && (Math.Abs(e.Y - e.startY) <= e.Texture.Height))
                                {
                                    e.is_going = false;
                                }
                                e.Update(e.startX, e.startY);
                            }
                        }
                    }
                    else if (e.type == 1)
                    {                     
                        if (Potions.Count > 1)
                        {
                            if (!select)
                            {
                                temp4 = random.Next(0, Potions.Count - 1);
                                select = true;
                            }
                            if (temp4<=Potions.Count-1)
                            {
                                e.Update(Potions[temp4].X, Potions[temp4].Y);
                                if ((e.X + Potions[temp4].Texture.Width / 2 >= Potions[temp4].X) && (e.X + Potions[temp4].Texture.Width / 2 <= (Potions[temp4].X + Potions[temp4].Texture.Width)) && ((e.Y + Potions[temp4].Texture.Height / 2) >= Potions[temp4].Y) && ((e.Y + Potions[temp4].Texture.Height / 2) <= (Potions[temp4].Y + Potions[temp4].Texture.Height)))
                                {
                                    KillList.Add(Potions[temp4]);
                                    Potions.Remove(Potions[temp4]);
                                    select = false;
                                }
                            }
                            else
                            {
                                select = false;
                            }
                        }
                        else
                        {
                            if (!e.is_going)
                            {
                                e.startX = random.Next(100, 1800);
                                e.startY = random.Next(50, 600);
                                e.is_going = true;
                            }
                            if ((Math.Abs(e.X - e.startX) <= e.Texture.Width) && (Math.Abs(e.Y - e.startY) <= e.Texture.Height))
                            {
                                e.is_going = false;
                            }
                            e.Update(e.startX, e.startY);
                        }
                    }

                }
                foreach (Hero h in heroes.ToArray())
                {
                    h.Update(Potions, KillList, ref badp);
                    if (h.Health == 0)
                    {
                        h.is_died = true;
                    }
                }
                if (heroes[0].is_died && (players == 2 ? heroes[1].is_died : true))
                {
                    _state = GameState.EndOfGame;
                }
            }
        }
        private void UpdateMainMenu(GameTime deltaTime)
        {
            if (!m1)
            {
                MediaPlayer.Play(Music[0]);
                MediaPlayer.IsRepeating = true;
                m1 = true;
                m2 = false;
                m3 = false;
            }
            KeyboardHandler();
        }
        private void UpdateEndOfGame(GameTime deltaTime)
        {
            if (!m3)
            {
                MediaPlayer.Play(Music[2]);
                MediaPlayer.IsRepeating = false;
                m3 = true;
                m1 = false;
                m2 = false;
            }
            KeyboardHandler();
        }
        private void UpdateControls(GameTime deltaTime) { 
            KeyboardHandler();
        }
        private void UpdateOptions(GameTime deltaTime)
        {
            KeyboardHandler();
        }
        private void UpdateHistory(GameTime deltaTime)
        {
            KeyboardHandler();
        }
        private void UpdateLogoScreen(GameTime gameTime)
        {
            if (drawSplashScreen)
            {
                if (splashAlpha < 1.0f && tSeconds < 3) { splashAlpha += 0.01f; }
                if (splashAlpha >= 1.0f)
                {
                    secs += gameTime.ElapsedGameTime.TotalSeconds;
                    secsRounded = Math.Floor(secs);
                    secsRounded = (MathHelper.Clamp((int)secsRounded, 1, 60)); 
                    tSeconds = (int)secsRounded; 
                    splashAlpha = 1.0f;

                    if (tSeconds >= 3) { tSeconds = 3; } 
                }
                if (splashAlpha > 0.0f && tSeconds == 3) { splashAlpha -= 0.01f; }
                if (splashAlpha <= 0.0f && tSeconds == 3)
                {
                    splashAlpha = 0.0f;
                    drawSplashScreen = false; 
                    secs = 0; secsRounded = 0; tSeconds = 0; 
                    timer = 0; steps = 1;
                }
            }

            base.Update(gameTime);
        }
        protected override void Update(GameTime gameTime)
        {
            if (darktheme)
            {
                color = Color.White;
            }
            else
            {
                color = Color.Black;
            }
            MediaPlayer.Volume = volume/100.0f;
            SoundEffect.MasterVolume = volume2 / 100.0f;
            KeyboardHandler();
            if (!pause)
            {
                if (!heroes[0].is_died)
                {
                    temp = "";
                    temp = string.Concat(heroes[0].Health.ToString(), "\n", heroes[0].Dash.ToString(), "\n", heroes[0].keys.ToString());
                }
                if (players == 2)
                {
                    if (!heroes[1].is_died)
                    {

                        temp1 = "";
                        temp1 = string.Concat(heroes[1].Health.ToString(), "\n", heroes[1].Dash.ToString(), "\n", heroes[1].keys.ToString());
                    }
                }
            }
            switch (_state)
            {
                case GameState.MainMenu:
                    UpdateMainMenu(gameTime);
                    break;
                case GameState.Gameplay:
                    UpdateGameplay(gameTime);
                    break;
                case GameState.EndOfGame:
                    UpdateEndOfGame(gameTime);
                    break;
                case GameState.Options:
                    UpdateOptions(gameTime);
                    break;
                case GameState.Controls:
                    UpdateControls(gameTime);
                    break;
                case GameState.History:
                    UpdateHistory(gameTime);
                    break;
                case GameState.LogoScreen:
                    UpdateLogoScreen(gameTime);
                    break;
            }  
            base.Update(gameTime);
        }
        private void DrawMainMenu(GameTime gameTime)
        {
            GamePadState state1 = GamePad.GetState(PlayerIndex.One);
            if (darktheme)
            {
                GraphicsDevice.Clear(Color.Black);
            }
            else
            {
                GraphicsDevice.Clear(Color.White);
            }
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            string mainmenu = "DUNGEONMAN!";
            if (players == 2)
            {
                mainmenu = "DUNGEONMEN!";
                spriteBatch.Draw(Icon2, new Vector2(760, 0), Color.White * 0.65f);
            }
            spriteBatch.Draw(Icon1, new Vector2(900, 0), Color.White);
            spriteBatch.DrawString(mmfont, mainmenu, new Vector2(95, 470), color);
            spriteBatch.DrawString(font, "FIND THE WAY FROM THE DUNGEON, AND BECOME", new Vector2(100, 460), color);
            spriteBatch.DrawString(font, String.Concat((rus==true)?"НОВАЯ ИГРА: ":"NEW GAME: ",gametype==0 ? ((rus==true)? "1 ИГРОК (КЛАВИАТУРА)" : "1 PLAYER (KEYBOARD)") : gametype == 1 ? ((rus == true) ? "1 ИГРОК (ГЕЙМПАД)" : "1 PLAYER (GAMEPAD)") : gametype ==2 ? ((rus == true) ? "2 ИГРОКА (КЛАВИАТУРА + ГЕЙМПАД)" : "2 PLAYERS (KEYBOARD + GAMEPAD)") : gametype == 3 ? ((rus == true) ? "2 ИГРОКА (ГЕЙМПАД + КЛАВИАТУРА)" : "2 PLAYERS (GAMEPAD + KEYBOARD)") : ((rus == true) ? "2 ИГРОКА (КЛАВИАТУРА + КЛАВИАТУРА)" : "2 PLAYERS (KEYBOARD + KEYBOARD)")), new Vector2(100, 680), switcher==0 ? new Color(186, 123, 70) : color);
            spriteBatch.DrawString(font, (rus==false)? "OPTIONS" : "ОПЦИИ", new Vector2(100, 710), switcher == 1 ? new Color(186, 123, 70) : color);
            spriteBatch.DrawString(font, (rus == false) ? "CONTROLS" : "УПРАВЛЕНИЕ", new Vector2(100, 740), switcher == 2 ? new Color(186, 123, 70) : color);
            spriteBatch.DrawString(font, (rus == false) ? "HISTORY OF VERSIONS" : "ИСТОРИЯ ВЕРСИЙ", new Vector2(100, 770), switcher == 3 ? new Color(186, 123, 70) : color);
            if (!state1.IsConnected && (gametype == 1 || gametype == 2 || gametype == 3))
            {
                spriteBatch.DrawString(font, (rus == false) ? "! GAMEPAD IS NOT CONNECTED. TO PLAY WITH IT, CONNECT A GAMEPAD AND TRY AGAIN !" : "! ГЕЙМПАД НЕ ПОДКЛЮЧЕН. ДЛЯ ИГРЫ С ГЕЙМПАДОМ, ПОДКЛЮЧИТЕ ЕГО И ПОПРОБУЙТЕ СНОВА !", new Vector2(100, 830), new Color(152, 52, 41));
            }
            spriteBatch.DrawString(font, (rus == false) ? "EXIT" : "ВЫХОД", new Vector2(100, 1010), switcher == 4 ? new Color(186, 123, 70) : color);
            spriteBatch.DrawString(font, "v1.96", new Vector2(10, 1050), darktheme == true ? new Color(40, 40, 40) : new Color(220, 220, 220));
            spriteBatch.End();
        }
        private void DrawOptions(GameTime gameTime)
        {
            if (darktheme)
            {
                GraphicsDevice.Clear(Color.Black);
            }
            else
            {
                GraphicsDevice.Clear(Color.White);
            }
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            string mainmenu = "DUNGEONMAN!";
            if (players == 2)
            {
                mainmenu = "DUNGEONMEN!";
                spriteBatch.Draw(Icon2, new Vector2(760, 0), Color.White * 0.65f);
            }
            spriteBatch.Draw(Icon1, new Vector2(900, 0), Color.White);
            spriteBatch.DrawString(mmfont, mainmenu, new Vector2(95, 470), color);
            spriteBatch.DrawString(font, "FIND THE WAY FROM THE DUNGEON, AND BECOME", new Vector2(100, 460), color);
            spriteBatch.DrawString(font, (rus == false) ? String.Concat("DIFFICULTY: ", difficulty == 0 ? "NORMAL" : difficulty == 1 ? "HARD" : "ULTRAHARD") : String.Concat("СЛОЖНОСТЬ: ", difficulty == 0 ? "НОРМАЛЬНАЯ" : difficulty == 1 ? "СЛОЖНАЯ" : "УЛЬТРАСЛОЖНАЯ"), new Vector2(100, 680), switcher2 == 0 ? new Color(186, 123, 70) : color);
            spriteBatch.DrawString(font, (rus == false) ? String.Concat("MODES: ", modes == false ? "OFF" :  "ON") : String.Concat("РЕЖИМЫ: ", modes == false ? "ВЫКЛ" : "ВКЛ"), new Vector2(100, 710), switcher2 == 1 ? new Color(186, 123, 70) : color);
            spriteBatch.DrawString(font, String.Concat((rus == false) ? "MUSIC VOLUME: " : "ГРОМКОСТЬ МУЗЫКИ: ", volume.ToString()), new Vector2(100, 740), switcher2 == 2 ? new Color(186, 123, 70) : color);
            spriteBatch.DrawString(font, String.Concat((rus == false) ? "SOUND EFFECTS VOLUME: " : "ГРОМКОСТЬ ЭФФЕКТОВ: ", volume2.ToString()), new Vector2(100, 770), switcher2 == 3 ? new Color(186, 123, 70) : color);
            spriteBatch.DrawString(font, (rus == false) ? String.Concat("LANGUAGE: ", (rus==true) ? "RUS" : "ENG"): String.Concat("ЯЗЫК: ", (rus == true) ? "РУС" : "АНГЛ"), new Vector2(100, 800), switcher2 == 4 ? new Color(186, 123, 70) : color);
            spriteBatch.DrawString(font, (rus == false) ? String.Concat("DARK THEME: ", (darktheme == true) ? "ON" : "OFF") : String.Concat("ТЕМНАЯ ТЕМА: ", (darktheme == true) ? "ВКЛ" : "ВЫКЛ"), new Vector2(100, 830), switcher2 == 5 ? new Color(186, 123, 70) : color);

            spriteBatch.DrawString(font, (rus == false) ? "BACK": "НАЗАД", new Vector2(100, 1010), switcher2 == 6 ? new Color(186, 123, 70) : color);
            spriteBatch.DrawString(font, "v1.96", new Vector2(10, 1050), darktheme == true ? new Color(40, 40, 40) : new Color(220, 220, 220));
            spriteBatch.End();
        }
        private void DrawControls(GameTime gameTime)
        {
            if (darktheme)
            {
                GraphicsDevice.Clear(Color.Black);
            }
            else
            {
                GraphicsDevice.Clear(Color.White);
            }
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            string mainmenu = "DUNGEONMAN!";
            if (players == 2)
            {
                mainmenu = "DUNGEONMEN!";
                spriteBatch.Draw(Icon2, new Vector2(760, 0), Color.White * 0.65f);
            }
            spriteBatch.Draw(Icon1, new Vector2(900, 0), Color.White);
            spriteBatch.DrawString(mmfont, mainmenu, new Vector2(95, 470), color);
            spriteBatch.DrawString(font, "FIND THE WAY FROM THE DUNGEON, AND BECOME", new Vector2(100, 460), color);
            if (gametype == 0 || gametype == 2 || gametype == 4)
            {
                spriteBatch.Draw(Icons[4], new Vector2(60, 650), new Color(186, 123, 70));
                spriteBatch.DrawString(cfont, "W", new Vector2(230, 710), color);
                spriteBatch.DrawString(cfont, "A", new Vector2(132, 805), color);
                spriteBatch.DrawString(cfont, "S", new Vector2(230, 805), color);
                spriteBatch.DrawString(cfont, "D", new Vector2(328, 805), color);
                spriteBatch.DrawString(cfont, "SHIFT", new Vector2(473, 805), color);
                spriteBatch.DrawString(font, "WASD buttons to move, SHIFT to dash", new Vector2(100, 890), Color.White);
            }
            else
            {
                spriteBatch.Draw(Icons[6], new Vector2(60, 650), new Color(186, 123, 70));
                spriteBatch.DrawString(cfont, "L", new Vector2(232, 751), color);
                spriteBatch.DrawString(cfont, "X", new Vector2(493, 803), color);
                spriteBatch.DrawString(font, "LEFT STICK to move, X to dash", new Vector2(100, 890), Color.White);
            }
            if (players == 2)
            {
                if (gametype == 0 || gametype == 2)
                {
                    spriteBatch.Draw(Icons[6], new Vector2(800, 650), new Color(112, 142, 47));
                    spriteBatch.DrawString(cfont, "L", new Vector2(972, 751), color);
                    spriteBatch.DrawString(cfont, "X", new Vector2(1233, 803), color);
                    spriteBatch.DrawString(font, "LEFT STICK to move, X to dash", new Vector2(860, 890), Color.White);
                }
                else
                {
                    spriteBatch.Draw(Icons[4], new Vector2(800, 650), new Color(112, 142, 47));
                    spriteBatch.Draw(Icons[5], new Vector2(800, 650), color);
                    spriteBatch.DrawString(cfont, "SPACE", new Vector2(1205, 805), color);
                    spriteBatch.DrawString(font, "ARROW buttons to move, SPACE to dash", new Vector2(840, 890), Color.White);
                }
            }
            spriteBatch.DrawString(font, (rus == false) ? "BACK":"НАЗАД", new Vector2(100, 1010), switcher3 == 0 ? new Color(186, 123, 70) : color);
            spriteBatch.DrawString(font, "v1.96", new Vector2(10, 1050), darktheme ==true ? new Color(40, 40, 40) : new Color(220, 220, 220));
            spriteBatch.End();
        }
        private void DrawHistory(GameTime gameTime)
        {
            if (darktheme)
            {
                GraphicsDevice.Clear(Color.Black);
            }
            else
            {
                GraphicsDevice.Clear(Color.White);
            }
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            string mainmenu = "DUNGEONMAN!";
            if (players == 2)
            {
                mainmenu = "DUNGEONMEN!";
                spriteBatch.Draw(Icon2, new Vector2(760, 0), Color.White * 0.65f);
            }
            spriteBatch.Draw(Icon1, new Vector2(900, 0), Color.White);
            spriteBatch.DrawString(mmfont, mainmenu, new Vector2(95, 470), color);
            spriteBatch.DrawString(font, "FIND THE WAY FROM THE DUNGEON, AND BECOME", new Vector2(100, 460), color);
            spriteBatch.DrawString(font, "[PRE-ALPHA-SUPER-MEGA VERSION 1.96] UPDATE:", new Vector2(100, 670), new Color(186, 123, 70));
            spriteBatch.DrawString(font, (rus == false) ? "  Gamepad support fixes. New gamemodes. 'Control' fixes. Font fixes." : " Фиксы поддержки геймпадов. Новые игровые режимы. Фиксы раздела 'Управление'. Фиксы шрифта.", new Vector2(100, 700), color);
            spriteBatch.DrawString(font, "[PRE-ALPHA-SUPER-MEGA VERSION 1.9] UPDATE:", new Vector2(100, 760), new Color(186, 123, 70));
            spriteBatch.DrawString(font, (rus == false) ? "  Gamepad support." : "  Поддержка геймпадов.", new Vector2(100, 790), color);
            spriteBatch.DrawString(font, "[PRE-ALPHA-SUPER-MEGA VERSION 1.8] UPDATE:", new Vector2(100, 850), new Color(186, 123, 70));
            spriteBatch.DrawString(font, (rus == false) ? "  New gameplay feature - Modes. 'Options' improvements. White Theme for UI" : "  Новая особенность - Режимы игры. Улучшение раздела 'Опции'. Белая тема для UI.", new Vector2(100, 880), color);
            spriteBatch.DrawString(font, (rus == false) ? "BACK" : "НАЗАД", new Vector2(100, 1010), switcher5 == 0 ? new Color(186, 123, 70) : color);
            spriteBatch.DrawString(font, "v1.96", new Vector2(10, 1050), darktheme == true ? new Color(40, 40, 40) : new Color(220, 220, 220));
            spriteBatch.End();
        }
        private void DrawGameplay(GameTime gameTime)
        {
            if (mode == 0)
            {
                modetype = rus == false ? "NORMAL MODE" : "СТАНДАРТНЫЙ РЕЖИМ";
            }
            else if (mode == 1)
            {
                modetype = rus == false ? "DARK MODE" : "РЕЖИМ ТЕМНОТЫ";
            }
            else if (mode == 2)
            {
                modetype = rus == false ? "NO POTIONS!" : "НЕТ ЗЕЛЬЯМ!";
            }
            else if (mode == 3)
            {
                modetype = rus == false ? "ONE LIFE" : "ОДНА ЖИЗНЬ";
            }
            else if (mode == 4)
            {
                modetype = rus == false ? "NOIR MODE" : "РЕЖИМ НУАРА";
            }
            else if (mode == 5)
            {
                modetype = rus == false ? "RAINBOW POTIONS?!" : "РАДУЖНЫЕ ЗЕЛЬЯ?!";
            }
            if (mode == 1)
            {
                GraphicsDevice.SetRenderTarget(lightsTarget);
                GraphicsDevice.Clear(Color.Black);
                spriteBatch.Begin();

                if (!heroes[0].is_died)
                {
                    spriteBatch.Draw(difficulty == 0 ? lightMask : difficulty == 1 ? lightMask2 : lightMask3, new Vector2(heroes[0].Position.X - (difficulty == 0 ? 224 : difficulty == 1 ? 160 : 112), heroes[0].Position.Y - (difficulty == 0 ? 224 : difficulty == 1 ? 160 : 112)), Color.White);
                }
                if (players == 2)
                {
                    if (!heroes[1].is_died)
                    {
                        spriteBatch.Draw(difficulty == 0 ? lightMask : difficulty == 1 ? lightMask2 : lightMask3, new Vector2(heroes[1].Position.X - (difficulty == 0 ? 224 : difficulty == 1 ? 160 : 112), heroes[1].Position.Y - (difficulty == 0 ? 224 : difficulty == 1 ? 160 : 112)), Color.White);
                    }
                }
                spriteBatch.End();
            

            GraphicsDevice.SetRenderTarget(mainTarget);
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null,null);
            }
            else if (mode == 4)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                effects[1].CurrentTechnique.Passes[0].Apply();
            }
            else
            {
                spriteBatch.Begin();

            }
            spriteBatch.Draw(map==0 ? Back1 : Back2, new Rectangle(0, 0, 1920, 1080), Color.White);
            if (pause)
            {
                spriteBatch.DrawString(mmfont, (rus == false) ? "PAUSE" : "ПАУЗА", new Vector2(720, 400), map == 0 ? new Color(118, 89, 57) : new Color(176, 144, 110));
            }
            else
            {
                if (rus == false)
                {
                    spriteBatch.DrawString(mmfont, string.Concat("TIME:", Math.Round(time).ToString()), new Vector2(960- mmfont.MeasureString(string.Concat("TIME:", Math.Round(time).ToString())).X/2, 400),map==0 ? new Color(118, 89, 57): new Color(176, 144, 110));
                }
                else
                {
                    spriteBatch.DrawString(mmfont, string.Concat("ВРЕМЯ:", Math.Round(time).ToString()), new Vector2(960 - mmfont.MeasureString(string.Concat("ВРЕМЯ:", Math.Round(time).ToString())).X/2, 400), map == 0 ? new Color(118, 89, 57) : new Color(176, 144, 110));
                }
            }
            Vector2 textsize = font.MeasureString(modetype);
            spriteBatch.DrawString(font, modetype, new Vector2(960-textsize.X/2, 530), map == 0 ? new Color(118, 89, 57) : new Color(176, 144, 110));
            if (mode == 5)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                effects[2].CurrentTechnique.Passes[0].Apply();
                foreach (Potion p in Potions)
                {
                    p.Draw(spriteBatch);
                }
                spriteBatch.End();
            }
            else
            {
                foreach (Potion p in Potions)
                {
                    p.Draw(spriteBatch);
                }
                spriteBatch.End();
            }
            foreach (Hero h in heroes)
            {
                h.Draw(spriteBatch, PlayerPosition,mode, effects[1]);
            }           
            if (mode == 4)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                effects[1].CurrentTechnique.Passes[0].Apply();
            }
            else
            {
                spriteBatch.Begin();
            }
            foreach (Enemy e in Enemies)
            {
                e.Draw(spriteBatch);
            }
            spriteBatch.End();
            if (mode == 1)
            {
                GraphicsDevice.SetRenderTarget(null);
                GraphicsDevice.Clear(Color.Black);
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                effects[0].Parameters["lightMask"].SetValue(lightsTarget);
                effects[0].CurrentTechnique.Passes[0].Apply();
                spriteBatch.Draw(mainTarget, Vector2.Zero, Color.White);
                spriteBatch.End();
            }
            if (mode == 4)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                effects[1].CurrentTechnique.Passes[0].Apply();
            }
            else
            {
                spriteBatch.Begin();
            }
            spriteBatch.DrawString(font, map == 0 ? "Classic Dungeon " : "Egypt Dungeon ", new Vector2(960-font.MeasureString(map == 0 ? "Classic Dungeon " : "Egypt Dungeon ").X/2, 1024), Color.White);
            if (!heroes[0].is_died)
            {
                spriteBatch.DrawString(font, heroes[0].Name, new Vector2(heroes[0].Position.X + 18, heroes[0].Position.Y - 15), new Color(186, 123, 70));
                spriteBatch.DrawString(font, "P1: ", new Vector2(16, 942), new Color(186, 123, 70));
                spriteBatch.DrawString(font, heroes[0].Score.ToString(), new Vector2(65, 942), Color.White);
                spriteBatch.Draw(Icons[0], new Rectangle(16, 965, 32, 32), new Color(186, 123, 70));
                spriteBatch.Draw(Icons[1], new Rectangle(16, 993, 32, 32), new Color(186, 123, 70));
                spriteBatch.Draw(Icons[2], new Rectangle(16, 1024, 32, 32), new Color(186, 123, 70));
                spriteBatch.DrawString(font, temp, new Vector2(65, 973), Color.White);
            }
            if (players == 2)
            {
                if (!heroes[1].is_died)
                {
                    spriteBatch.DrawString(font, heroes[1].Name, new Vector2(heroes[1].Position.X + 16, heroes[1].Position.Y - 15), new Color(112, 142, 47));
                    spriteBatch.DrawString(font, "P2: ", new Vector2(1800, 942), new Color(112, 142, 47));
                    spriteBatch.DrawString(font, heroes[1].Score.ToString(), new Vector2(1849, 942), Color.White);
                    spriteBatch.Draw(Icons[0], new Rectangle(1800, 965, 32, 32), new Color(112, 142, 47));
                    spriteBatch.Draw(Icons[1], new Rectangle(1800, 993, 32, 32), new Color(112, 142, 47));
                    spriteBatch.Draw(Icons[2], new Rectangle(1800, 1024, 32, 32), new Color(112, 142, 47));
                    spriteBatch.DrawString(font, temp1, new Vector2(1849, 973), Color.White);
                }
            }
            spriteBatch.End();
        }
        private void DrawEndOfGame(GameTime gameTime)
        {
            if (darktheme)
            {
                GraphicsDevice.Clear(Color.Black);
            }
            else
            {
                GraphicsDevice.Clear(Color.White);
            }
            List <string> Phrases = new List<string>();
            Phrases.Add((rus==false)?"Okey...That was bad, to be honest":"Окей...Это было ужасно, если честно");
            Phrases.Add((rus == false) ? "Sorry, but my mom plays better...":"Прости, но моя мама играет лучше...");
            Phrases.Add((rus == false) ? string.Concat("Ooh, just ", Math.Round(time).ToString(), " seconds.."): string.Concat("Оуу, всего ", Math.Round(time).ToString(), " секунд.."));
            Phrases.Add((rus == false) ? "Come on, I'm so tired of this shit":"Ооо, я так устал от ЭТОГО");
            Phrases.Add("._.");
            Phrases.Add((rus == false) ? "Seriously?..":"Серьезно?..");
            Phrases.Add((rus == false) ? "Finally! We have a winner!":"Наконец! У нас есть победитель!");
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            string mainmenu = "DUNGEONMAN!";
            if (players == 2)
            {
                mainmenu = "DUNGEONMEN!";
                spriteBatch.Draw(Icon2, new Vector2(760, 0), Color.White * 0.65f);
            }
            spriteBatch.Draw(Icon1, new Vector2(900, 0), Color.White);

            if (players == 2)
            {
                spriteBatch.DrawString(mmfont, mainmenu, new Vector2(95, 470), (heroes[0].is_collected || heroes[1].is_collected) ? new Color(226, 149, 63) : new Color(152, 52, 41));
            }
            else
            {
                spriteBatch.DrawString(mmfont, mainmenu, new Vector2(95, 470), (heroes[0].is_collected) ? new Color(226, 149, 63) : new Color(152, 52, 41));
            }
            spriteBatch.DrawString(font, "FIND THE WAY FROM THE DUNGEON, AND BECOME", new Vector2(100, 460), color);
            if (players == 2)
            {
                if (heroes[0].is_collected || heroes[1].is_collected)
                {
                    spriteBatch.DrawString(font, Phrases[6], new Vector2(100, 680), color);
                }
                else
                {
                    spriteBatch.DrawString(font, Phrases[phrase], new Vector2(100, 680), color);
                }
            }
            else
            {
                if (heroes[0].is_collected)
                {
                    spriteBatch.DrawString(font, Phrases[6], new Vector2(100, 680), color);
                }
                else
                {
                    spriteBatch.DrawString(font, Phrases[phrase], new Vector2(100, 680), color);
                }
            }
            
            if (heroes[0].is_collected)
            {
                if (rus == false)
                {
                    spriteBatch.DrawString(font, "Copper ", new Vector2(100, 710), new Color(186, 123, 70));
                    spriteBatch.DrawString(font, string.Concat("collected all keys and his score is ", heroes[0].Score.ToString(), "! Congratulations!"), new Vector2(220, 710), color);
                }
                else
                {
                    spriteBatch.DrawString(font, "Коппер ", new Vector2(100, 710), new Color(186, 123, 70));
                    spriteBatch.DrawString(font, string.Concat("собрал все ключи и его рекорд ", heroes[0].Score.ToString(), "! Поздравляем!"), new Vector2(220, 710), color);
                }
            }
            else if(!heroes[0].is_collected)
            {
                if (rus == false)
                {
                    spriteBatch.DrawString(font, "Meanwhile, the ", new Vector2(100, 710), color);
                    spriteBatch.DrawString(font, "Copper's ", new Vector2(330, 710), new Color(186, 123, 70));
                    spriteBatch.DrawString(font, string.Concat("score is ", heroes[0].Score.ToString()), new Vector2(500, 710), color);
                }
                else
                {
                    spriteBatch.DrawString(font, "Тем временем, рекорд ", new Vector2(100, 710), color);
                    spriteBatch.DrawString(font, "Коппера ", new Vector2(470, 710), new Color(186, 123, 70));
                    spriteBatch.DrawString(font, heroes[0].Score.ToString(), new Vector2(620, 710), color);
                }
            }
            if (players == 2)
            {
                if (heroes[0].is_collected)
                {
                    if (rus == false)
                    {
                        spriteBatch.DrawString(font, "Copper ", new Vector2(100, 710), new Color(186, 123, 70));
                        spriteBatch.DrawString(font, string.Concat("collected all keys and his score is ", heroes[0].Score.ToString(), "! Congratulations!"), new Vector2(220, 710), color);
                    }
                    else
                    {
                        spriteBatch.DrawString(font, "Коппер ", new Vector2(100, 710), new Color(186, 123, 70));
                        spriteBatch.DrawString(font, string.Concat("собрал все ключи и его рекорд ", heroes[0].Score.ToString(), "! Поздравляем!"), new Vector2(220, 710), color);
                    }
                }
                else if (heroes[1].is_collected)
                {
                    if (rus == false)
                    {
                        spriteBatch.DrawString(font, "Green ", new Vector2(100, 710), new Color(112, 142, 47));
                        spriteBatch.DrawString(font, string.Concat("collected all keys and his score is ", heroes[0].Score.ToString(), "! Congratulations!"), new Vector2(170, 710), color);
                    }
                    else
                    {
                        spriteBatch.DrawString(font, "Грин ", new Vector2(100, 710), new Color(112, 142, 47));
                        spriteBatch.DrawString(font, string.Concat("собрал все ключи и его рекорд ", heroes[0].Score.ToString(), "! Поздравляем!"), new Vector2(170, 710), color);
                    }
                }
                else if(!heroes[1].is_collected)
                {
                    if (rus == false)
                    {
                        spriteBatch.DrawString(font, "and the ", new Vector2(105, 740), color);
                        spriteBatch.DrawString(font, "Green's ", new Vector2(240, 740), new Color(112, 142, 47));
                        spriteBatch.DrawString(font, string.Concat("score is ", heroes[1].Score.ToString()), new Vector2(380, 740), color);
                    }
                    else
                    {
                        spriteBatch.DrawString(font, "а рекорд ", new Vector2(105, 740), color);
                        spriteBatch.DrawString(font, "Грина ", new Vector2(270, 740), new Color(112, 142, 47));
                        spriteBatch.DrawString(font, heroes[1].Score.ToString(), new Vector2(380, 740), color);
                    }
                }
            }
            if (players == 1 || heroes[0].Score >= heroes[1].Score)
            {
                score = heroes[0].Score;
                if (score > best)
                {
                    b = true;
                }
                if (b)
                {
                    if (rus == false)
                    {
                        spriteBatch.DrawString(font, "Copper's ", new Vector2(100, (players == 2) ? 800 : 770), new Color(186, 123, 70));
                    }
                    else
                    {
                        spriteBatch.DrawString(font, "Коппер - ", new Vector2(100, (players == 2) ? 800 : 770), new Color(186, 123, 70));
                    }
                }
            }
            else
            {
                score = heroes[1].Score;
                if (score > best)
                {
                    b = true;
                }
                if (b)
                {
                    if (rus == false)
                    {
                        spriteBatch.DrawString(font, "Green's ", new Vector2(100, (players == 2) ? 800 : 770), new Color(112, 142, 47));
                    }
                    else
                    {
                        spriteBatch.DrawString(font, "Грин - ", new Vector2(100, (players == 2) ? 800 : 770), new Color(112, 142, 47));
                    }
                }
            }
            if (b)
            {
                if (rus == false)
                {
                    spriteBatch.DrawString(font, "name: ", new Vector2(230, (players == 2) ? 820 : 770), color);
                }
                else
                {
                    spriteBatch.DrawString(font, "имя: ", new Vector2(230, (players == 2) ? 820 : 770), color);
                }
                if (!entered)
                {
                    spriteBatch.Draw(Icons[3], new Vector2(312 + (shift_right * 16 + 1 * shift_right), (players == 2) ? 775 : 745), Color.White * 0.65f);
                }
                spriteBatch.DrawString(font, name1, new Vector2(320, (players == 2) ? 800 : 770), color);
                if (entered)
                {
                    if (!saving)
                    {
                        if (score > 1000)
                        {
                            WriteToTableOfRecords();
                            saving = true;
                        }
                    }
                    spriteBatch.DrawString(font, (rus == false) ? "MAIN MENU":"ГЛАВНОЕ МЕНЮ", new Vector2(100, players == 2 ? 850 : 820), switcher4 == 0 ? new Color(186, 123, 70) : color);
                    spriteBatch.DrawString(font, (rus == false) ? "EXIT":"ВЫХОД", new Vector2(100, players == 2 ? 880 : 850), switcher4 == 1 ? new Color(186, 123, 70) : color);

                }
            }
            else
            {
                spriteBatch.DrawString(font, string.Concat((rus == false) ? "Legendary dungeonman is ":"Легендарный данжмен ",name2, (rus == false) ? " and his score is ":" и его рекорд ", best.ToString()), new Vector2(100, (players == 2) ? 800 : 770), color);
                spriteBatch.DrawString(font, (rus == false) ? "MAIN MENU" : "ГЛАВНОЕ МЕНЮ", new Vector2(100, players == 2 ? 850 : 820), switcher4 == 0 ? new Color(186, 123, 70) : color);
                spriteBatch.DrawString(font, (rus == false) ? "EXIT" : "ВЫХОД", new Vector2(100, players == 2 ? 880 : 850), switcher4 == 1 ? new Color(186, 123, 70) : color);
            }
            spriteBatch.DrawString(font, "v1.96", new Vector2(10, 1050), darktheme == true ? new Color(40, 40, 40) : new Color(220, 220, 220));
            spriteBatch.End();
        }
        private void DrawLogoScreen(GameTime gameTime)
        {
            if (timer < 40 && steps == 0) { timer += 1; }
            if (timer >= 40) { timer = 40; drawSplashScreen = true; }

            if (drawSplashScreen == true)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                spriteBatch.Draw(Screen, new Vector2(1920 / 2, 1080 / 2), null, Color.White * splashAlpha, 0,
                    new Vector2(Screen.Width / 2, Screen.Height / 2), 1.0f, SpriteEffects.None, 1.0f);
                spriteBatch.End();
                if (splashAlpha == 1f)
                {
                    _state = GameState.MainMenu;
                }
            }         
        }
        protected override void Draw(GameTime gameTime)
        {
            switch (_state)
            {
                case GameState.MainMenu:
                    DrawMainMenu(gameTime);
                    break;
                case GameState.Gameplay:
                    DrawGameplay(gameTime);
                    break;
                case GameState.EndOfGame:
                    DrawEndOfGame(gameTime);
                    break;
                case GameState.LogoScreen:
                    DrawLogoScreen(gameTime);
                    break;
                case GameState.Options:
                    DrawOptions(gameTime);
                    break;
                case GameState.History:
                    DrawHistory(gameTime);
                    break;
                case GameState.Controls:
                    DrawControls(gameTime);
                    break;
            }
            base.Draw(gameTime);
        }
        void ControlGameplay()
        {
            GamePadState state1 = GamePad.GetState(PlayerIndex.One);
            KeyboardState curState;
            curState = Keyboard.GetState();
            
            if (curState.IsKeyDown(Keys.P)||(state1.IsButtonDown(Buttons.Start)))
            {
                pausecount++;
                if (pausecount % 10 == 0)
                {
                    pause = !pause;
                }
            }
        }
        void ControlMainMenu()
        {
            GamePadState state1 = GamePad.GetState(PlayerIndex.One);
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Enter)|| state1.IsButtonDown(Buttons.A))
            {
                if (switcher == 0)
                {
                    if (!state1.IsConnected && (gametype == 1 || gametype == 2 || gametype == 3))
                    {

                    }
                    else
                    {
                        _state = GameState.Gameplay;
                    }
                }
                else if(switcher == 1)
                {
                    _state = GameState.Options;
                }
                else if (switcher == 2)
                {
                    _state = GameState.Controls;
                }
                else if (switcher == 3)
                {
                    _state = GameState.History;
                }
                else if (switcher == 4)
                {
                    Exit();
                }
                switcher = -1;
            }
            if (switcher == 0)
            {
                if (state.IsKeyDown(Keys.Right) || state1.IsButtonDown(Buttons.DPadRight))
                {
                    switchercount++;
                    if (switchercount % 15 == 0)
                    {
                        gametype++;
                    }
                    if (gametype == 3 || gametype == 4 || gametype == 2)
                    {
                        players = 2;
                    }
                    else
                    {
                        players = 1;
                    }
                    if (gametype > 4)
                    {
                        gametype = 0;
                    }
                }
                if (state.IsKeyDown(Keys.Left) || state1.IsButtonDown(Buttons.DPadLeft))
                {
                    switchercount++;
                    if (switchercount % 15 == 0)
                    {
                        gametype--;
                    }
                    if (gametype == 3 || gametype == 4|| gametype == 2)
                    {
                        players = 2;
                    }
                    else
                    {
                        players = 1;
                    }
                    if (gametype < 0)
                    {
                        gametype = 4;
                    }
                }
            }

            if (state.IsKeyDown(Keys.Up) || state1.IsButtonDown(Buttons.DPadUp))
            {
                switchercount++;
                if (switchercount % 15==0)
                {
                    switcher--;
                    if (switcher < 0)
                    {
                        switcher = 4;
                    }
                }
            }
            if (state.IsKeyDown(Keys.Down) || state1.IsButtonDown(Buttons.DPadDown))
            {
                switchercount++;
                if (switchercount % 15==0)
                {
                    switcher++;
                    if (switcher > 4)
                    {
                        switcher = 0;
                    }
                }              
            }
        }
        void ControlOptions()
        {
            GamePadState state1 = GamePad.GetState(PlayerIndex.One);
            KeyboardState state = Keyboard.GetState();
            if (state1.IsButtonDown(Buttons.B)){
                _state = GameState.MainMenu;
                if (modes)
                {
                    mode = random.Next(0, 6);
                }
                else
                {
                    mode = 0;
                }
                switcher2 = -1;
            }
            if (state.IsKeyDown(Keys.Enter) || state1.IsButtonDown(Buttons.A))
            {
                if (switcher2 == 6)
                {
                    _state = GameState.MainMenu;
                    if (modes)
                    {
                        mode = random.Next(0, 4);
                    }
                    else
                    {
                        mode = 0;
                    }
                }
                switcher2 = -1;
            }
            if (switcher2 != 6)
            {
                if (state.IsKeyDown(Keys.Right) || state1.IsButtonDown(Buttons.DPadRight))
                {
                    switchercount++;
                if (switcher2 == 0)
                {
                    if (switchercount % 15==0)
                    {
                        difficulty++;
                        if (difficulty >= 2)
                        {
                            difficulty = 2;
                        }
                    }
                }
                    else if (switcher2 == 1)
                    {
                        if (switchercount % 25 == 0)
                        {
                            modes=!modes;
                           
                        }
                    }
                    else if (switcher2 == 2)
                {
                    if (switchercount % 15==0)
                    {
                        volume+=2;
                        if (volume >= 100)
                        {
                            volume = 100;
                        }
                    }
                }
                else if (switcher2 == 3)
                {
                    if (switchercount % 15==0)
                    {
                        volume2+=2;
                        if (volume2 >= 100)
                        {
                            volume2 = 100;
                        }
                    }
                }
                else if (switcher2 == 4)
                {
                    if (switchercount % 25==0)
                    {
                        rus=!rus;                      
                    }
                }
                    else if (switcher2 == 5)
                    {
                        if (switchercount % 25 == 0)
                        {
                            darktheme = !darktheme;
                        }
                    }
                }
            
                if (state.IsKeyDown(Keys.Left) || state1.IsButtonDown(Buttons.DPadLeft))
                {
                    if (switcher2 == 0)
                    {
                        switchercount++;
                        if (switchercount % 15==0)
                        {
                            difficulty--;
                            if (difficulty <= 0)
                            {
                                difficulty = 0;
                            }
                        }
                    }
                    else if (switcher2 == 1)
                    {
                        switchercount++;
                        if (switchercount % 25 == 0)
                        {
                            modes=!modes;                          
                        }
                    }
                    else if (switcher2 == 2)
                    {
                        switchercount++;
                        if (switchercount % 15==0)
                        {
                            volume-=2;
                            if (volume <= 0)
                            {
                                volume = 0;
                            }
                        }
                    }
                    else if (switcher2 == 3)
                    {
                        switchercount++;
                        if (switchercount % 15==0)
                        {
                            volume2-=2;
                            if (volume2 <= 0)
                            {
                                volume2 = 0;
                            }
                        }
                    }
                    else if (switcher2 == 4)
                    {
                        switchercount++;
                        if (switchercount % 25==0)
                        {
                            rus = !rus;
                        }
                    }
                    else if (switcher2 == 5)
                    {
                        switchercount++;
                        if (switchercount % 25 == 0)
                        {
                            darktheme = !darktheme;
                        }
                    }
                }
            }

            if (state.IsKeyDown(Keys.Up) || state1.IsButtonDown(Buttons.DPadUp))
            {
                switchercount++;
                if (switchercount % 15==0)
                {
                    switcher2--;
                    if (switcher2 < 0)
                    {
                        switcher2 = 6;
                    }
                }
            }
            if (state.IsKeyDown(Keys.Down) || state1.IsButtonDown(Buttons.DPadDown))
            {
                switchercount++;
                if (switchercount % 15==0)
                {
                    switcher2++;
                    if (switcher2 > 6)
                    {
                        switcher2 = 0;
                    }
                }
            }

        }
        void ControlEndOfGame()
        {
            GamePadState state1 = GamePad.GetState(PlayerIndex.One);
            KeyboardState state = Keyboard.GetState();
                if (state.IsKeyDown(Keys.Enter) || state1.IsButtonDown(Buttons.A))
            {
                entered = true;
            }
            if (keytimer % 13 == 0)
            {             
                if (!entered)
                {
                    if (state.IsKeyDown(Keys.Up) || state1.IsButtonDown(Buttons.DPadUp))
                    {
                        shift_down--;
                    }
                    if (state.IsKeyDown(Keys.Down) || state1.IsButtonDown(Buttons.DPadDown))
                    {
                        shift_down++;
                    }
                    if (shift_down < 0)
                    {
                        shift_down = 25;
                    }
                    else if (shift_down > 25)
                    {
                        shift_down = 0;
                    }
                    if (state.IsKeyDown(Keys.Right) || state1.IsButtonDown(Buttons.DPadRight))
                    {
                        shift_right++;
                    }
                    if (state.IsKeyDown(Keys.Left) || state1.IsButtonDown(Buttons.DPadLeft))
                    {
                        shift_right--;
                    }
                    if (shift_right < 0)
                    {
                        shift_right = 0;
                    }
                    else if (shift_right > 5)
                    {
                        shift_right = 5;
                    }
                    Name[shift_right] = alphabet[shift_down];
                }
                
            }
            keytimer++;
            name1 = "";
            foreach(Char c in Name)
            {
                name1 += c;
            }
            if (!b||entered)
            {
                if (state1.IsButtonDown(Buttons.B))
                {
                    switcher = -1;
                    switcher2 = -1;
                    switcher3 = -1;
                    switcher4 = -1;
                    switcher5 = -1;
                    timer = 0;
                    foreach (Hero h in heroes.ToArray())
                    {
                        heroes.Remove(h);
                    }
                    foreach (Enemy e in Enemies.ToArray())
                    {
                        Enemies.Remove(e);
                    }
                    foreach (Potion p in Potions.ToArray())
                    {
                        Potions.Remove(p);
                    }
                    Initialize();
                    _state = GameState.MainMenu;

                switcher4 = -1;
            }
                if (state.IsKeyDown(Keys.Enter) || state1.IsButtonDown(Buttons.A))
                {
                    if (switcher4 == 0)
                    {
                        switcher = -1;
                        switcher2 = -1;
                        switcher3 = -1;
                        switcher4 = -1;
                        switcher5 = -1;
                        //map = random.Next(0, 2);
                        timer = 0;
                        foreach(Hero h in heroes.ToArray())
                        {
                            heroes.Remove(h);
                        }
                        foreach (Enemy e in Enemies.ToArray())
                        {
                            Enemies.Remove(e);
                        }
                        foreach(Potion p in Potions.ToArray())
                        {
                            Potions.Remove(p);
                        }
                        Initialize();
                        _state = GameState.MainMenu;
                    }
                    else if (switcher4 == 1)
                    {
                        Exit();
                    }

                    switcher4 = -1;
                }
                
                if (state.IsKeyDown(Keys.Up) || state1.IsButtonDown(Buttons.DPadUp))
                {
                    switchercount++;
                    if (switchercount % 15 == 0)
                    {
                        switcher4--;
                        if (switcher4 < 0)
                        {
                            switcher4 = 1;
                        }
                        switchercount = 0;
                    }
                }
                if (state.IsKeyDown(Keys.Down) || state1.IsButtonDown(Buttons.DPadDown))
                {
                    switchercount++;
                    if (switchercount % 15 == 0)
                    {
                        switcher4++;
                        if (switcher4 >1)
                        {
                            switcher4 = 0;
                        }
                        switchercount = 0;
                    }
                }
            }
        }
        void ControlLogoScreen()
        {

        }
        void ControlControls()
        {
            GamePadState state1 = GamePad.GetState(PlayerIndex.One);
            KeyboardState state = Keyboard.GetState();
            if(state1.IsButtonDown(Buttons.B)){
                _state = GameState.MainMenu;
                switcher3 = -1;
            }
            if (state.IsKeyDown(Keys.Enter) || state1.IsButtonDown(Buttons.A))
            {
                if (switcher3 == 0)
                {
                    _state = GameState.MainMenu;
                }
                switcher3 = -1;
            }

            if (state.IsKeyDown(Keys.Up) || state1.IsButtonDown(Buttons.DPadUp))
            {
                switchercount++;
                if (switchercount % 15==0)
                {
                    switcher3--;
                    if (switcher3 < 0)
                    {
                        switcher3 = 1;
                    }
                }
            }
            if (state.IsKeyDown(Keys.Down) || state1.IsButtonDown(Buttons.DPadDown))
            {
                switchercount++;
                if (switchercount % 15 == 0)
                {
                    switcher3++;
                    if (switcher3 > 1)
                    {
                        switcher3 = 0;
                    }
                }
            }
        }
        void ControlHistory()
        {
            GamePadState state1 = GamePad.GetState(PlayerIndex.One);
            KeyboardState state = Keyboard.GetState();
            if (state1.IsButtonDown(Buttons.B))
            {
                _state = GameState.MainMenu;
                switcher5 = -1;
            }
                if (state.IsKeyDown(Keys.Enter) || state1.IsButtonDown(Buttons.A))
            {
                if (switcher5 == 0)
                {
                    _state = GameState.MainMenu;
                }
                switcher5 = -1;
            }

            if (state.IsKeyDown(Keys.Up) || state1.IsButtonDown(Buttons.DPadUp))
            {
                switchercount++;
                if (switchercount % 15==0)
                {
                    switcher5--;
                    if (switcher5 < 0)
                    {
                        switcher5 = 1;
                    }
                }
            }
            if (state.IsKeyDown(Keys.Down) || state1.IsButtonDown(Buttons.DPadDown))
            {
                switchercount++;
                if (switchercount % 15==0)
                {
                    switcher5++;
                    if (switcher5 > 1)
                    {
                        switcher5 = 0;
                    }
                }
            }
        }
        void KeyboardHandler()
        {
            KeyboardState state = Keyboard.GetState();
            switch (_state)
            {
                case GameState.MainMenu:
                    ControlMainMenu();
                    break;
                case GameState.Gameplay:
                    ControlGameplay();
                    break;
                case GameState.EndOfGame:
                    ControlEndOfGame();
                    break;
                case GameState.LogoScreen:
                    ControlLogoScreen();
                    break;
                case GameState.Options:
                    ControlOptions();
                    break;
                case GameState.Controls:
                    ControlControls();
                    break;
                case GameState.History:
                    ControlHistory();
                    break;
            }
            
        }
        void WriteToTableOfRecords()
        {
            using (StreamWriter s = File.CreateText("Records.txt"))
            {
                s.WriteLine(string.Concat(name1, " ", score));
            }
        }
        private void CreatePotion(List<Potion> Potions, Texture2D Potion, int type)
        {
            Potions.Add(new Potion(Potion, random.Next(100, 1800), random.Next(50, 600), type));
        }
        private void CreateEnemy(int type)
        {
            if (type == 0)
            {
                Enemies.Add(new Enemy(Eanimations, random.Next(100, 1800), random.Next(50, 600), 1, difficulty==0 ? random.Next(1, 4) : difficulty == 1 ? random.Next(2, 4) : random.Next(3,4), type));

            }
            else if (type == 1)
            {
                Enemies.Add(new Enemy(Eanimations, random.Next(100, 1800), random.Next(50, 600), 1, 1, type));
            }
        }
        private void RemovingEnemies()
        {
            foreach (Enemy e in KillList2.ToArray())
            {
                KillList2.Remove(e);
            }
        }
        private void EnemyAttack(List<Hero> Heroes, List <SoundEffect> sf)
        {
            foreach (Enemy e in Enemies.ToArray())
            {
                e.Attack(heroes, SoundEffects);
            }
        }   
    }
}
