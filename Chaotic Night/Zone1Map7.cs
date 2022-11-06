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
    public class Zone1Map7 : GameplayScreen
    {
        public Zone1Map7(Game1 game, EventHandler SEvent) : base(game, SEvent)
        {
            MapTex = game.Content.Load<Texture2D>("Tileset_Zone1_7");
            SpawnLC(54, 1140);
            SpawnPos = new Vector2(1435, 400);
            GameCamera.CamPos = PlayerCha.GetOrigin() - new Vector2(ScreenW / 2, ScreenH / 2);
            SK = new Shopkeeper(1350, 205);
            SK.Load(game.Content, game._spriteBatch, "Hum", 216, 216);
            for (int i = 0; i < 18; i++) //1
            {
                GameObj.Add(new GameObject(31 + (24 * i), 862));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 18; i < 48; i++) //2
            {
                GameObj.Add(new GameObject(462, 144 + (24 * (i-18))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 48; i < 97; i++) //3
            {
                GameObj.Add(new GameObject(462 + (24 * (i - 48)), 144));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 97; i < 115; i++) //4
            {
                GameObj.Add(new GameObject(1626 , 144 + (24 * (i - 97))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 115; i < 133; i++) //5
            {
                GameObj.Add(new GameObject(1194 + (24 * (i - 115)), 576));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 133; i < 163; i++) //6
            {
                GameObj.Add(new GameObject(1194 , 576 + (24 * (i - 133))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 163; i < 211; i++) //7
            {
                GameObj.Add(new GameObject(31 + (24 * (i - 163)), 1296));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 211; i < 229; i++) //8
            {
                GameObj.Add(new GameObject(31 , 862 + (24 * (i - 211))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }

            SpawnEnemy(0, 2, 960, 910, 690, 620);
            SpawnEnemy(1, 1, 960, 910, 690, 620);
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


            SpawnEnemy(0, 2, 960, 910, 690, 620);
            SpawnEnemy(1, 1, 960, 910, 690, 620);
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
