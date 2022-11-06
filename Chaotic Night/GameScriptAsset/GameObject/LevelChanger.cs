using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    public class LevelChanger : GameObject
    {
        public Screen NextLevel;
        public LevelChanger(Screen nextlevel) : base()
        {
            NextLevel = nextlevel;
            ObjectPos = Vector2.Zero;
        }
        public LevelChanger(Screen nextlevel,int X,int Y) : base(X,Y)
        {
            NextLevel = nextlevel;
            ObjectPos = new Vector2(X, Y);
        }
        public LevelChanger(Screen nextlevel,Vector2 Pos) : base(Pos)
        {
            NextLevel = nextlevel;
            ObjectPos = Pos;
        }
        public override void Load(ContentManager Content, SpriteBatch _SB)
        {
            ObjectTexture = Content.Load<Texture2D>("Player-Placeholder");
            Hitbox = new Rectangle((int)ObjectPos.X, (int)ObjectPos.Y, 24, 24);
            SB = _SB;
        }
        public void MoveToNextLevel(EventHandler ScreenEvent)
        {
            ScreenEvent.Invoke(NextLevel, new EventArgs());
        }
    }
}
