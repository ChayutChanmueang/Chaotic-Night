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
    public class Zone3Map12 : GameplayScreen
    {
        public Zone3Map12(Game1 game, EventHandler SEvent) : base(game, SEvent)
        {
            MapTex = game.Content.Load<Texture2D>("Tileset_Zone3_12(1)");
            SpawnLC(1430, 1700);
            SpawnPos = new Vector2(360, 685);
            GameCamera.CamPos = PlayerCha.GetOrigin() - new Vector2(ScreenW / 2, ScreenH / 2);
            SK = new Shopkeeper(90, 400);
            SK.Load(game.Content, game._spriteBatch, "Hum", 216, 216);
            SpawnEnemy(0, 2, 1430, 1160, 1320, 910);
            SpawnEnemy(1, 1, 1430, 1160, 1320, 910);
            for (int i = 0; i < 73; i++) //1
            {
                GameObj.Add(new GameObject(24 + (24 * i), 142));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 73; i < 117; i++) //2
            {
                GameObj.Add(new GameObject(24 + (24 * (i-73)), 816));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 117; i < 146; i++) //3
            {
                GameObj.Add(new GameObject(1080 + (24 * (i - 117)), 1728));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 146; i < 174; i++) //4
            {
                GameObj.Add(new GameObject(24 , 142 + (24 * (i - 146))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 174; i < 240; i++) //5
            {
                GameObj.Add(new GameObject(1776, 142 + (24 * (i - 174))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 240; i < 278; i++) //6
            {
                GameObj.Add(new GameObject(1080, 816 + (24 * (i - 240))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }

            for (int i = 278; i < 351; i++) //7
            {
                GameObj.Add(new GameObj_IgnoreBullets(24 + (24 * (i - 278)), 226));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 351; i < 363; i++) //8
            {
                GameObj.Add(new GameObj_IgnoreBullets(780 + (24 * (i - 351)), 384));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 363; i < 375; i++) //9
            {
                GameObj.Add(new GameObj_IgnoreBullets(780 + (24 * (i - 363)), 624));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 375; i < 383; i++) //10
            {
                GameObj.Add(new GameObj_IgnoreBullets(1596 + (24 * (i - 375)), 780));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 383; i < 391; i++) //11
            {
                GameObj.Add(new GameObj_IgnoreBullets(1596 + (24 * (i - 383)), 816));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 391; i < 397; i++) //12
            {
                GameObj.Add(new GameObj_IgnoreBullets(780 , 225 + (24 * (i - 391))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 397; i < 403; i++) //13
            {
                GameObj.Add(new GameObj_IgnoreBullets(1066, 225 + (24 * (i - 397))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 403; i < 411; i++) //14
            {
                GameObj.Add(new GameObj_IgnoreBullets(780, 624 + (24 * (i - 403))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 411; i < 419; i++) //14
            {
                GameObj.Add(new GameObj_IgnoreBullets(1066, 624 + (24 * (i - 411))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 419; i < 424; i++) //15
            {
                GameObj.Add(new GameObj_IgnoreBullets(1596, 720 + (24 * (i - 411))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
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

            SpawnEnemy(0, 2, 1430, 1160, 1320, 910);
            SpawnEnemy(1, 1, 1430, 1160, 1320, 910);
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
