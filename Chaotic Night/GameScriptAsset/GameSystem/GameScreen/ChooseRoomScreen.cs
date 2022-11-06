using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    public class ChooseRoomScreen : Screen
    {
        SpriteFont font;
        List<NextLevelButton> Buttons;
        MouseState PlayerMouse;
        KeyboardState PlayerKeyboard;
        Game1 game;
        int OldMapNum;
        int NextMapNum;
        Random RAND;
        public bool RandomIsDone = false;
        public ChooseRoomScreen(Game1 game, EventHandler SEvent) : base(game, SEvent)
        {
            font = game.Content.Load<SpriteFont>("BM_Space_8");
            this.game = game;
            RAND = new Random();
            Buttons = new List<NextLevelButton>();
            Buttons.Add(new NextLevelButton(null, font, 100, 100, "Start"));
            Buttons.Add(new NextLevelButton(game.Shop, font, 100, 300, "Shop"));
            foreach (NextLevelButton BT in Buttons)
            {
                BT.Load(game.Content, game._spriteBatch, "button_selection_1 (1)", 72 * 3, 24 * 3);
            }
        }
        public override void Update(GameTime gameTime)
        {
            PlayerMouse = Mouse.GetState();
            PlayerKeyboard = Keyboard.GetState();
            foreach (NextLevelButton BT in Buttons)
            {
                BT.CheckCursor(PlayerMouse.X, PlayerMouse.Y);
                if (BT.IsSelected == true)
                {
                    if (game.NextLevel != BT.NextScreen && BT.NextScreen !=null)
                    {
                        game.NextLevel = BT.NextScreen;
                    }
                    BT.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
                }
                if (PlayerMouse.LeftButton == ButtonState.Pressed)
                {
                    if (BT.IsSelected == true)
                    {
                        /*if (game.NextLevel == null)
                        {
                            game.Exit();
                        }*/
                        ScreenFadeOut(gameTime);
                    }
                }
                if (RandomIsDone == false)
                {
                    while (NextMapNum == OldMapNum)
                    {
                        RandomNextMap();
                    }
                    NextMapNum = OldMapNum;
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
                if (game.NextLevel!=null)
                {
                    ScreenEvent.Invoke(game.LoadingScreen, new EventArgs());
                }
                ScreenOpa = 1;
                IsPaused = true;
            }
        }
        public void RandomNextMap()
        {
            NextMapNum = RAND.Next(1, 4);
            if(NextMapNum==OldMapNum)
            {
                NextMapNum = RAND.Next(1, 3);
            }
            else
            {
                if (NextMapNum == 1 && OldMapNum != 1)
                {
                    Buttons[0].NextScreen = game.ProtoMap1;
                }
                if (NextMapNum == 2 && OldMapNum != 2)
                {
                    Buttons[0].NextScreen = game.ProtoMap2;
                }
                if (NextMapNum == 3 && OldMapNum != 3)
                {
                    Buttons[0].NextScreen = game.ProtoMap3;
                }
                RandomIsDone = true;
            }
        }
        public override void Load()
        {
            IsAlreadyLoaded = true;
        }
    }
}
