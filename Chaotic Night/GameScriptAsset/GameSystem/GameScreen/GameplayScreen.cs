using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System.Text;

namespace Chaotic_Night
{
    public class GameplayScreen : Screen
    {
        protected SpriteFont font;
        protected SpriteFont Title;
        public PlayableCharacter PlayerCha;
        List<Enemy> Enemies = new List<Enemy>();
        protected List<CollectableObject> Pickup = new List<CollectableObject>();
        protected List<BulletShooter> BS = new List<BulletShooter>();
        protected Texture2D HitboxTest;
        protected Texture2D MapTex;
        protected Texture2D HPBar;
        protected Texture2D SPBar;
        protected KeyboardState PlayerKeyboard;
        protected MouseState PlayerMouse;
        protected MouseState OldPlayerMouse;
        protected Vector2 MousePos;
        protected Vector2 dPos;
        protected float WeaponRot;
        protected float elapsed;
        protected List<GameObject> GameObj = new List<GameObject>();
        protected bool ShowShop = false;
        protected bool ShowReward = false;
        protected bool RewardIsSelected = false;
        protected Texture2D ScreenBG;
        protected float ScreenBGOpa = 0.5f;
        protected bool NextLevelIsSet = false;
        protected bool CanGetReward = true;
        Color[] ColourData = new Color[1920 * 1080];
        List<Button> Rewards = new List<Button>();
        protected Shopkeeper SK;
        List<Button> Shops = new List<Button>();
        List<NextLevelButton> GameOvers = new List<NextLevelButton>();
        bool AlreadyBuySMT = false;
        protected Vector2 SpawnPos = Vector2.Zero;
        public bool ShopIsOpen = false;
        protected bool GameIsEnded = false;


