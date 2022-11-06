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
    public class Zone2Map6 : GameplayScreen
    {
        public Zone2Map6(Game1 game, EventHandler SEvent) : base(game, SEvent)
        {
            MapTex = game.Content.Load<Texture2D>("Tileset_Zone2_6");
            SpawnLC(50, 520);
            SpawnPos = new Vector2(2250, 508);
            GameCamera.CamPos = PlayerCha.GetOrigin() - new Vector2(ScreenW / 2, ScreenH / 2);
            SK = new Shopkeeper(1400, 210);
            SK.Load(game.Content, game._spriteBatch, "Hum", 216, 216);
            for (int i = 0; i < 101; i++) //1
            {
                GameObj.Add(new GameObject(24 + (24 * i), 142));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 101; i < 127; i++) //2
            {
                GameObj.Add(new GameObject(2448 , 142 + (24 * (i-101))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 127; i < 228; i++) //3
            {
                GameObj.Add(new GameObject(24 + (24 * (i-127)), 768));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 228; i < 254; i++) //4
            {
                GameObj.Add(new GameObject(24, 142 + (24 * (i - 228))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }

            SpawnEnemy(0, 2, 1540, 580, 980, 440);
            SpawnEnemy(1, 1, 1540, 580, 980, 440);
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


            SpawnEnemy(0, 2, 1540, 580, 980, 440);
            SpawnEnemy(1, 1, 1540, 580, 980, 440);
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
