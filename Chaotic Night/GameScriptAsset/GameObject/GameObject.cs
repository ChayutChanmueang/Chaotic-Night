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
        protected SpriteBatch SB;
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
            Hitbox = new Rectangle((int)ObjectPos.X, (int)ObjectPos.Y, 24, 24);
            SB = _SB;
        }
        public virtual void Load(ContentManager Content, SpriteBatch _SB, string ObjTexName)
        {
            ObjectTexture = Content.Load<Texture2D>(ObjTexName);
            Hitbox = new Rectangle((int)ObjectPos.X, (int)ObjectPos.Y, 24, 24);
            SB = _SB;
        }
        public virtual void Load(ContentManager Content, SpriteBatch _SB,int Width,int Hight)
        {
            ObjectTexture = Content.Load<Texture2D>("Player-Placeholder");
            Hitbox = new Rectangle((int)ObjectPos.X, (int)ObjectPos.Y, Width, Hight);
            SB = _SB;
        }
        public virtual void Load(ContentManager Content, SpriteBatch _SB, string ObjTexName, int Width, int Hight)
        {
            ObjectTexture = Content.Load<Texture2D>(ObjTexName);
            Hitbox = new Rectangle((int)ObjectPos.X, (int)ObjectPos.Y, Width, Hight);
            SB = _SB;
        }
        public virtual void Draw(Vector2 CamPos)
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
