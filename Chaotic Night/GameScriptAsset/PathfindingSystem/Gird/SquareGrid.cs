using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
        public class SquareGrid 
    {
        public bool ShowGrid;

        public Vector2 SlotsDims,GridDims,PhysStartPos,TotalPhysDims,CurHoverSlot;

        public Texture2D GridTex;

        public List<List<GridLocation>> slots = new List<List<GridLocation>>();

        public SquareGrid(ContentManager content ,Vector2 slotDims,Vector2 startPos,Vector2 totalDims)
        {
            ShowGrid = false;

            SlotsDims = slotDims;

            PhysStartPos = new Vector2((int)startPos.X,(int)startPos.Y);
            TotalPhysDims = new Vector2((int)totalDims.X, (int)totalDims.Y);

            CurHoverSlot = new Vector2(-1, -1);

            SetBaseGrid();

            GridTex = content.Load<Texture2D>("Grid");
        }
        public virtual void Update(Vector2 MousePos,Vector2 offset)
        {
            CurHoverSlot = GetSlotsFromPixel(MousePos,offset);
        }
        public virtual Vector2 GetPosFromLoc(Vector2 Loc)
        {
            return PhysStartPos + new Vector2((int)Loc.X * SlotsDims.X, (int)Loc.Y * SlotsDims.Y);
        }
        public virtual GridLocation GetSlotFromLocation(Vector2 Loc)
        {
            if(Loc.X>=0 && Loc.Y>=0 && Loc.X<slots.Count && Loc.Y<slots[(int)Loc.X].Count)
            {
                return slots[(int)Loc.X][(int)Loc.Y];
            }
            return null;
        }
        public virtual Vector2 GetSlotsFromPixel(Vector2 Pixel,Vector2 Offset)
        {
            Vector2 AdjustedPos = Pixel - PhysStartPos + Offset;

            Vector2 TempVec = new Vector2(Math.Min(Math.Max(0, (int)(AdjustedPos.X / SlotsDims.X)), slots.Count - 1), Math.Min(Math.Max(0, (int)(AdjustedPos.Y / SlotsDims.Y)), slots[0].Count - 1));

            return TempVec;
        }
        public virtual void SetBaseGrid()
        {
            GridDims = new Vector2((int)TotalPhysDims.X / SlotsDims.X, (int)TotalPhysDims.Y / SlotsDims.Y);

            slots.Clear();
            for(int i = 0;i<GridDims.X;i++)
            {
                slots.Add(new List<GridLocation>());
                for (int j = 0; j < GridDims.Y; j++)
                {
                    slots[i].Add(new GridLocation(1, false));
                }

            }
        }
        public virtual void DrawGrid(SpriteBatch SB,Vector2 CamPos, int MapW, int MapH, Vector2 Offset)
        {
            Vector2 topLeft = GetSlotsFromPixel(new Vector2(0, 0), Vector2.Zero);
            Vector2 botRight = GetSlotsFromPixel(new Vector2(MapW, MapH), Vector2.Zero);


            if(ShowGrid==true)
            {
                for (int j = (int)topLeft.X; j <= botRight.X && j < slots.Count; j++)
                {
                    for (int k = (int)topLeft.Y; k <= botRight.Y && k < slots[0].Count; k++)
                    {
                        if (CurHoverSlot.X == j && CurHoverSlot.Y == k)
                        {
                            SB.Draw(GridTex, (Offset + PhysStartPos + new Vector2(j * SlotsDims.X, k * SlotsDims.Y)) - CamPos, Color.Red);
                        }
                        else
                        {
                            SB.Draw(GridTex, (Offset + PhysStartPos + new Vector2(j * SlotsDims.X, k * SlotsDims.Y)) - CamPos, Color.Green);
                        }
                        if (slots[j][k].Filled == true)
                        {
                            SB.Draw(GridTex, (Offset + PhysStartPos + new Vector2(j * SlotsDims.X, k * SlotsDims.Y)) - CamPos, Color.Blue);
                        }
                    }
                }
            }
        }
        public virtual void CheckAndSetGrid(GameObject Object)
        {
            int EndX = Object.GetHitbox().Right;
            int EndY = Object.GetHitbox().Bottom;
            for (int MultiY = 0; Object.GetHitbox().Top + (MultiY * 24) <= EndY; MultiY++)
            {
                for (int MultiX = 0; Object.GetHitbox().Left + (MultiX * 24) <= EndX; MultiX++)
                {
                    Vector2 TempLoc = GetSlotsFromPixel(new Vector2(Object.GetPos().X + (MultiX * 24), Object.GetPos().Y + (MultiY * 24)), Vector2.Zero);
                    GridLocation Loc = GetSlotFromLocation(TempLoc);
                    Loc.SetToFilled(true);
                }
            }
        }

        #region A*Pathfinding

        public List<Vector2> GetPath(Vector2 Start, Vector2 End,bool AllowDiagnal)
        {
            List<GridLocation> Viewable = new List<GridLocation>(), used = new List<GridLocation>();
            List<List<GridLocation>> MasterGrid = new List<List<GridLocation>>();

            bool Impassible = false;
            float Cost = 1;
            for(int i=0;i<slots.Count;i++)
            {
                MasterGrid.Add(new List<GridLocation>());
                for(int j=0;j<slots[i].Count;j++)
                {
                    Impassible = slots[i][j].Impassible;
                    if(slots[i][j].Impassible||slots[i][j].Filled)
                    {
                        Impassible = true;
                    }

                    Cost = slots[i][j].Cost;
                    MasterGrid[i].Add(new GridLocation(new Vector2(i, j), Cost, Impassible, 999999));
                }
            }

            Viewable.Add(MasterGrid[(int)Start.X][(int)Start.Y]);
            while(Viewable.Count>0 && !(Viewable[0].Pos.X == End.X && Viewable[0].Pos.Y == End.Y))
            {
                TestAStarNode(MasterGrid, Viewable, used, End, AllowDiagnal);
            }
            List<Vector2> Path = new List<Vector2>();
            if (Viewable.Count>0)
            {
                int CurrentViewableStart = 0;
                GridLocation CurrentNode = Viewable[CurrentViewableStart];

                Path.Clear();
                Vector2 TempPos;

                while(true)
                {
                    TempPos = GetPosFromLoc(CurrentNode.Pos) + SlotsDims / 2;
                    Path.Add(new Vector2(TempPos.X, TempPos.Y));

                    if(CurrentNode.Pos==Start)
                    {
                        break;
                    }
                    else
                    {
                        if((int)CurrentNode.Parent.X!=-1&&(int)CurrentNode.Parent.Y!=-1)
                        {
                            if(CurrentNode.Pos.X==MasterGrid[(int)CurrentNode.Parent.X][(int)CurrentNode.Parent.Y].Pos.X && CurrentNode.Pos.Y == MasterGrid[(int)CurrentNode.Parent.X][(int)CurrentNode.Parent.Y].Pos.Y)
                            {
                                CurrentNode = Viewable[CurrentViewableStart];
                                CurrentViewableStart++;
                            }
                            CurrentNode = MasterGrid[(int)CurrentNode.Parent.X][(int)CurrentNode.Parent.Y];
                        }
                        else
                        {
                            CurrentNode = Viewable[CurrentViewableStart];
                            CurrentViewableStart++;
                        }
                    }
                }
                Path.Reverse();
            }
            return Path;
        }

        public void TestAStarNode(List<List<GridLocation>> mastergrid,List<GridLocation> viewable,List<GridLocation> used,Vector2 end,bool allowdiagnal)
        {
            GridLocation currentNode;
            bool up = true, down = true, left = true, right = true;

            //Above
            if (viewable[0].Pos.Y > 0 && viewable[0].Pos.Y < mastergrid[0].Count && !mastergrid[(int)viewable[0].Pos.X][(int)viewable[0].Pos.Y - 1].Impassible)
            {
                currentNode = mastergrid[(int)viewable[0].Pos.X][(int)viewable[0].Pos.Y - 1];
                up = currentNode.Impassible;
                SetAStarNode(viewable, used, currentNode, new Vector2(viewable[0].Pos.X, viewable[0].Pos.Y), viewable[0].CurDist, end, 1);
            }

            //Below
            if (viewable[0].Pos.Y >= 0 && viewable[0].Pos.Y + 1 < mastergrid[0].Count && !mastergrid[(int)viewable[0].Pos.X][(int)viewable[0].Pos.Y + 1].Impassible)
            {
                currentNode = mastergrid[(int)viewable[0].Pos.X][(int)viewable[0].Pos.Y + 1];
                down = currentNode.Impassible;
                SetAStarNode(viewable, used, currentNode, new Vector2(viewable[0].Pos.X, viewable[0].Pos.Y), viewable[0].CurDist, end, 1);
            }

            //Left
            if (viewable[0].Pos.X > 0 && viewable[0].Pos.X < mastergrid.Count && !mastergrid[(int)viewable[0].Pos.X - 1][(int)viewable[0].Pos.Y].Impassible)
            {
                currentNode = mastergrid[(int)viewable[0].Pos.X - 1][(int)viewable[0].Pos.Y];
                left = currentNode.Impassible;
                SetAStarNode(viewable, used, currentNode, new Vector2(viewable[0].Pos.X, viewable[0].Pos.Y), viewable[0].CurDist, end, 1);
            }

            //Right
            if (viewable[0].Pos.X >= 0 && viewable[0].Pos.X + 1 < mastergrid.Count && !mastergrid[(int)viewable[0].Pos.X + 1][(int)viewable[0].Pos.Y].Impassible)
            {
                currentNode = mastergrid[(int)viewable[0].Pos.X + 1][(int)viewable[0].Pos.Y];
                right = currentNode.Impassible;
                SetAStarNode(viewable, used, currentNode, new Vector2(viewable[0].Pos.X, viewable[0].Pos.Y), viewable[0].CurDist, end, 1);
            }

            if (allowdiagnal)
            {
                // Up and Right
                if (viewable[0].Pos.X >= 0 && viewable[0].Pos.X + 1 < mastergrid.Count && viewable[0].Pos.Y > 0 && viewable[0].Pos.Y < mastergrid[0].Count && !mastergrid[(int)viewable[0].Pos.X + 1][(int)viewable[0].Pos.Y - 1].Impassible && (!up || !right))
                {
                    currentNode = mastergrid[(int)viewable[0].Pos.X + 1][(int)viewable[0].Pos.Y - 1];

                    SetAStarNode(viewable, used, currentNode, new Vector2(viewable[0].Pos.X, viewable[0].Pos.Y), viewable[0].CurDist, end, (float)Math.Sqrt(2));
                }

                //Down and Right
                if (viewable[0].Pos.X >= 0 && viewable[0].Pos.X + 1 < mastergrid.Count && viewable[0].Pos.Y >= 0 && viewable[0].Pos.Y + 1 < mastergrid[0].Count && !mastergrid[(int)viewable[0].Pos.X + 1][(int)viewable[0].Pos.Y + 1].Impassible && (!down || !right))
                {
                    currentNode = mastergrid[(int)viewable[0].Pos.X + 1][(int)viewable[0].Pos.Y + 1];

                    SetAStarNode(viewable, used, currentNode, new Vector2(viewable[0].Pos.X, viewable[0].Pos.Y), viewable[0].CurDist, end, (float)Math.Sqrt(2));
                }

                //Down and Left
                if (viewable[0].Pos.X > 0 && viewable[0].Pos.X < mastergrid.Count && viewable[0].Pos.Y >= 0 && viewable[0].Pos.Y + 1 < mastergrid[0].Count && !mastergrid[(int)viewable[0].Pos.X - 1][(int)viewable[0].Pos.Y + 1].Impassible && (!down || !left))
                {
                    currentNode = mastergrid[(int)viewable[0].Pos.X - 1][(int)viewable[0].Pos.Y + 1];

                    SetAStarNode(viewable, used, currentNode, new Vector2(viewable[0].Pos.X, viewable[0].Pos.Y), viewable[0].CurDist, end, (float)Math.Sqrt(2));
                }

                // Up and Left
                if (viewable[0].Pos.X > 0 && viewable[0].Pos.X < mastergrid.Count && viewable[0].Pos.Y > 0 && viewable[0].Pos.Y < mastergrid[0].Count && !mastergrid[(int)viewable[0].Pos.X - 1][(int)viewable[0].Pos.Y - 1].Impassible && (!up || !left))
                {
                    currentNode = mastergrid[(int)viewable[0].Pos.X - 1][(int)viewable[0].Pos.Y - 1];

                    SetAStarNode(viewable, used, currentNode, new Vector2(viewable[0].Pos.X, viewable[0].Pos.Y), viewable[0].CurDist, end, (float)Math.Sqrt(2));
                }
            }
            viewable[0].HasBeenUsed = true;
            used.Add(viewable[0]);
            viewable.RemoveAt(0);
        }
        public void SetAStarNode(List<GridLocation> viewable, List<GridLocation> used, GridLocation nextNode, Vector2 nextParent, float d, Vector2 target, float DISTMULT)
        {
            float f = d;
            float addedDist = (nextNode.Cost * DISTMULT);




            //Add item
            if (!nextNode.IsViewable && !nextNode.HasBeenUsed)
            {
                //viewable.Add(new AStarNode(nextParent, f, new Vector2(nextNode.Pos.X, nextNode.Pos.Y), nextNode.CurrentDist + 1, nextNode.Cost, nextNode.Impassable));

                nextNode.SetNode(nextParent, f, d + addedDist);
                nextNode.IsViewable = true;

                SetAStarNodeInsert(viewable, nextNode);
            }
            //Node is in viewable, so check if Fscore needs revised
            else if (nextNode.IsViewable)
            {

                if (f < nextNode.FScore)
                {
                    nextNode.SetNode(nextParent, f, d + addedDist);
                }
            }
        }

        public virtual void SetAStarNodeInsert(List<GridLocation> List,GridLocation NewNode)
        {
            bool added = false;
            for(int i=0;i<List.Count;i++)
            {
                if(List[i].FScore>NewNode.FScore)
                {
                    List.Insert(Math.Max(1, i), NewNode);
                    added = true;
                    break;
                }
            }
            if(added)
            {
                List.Add(NewNode);
            }
        }
        
        #endregion
    }
}
