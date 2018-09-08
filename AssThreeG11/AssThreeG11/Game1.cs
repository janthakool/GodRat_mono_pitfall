using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


namespace AssThreeG11
{

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public List<int> check = new List<int>();
        public bool isItem;
        public float amountH;
        public bool death_sound;
        public bool item_sound;
        public bool main_sound;
        public bool timeout;


        public int checkBonusTime;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int W, H;
        public int SPEED, SPEED_BONUSbg;
        // player
        Player player;
        Player player_02;

        // player
        Boss boss;

        // background
        MovingBG background_0;
        MovingBG background_1;
        MovingBG background_2;
        MovingBG background_3;

        // TD
        Texture2D TDD_01, TDD_02, TDD_03, TDD_04, TDD_05, TDD_06, TDD_07, TDD_08;
        Random random;
        List<MovingLight> TDs;
        TimeSpan TDTimeSpawnTime;
        TimeSpan previousSpawnTime;


        int inputTD;

        SpriteFont font;

        //Bonus
        List<BonusItem> BNs;
        Texture2D Bonus;
        TimeSpan BNTimeSpawnTime;
        TimeSpan previousSpawnBNTime;

        //Bonus
        List<BonusItem> HBNs;
        Texture2D HBonus;
        TimeSpan HBNTimeSpawnTime;
        TimeSpan previousSpawnHBNTime;

        //Sound
        Song Main_Sound;
        Song Death_Sound;
        Song ItemMusic_Sound;
        SoundEffect gotItem;

        //bullet

        Texture2D bulletTextureBoss;

        
        //Bullet
        List<Bullet> bullets;
        Texture2D bulletTexture;
        TimeSpan fireTime;
        TimeSpan previousFireTime;

        //bossactack
        List<BossActack> bossactacks;
        Texture2D bossactackTexture;
        TimeSpan actackTime;
        TimeSpan previousactackTime;


        KeyboardState currentKBState;
        KeyboardState previousKBState;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = W = 640;
            graphics.PreferredBackBufferHeight = H = 960;





        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            boss = new Boss();


            player = new Player();
            player_02 = new Player();
            player_02.isActive = true;
            SPEED = 3;
            SPEED_BONUSbg = 10;
            background_0 = new MovingBG();
            background_1 = new MovingBG();
            background_2 = new MovingBG();
            background_3 = new MovingBG();


            TDs = new List<MovingLight>();
            BNs = new List<BonusItem>();
            HBNs = new List<BonusItem>();

            death_sound = false;
            item_sound = false;
            main_sound = false;

            timeout = false;
            previousSpawnTime = TimeSpan.Zero;
            TDTimeSpawnTime = TimeSpan.FromSeconds(0.3f);


            previousSpawnBNTime = TimeSpan.Zero;
            BNTimeSpawnTime = TimeSpan.FromSeconds(5f);
            amountH = 1;
            previousSpawnHBNTime = TimeSpan.Zero;

            bullets = new List<Bullet>();
            fireTime = TimeSpan.FromSeconds(0.4f);
            previousFireTime = TimeSpan.Zero;

            bossactacks = new List<BossActack>();
            actackTime = TimeSpan.FromSeconds(0.4f);
            previousactackTime = TimeSpan.Zero;

            random = new Random();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Vector2 playerPos = new Vector2(W * 45 / 100, H * 90 / 100);
            Vector2 playerPos_02 = new Vector2(W * 45 / 100 -20, H * 90 / 100);

            Vector2 playerPosBOS = new Vector2(W * 45 / 100 - 20, 10);


            boss.Initialize(Content.Load<Texture2D>("BOSS"), playerPosBOS);

            player.Initialize(Content.Load<Texture2D>("Player_11"), playerPos);
            player_02.Initialize(Content.Load<Texture2D>("Player_2"), playerPos_02);
            



            background_0.Initialize(Content.Load<Texture2D>("SBG_1"), SPEED, W, H);
            background_1.Initialize(Content.Load<Texture2D>("BBG_1"), SPEED_BONUSbg, W, H);
            background_2.Initialize(Content.Load<Texture2D>("BG_02"), SPEED, W, H);
            background_3.Initialize(Content.Load<Texture2D>("BG_03"), SPEED, W, H);




            font = Content.Load<SpriteFont>("gameFont");

            ////left || right
            TDD_01 = TDD_05 = Content.Load<Texture2D>("LINE_120");
            TDD_02 = TDD_06 = Content.Load<Texture2D>("LINE_130");
            TDD_03 = TDD_07 = Content.Load<Texture2D>("LINE_150");
            TDD_04 = TDD_08 = Content.Load<Texture2D>("LINE_180");

