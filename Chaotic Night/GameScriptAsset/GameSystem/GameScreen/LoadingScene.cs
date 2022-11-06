using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    public class LoadingScene : Screen
    {
        SpriteFont font;
        Screen LevelToGo;
        Game1 game;
        public LoadingScene(Game1 game, EventHandler SEvent) : base(game, SEvent)
        {
            font = game.Content.Load<SpriteFont>("BM_Space_8");
            this.game = game;
        }
        public override void Update(GameTime gameTime)
        {
            if(game.NextLevel.IsAlreadyLoaded==true)
            {
                game.NextLevel.Reload();
                ScreenEvent.Invoke(game.NextLevel, new EventArgs());
            }
            else
            {
                game.NextLevel.Load();
            }
            base.Update(gameTime);
        }
        
    }
}