        protected LevelChanger LC;
        public Camera GameCamera;
        protected Game1 game;
        protected bool GodMode = false;
        private readonly List<IEntity> _entities = new List<IEntity>();
        public readonly CollisionComponent _collisionComponent;
        protected int EnemyAmount = 0;
        /*
         World To Screen = ObjPos-CamPos
         Screen To World = ObjPos-CamPos
         */
        protected Random RAND;
        bool StatsIsLoaded = false;
        //Debug
        protected bool Debuging = false;
        public static int ScreenW;
        public static int ScreenH;
        Character FollowedCharacter = null;
        public bool ShowGrid = false;
        public bool CamFollowEnemy = false;
        protected int EnemyNum = 0;
        protected bool RoomIsReset = false;
        public GameplayScreen(Game1 game, EventHandler SEvent) :base(game,SEvent)
        {
            //set game's screen
            ScreenW = game._graphics.PreferredBackBufferWidth;
            ScreenH = game._graphics.PreferredBackBufferHeight;
            this.game = game;
            RAND = new Random();
            _collisionComponent = new CollisionComponent(new RectangleF(0, 0, 1920, 1080));
            GameCamera = new Camera(ScreenW, ScreenH);
            PlayerCha = new PlayableCharacter(game, SpawnPos);
            GameCamera.CamPos = PlayerCha.GetOrigin() - new Vector2(ScreenW / 2, ScreenH / 2);
            GameCamera.FollowCharacter(PlayerCha);
            ScreenBG = new Texture2D(game.GraphicsDevice, 1920, 1080);
            for (int i = 0; i < ColourData.Length; i++)
            {
                ColourData[i] = Color.Black;
            }
            ScreenBG.SetData(ColourData);
            RandomReward();
            RandomShop();

        }
        public override void Load()
        {
            PlayerCha.Load(game.Content, game._spriteBatch,new RangeWeapons(PlayerCha));
            font = game.Content.Load<SpriteFont>("BM_Space_8");
            Title = game.Content.Load<SpriteFont>("BM_Space Large");
            HitboxTest = game.Content.Load<Texture2D>("Player-Placeholder");
            HPBar = game.Content.Load<Texture2D>("hp_BarV2");
            SPBar = game.Content.Load<Texture2D>("mana_BarV2");
            GameOvers.Add(new NextLevelButton(game.MainMenu, font, 700, 800, "Back To Main Menu"));
            GameOvers.Add(new NextLevelButton(game.Z1M1, font, 1100, 800, "Play Again"));
            foreach (NextLevelButton GOB in GameOvers)
            {
                GOB.Load(game.Content, game._spriteBatch, "button_selection_1 (1)", 72 * 3, 24 * 3);
            }
            IsAlreadyLoaded = true;
        }
        public void SpawnEnemy(int Type,int Count,int AreaMaxX,int AreaMaxY,int AreaMinX,int AreaMinY)
        {
            if (Type == -1)
            {
                for (int i = 0; i < Count; i++)
                {
                    Enemies.Add(new Enemy(game, new Vector2(10000, 10000)));
                    Enemies[i].Load(game.Content, game._spriteBatch);
                    EnemyAmount += 1;
                    Enemies[i].SubtraceHP(1000);
                }
            }
            if (Type==0)
            {
                for (int i = 0; i < Count; i++)
                {
                    Enemies.Add(new Enemy(game, new Vector2(RAND.Next(AreaMinX, AreaMaxX), RAND.Next(AreaMinY, AreaMaxY))));
                    Enemies[i].Load(game.Content, game._spriteBatch);
                    EnemyAmount += 1;
                }
            }
            if (Type == 1)
            {
                int OldEnemyCount=Enemies.Count;
                for (int i = Enemies.Count; i < Count+OldEnemyCount; i++)
                {
                    Enemies.Add(new Small_Imp(game, new Vector2(RAND.Next(AreaMinX, AreaMaxX), RAND.Next(AreaMinY, AreaMaxY))));
                    Enemies[i].Load(game.Content, game._spriteBatch);
                    EnemyAmount += 1;
                }
            }
            if (Type == 2)
            {
                int OldEnemyCount = Enemies.Count;
                for (int i = Enemies.Count; i < Count + OldEnemyCount; i++)
                {
                    Enemies.Add(new Frankensteint(game, new Vector2(RAND.Next(AreaMinX, AreaMaxX), RAND.Next(AreaMinY, AreaMaxY))));
                    Enemies[i].Load(game.Content, game._spriteBatch);
                    EnemyAmount += 1;
                }
            }
            if (Type == 3)
            {
                int OldEnemyCount = Enemies.Count;
                for (int i = Enemies.Count; i < Count + OldEnemyCount; i++)
                {
                    Enemies.Add(new Vampire(game, new Vector2(RAND.Next(AreaMinX, AreaMaxX), RAND.Next(AreaMinY, AreaMaxY))));
                    Enemies[i].Load(game.Content, game._spriteBatch);
                    EnemyAmount += 1;
                }
            }
        }
        public void SpawnLC(int LocX,int LocY)
        {
            LC = new LevelChanger(game.LoadingScreen, LocX, LocY);
            LC.Load(game.Content, game._spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            if(StatsIsLoaded==false)
            {
                LoadCharacterStats();
            }
            if(FadingIsDone==false)
            {
                ScreenFadeIn(gameTime);
            }
            if (game.Room == 4)
            {
                ShopIsOpen = true;
            }
            PlayerKeyboard = Keyboard.GetState();
            PlayerMouse = Mouse.GetState();
            if(IsPaused==false)
            {
                game.HP = PlayerCha.HealthPoint;
                game.SP = PlayerCha.GetSP();
                game.PlayerWeapon = PlayerCha.HoldedWeapon;
                if (PlayerKeyboard.IsKeyDown(Keys.F1))
                {
                    if (Debuging == false)
                    {
                        Debuging = true;
                    }
                    else if (Debuging == true)
                    {
                        Debuging = false;
                    }
                }
                if (EnemyAmount > 0)
                {
                    EnemyAmount = Enemies.Count;
                }
                else if(EnemyAmount <= 0)
                {
                    EnemyAmount = 0;
                }
                foreach (Enemy enemy in Enemies)
                {
                    if (enemy.IsDead == false)
                    {
                        if(enemy.IsAlive==true)
                        {
                            if(enemy.GetType()!=typeof(Small_Imp))
                            {
                                if (enemy.GetAttackRange().Intersects(PlayerCha.GetHitbox()))
                                {
                                    if (GodMode == false)
                                    {
                                        enemy.Attack(PlayerCha);
                                    }
                                }
                            }
                            else if (enemy.GetType() == typeof(Small_Imp))
                            {
                                enemy.Attack(PlayerCha);
                            }
                            if(enemy.GetType() == typeof(Small_Imp)|| enemy.GetType() == typeof(Vampire))
                            {
                                foreach(Bullet i in enemy.EnemyWeapon.GetBullets())
                                {
                                    if (i.Hitbox.Intersects(PlayerCha.GetHitbox()))
                                    {
                                        PlayerCha.SubtraceHP(i.Damage - (int)(game.PlayerRelic.RangeDamageResistance));
                                        enemy.EnemyWeapon.GetBullets().Remove(i);
                                        break;
                                    }
                                }
                            }
                            if (enemy.GetType() == typeof(Frankensteint)|| enemy.GetType() == typeof(Vampire))
                            {
                                if (Math.Abs(PlayerCha.GetOrigin().X - enemy.GetOrigin().X) > 300 || Math.Abs(PlayerCha.GetOrigin().Y - enemy.GetOrigin().Y) > 300)
                                {
                                    enemy.SpecialAttack(PlayerCha);
                                }
                            }
                            if (enemy.EnemyWeapon.UpdateAnim == true)
                            {
                                enemy.UpdateWeaponFrame(elapsed);
                            }
                        }
                        foreach (GameObject Wall in GameObj)
                        {
                            enemy.CalculateHitDis(Wall);
                        }
                        if (PlayerCha.GetWeapon().GetBullets() != null)
                        {
                            foreach (Bullet i in PlayerCha.GetWeapon().GetBullets())
                            {
                                if (i.Hitbox.Intersects(enemy.GetHitbox()))
                                {
                                    if(enemy.HealthPoint>0)
                                    {
                                        if (enemy.GetType() != typeof(Frankensteint)&& enemy.GetType() != typeof(Vampire))
                                        {
                                            PlayerCha.GetWeapon().Damages.Add(new PopUpDamage(i.Damage, enemy.CharacterPos, false, PlayerCha));
                                            enemy.SubtraceHP((int)(i.Damage * game.PlayerRelic.NormalEnemyDamageMultiRate));
                                            PlayerCha.GetWeapon().GetBullets().Remove(i);
                                        }
                                        else if (enemy.GetType() == typeof(Vampire))
                                        {
                                            PlayerCha.GetWeapon().Damages.Add(new PopUpDamage(i.Damage, enemy.CharacterPos, false, PlayerCha));
                                            enemy.SubtraceHP((int)(i.Damage));
                                            PlayerCha.GetWeapon().GetBullets().Remove(i);
                                        }
                                        else if (enemy.GetType() == typeof(Frankensteint))
                                        {
                                            if (enemy.FramePosY == 1 || enemy.FramePosY == 3)
                                            {
                                                PlayerCha.GetWeapon().Damages.Add(new PopUpDamage(i.Damage, enemy.CharacterPos, false, PlayerCha));
                                                enemy.SubtraceHP(i.Damage);
                                                PlayerCha.GetWeapon().GetBullets().Remove(i);
                                            }
                                        }
                                        if (enemy.IsAlive == false)
                                        {
                                            game.Money += RAND.Next(enemy.BaseMoneyDrop-100, enemy.BaseMoneyDrop+250);
                                            game.LVProgess += enemy.ExpDrop;
                                            int TempSP = (int)(RAND.Next(0, 5) * game.SPGainRate);
                                            if (PlayerCha.GetSP() < 100)
                                            {
                                                if (PlayerCha.GetSP() + TempSP < 100)
                                                {
                                                    PlayerCha.AddSP(TempSP);
                                                }
                                                else
                                                {
                                                    PlayerCha.AddSP((PlayerCha.GetSP() + TempSP) - 100);
                                                }
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                        enemy.UpdateCharacter(elapsed);
                        //enemy.CalculatePath(Grid, PlayerCha);
                        enemy.MoveTo(PlayerCha.GetOrigin());
                    }
                    else
                    {
                        if(EnemyAmount>0)
                        {
                            EnemyAmount -= 1;
                        }
                    }
                }
                foreach (GameObject j in GameObj)
                {
                    if (PlayerCha.GetWeapon().GetBullets() != null)
                    {
                        foreach (Bullet i in PlayerCha.GetWeapon().GetBullets())
                        {
                            if (i.Hitbox.Intersects(j.GetHitbox()))
                            {
                                if(j.GetType()!=typeof(GameObj_IgnoreBullets))
                                {
                                    PlayerCha.GetWeapon().GetBullets().Remove(i);
                                    break;
                                }
                            }
                        }
                    }
                    foreach(Enemy i in Enemies)
                    {
                        if(i.EnemyWeapon.GetBullets() != null)
                        {
                            foreach(Bullet k in i.EnemyWeapon.GetBullets())
                            {
                                if(k.Hitbox.Intersects(j.GetHitbox()))
                                {
                                    if (j.GetType() != typeof(GameObj_IgnoreBullets))
                                    {
                                        i.EnemyWeapon.GetBullets().Remove(k);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                PlayerCha.UpdateCharacter(elapsed, GameCamera.GetCamPos(), GameObj, Enemies, new Vector2(PlayerMouse.X, PlayerMouse.Y));

                elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                // TODO: Add your update logic here
                if (Debuging == false)
                {
                    GameCamera.FollowCharacter(PlayerCha);
                }
                else if (Debuging == true)
                {
                    bool QPressed = false;
                    bool EPressed = false;
                    if (PlayerKeyboard.IsKeyDown(Keys.Q))
                    {
                        if (QPressed == false)
                        {
                            if (EnemyNum - 1 < 0)
                            {
                                EnemyNum = Enemies.Count - 1;
                            }
                            else
                            {
                                EnemyNum -= 1;
                            }
                            QPressed = true;
                        }
                    }
                    else if (PlayerKeyboard.IsKeyDown(Keys.E))
                    {
                        if (EPressed == false)
                        {
                            if (EnemyNum + 1 > Enemies.Count - 1)
                            {
                                EnemyNum = 0;
                            }
                            else
                            {
                                EnemyNum += 1;
                            }
                            EPressed = true;
                        }
                    }
                    if (PlayerKeyboard.IsKeyUp(Keys.Q))
                    {
                        QPressed = false;
                    }
                    else if (PlayerKeyboard.IsKeyUp(Keys.E))
                    {
                        EPressed = false;
                    }
                    if (PlayerKeyboard.IsKeyDown(Keys.T))
                    {
                        if (CamFollowEnemy == false)
                        {
                            CamFollowEnemy = true;
                        }
                        else
                        {
                            CamFollowEnemy = false;
                        }
                    }
                    if (CamFollowEnemy == false)
                    {
                        if (Enemies.Count != 0)
                        {
                            FollowedCharacter = Enemies[EnemyNum];
                        }
                    }
                    else
                    {
                        FollowedCharacter = PlayerCha;
                    }
                    if (FollowedCharacter == null)
                    {
                        FollowedCharacter = PlayerCha;
                    }
                    if (PlayerKeyboard.IsKeyDown(Keys.G))
                    {
                        GodMode = true;
                    }
                    if (PlayerKeyboard.IsKeyDown(Keys.U))
                    {
                        if(PlayerCha.HoldedWeapon.GetType()==typeof(RangeWeapons))
                        {
                            PlayerCha.HoldedWeapon = new MeleeWeapons(PlayerCha);
                            PlayerCha.HoldedWeapon.Load(game.Content, game._spriteBatch);
                        }
                        else if (PlayerCha.HoldedWeapon.GetType() == typeof(MeleeWeapons))
                        {
                            PlayerCha.HoldedWeapon = new RangeWeapons(PlayerCha);
                            PlayerCha.HoldedWeapon.Load(game.Content, game._spriteBatch);
                        }
                    }
                    GameCamera.FollowCharacter(FollowedCharacter);

                }
                foreach (CollectableObject PU in Pickup)
                {
                    if (PU.CheckCollision(PlayerCha))
                    {
                        PU.Collect(PlayerCha);
                        Pickup.Remove(PU);
                        break;
                    }
                }

                /*if(Grid!=null)
                {
                    Grid.Update(Vector2.Zero+GameCamera.GetCamPos(), MousePos);
                }*/

                if(game.LVProgess>game.LVBarMax)
                {
                    game.LV += 1;
                    game.LVProgess -= game.LVBarMax;
                    game.LVDamageMuti += 0.1f;
                    game.SPGainRate += 0.1f;
                    PlayerCha.SetHP(100);
                }
                if(EnemyAmount<=0)
                {
                    ShowReward = true;
                    if(NextLevelIsSet!=true)
                    {
                        int MapNum = RAND.Next(0, 11);
                        if(game.Stage==1)
                        {
                            if (MapNum == 0)
                            {
                                game.NextLevel = game.Z1M2;
                            }
                            if (MapNum == 1)
                            {
                                game.NextLevel = game.Z1M3;
                            }
                            if (MapNum == 2)
                            {
                                game.NextLevel = game.Z1M4;
                            }
                            if (MapNum == 3)
                            {
                                game.NextLevel = game.Z1M5;
                            }
                            if (MapNum == 4)
                            {
                                game.NextLevel = game.Z1M6;
                            }
                            if (MapNum == 5)
                            {
                                game.NextLevel = game.Z1M7;
                            }
                            if (MapNum == 6)
                            {
                                game.NextLevel = game.Z1M8;
                            }
                            if (MapNum == 7)
                            {
                                game.NextLevel = game.Z1M9;
                            }
                            if (MapNum == 8)
                            {
                                game.NextLevel = game.Z1M10;
                            }
                            if (MapNum == 9)
                            {
                                game.NextLevel = game.Z1M11;
                            }
                            if (MapNum == 10)
                            {
                                game.NextLevel = game.Z1M12;
                            }
                        }
                        if(game.Stage == 2)
                        {
                            if (MapNum == 0)
                            {
                                game.NextLevel = game.Z2M2;
                            }
                            if (MapNum == 1)
                            {
                                game.NextLevel = game.Z2M3;
                            }
                            if (MapNum == 2)
                            {
                                game.NextLevel = game.Z2M4;
                            }
                            if (MapNum == 3)
                            {
                                game.NextLevel = game.Z2M5;
                            }
                            if (MapNum == 4)
                            {
                                game.NextLevel = game.Z2M6;
                            }
                            if (MapNum == 5)
                            {
                                game.NextLevel = game.Z2M7;
                            }
                            if (MapNum == 6)
                            {
                                game.NextLevel = game.Z2M8;
                            }
                            if (MapNum == 7)
                            {
                                game.NextLevel = game.Z2M9;
                            }
                            if (MapNum == 8)
                            {
                                game.NextLevel = game.Z2M10;
                            }
                            if (MapNum == 9)
                            {
                                game.NextLevel = game.Z2M11;
                            }
                            if (MapNum == 10)
                            {
                                game.NextLevel = game.Z2M12;
                            }
                        }
                        if (game.Stage == 3)
                        {
                            if (MapNum == 0)
                            {
                                game.NextLevel = game.Z3M2;
                            }
                            if (MapNum == 1)
                            {
                                game.NextLevel = game.Z3M3;
                            }
                            if (MapNum == 2)
                            {
                                game.NextLevel = game.Z3M4;
                            }
                            if (MapNum == 3)
                            {
                                game.NextLevel = game.Z3M5;
                            }
                            if (MapNum == 4)
                            {
                                game.NextLevel = game.Z3M6;
                            }
                            if (MapNum == 5)
                            {
                                game.NextLevel = game.Z3M7;
                            }
                            if (MapNum == 6)
                            {
                                game.NextLevel = game.Z3M8;
                            }
                            if (MapNum == 7)
                            {
                                game.NextLevel = game.Z3M9;
                            }
                            if (MapNum == 8)
                            {
                                game.NextLevel = game.Z3M10;
                            }
                            if (MapNum == 9)
                            {
                                game.NextLevel = game.Z3M11;
                            }
                            if (MapNum == 10)
                            {
                                game.NextLevel = game.Z3M12;
                            }
                        }
                        NextLevelIsSet = true;
                    }
                }
                if (ShowReward == true && RewardIsSelected == false && CanGetReward == true)
                {
                    UpdateReward();
                }
                if(ShopIsOpen==true)
                {
                    if (SK != null)
                    {
                        UpdateShop((float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                }
                if(PlayerCha.IsDead==true)
                {
                    UpdateGameOverScene(gameTime);
                }
                if(PlayerMouse.LeftButton==ButtonState.Released)
                {
                    AlreadyBuySMT = false;
                }
                if(GameIsEnded==true)
                {
                    UpdateGameEndScene(gameTime);
                }
            }
        }
        public override void Draw(SpriteBatch _spriteBatch)
        {
            /*foreach (GameObject Wall in GameObj)
            {
                Wall.Draw(GameCamera.GetCamPos());
                //Wall.Draw(Vector2.Zero);
            }*/
            _spriteBatch.Draw(MapTex, Vector2.Zero - GameCamera.GetCamPos(), Color.White);
            /*if(LC!=null)
            {
                LC.Draw(GameCamera.GetCamPos());
                //LC.Draw(Vector2.Zero);
            }*/
            #region Show Debug

            if (Debuging == true)
            {
                _spriteBatch.Draw(HitboxTest, PlayerCha.GetWeaponHitzoneVector() - GameCamera.GetCamPos(), PlayerCha.GetWeapon().GetHitzone(), Color.White);
                _spriteBatch.DrawString(font, PlayerCha.WeaponRot.ToString(), new Vector2(PlayerCha.CharacterPos.X - GameCamera.GetCamPos().X, (PlayerCha.CharacterPos.Y + 64 + 120) - GameCamera.GetCamPos().Y), Color.White);
                _spriteBatch.DrawString(font, PlayerCha.CharacterPos.ToString(), new Vector2(PlayerCha.CharacterPos.X - GameCamera.GetCamPos().X, (PlayerCha.CharacterPos.Y + 74 + 120) - GameCamera.GetCamPos().Y), Color.White);
                _spriteBatch.DrawString(font, PlayerCha.GetAttacking().ToString(), new Vector2(PlayerCha.CharacterPos.X - GameCamera.GetCamPos().X, (PlayerCha.CharacterPos.Y + 84 + 120) - GameCamera.GetCamPos().Y), Color.White);
                _spriteBatch.DrawString(font, PlayerCha.GetWeapon().GetFrame().ToString(), new Vector2(PlayerCha.CharacterPos.X - GameCamera.GetCamPos().X, (PlayerCha.CharacterPos.Y + 92 + 120) - GameCamera.GetCamPos().Y), Color.White);
                _spriteBatch.DrawString(font, PlayerCha.GetHP().ToString(), new Vector2(PlayerCha.CharacterPos.X - GameCamera.GetCamPos().X, (PlayerCha.CharacterPos.Y + 100 + 120) - GameCamera.GetCamPos().Y), Color.White);
                foreach (Enemy enemy in Enemies)
                {
                    _spriteBatch.DrawString(font, enemy.GetHP().ToString(), new Vector2(enemy.CharacterPos.X - GameCamera.GetCamPos().X, (enemy.CharacterPos.Y + 64) - GameCamera.GetCamPos().Y), Color.White);
                }
                _spriteBatch.DrawString(font, MousePos.ToString(), new Vector2(PlayerCha.CharacterPos.X - GameCamera.GetCamPos().X, (PlayerCha.CharacterPos.Y + 124) - GameCamera.GetCamPos().Y), Color.White);
                _spriteBatch.DrawString(font, dPos.ToString(), new Vector2(PlayerCha.CharacterPos.X - GameCamera.GetCamPos().X, (PlayerCha.CharacterPos.Y + 144) - GameCamera.GetCamPos().Y), Color.White);
                _spriteBatch.DrawString(font, "Health : " + game.HP, new Vector2(10, 10), Color.White);

            }

            #endregion
            if (Pickup != null)
            {
                foreach (GameObject PU in Pickup)
                {
                    {
                        PU.Draw(GameCamera.GetCamPos());
                    }
                }
            }
            PlayerCha.DrawCharacter(_spriteBatch, GameCamera.GetCamPos());
            //PlayerCha.DrawCharacter(_spriteBatch, Vector2.Zero);
            if(PlayerCha.GetWeapon().GetBullets()!=null)
            {
                foreach (Bullet bullet in PlayerCha.GetWeapon().GetBullets())
                {
                    bullet.Draw(_spriteBatch, GameCamera.GetCamPos());
                }
            }
            foreach (Enemy enemy in Enemies)
            {
                if (enemy.IsDead == false)
                {
                    enemy.DrawCharacter(_spriteBatch, GameCamera.GetCamPos());
                    //enemy.DrawCharacter(_spriteBatch, Vector2.Zero);
                }
            }
            if(ShopIsOpen == true)
            {
                if (SK != null)
                {
                    DrawShop(_spriteBatch);
                }
            }
            if (GameIsEnded == true)
            {
                DrawGameEndScene(_spriteBatch);
            }




            /*_spriteBatch.DrawString(font, "Skill Point : " + PlayerCha.GetSP(), new Vector2(10, 30), Color.White);*/
            //there must be a better way of doing this but idk how
            foreach (PopUpDamage PUD in PlayerCha.GetWeapon().Damages)
            {
                PUD.Draw(_spriteBatch, font, GameCamera.GetCamPos());
            }
            _spriteBatch.Draw(HPBar, new Vector2(10, 10), new Rectangle(0, 0, 344, 36), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            _spriteBatch.Draw(HPBar, new Vector2(10, 10), new Rectangle(344-(((344 * PlayerCha.HealthPoint) / (int)(game.MaxHP * game.PlayerRelic.MaxHPMultiRate))), 36, (344 * PlayerCha.HealthPoint) / (int)(game.MaxHP * game.PlayerRelic.MaxHPMultiRate), 36), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            _spriteBatch.Draw(HPBar, new Vector2(10, 10), new Rectangle(0, 72, 344, 36), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            _spriteBatch.Draw(SPBar, new Vector2(10, 56), new Rectangle(0, 0, 284, 28), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            _spriteBatch.Draw(SPBar, new Vector2(10, 56), new Rectangle(284 - (((284 * PlayerCha.GetSP()) / 100)), 28, (284 * PlayerCha.GetSP()) / 100, 28), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            _spriteBatch.Draw(SPBar, new Vector2(10, 56), new Rectangle(0, 56, 284, 28), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            
            _spriteBatch.DrawString(font, "Money : " + game.Money, new Vector2(10, 86), Color.White);
            _spriteBatch.DrawString(font, "Enemies : " + EnemyAmount, new Vector2(10, 106), Color.White);
            _spriteBatch.DrawString(font, "LV : " + game.LV, new Vector2(10, 126), Color.White);
            _spriteBatch.DrawString(font, + game.LVProgess + " / "  + game.LVBarMax, new Vector2(75, 126), Color.White);
            
            if(SK!=null)
            {
                if (SK.IsSelected == true && ShowShop == false && EnemyAmount <= 0)
                {
                    _spriteBatch.DrawString(font, "Press E to Open Shop", new Vector2(PlayerMouse.Position.X, PlayerMouse.Position.Y), Color.White);
                }
                if (SK.IsSelected == true && EnemyAmount > 0)
                {
                    _spriteBatch.DrawString(font, "Defeat The Enemies first !", new Vector2(PlayerMouse.Position.X, PlayerMouse.Position.Y), Color.White);
                }
            }

            if (ShowReward == true && RewardIsSelected == false && CanGetReward == true)
            {
                DrawReward(_spriteBatch);
            }

            if(PlayerCha.IsDead==true)
            {
                DrawGameOverScene(_spriteBatch);
            }
            _spriteBatch.Draw(ScreenHider, Vector2.Zero, Color.White * ScreenOpa);
        }
        public virtual void ResetRoom()
        {
            /*if(RoomIsReset==false)
            {
                PlayerCha.CharacterPos = new Vector2(300, 500);
                PlayerCha.GetWeapon().ClearBullet();
                for (int i = Enemies.Count; i > 0; i--)
                {
                    Enemies.RemoveAt(i - 1);
                }
                for (int i = Pickup.Count; i > 0; i--)
                {
                    Pickup.RemoveAt(i - 1);
                }
                IsPaused = true;
                StatsIsLoaded = false;
                if (FadingInIsSet == true)
                {
                    ScreenOpa = 1;
                    FadingInIsSet = false;
                }
                RoomIsReset = true;
            }*/
            PlayerCha.CharacterPos = SpawnPos;
            GameCamera.CamPos = PlayerCha.GetOrigin() - new Vector2(ScreenW / 2, ScreenH / 2);
            PlayerCha.GetWeapon().ClearBullet();
            for (int i = Enemies.Count; i > 0; i--)
            {
                Enemies.RemoveAt(i - 1);
            }
            for (int i = Pickup.Count; i > 0; i--)
            {
                Pickup.RemoveAt(i - 1);
            }
            IsPaused = true;
            StatsIsLoaded = false;
            if(FadingIsDone==true)
            {
                ScreenOpa = 1;
                FadingIsDone = false;
            }
            PlayerCha.FramePosX = 0;
            PlayerCha.FramePosY = 0;
            PlayerCha.EndFrame = 4;
            RandomReward();
            RandomShop();
            NextLevelIsSet = false;
        }
        public void LoadCollectable()
        {
            if (Pickup != null)
            {
                foreach (CollectableObject PU in Pickup)
                {
                    PU.Load(game.Content, game._spriteBatch);
                }
            }
        }
        public void LoadCharacterStats()
        {
            PlayerCha.HealthPoint = game.HP;
            PlayerCha.SP = game.SP;
            if(game.PlayerWeapon!=null)
            {
                if(game.PlayerWeapon.GetType()==typeof(MeleeWeapons))
                {
                    PlayerCha.HoldedWeapon = new MeleeWeapons(PlayerCha);
                    PlayerCha.HoldedWeapon.Load(game.Content, game._spriteBatch);
                }
                if (game.PlayerWeapon.GetType() == typeof(RangeWeapons))
                {
                    PlayerCha.HoldedWeapon = new RangeWeapons(PlayerCha);
                    PlayerCha.HoldedWeapon.Load(game.Content, game._spriteBatch);
                }
            }
            StatsIsLoaded = true;
        }
        public override void ScreenFadeIn(GameTime time)
        {
            RoomIsReset = true;
            TimeProg += (float)time.ElapsedGameTime.TotalSeconds;
            if (ScreenOpa > 0)
            {
                if (TimeProg > 0.07)
                {
                    ScreenOpa -= (float)0.1;
                    TimeProg = 0;
                }
                IsPaused = true;
            }
            else
            {
                ScreenOpa = 0;
                IsPaused = false;
                FadingIsDone = true;
                RoomIsReset = false;
            }

        }

        public virtual void DrawReward(SpriteBatch _SB)
        {
            _SB.Draw(ScreenBG, Vector2.Zero, Color.White * ScreenBGOpa);
            foreach(Button RBT in Rewards)
            {
                RBT.Draw(Vector2.Zero);
            }
        }
        public virtual void UpdateReward()
        {
            foreach (Button RBT in Rewards)
            {
                RBT.CheckCursor(PlayerMouse.X, PlayerMouse.Y);
                if (PlayerMouse.LeftButton == ButtonState.Pressed)
                {
                    if (RBT.IsSelected == true)
                    {
                        RBT.Interact();
                        RewardIsSelected = true;
                        ShowReward = false;
                    }
                }
            }
            if(Rewards[0].IsSelected == false && Rewards[1].IsSelected == false && Rewards[2].IsSelected == false)
            {
                if (PlayerMouse.LeftButton == ButtonState.Pressed)
                {
                    RewardIsSelected = true;
                    ShowReward = false;
                }
            }
            /*Rewards[3].CheckCursor(PlayerMouse.X, PlayerMouse.Y);
            if (PlayerMouse.LeftButton == ButtonState.Pressed)
            {
                if (Rewards[3].IsSelected == true)
                {
                    Rewards[3].Interact();
                    RewardIsSelected = true;
                    ShowReward = false;
                }
            }*/

        }
        public virtual void DrawShop(SpriteBatch _SB)
        {
            SK.Draw(GameCamera.GetCamPos());
            if(ShowShop==true)
            {
                _SB.Draw(ScreenBG, Vector2.Zero, Color.White * ScreenBGOpa);
                foreach (Button SBT in Shops)
                {
                    SBT.Draw(Vector2.Zero);
                }
            }
        }
        public virtual void UpdateShop(float time)
        {
            SK.UpdateFrame(time);
            SK.CheckCursor(PlayerMouse.X, PlayerMouse.Y);
            if (PlayerKeyboard.IsKeyDown(Keys.E))
            {
                if(Math.Abs(PlayerCha.CharacterPos.X - SK.GetPos().X) < 200 && Math.Abs(PlayerCha.CharacterPos.Y - SK.GetPos().Y) < 100)
                {
                    if (SK.IsSelected == true)
                    {
                        SK.Interact();
                        ShowShop = true;
                    }
                }
            }

            if(ShowShop==true)
            {
                foreach (Button SBT in Shops)
                {
                    SBT.CheckCursor(PlayerMouse.X, PlayerMouse.Y);
                    if (PlayerMouse.LeftButton == ButtonState.Pressed && AlreadyBuySMT == false)
                    {
                        if (SBT.IsSelected == true)
                        {
                            SBT.Interact();
                            PlayerCha.HealthPoint = game.HP;
                            PlayerCha.SP = game.SP;
                            AlreadyBuySMT = true;
                        }
                    }

                }
                if (Shops[0].IsSelected == false && Shops[1].IsSelected == false && Shops[2].IsSelected == false && Shops[3].IsSelected == false && Shops[4].IsSelected == false)
                {
                    if (PlayerMouse.LeftButton == ButtonState.Pressed)
                    {
                        ShowShop = false;
                        SK.FramePosY = 2;
                        SK.FramePosX = 0;
                        SK.PlayAnim = true;
                    }
                }
            }
        }
        public void RandomReward()
        {
            RewardIsSelected = false;
            ShowReward = false;
            while(Rewards.Count>0)
            {
                for (int i = Rewards.Count; i > 0; i--)
                {
                    Rewards.RemoveAt(i - 1);
                }
            }
            for(int i = 0; i<3;i++)
            {
                Rewards.Add(new Reward_Button(game, font, 400 + (i*400), 400, RAND.Next(0, 7), RAND.Next(0, 1)));
                Rewards[i].Load(game.Content, game._spriteBatch, "Relic",350,350);
            }
            /*Rewards.Add(new Exit_Button(game, font, 400, 900));
            Rewards[3].Load(game.Content, game._spriteBatch, "button_selection_1 (1)");*/
        }
        public void RandomShop()
        {
            ShowShop = false;
            while (Shops.Count > 0)
            {
                for (int i = Shops.Count; i > 0; i--)
                {
                    Shops.RemoveAt(i - 1);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                Shops.Add(new BuyRelic(game, font, 400 + (i * 400), 400,"", RAND.Next(0, 7), RAND.Next(0, 2)));
                Shops[i].Load(game.Content, game._spriteBatch, "Relic", 350, 350);
            }
            Shops.Add(new BuyHPButton(game, font, 330, 100, "Buy more Health : 250 $"));
            Shops.Add(new BuySPButton(game, font, 550, 100, "Buy more Skill Point : 250 $"));
            for(int i = 3;i<5;i++)
            {
                Shops[i].Load(game.Content, game._spriteBatch, "button_selection_1 (1)", 72 * 3, 24 * 3);
            }
        }
        public void DrawGameOverScene(SpriteBatch _SB)
        {
            _SB.Draw(ScreenBG, Vector2.Zero, Color.White);
            _SB.DrawString(Title, "Game Over", new Vector2(760, 400), Color.White);
            foreach (NextLevelButton GOB in GameOvers)
            {
                GOB.Draw(Vector2.Zero);
            }
        }
        public void UpdateGameOverScene(GameTime gameTime)
        {
            foreach (NextLevelButton BT in GameOvers)
            {
                BT.CheckCursor(PlayerMouse.X, PlayerMouse.Y);
                if (BT.IsSelected == true)
                {
                    if (game.NextLevel != BT.NextScreen && BT.NextScreen != null)
                    {
                        game.NextLevel = BT.NextScreen;
                    }
                    BT.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
                }
                if (PlayerMouse.LeftButton == ButtonState.Pressed)
                {
                    if (BT.IsSelected == true)
                    {
                        ResetStats();
                        ResetRoom();
                        ScreenEvent.Invoke(game.LoadingScreen, new EventArgs());
                    }
                }
            }
        }
        public void ResetStats()
        {
            IsPaused = true;
            PlayerCha.ChaOpa = 1;
            PlayerCha.IsAlive = true;
            PlayerCha.IsDead = false;
            game.Room = 1;
            game.Stage = 1;
            game.HP = 100;
            game.MaxHP = 100;
            game.SP = 100;
            game.Money = 0;
            game.LV = 0;
            game.LVProgess = 0;
            game.LVBarMax = 100;
            game.LVDamageMuti = 1;
            game.WepDamageMuti = 1;
            game.SPGainRate = 1;
            game.PlayerRelic = new Relic();
            GameIsEnded = false;
        }
        public void DrawGameEndScene(SpriteBatch _SB)
        {
            _SB.Draw(ScreenBG, Vector2.Zero, Color.White);
            _SB.DrawString(Title, "Game Ended", new Vector2(760, 00), Color.White);
            _SB.DrawString(Title, "Thank you for playing", new Vector2(560, 400), Color.White);
            foreach (NextLevelButton GOB in GameOvers)
            {
                GOB.Draw(Vector2.Zero);
            }
        }
        public void UpdateGameEndScene(GameTime gameTime)
        {
            foreach (NextLevelButton BT in GameOvers)
            {
                BT.CheckCursor(PlayerMouse.X, PlayerMouse.Y);
                if (BT.IsSelected == true)
                {
                    if (game.NextLevel != BT.NextScreen && BT.NextScreen != null)
                    {
                        game.NextLevel = BT.NextScreen;
                    }
                    BT.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
                }
                if (PlayerMouse.LeftButton == ButtonState.Pressed)
                {
                    if (BT.IsSelected == true)
                    {
                        ResetStats();
                        ResetRoom();
                        ScreenEvent.Invoke(game.LoadingScreen, new EventArgs());
                    }
                }
            }
        }

    }
}