            //Bonus item

            Bonus = Content.Load<Texture2D>("BNitem");
            HBonus = Content.Load<Texture2D>("bigheart501");

            //Sound
            Main_Sound = Content.Load<Song>("Main_Sound");
            Death_Sound = Content.Load<Song>("Death_sound");
            ItemMusic_Sound = Content.Load<Song>("ItemMusic_Sound");
            gotItem = Content.Load<SoundEffect>("Got_Item");

            //sound
            if ((player.isActive == true) && (isItem == false))
            {
                MediaPlayer.Play(Main_Sound);
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = 0.6f;
            }


            bulletTexture = Content.Load<Texture2D>("laser");
            bossactackTexture = Content.Load<Texture2D>("laserB");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if(player.Health<= 0 || player_02.Health <= 0)
            {
                player.isActive = false;
                player_02.isActive = false;

            }

            if (boss.Health < 0)
            {
                boss.Position.X = 1000;
                boss.Position.Y = 1000;

            }


            if (player.isActive)
            {
                currentKBState = Keyboard.GetState();

                HBNTimeSpawnTime = TimeSpan.FromSeconds(amountH);

                if (boss.isActive)
                {
                    int randomB = random.Next(1, 100);
                    if (randomB <= 50)
                    {
                        boss.MoveRight();
                    }
                    else { boss.MoveLeft(); }
                }


                //key move
                if (currentKBState.IsKeyDown(Keys.D))
                {
                    player_02.MoveRight();
                }
                if (currentKBState.IsKeyDown(Keys.A))
                {
                    player_02.MoveLeft();
                }

                if (currentKBState.IsKeyDown(Keys.Left))
                {
                    player.MoveLeft();
                }

                if (currentKBState.IsKeyDown(Keys.Right))
                {
                    player.MoveRight();
                }

                if (gameTime.TotalGameTime - previousSpawnTime > TDTimeSpawnTime)
                {
                    Vector2 TDpos = new Vector2(100, 0);
                    Vector2 TDpos1 = new Vector2(530 - 120, 0);
                    Vector2 TDpos2 = new Vector2(530 - 130, 0);
                    Vector2 TDpos3 = new Vector2(530 - 150, 0);
                    Vector2 TDpos4 = new Vector2(530 - 180, 0);
                    List<Texture2D> RD = new List<Texture2D>(new Texture2D[] { TDD_01, TDD_02, TDD_03, TDD_04 });
                    int KKK = random.Next(0, 4);

                    AddTD(RD[KKK], TDpos);
                    List<Texture2D> RD1 = new List<Texture2D>(new Texture2D[] { TDD_05, TDD_06, TDD_07, TDD_08 });
                    int OOO = random.Next(0, 4);

                    switch (OOO)
                    {
                        case 0:
                            AddTD(RD1[OOO], TDpos1);
                            break;
                        case 1:
                            AddTD(RD1[OOO], TDpos2);
                            break;
                        case 2:
                            AddTD(RD1[OOO], TDpos3);
                            break;
                        case 3:
                            AddTD(RD1[OOO], TDpos4);
                            break;
                    }



                    previousSpawnTime = gameTime.TotalGameTime;
                }

                if (gameTime.TotalGameTime - previousSpawnBNTime > BNTimeSpawnTime)
                {
                    int jjj = random.Next(5, W);
                    Vector2 PossiBN = new Vector2(random.Next(5, 550), 0);
                    AddBN(Bonus, PossiBN);

                    previousSpawnBNTime = gameTime.TotalGameTime;

                }

                if (gameTime.TotalGameTime - previousSpawnHBNTime > HBNTimeSpawnTime)
                {

                    Vector2 PossiHBN = new Vector2(random.Next(5, 550), 0);
                    AddHBN(HBonus, PossiHBN);

                    previousSpawnHBNTime = gameTime.TotalGameTime;

                }


                if (gameTime.TotalGameTime - previousFireTime > fireTime)
                {
                    Vector2 bulletPosition
                        = new Vector2(player.Position.X + player.Width/2,
                        player.Position.Y + player.Height / 2);

                    Vector2 bulletPosition1
                        = new Vector2(player_02.Position.X + player_02.Width / 2,
                        player_02.Position.Y + player_02.Height / 2);

                    AddBullet(bulletPosition1);
                    AddBullet(bulletPosition);
                    previousFireTime = gameTime.TotalGameTime;
                }


                


                for (int i = TDs.Count - 1; i >= 0; i--)
                {
                    TDs[i].Update();
                    if (TDs[i].isActive == false)
                    {
                        TDs.RemoveAt(i);
                    }
                }

                for (int j = BNs.Count - 1; j >= 0; j--)
                {
                    BNs[j].Update();
                    if (BNs[j].isActive == false)
                    {
                        BNs.RemoveAt(j);
                    }
                }

                for (int k = HBNs.Count - 1; k >= 0; k--)
                {
                    HBNs[k].Update();
                    if (HBNs[k].isActive == false)
                    {
                        HBNs.RemoveAt(k);
                    }
                }

                for (int n = bullets.Count - 1; n >= 0; n--)
                {
                    bullets[n].Update();
                    if (bullets[n].isActive == false)
                    {
                        bullets.RemoveAt(n);
                    }
                }

                if (gameTime.TotalGameTime - previousactackTime > actackTime )//&& gameTime.TotalGameTime.TotalSeconds >= 70)
                {
                    Vector2 actackPos
                        = new Vector2(boss.Position.X + boss.Width / 2,
                        boss.Position.Y + boss.Height / 2);

                    

                    
                    AddBossActack(actackPos);
                    previousactackTime = gameTime.TotalGameTime;
                }

                for (int m = bossactacks.Count - 1; m >= 0; m--)
                {
                    bossactacks[m].Update();
                    if (bossactacks[m].isActive == false)
                    {
                        bossactacks.RemoveAt(m);
                    }
                }

                UpdateCollision();




                if (isItem == true)
                {
                    SPEED = 20;
                    amountH = 0.1f;

                    float checker = (float)gameTime.TotalGameTime.TotalSeconds;
                    check.Add((int)(checker));
                    if (check[0] + 5 <= (int)checker)
                    {
                        SPEED = 3;
                        amountH = 2.0f;
                        isItem = false;
                        check.Clear();
                    }


                }



                // Check out of range
                player.Position.X = MathHelper.Clamp(player.Position.X, 90, W - player.Width - 90);
                player.Position.Y = MathHelper.Clamp(player.Position.Y, 0, H - player.Height);
                player_02.Position.X = MathHelper.Clamp(player_02.Position.X, 90, W - player_02.Width - 90);
                player_02.Position.Y = MathHelper.Clamp(player_02.Position.Y, 0, H - player_02.Height);
                boss.Position.X = MathHelper.Clamp(boss.Position.X, 90, W - boss.Width - 90);
                boss.Position.Y = MathHelper.Clamp(boss.Position.Y, 0, H - boss.Height);



                if (isItem == true)
                {
                    background_1.Update();

                }
                if(isItem == false)
                {
                    SPEED = 3;

                    background_0.Update();
                    background_2.Update();
                    background_3.Update();
                }
                

                

                if (gameTime.TotalGameTime.TotalSeconds >= 100 && timeout == false)
                {
                    timeout = true;
                    player.isActive = false;
                    player_02.isActive = false;

                }
            }

