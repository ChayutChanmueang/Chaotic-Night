using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    public class InteractableObject : GameObject
    {
        public bool IsSelected = false;
        public InteractableObject(int X,int Y) : base(X,Y)
        {
        }
        public void CheckCursor(int MouseX,int MouseY)
        {
            if(MouseX>Hitbox.Left&&MouseX<Hitbox.Right&&MouseY>Hitbox.Top&&MouseY<Hitbox.Bottom)
            {
                IsSelected = true;
            }
            else
            {
                IsSelected = false;
            }
        }
        public virtual void Interact()
        {
            if(IsSelected)
            {
                Interaction();
            }
        }
        protected virtual void Interaction()
        {

        }
    }
}
