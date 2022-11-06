using System;
using System.Collections.Generic;
using Roy_T.AStar.Grids;
using Roy_T.AStar.Primitives;
using Roy_T.AStar.Paths;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public MenuScreen MainMenu;
        public TestMap TMap;
        public TestMap2 TMap2;
        public PrototypeMap1 ProtoMap1;
        public PrototypeMap2 ProtoMap2;
        public PrototypeMap3 ProtoMap3;
        public Zone1Map1 Z1M1;
        public Zone1Map2 Z1M2;
        public Zone1Map3 Z1M3;
        public Zone1Map4 Z1M4;
        public Zone1Map5 Z1M5;
        public Zone1Map6 Z1M6;
        public Zone1Map7 Z1M7;
        public Zone1Map8 Z1M8;
        public Zone1Map9 Z1M9;
        public Zone1Map10 Z1M10;
        public Zone1Map11 Z1M11;
        public Zone1Map12 Z1M12;
        public Zone1Boss Z1Boss;
        public Zone2Map1 Z2M1;
        public Zone2Map2 Z2M2;
        public Zone2Map3 Z2M3;
        public Zone2Map4 Z2M4;
        public Zone2Map5 Z2M5;
        public Zone2Map6 Z2M6;
        public Zone2Map7 Z2M7;
        public Zone2Map8 Z2M8;
        public Zone2Map9 Z2M9;
        public Zone2Map10 Z2M10;
        public Zone2Map11 Z2M11;
        public Zone2Map12 Z2M12;
        public Zone2Boss Z2Boss;
        public Zone3Map1 Z3M1;
        public Zone3Map2 Z3M2;
        public Zone3Map3 Z3M3;
        public Zone3Map4 Z3M4;
        public Zone3Map5 Z3M5;
        public Zone3Map6 Z3M6;
        public Zone3Map7 Z3M7;
        public Zone3Map8 Z3M8;
        public Zone3Map9 Z3M9;
        public Zone3Map10 Z3M10;
        public Zone3Map11 Z3M11;
        public Zone3Map12 Z3M12;
        public Zone3Boss Z3Boss;
        public Screen CurMap;
        public Screen NextLevel;
        public LoadingScene LoadingScreen;
        public ChooseRoomScreen CRS;
        public ChooseRoomScreen CRS2;
        public ChooseWeaponsScene CWS;
        public ShopScreen Shop;
        public ShopScreen Shop2;

        //Character Val.
        public int HP;
        public int MaxHP = 100;
        public int SP;
        public int Money = 0;
        public int LV = 0;
        public float LVProgess = 0;
        public int LVBarMax = 100;
        public float LVDamageMuti = 1;
        public float WepDamageMuti = 1;
        public float SPGainRate = 1;
        public Weapons PlayerWeapon;
        public Relic PlayerRelic = new Relic();
        public int Stage = 1;
        public int Room = 0;
        public bool ShopAvailable = true;
        public int ShopSpawnRate = 5;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            HP = 100;
            SP = 100;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //Every maps must be loaded before MainMenu and Loading Scene so they know those other map exist idk why it's like this.
            TMap = new TestMap(this, new EventHandler(GameplayScreenEvent));
            TMap2 = new TestMap2(this, new EventHandler(GameplayScreenEvent));
            ProtoMap3 = new PrototypeMap3(this, new EventHandler(GameplayScreenEvent));
            ProtoMap2 = new PrototypeMap2(this, new EventHandler(GameplayScreenEvent));
            ProtoMap1 = new PrototypeMap1(this, new EventHandler(GameplayScreenEvent));
            Z1M1 = new Zone1Map1(this, new EventHandler(GameplayScreenEvent));
            Z1M2 = new Zone1Map2(this, new EventHandler(GameplayScreenEvent));
            Z1M3 = new Zone1Map3(this, new EventHandler(GameplayScreenEvent));
            Z1M4 = new Zone1Map4(this, new EventHandler(GameplayScreenEvent));
            Z1M5 = new Zone1Map5(this, new EventHandler(GameplayScreenEvent));
            Z1M6 = new Zone1Map6(this, new EventHandler(GameplayScreenEvent));
            Z1M7 = new Zone1Map7(this, new EventHandler(GameplayScreenEvent));
            Z1M8 = new Zone1Map8(this, new EventHandler(GameplayScreenEvent));
            Z1M9 = new Zone1Map9(this, new EventHandler(GameplayScreenEvent));
            Z1M10 = new Zone1Map10(this, new EventHandler(GameplayScreenEvent));
            Z1M11 = new Zone1Map11(this, new EventHandler(GameplayScreenEvent));
            Z1M12 = new Zone1Map12(this, new EventHandler(GameplayScreenEvent));
            Z1Boss=new Zone1Boss(this, new EventHandler(GameplayScreenEvent));
            Z2M1 = new Zone2Map1(this, new EventHandler(GameplayScreenEvent));
            Z2M2 = new Zone2Map2(this, new EventHandler(GameplayScreenEvent));
            Z2M3 = new Zone2Map3(this, new EventHandler(GameplayScreenEvent));
            Z2M4 = new Zone2Map4(this, new EventHandler(GameplayScreenEvent));
            Z2M5 = new Zone2Map5(this, new EventHandler(GameplayScreenEvent));
            Z2M6 = new Zone2Map6(this, new EventHandler(GameplayScreenEvent));
            Z2M7 = new Zone2Map7(this, new EventHandler(GameplayScreenEvent));
            Z2M8 = new Zone2Map8(this, new EventHandler(GameplayScreenEvent));
            Z2M9 = new Zone2Map9(this, new EventHandler(GameplayScreenEvent));
            Z2M10 = new Zone2Map10(this, new EventHandler(GameplayScreenEvent));
            Z2M11 = new Zone2Map11(this, new EventHandler(GameplayScreenEvent));
            Z2M12 = new Zone2Map12(this, new EventHandler(GameplayScreenEvent));
            Z2Boss = new Zone2Boss(this, new EventHandler(GameplayScreenEvent));
            Z3M1 = new Zone3Map1(this, new EventHandler(GameplayScreenEvent));
            Z3M2 = new Zone3Map2(this, new EventHandler(GameplayScreenEvent));
            Z3M3 = new Zone3Map3(this, new EventHandler(GameplayScreenEvent));
            Z3M4 = new Zone3Map4(this, new EventHandler(GameplayScreenEvent));
            Z3M5 = new Zone3Map5(this, new EventHandler(GameplayScreenEvent));
            Z3M6 = new Zone3Map6(this, new EventHandler(GameplayScreenEvent));
            Z3M7 = new Zone3Map7(this, new EventHandler(GameplayScreenEvent));
            Z3M8 = new Zone3Map8(this, new EventHandler(GameplayScreenEvent));
            Z3M9 = new Zone3Map9(this, new EventHandler(GameplayScreenEvent));
            Z3M10 = new Zone3Map10(this, new EventHandler(GameplayScreenEvent));
            Z3M11 = new Zone3Map11(this, new EventHandler(GameplayScreenEvent));
            Z3M12 = new Zone3Map12(this, new EventHandler(GameplayScreenEvent));
            Z3Boss = new Zone3Boss(this, new EventHandler(GameplayScreenEvent));
            Shop = new ShopScreen(this, new EventHandler(GameplayScreenEvent));
            CRS = new ChooseRoomScreen(this, new EventHandler(GameplayScreenEvent));
            CWS = new ChooseWeaponsScene(this, new EventHandler(GameplayScreenEvent));

            LoadingScreen = new LoadingScene(this, new EventHandler(GameplayScreenEvent));
            MainMenu = new MenuScreen(this, new EventHandler(GameplayScreenEvent));
            CurMap = MainMenu;
            NextLevel = ProtoMap1;
        }

        protected override void Update(GameTime gameTime)
        {
            /*if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();*/
            CurMap.Update(gameTime);
            if(Room>=5)
            {
                if (Stage == 1)
                {
                    if(NextLevel != Z1Boss)
                    {
                        NextLevel = Z1Boss;
                    }
                }
                if (Stage == 2)
                {
                    if (NextLevel != Z2Boss)
                    {
                        NextLevel = Z2Boss;
                    }
                }
                if (Stage == 3)
                {
                    if (NextLevel != Z3Boss)
                    {
                        NextLevel = Z3Boss;
                    }
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(39,33,48));
            _spriteBatch.Begin();
            CurMap.Draw(_spriteBatch);
            _spriteBatch.End();
            // TODO: Add your drawing code here
            
            base.Draw(gameTime);
        }
        public void GameplayScreenEvent(object obj,EventArgs e)
        {
            CurMap = (Screen)obj;
        }
        public void SaveStats(int hp,int sp,Weapons PWeapon)
        {
            HP = hp;
            SP = sp;
            PlayerWeapon = PWeapon;
        }
    }
}
