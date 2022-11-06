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
    public class Zone3Map11 : GameplayScreen
    {
        public Zone3Map11(Game1 game, EventHandler SEvent) : base(game, SEvent)
        {
            MapTex = game.Content.Load<Texture2D>("Tileset_Zone3_11(1)");
            SpawnLC(400, 290);
            SpawnPos = new Vector2(1400, 1150);
            GameCamera.CamPos = PlayerCha.GetOrigin() - new Vector2(ScreenW / 2, ScreenH / 2);
            SK = new Shopkeeper(1255, 1120);
            SK.Load(game.Content, game._spriteBatch, "Hum", 216, 216);
            for (int i = 0; i < 29; i++) //1
            {
                GameObj.Add(new GameObject(30 + (24 * i), 144));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 29; i < 43; i++) //2
            {
                GameObj.Add(new GameObject(714 + (24 * (i-29)), 1316));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 43; i < 75; i++) //3
            {
                GameObj.Add(new GameObject(1038 + (24 * (i - 43)), 1060));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 75; i < 150; i++) //4
            {
                GameObj.Add(new GameObject(30 + (24 * (i - 75)), 1773));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 150; i < 218; i++) //5
            {
                GameObj.Add(new GameObject(30, 144 + (24 * (i - 150))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 218; i < 267; i++) //6
            {
                GameObj.Add(new GameObject(714, 144 + (24 * (i - 218))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 267; i < 278; i++) //7
            {
                GameObj.Add(new GameObject(1038, 1060 + (24 * (i - 267))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 278; i < 307; i++) //8
            {
                GameObj.Add(new GameObject(1818, 1060 + (24 * (i - 278))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }

            for (int i = 307; i < 314; i++) //9
            {
                GameObj.Add(new GameObj_IgnoreBullets(30 + (24 * (i - 307)), 780));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 314; i < 321; i++) //10
            {
                GameObj.Add(new GameObj_IgnoreBullets(30 + (24 * (i - 314)), 1020));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 321; i < 334; i++) //11
            {
                GameObj.Add(new GameObj_IgnoreBullets(720 + (24 * (i - 321)), 1344));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 334; i < 347; i++) //12
            {
                GameObj.Add(new GameObj_IgnoreBullets(720 + (24 * (i - 334)), 1584));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 347; i < 422; i++) //13
            {
                GameObj.Add(new GameObj_IgnoreBullets(30 + (24 * (i - 347)), 1728));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 422; i < 432; i++) //14
            {
                GameObj.Add(new GameObj_IgnoreBullets(204, 780 + (24 * (i - 422))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            SpawnEnemy(0, 2, 490, 1360, 260, 1220);
            SpawnEnemy(1, 1, 490, 1360, 260, 1220);

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

            SpawnEnemy(0, 2, 490, 1260, 260, 1020);
            SpawnEnemy(1, 1, 490, 1260, 260, 1020);
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
