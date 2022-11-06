using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    public class ChooseWeaponsScene : Screen
    {
        SpriteFont font;
        SpriteFont BiggerFont;
        Texture2D Melee;
        Texture2D Range;
        List<NextLevelButton> Buttons;
        MouseState PlayerMouse;
        KeyboardState PlayerKeyboard;
        Screen LevelToGo;
        Game1 game;
        protected bool RoomIsReset = false;
        public ChooseWeaponsScene(Game1 game, EventHandler SEvent) : base(game, SEvent)
        {
            font = game.Content.Load<SpriteFont>("BM_Space_8");
            BiggerFont = game.Content.Load<SpriteFont>("BM_Space Large");
            Melee = game.Content.Load<Texture2D>("S");
            Range = game.Content.Load<Texture2D>("G");
            this.game = game;
            Buttons = new List<NextLevelButton>();
            Buttons.Add(new NextLevelButton(game.Z1M1, font, 700, 800, "Melee"));
            Buttons.Add(new NextLevelButton(game.Z1M1, font, 1100, 800, "Range"));
            foreach (NextLevelButton BT in Buttons)
            {
                BT.Load(game.Content, game._spriteBatch, "button_selection_1 (1)", 72 * 3, 24 * 3);
            }
        }
        public override void Update(GameTime gameTime)
        {
            PlayerMouse = Mouse.GetState();
            PlayerKeyboard = Keyboard.GetState();
            if (FadingIsDone == false)
            {
                ScreenFadeIn(gameTime);
            }
            foreach (NextLevelButton BT in Buttons)
            {
                if(IsPaused==false)
                {
                    BT.CheckCursor(PlayerMouse.X, PlayerMouse.Y);
                    if (BT.IsSelected == true)
                    {
                        if (game.NextLevel != BT.NextScreen)
                        {
                            game.NextLevel = BT.NextScreen;
                        }
                        BT.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                }
                if (PlayerMouse.LeftButton == ButtonState.Pressed)
                {
                    if (BT.IsSelected == true)
                    {
                        if(BT.NextScreenName=="Melee")
                        {
                            game.PlayerWeapon = new MeleeWeapons(game.ProtoMap1.PlayerCha);
                        }
                        if (BT.NextScreenName == "Range")
                        {
                            game.PlayerWeapon = new RangeWeapons(game.ProtoMap1.PlayerCha);
                        }
                        if (game.NextLevel == null)
                        {
                            game.Exit();
                        }
                        //ScreenFadeOut(gameTime);
                        ScreenEvent.Invoke(game.LoadingScreen, new EventArgs());
                    }
                }
            }
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch _spriteBatch)
        {
            foreach (NextLevelButton BT in Buttons)
            {
                BT.Draw(Vector2.Zero);
            }
            base.Draw(_spriteBatch);

            _spriteBatch.Draw(Melee, new Vector2(700, 600), Color.White);
            _spriteBatch.Draw(Range, new Vector2(1100, 600), Color.White);
            _spriteBatch.DrawString(BiggerFont, "Choose your Weapon", new Vector2(560, 400), Color.White);
            _spriteBatch.Draw(ScreenHider, Vector2.Zero, Color.White * ScreenOpa);
        }
        public override void ScreenFadeOut(GameTime time)
        {
            TimeProg += (float)time.ElapsedGameTime.TotalSeconds;
            if (ScreenOpa < 1)
            {
                if (TimeProg > 0.07)
                {
                    ScreenOpa += (float)0.1;
                    TimeProg = 0;
                }
            }
            else
            {
                ScreenEvent.Invoke(game.LoadingScreen, new EventArgs());
                ScreenOpa = 1;
            }
        }
        public override void Load()
        {
            IsAlreadyLoaded = true;
        }
        public virtual void ResetRoom()
        {
            IsPaused = true;
            if (FadingIsDone == true)
            {
                ScreenOpa = 1;
                FadingIsDone = false;
            }
        }
        public override void Reload()
        {
            ResetRoom();
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
    }
}
