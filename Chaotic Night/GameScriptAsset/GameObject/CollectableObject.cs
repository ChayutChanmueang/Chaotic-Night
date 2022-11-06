using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Chaotic_Night
{
    public class CollectableObject : GameObject
    {
        public CollectableObject() : base()
        {
        }
        public CollectableObject(int X, int Y) : base(X, Y)
        {
        }
        public CollectableObject(Vector2 Pos) : base(Pos)
        {
        }
        public virtual bool CheckCollision(MovableCharacter character)
        {
            if(Hitbox.Intersects(character.GetHitbox()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual void Collect(MovableCharacter character)
        {

        }
    }
}
