using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.ViewportAdapters;

namespace Chaotic_Night
{
    public class Zone3Map10 : GameplayScreen
    {
        public Zone3Map10(Game1 game, EventHandler SEvent) : base(game, SEvent)
        {
            MapTex = game.Content.Load<Texture2D>("Tileset_Zone3_10(1)");
            SpawnLC(2030, 1365);
            SpawnPos = new Vector2(54, 1365);
            GameCamera.CamPos = PlayerCha.GetOrigin() - new Vector2(ScreenW / 2, ScreenH / 2);
            SK = new Shopkeeper(750, 675);
            SK.Load(game.Content, game._spriteBatch, "Hum", 216, 216);
            for (int i = 0; i < 17; i++) //1
            {
                GameObj.Add(new GameObject(846 + (24 * i), 144));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 17; i < 31; i++) //2
            {
                GameObj.Add(new GameObject(510 + (24 * (i-17)), 624));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 31; i < 45; i++) //3
            {
                GameObj.Add(new GameObject(1214 + (24 * (i-31)), 624));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 45; i < 65; i++) //4
            {
                GameObj.Add(new GameObject(30 + (24 * (i - 45)), 1057));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 65; i < 85; i++) //5
            {
                GameObj.Add(new GameObject(1578 + (24 * (i - 65)), 1057));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 85; i < 170; i++) //6
            {
                GameObj.Add(new GameObject(30 + (24 * (i - 85)), 1728));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 170; i < 190; i++) //7
            {
                GameObj.Add(new GameObject(846, 144 + (24 * (i - 170))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 190; i < 210; i++) //8
            {
                GameObj.Add(new GameObject(1242, 144 + (24 * (i - 190))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 210; i < 228; i++) //9
            {
                GameObj.Add(new GameObject(510, 642 + (24 * (i - 210))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 228; i < 246; i++) //10
            {
                GameObj.Add(new GameObject(1578, 642 + (24 * (i - 228))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 246; i < 274; i++) //11
            {
                GameObj.Add(new GameObject(30, 1057 + (24 * (i - 246))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 274; i < 302; i++) //12
            {
                GameObj.Add(new GameObject(2058, 1057 + (24 * (i - 274))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 302; i < 316; i++) //13
            {
                GameObj.Add(new GameObj_IgnoreBullets(876 + (24 * (i - 302)), 1212));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 316; i < 401; i++) //14
            {
                GameObj.Add(new GameObj_IgnoreBullets(30 + (24 * (i - 316)), 1574));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 401; i < 416; i++) //15
            {
                GameObj.Add(new GameObj_IgnoreBullets(876, 1212 + (24 * (i - 401))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 416; i < 431; i++) //15
            {
                GameObj.Add(new GameObj_IgnoreBullets(1210, 1212 + (24 * (i - 416))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }

            SpawnEnemy(0, 2, 1060, 1040, 870, 870);
            SpawnEnemy(1, 1, 1060, 1040, 870, 870);
        }
        public override void Update(GameTime gameTime)
        {
            if (RoomIsReset == false)
            {
                if (LC.GetHitbox().Intersects(PlayerCha.GetHitbox()))
                {
                    if (EnemyAmount <= 0)
                    {
                        game.Room += 1;
                        ScreenEvent.Invoke(game.LoadingScreen, new EventArgs());
                    }
                }
            }
            // _tileMapRenderer.Update(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch _spriteBatch)
        {
            // _tileMapRenderer.Draw();
            base.Draw(_spriteBatch);
        }
        public override void ResetRoom()
        {
            base.ResetRoom();

            SpawnEnemy(0, 2, 1060, 1040, 870, 870);
            SpawnEnemy(1, 1, 1060, 1040, 870, 870);
        }
        public override void Reload()
        {
            ResetRoom();
            PlayerCha.GetWeapon().ClearBullet();
            LoadCharacterStats();
            GameCamera.CamPos = SpawnPos - new Vector2(ScreenW / 2, ScreenH / 2);
        }
    }
}
