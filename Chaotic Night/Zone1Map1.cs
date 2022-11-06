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
    public class Zone1Map1 : GameplayScreen
    {
        public Zone1Map1(Game1 game, EventHandler SEvent) : base(game, SEvent)
        {
            MapTex = game.Content.Load<Texture2D>("Tileset_Zone1_1");
            SpawnLC(511, 936);
            SpawnPos = new Vector2(510, 340);
            GameCamera.CamPos = PlayerCha.GetOrigin() - new Vector2(ScreenW / 2, ScreenH / 2);
            SK = new Shopkeeper(660, 160);
            SK.Load(game.Content, game._spriteBatch, "Hum", 216, 216);
            CanGetReward = false;
            //40 * 35
            for (int i = 0; i < 40; i++)
            {
                GameObj.Add(new GameObject(30 + (24 * i), 141));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 40; i < 75; i++)
            {
                GameObj.Add(new GameObject(990, 141 + (24 * (i-40))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 75; i < 115; i++)
            {
                GameObj.Add(new GameObject(30 + (24 * (i-75)), 960));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 115; i < 150; i++)
            {
                GameObj.Add(new GameObject(30, 141 + (24 * (i-115))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            SpawnEnemy(-1, 1, 1000, 1000, 1000, 1000);
            ShopIsOpen = true;
            game.ShopAvailable = true;
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

            SpawnEnemy(-1, 1, -100, -100, -100, -100);
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