            if (player.isActive == false || player_02.isActive == false)
            {


                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    this.Exit();
                    Game1 newGame = new Game1();
                    newGame.Run();

                }

            }

            //Sound

            if (((player.isActive == false||player_02.isActive == false) && death_sound == false))
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(Death_Sound);
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = 0.6f;
                death_sound = true;

            }
            if (isItem == true && item_sound == false)
            {
                MediaPlayer.Stop();
                item_sound = true;
                MediaPlayer.Play(ItemMusic_Sound);
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = 0.6f;

            }

            if (isItem == false && main_sound == false)
            {
                MediaPlayer.Stop();
                main_sound = true;

                MediaPlayer.Play(Main_Sound);
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = 0.6f;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            
            if (player.isActive == true && player_02.isActive == true)
            {



                if (isItem == true)
                {
                    background_1.Draw(spriteBatch);

                }
                if (isItem == false && (gameTime.TotalGameTime.TotalSeconds > 30  && gameTime.TotalGameTime.TotalSeconds <= 50))
                {
                    background_2.Draw(spriteBatch);

                }

                if (isItem == false && (gameTime.TotalGameTime.TotalSeconds > 50 && gameTime.TotalGameTime.TotalSeconds <= 100))
                {
                    background_3.Draw(spriteBatch);

                }
                if (isItem == false && gameTime.TotalGameTime.TotalSeconds <= 30)
                {
                    background_0.Draw(spriteBatch);

                }


                player_02.Draw(spriteBatch);
                player.Draw(spriteBatch);


                for (int i = 0; i < bullets.Count; i++)
                {
                    bullets[i].Draw(spriteBatch);
                }

                for (int i = 0; i < TDs.Count; i++)
                {
                    TDs[i].Draw(spriteBatch);
                }

                for (int j = 0; j < BNs.Count; j++)
                {
                    BNs[j].Draw(spriteBatch);
                }

                for (int k = 0; k < HBNs.Count; k++)
                {
                    HBNs[k].Draw(spriteBatch);
                }

                




                if ((player.isActive && player_02.isActive == true) && timeout == false)
                {
                    spriteBatch.DrawString(font, "Lives: " + player.Health, new Vector2(10, 10), Color.White);
                    spriteBatch.DrawString(font, "Time: " + ((int)gameTime.TotalGameTime.TotalSeconds), new Vector2(10, 50), Color.White);
                    spriteBatch.DrawString(font, "POINT P1: " + (int)(gameTime.TotalGameTime.TotalSeconds / 2) * player.Health, new Vector2(10, 90), Color.White);

                    spriteBatch.DrawString(font, "Lives: " + player.Health, new Vector2(400, 10), Color.White);
                    spriteBatch.DrawString(font, "Time: " + ((int)gameTime.TotalGameTime.TotalSeconds), new Vector2(400, 50), Color.White);
                    spriteBatch.DrawString(font, "POINT P2: " + (int)(gameTime.TotalGameTime.TotalSeconds / 2) * player_02.Health, new Vector2(400, 90), Color.White);
                }

                
            }

            

            if ((player.isActive || player_02.isActive == false  ) && (timeout == true))
            {
                background_0.Draw(spriteBatch);

                spriteBatch.DrawString(font, "PRESS ENTER TO PLAY AGAIN", new Vector2(W / 2 - 155, H / 2 + 40), Color.White);
                spriteBatch.DrawString(font, "POINT P1: " + (int)(100 / 2) * player.Health 
                    + "\nPOINT P2: " + (int)(100 / 2) * player_02.Health,   new Vector2(W / 2 - 155, H / 2 + 80), Color.White);


            }
            else if (player.isActive == false || player_02.isActive == false)
            {
                background_1.Draw(spriteBatch);

                spriteBatch.DrawString(font, "YOU FAIL", new Vector2(W / 2 - 45, H / 2), Color.Red);
                spriteBatch.DrawString(font, "PRESS ENTER TO PLAY AGAIN", new Vector2(W / 2 - 155, H / 2 + 40), Color.White);

            }

            if(true)//gameTime.TotalGameTime.TotalSeconds >= 70)
            {
                boss.Draw(spriteBatch);
                for (int m = 0; m < bossactacks.Count; m++)
                {
                    bossactacks[m].Draw(spriteBatch);
                }

            }



            spriteBatch.End();




            base.Draw(gameTime);
        }

        private void AddTD(Texture2D td, Vector2 position)
        {
            MovingLight TD = new MovingLight();
            TD.Initialize(td, position, 3);
            TDs.Add(TD);
        }

        private void AddBN(Texture2D td, Vector2 position)
        {
            BonusItem BN = new BonusItem();
            BN.Initialize(td, position, SPEED);
            BNs.Add(BN);
        }

        private void AddHBN(Texture2D td, Vector2 position)
        {
            BonusItem HBN = new BonusItem();
            HBN.Initialize(td, position, SPEED);
            HBNs.Add(HBN);
        }

        private void AddBullet(Vector2 position)
        {
            Bullet bullet = new Bullet();
            bullet.Intialize(bulletTexture, position, H);
            bullets.Add(bullet);
        }

        private void AddBossActack(Vector2 position)
        {
            BossActack bossactack = new BossActack();
            bossactack.Intialize(bossactackTexture, position, H);
            bossactacks.Add(bossactack);
        }


        private void UpdateCollision()
        {
            Rectangle rect1;
            Rectangle rect1_1;

            Rectangle rect2;
            Rectangle rect3;
            Rectangle rect4;
            Rectangle rect5;





            rect1 = new Rectangle((int)player.Position.X,
                (int)player.Position.Y,
                player.Width,
                player.Height);

            rect1_1 = new Rectangle((int)player_02.Position.X,
                (int)player_02.Position.Y,
                player_02.Width,
                player_02.Height);

            //---------------------PLAYER1-------------------------
            for (int i = 0; i < TDs.Count; i++)
            {
                rect2 = new Rectangle((int)TDs[i]._position.X,
                    (int)TDs[i]._position.Y,
                    TDs[i]._W,
                    TDs[i]._H);


                if ((rect1.Intersects(rect2)) && isItem == false)
                {
                    player.Health -= TDs[i].Damage;
                    TDs[i].Health = 0;
                    if (player.Health <= 0)
                    {
                        player.isActive = false;
                    }
                }
            }

            for (int j = 0; j < BNs.Count; j++)
            {
                rect3 = new Rectangle((int)BNs[j]._Position.X,
                    (int)BNs[j]._Position.Y,
                    BNs[j].BWidth,
                    BNs[j].BHeight);

                if (rect1.Intersects(rect3))
                {
                    main_sound = false;
                    item_sound = false;
                    isItem = true;
                    BNs.RemoveAt(j);
                }
            }

            for (int k = 0; k < HBNs.Count; k++)
            {
                rect4 = new Rectangle((int)HBNs[k]._Position.X,
                    (int)HBNs[k]._Position.Y,
                    HBNs[k].BWidth,
                    HBNs[k].BHeight);

                if (rect1.Intersects(rect4))
                {
                    gotItem.Play();
                    player.Health += 50;
                    HBNs.RemoveAt(k);
                    
                }
            }

            //--------------------PLAYER2-------------------------
            for (int i = 0; i < TDs.Count; i++)
            {
                rect2 = new Rectangle((int)TDs[i]._position.X,
                    (int)TDs[i]._position.Y,
                    TDs[i]._W,
                    TDs[i]._H);

                if ((rect1_1.Intersects(rect2)) && isItem == false)
                {
                    player_02.Health -= TDs[i].Damage;
                    TDs[i].Health = 0;
                    if (player_02.Health <= 0)
                    {
                        player_02.isActive = false;
                    }
                }
            }

            for (int j = 0; j < BNs.Count; j++)
            {
                rect3 = new Rectangle((int)BNs[j]._Position.X,
                    (int)BNs[j]._Position.Y,
                    BNs[j].BWidth,
                    BNs[j].BHeight);

                if (rect1_1.Intersects(rect3))
                {
                    main_sound = false;
                    item_sound = false;
                    isItem = true;
                    BNs.RemoveAt(j);
                }
            }

            for (int k = 0; k < HBNs.Count; k++)
            {
                rect4 = new Rectangle((int)HBNs[k]._Position.X,
                    (int)HBNs[k]._Position.Y,
                    HBNs[k].BWidth,
                    HBNs[k].BHeight);

                if (rect1_1.Intersects(rect4))
                {
                    gotItem.Play();
                    player_02.Health += 50;
                    HBNs.RemoveAt(k);

                }
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                for (int j = 0; j < TDs.Count; j++)
                {
                    rect1 = new Rectangle((int)bullets[i].Position.X,
                        (int)bullets[i].Position.Y, bullets[i].Texture.Width, bullets[i].Texture.Height);
                    rect2 = new Rectangle((int)TDs[j]._position.X, (int)TDs[j]._position.Y, TDs[j]._W, TDs[j]._H);
                    rect3 = new Rectangle((int)boss.Position.X, (int)boss.Position.Y, boss.Width, boss.Height);

                    if (rect1.Intersects(rect2))
                    {
                        TDs[j].Health -= bullets[i].Dramage;
                        bullets[i].isActive = false;
                    }

                    if (rect1.Intersects(rect3))
                    {
                        boss.Health -= 2;
                        bullets[i].isActive = false;
                    }

                }
            }

            for (int i = 0; i < bossactacks.Count; i++)
            {
                
                    rect1 = new Rectangle((int)bossactacks[i].Position.X,
                        (int)bossactacks[i].Position.Y, bossactacks[i].Texture.Width, bossactacks[i].Texture.Height);
                    rect2 = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.Width, player.Height);
                    rect3 = new Rectangle((int)player_02.Position.X, (int)player_02.Position.Y, player_02.Width, player_02.Height);


                if (rect1.Intersects(rect2))
                    {
                        player.Health -= bossactacks[i].Dramage;
                        bossactacks[i].isActive = false;
                    }
                if (rect1.Intersects(rect3))
                {
                    player_02.Health -= bossactacks[i].Dramage;
                    bossactacks[i].isActive = false;
                }


            }



        }


    }
}
