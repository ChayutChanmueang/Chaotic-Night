using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    public class ShopScreen : Screen
    {
        SpriteFont font;
        List<ShopButton> ShopButtons;
        NextLevelButton ExitButtons;
        MouseState PlayerMouse;
        KeyboardState PlayerKeyboard;
        Screen LevelToGo;
        Game1 game;
        public ShopScreen(Game1 game, EventHandler SEvent) : base(game, SEvent)
        {
            font = game.Content.Load<SpriteFont>("BM_Space_8");
            this.game = game;
            ShopButtons = new List<ShopButton>();
            ShopButtons.Add(new BuyDmgButton(game, font, 100, 100, "Upgrade Weapon : 750 $"));
            ShopButtons.Add(new BuyHPButton(game, font, 330, 100, "Buy more Health : 250 $"));
            ShopButtons.Add(new BuySPButton(game, font, 550, 100, "Buy more Skill Point : 250 $"));
            ExitButtons = new NextLevelButton(game.CRS, font, 100, 300, "Return");
            //Buttons.Add(new NextLevelButton(null, font, 100, 300, "Exit"));
            foreach (ShopButton BT in ShopButtons)
            {
                BT.Load(game.Content, game._spriteBatch, "button_selection_1 (1)", 72 * 3, 24 * 3);
            }
            ExitButtons.Load(game.Content, game._spriteBatch, "button_selection_1 (1)", 72 * 3, 24 * 3);
        }
        public override void Update(GameTime gameTime)
        {
            PlayerMouse = Mouse.GetState();
            PlayerKeyboard = Keyboard.GetState();

            ExitButtons.CheckCursor(PlayerMouse.X, PlayerMouse.Y);
            if (ExitButtons.IsSelected == true)
            {
                if (game.NextLevel != game.CRS)
                {
                    game.NextLevel = game.CRS;
                }
                ExitButtons.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (PlayerMouse.LeftButton == ButtonState.Pressed)
            {
                if (ExitButtons.IsSelected == true)
                {
                    /*if (game.NextLevel == null)
                    {
                        game.Exit();
                    }*/
                    ScreenFadeOut(gameTime);
                }
            }
            foreach(ShopButton SBT in ShopButtons)
            {
                SBT.CheckCursor(PlayerMouse.X, PlayerMouse.Y);
                if (SBT.IsSelected == true)
                {
                    SBT.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
                }
                if (PlayerMouse.LeftButton == ButtonState.Pressed)
                {
                    if (SBT.IsSelected == true)
                    {
                        SBT.Interact();
                    }
                }
            }
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch _spriteBatch)
        {
            foreach (ShopButton BT in ShopButtons)
            {
                BT.Draw(Vector2.Zero);
            }
            ExitButtons.Draw(Vector2.Zero);
            base.Draw(_spriteBatch);
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
                IsPaused = true;
            }
            else
            {
                ScreenEvent.Invoke(game.LoadingScreen, new EventArgs());
                ScreenOpa = 1;
                IsPaused = true;
            }
        }
        public override void Load()
        {
            IsAlreadyLoaded = true;
        }
    }
}
