using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    public class PrototypeMap3 : GameplayScreen
    {
        public PrototypeMap3(Game1 game, EventHandler SEvent) : base(game,SEvent)
        {
            MapTex = game.Content.Load<Texture2D>("Map_Prototype3");
            SpawnLC(1740, 612);
            for (int i = 0; i < 75; i++)
            {
                GameObj.Add(new GameObject(64 + (24 * i), 120));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 75; i < 114; i++)
            {
                GameObj.Add(new GameObject(40, 80 + (24 * (i - 75))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 114; i < 189; i++)
            {
                GameObj.Add(new GameObject(64 + (24 * (i - 114)), 1020));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 189; i < 228; i++)
            {
                GameObj.Add(new GameObject(1864, 1020 - (24 * (i - 189))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 228; i < 241; i++)
            {
                GameObj.Add(new GameObject(664, 900 - (24*(i-228))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 241; i < 254; i++)
            {
                GameObj.Add(new GameObject(1238, 900 - (24 * (i - 241))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 254; i < 278; i++)
            {
                GameObj.Add(new GameObject(1238 - (24*(i - 254)), 590));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            SpawnEnemy(0,3, 470, 824, 64, 144);
        }
        public override void Update(GameTime gameTime)
        {
            if(RoomIsReset == false)
            {
                if (LC.GetHitbox().Intersects(PlayerCha.GetHitbox()))
                {
                    if (EnemyAmount <= 0)
                    {
                        //RoomIsReset = false;
                        if (game.CRS.RandomIsDone == true)
                        {
                            game.CRS.RandomIsDone = false;
                        }
                        ScreenEvent.Invoke(game.CRS, new EventArgs());
                    }
                }
            }
            base.Update(gameTime);
        }
        public override void ResetRoom()
        {
            base.ResetRoom();
            SpawnEnemy(0,3, 470, 824, 64, 144);
        }
        public override void Reload()
        {
            ResetRoom();
            PlayerCha.GetWeapon().ClearBullet();
            LoadCharacterStats();
            GameCamera.CamPos = new Vector2(300, 500) - new Vector2(ScreenW / 2, ScreenH / 2);
        }
    }
}
