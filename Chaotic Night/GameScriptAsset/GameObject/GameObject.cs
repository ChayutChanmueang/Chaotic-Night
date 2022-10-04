using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    public class GameObject
    {
        protected Texture2D ObjectTexture;
        protected Vector2 ObjectPos;
        protected Rectangle Hitbox;
        private SpriteBatch SB;
        public GameObject()
        {
            ObjectPos = Vector2.Zero;
        }
        public GameObject(int X,int Y)
        {
            ObjectPos = new Vector2(X,Y);
        }
        public GameObject(Vector2 Pos)
        {
            ObjectPos = Pos;
        }
        public virtual void Load(ContentManager Content, SpriteBatch _SB)
        {
            ObjectTexture = Content.Load<Texture2D>("Player-Placeholder");
            Hitbox = new Rectangle((int)ObjectPos.X, (int)ObjectPos.Y, ObjectTexture.Width, ObjectTexture.Height);
            SB = _SB;
        }
        public void Draw(Vector2 CamPos)
        {
            SB.Draw(ObjectTexture, ObjectPos-CamPos, Color.White);
        }
        public Rectangle GetHitbox()
        {
            return Hitbox;
        }
        public Vector2 GetPos()
        {
            return ObjectPos;
        }
    }
}
