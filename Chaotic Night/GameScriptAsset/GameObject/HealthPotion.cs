using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Chaotic_Night
{
    class HealthPotion : CollectableObject
    {
        int Health = 50;
        public HealthPotion():base()
        {
        }
        public HealthPotion(int X,int Y):base(X,Y)
        {
        }
        public HealthPotion(Vector2 Pos):base(Pos)
        {
        }
        public override void Load(ContentManager Content, SpriteBatch _SB)
        {
            ObjectTexture = Content.Load<Texture2D>("hp_Potion (1)");
            Hitbox = new Rectangle((int)ObjectPos.X, (int)ObjectPos.Y, 57, 78);
            SB = _SB;
        }
        public override bool CheckCollision(MovableCharacter character)
        {
            if (Hitbox.Intersects(character.GetHitbox())&&character.HealthPoint<100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override void Collect(MovableCharacter character)
        {
            character.AddHP(Health);
        }
    }
}
