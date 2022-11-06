using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    public class GridLocation
    {
        public bool Filled, Impassible, UnPathable, HasBeenUsed, IsViewable;
        public float FScore,Cost,CurDist;
        public Vector2 Parent, Pos;

        public GridLocation(float cost,bool filled)
        {
            Cost = cost;
            Filled = filled;

            HasBeenUsed = false;
            IsViewable = false;
            UnPathable = false;
            Impassible = filled;
        }
        public GridLocation(Vector2 pos,float cost, bool filled,float fscore)
        {
            Cost = cost;
            Filled = filled;

            HasBeenUsed = false;
            IsViewable = false;
            UnPathable = false;
            Impassible = filled;

            Pos = pos;

            FScore = fscore;
        }
        public void SetNode(Vector2 parent,float fscore,float curdist)
        {
            Parent = parent;
            FScore = fscore;
            CurDist = curdist;
        }
        public virtual void SetToFilled(bool impassible)
        {
            Filled = true;
            Impassible = impassible;
        }
    }
}
