using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    public class MenuScreen : Screen
    {
        SpriteFont font;
        Texture2D TitleScreen1;
        Texture2D TitleScreen2;
        List<NextLevelButton> Buttons;
        MouseState PlayerMouse;
        KeyboardState PlayerKeyboard;
        Screen LevelToGo;
        Game1 game;
        public MenuScreen(Game1 game, EventHandler SEvent) : base(game,SEvent)
        {
            font = game.Content.Load<SpriteFont>("BM_Space Large");
            this.game = game;
            /*Buttons = new List<NextLevelButton>();
            Buttons.Add(new NextLevelButton(game.CWS,font,100,100,"Start"));
            Buttons.Add(new NextLevelButton(null, font, 100, 300, "Exit"));*/
            TitleScreen1 = game.Content.Load<Texture2D>("Start Game1");
            TitleScreen2 = game.Content.Load<Texture2D>("Start Game2");
            /*foreach (NextLevelButton BT in Buttons)
            {
                BT.Load(game.Content, game._spriteBatch, "button_selection_1 (1)",72*3,24*3);
            }*/
        }
        public override void Update(GameTime gameTime)
        {
            PlayerMouse = Mouse.GetState();
            PlayerKeyboard = Keyboard.GetState();
            if(PlayerKeyboard.IsKeyDown(Keys.Enter))
            {
                game.NextLevel = game.CWS;
                ScreenFadeOut(gameTime);
            }
            if (PlayerKeyboard.IsKeyDown(Keys.Escape))
            {
                game.Exit();
            }
                if (PlayerMouse.X > 652 && PlayerMouse.X < 1236 && PlayerMouse.Y > 715 && PlayerMouse.Y < 805)
            {
                if(PlayerMouse.LeftButton==ButtonState.Pressed)
                {
                    game.NextLevel = game.CWS;
                    ScreenFadeOut(gameTime);
                }
            }

            /*foreach(NextLevelButton BT in Buttons)
            {
                BT.CheckCursor(PlayerMouse.X, PlayerMouse.Y);
                if(BT.IsSelected==true)
                {
                    if (game.NextLevel != BT.NextScreen)
                    {
                        game.NextLevel = BT.NextScreen;
                    }
                    BT.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
                }
                if (PlayerMouse.LeftButton==ButtonState.Pressed)
                {
                    if(BT.IsSelected==true)
                    {
                        if(game.NextLevel==null)
                        {
                            game.Exit();
                        }
                        ScreenFadeOut(gameTime);
                    }
                }
            }*/
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch _spriteBatch)
        {
            /*foreach (NextLevelButton BT in Buttons)
            {
                BT.Draw(Vector2.Zero);
            }*/
            if(PlayerMouse.X>652&&PlayerMouse.X<1236&&PlayerMouse.Y>715&&PlayerMouse.Y<805)
            {
                _spriteBatch.Draw(TitleScreen2, Vector2.Zero, Color.White);
            }
            else
            {
                _spriteBatch.Draw(TitleScreen1, Vector2.Zero, Color.White);
            }
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
