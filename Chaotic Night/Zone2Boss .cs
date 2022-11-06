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
    public class Zone2Boss : GameplayScreen
    {
        public Zone2Boss(Game1 game, EventHandler SEvent) : base(game, SEvent)
        {
            MapTex = game.Content.Load<Texture2D>("Tileset_Zone2_13");
            SpawnLC(902, 2078);
            SpawnPos = new Vector2(900, 335);
            GameCamera.CamPos = PlayerCha.GetOrigin() - new Vector2(ScreenW / 2, ScreenH / 2);
            for (int i = 0; i < 73; i++) //1
            {
                GameObj.Add(new GameObject(30 + (24 * i), 192));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 73; i < 146; i++) //2
            {
                GameObj.Add(new GameObject(30 + (24 * (i-73)), 2112));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 146; i < 226; i++) //3
            {
                GameObj.Add(new GameObject(30 , 192 + (24 * (i-146))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 226; i < 306; i++) //4
            {
                GameObj.Add(new GameObject(1770, 192 + (24 * (i - 226))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }

            for (int i = 306; i < 321; i++) //5
            {
                GameObj.Add(new GameObj_IgnoreBullets(730, 192 + (24 * (i - 306))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 321; i < 336; i++) //6
            {
                GameObj.Add(new GameObj_IgnoreBullets(1070, 192 + (24 * (i - 321))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 336; i < 348; i++) //7
            {
                GameObj.Add(new GameObj_IgnoreBullets(826, 540 + (24 * (i - 336))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 348; i < 360; i++) //8
            {
                GameObj.Add(new GameObj_IgnoreBullets(974, 540 + (24 * (i - 348))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 360; i < 372; i++) //9
            {
                GameObj.Add(new GameObj_IgnoreBullets(826, 1644 + (24 * (i - 360))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 372; i < 384; i++) //10
            {
                GameObj.Add(new GameObj_IgnoreBullets(974, 1644 + (24 * (i - 372))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 384; i < 392; i++) //11
            {
                GameObj.Add(new GameObj_IgnoreBullets(730, 1930 + (24 * (i - 384))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 392; i < 400; i++) //12
            {
                GameObj.Add(new GameObj_IgnoreBullets(1070, 1930 + (24 * (i - 392))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 400; i < 404; i++) //13
            {
                GameObj.Add(new GameObj_IgnoreBullets(586, 826 + (24 * (i - 400))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 404; i < 408; i++) //14
            {
                GameObj.Add(new GameObj_IgnoreBullets(1214, 826 + (24 * (i - 404))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 408; i < 412; i++) //15
            {
                GameObj.Add(new GameObj_IgnoreBullets(586, 1546 + (24 * (i - 408))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 412; i < 416; i++) //16
            {
                GameObj.Add(new GameObj_IgnoreBullets(1214, 1546 + (24 * (i - 412))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 416; i < 422; i++) //17
            {
                GameObj.Add(new GameObj_IgnoreBullets(490, 922 + (24 * (i - 416))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 422; i < 428; i++) //18
            {
                GameObj.Add(new GameObj_IgnoreBullets(1310, 922 + (24 * (i - 422))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 428; i < 432; i++) //19
            {
                GameObj.Add(new GameObj_IgnoreBullets(230, 946 + (24 * (i - 428))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 432; i < 436; i++) //20
            {
                GameObj.Add(new GameObj_IgnoreBullets(1594, 946 + (24 * (i - 428))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 436; i < 440; i++) //21
            {
                GameObj.Add(new GameObj_IgnoreBullets(230, 1296 + (24 * (i - 428))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 440; i < 444; i++) //22
            {
                GameObj.Add(new GameObj_IgnoreBullets(1594, 1296 + (24 * (i - 428))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 444; i < 454; i++) //23
            {
                GameObj.Add(new GameObj_IgnoreBullets(490, 1296 + (24 * (i - 444))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 454; i < 464; i++) //24
            {
                GameObj.Add(new GameObj_IgnoreBullets(1310, 1296 + (24 * (i - 454))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 464; i < 468; i++) //25
            {
                GameObj.Add(new GameObj_IgnoreBullets(730 + (24 * (i - 464)), 540 ));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 468; i < 472; i++) //26
            {
                GameObj.Add(new GameObj_IgnoreBullets(974 + (24 * (i - 468)), 540));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 472; i < 476; i++) //27
            {
                GameObj.Add(new GameObj_IgnoreBullets(730 + (24 * (i - 472)), 1930));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 476; i < 480; i++) //28
            {
                GameObj.Add(new GameObj_IgnoreBullets(974 + (24 * (i - 476)), 1930));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 480; i < 490; i++) //29
            {
                GameObj.Add(new GameObj_IgnoreBullets(586 + (24 * (i - 480)), 826));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 490; i < 500; i++) //30
            {
                GameObj.Add(new GameObj_IgnoreBullets(974 + (24 * (i - 490)), 826));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 500; i < 510; i++) //31
            {
                GameObj.Add(new GameObj_IgnoreBullets(586 + (24 * (i - 500)), 1644));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 510; i < 520; i++) //32
            {
                GameObj.Add(new GameObj_IgnoreBullets(974 + (24 * (i - 510)), 1644));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 520; i < 524; i++) //33
            {
                GameObj.Add(new GameObj_IgnoreBullets(490 + (24 * (i - 520)), 922));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 524; i < 528; i++) //34
            {
                GameObj.Add(new GameObj_IgnoreBullets(1214 + (24 * (i - 524)), 922));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 528; i < 532; i++) //35
            {
                GameObj.Add(new GameObj_IgnoreBullets(490 + (24 * (i - 528)), 1548));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 532; i < 536; i++) //36
            {
                GameObj.Add(new GameObj_IgnoreBullets(1214 + (24 * (i - 532)), 1548));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 536; i < 546; i++) //37
            {
                GameObj.Add(new GameObj_IgnoreBullets(230 + (24 * (i - 536)), 1056));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 546; i < 556; i++) //38
            {
                GameObj.Add(new GameObj_IgnoreBullets(1310 + (24 * (i - 546)), 1056));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 556; i < 566; i++) //39
            {
                GameObj.Add(new GameObj_IgnoreBullets(230 + (24 * (i - 556)), 1296));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 566; i < 576; i++) //40
            {
                GameObj.Add(new GameObj_IgnoreBullets(1310 + (24 * (i - 566)), 1296));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 576; i < 584; i++) //41
            {
                GameObj.Add(new GameObj_IgnoreBullets(30 + (24 * (i - 576)), 946));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 584; i < 592; i++) //42
            {
                GameObj.Add(new GameObj_IgnoreBullets(1594 + (24 * (i - 584)), 946));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 592; i < 600; i++) //43
            {
                GameObj.Add(new GameObj_IgnoreBullets(30 + (24 * (i - 592)), 1404));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 600; i < 608; i++) //44
            {
                GameObj.Add(new GameObj_IgnoreBullets(1594 + (24 * (i - 600)), 1404));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            SpawnEnemy(-1, 1, 1000, 1000, 1000, 1000);
            //SpawnEnemy(3, 1, 912,1080 , 912, 1080);
            SpawnEnemy(2, 1, 970, 1265, 855, 1080);
        }
        public override void Update(GameTime gameTime)
        {
            if (RoomIsReset == false)
            {
                if (LC.GetHitbox().Intersects(PlayerCha.GetHitbox()))
                {
                    if (EnemyAmount <= 0)
                    {
                        game.Room = 0;
                        game.Stage = 3;
                        game.ShopAvailable = true;
                        game.ShopSpawnRate = 5;
                        ScreenEvent.Invoke(game.LoadingScreen, new EventArgs());
                    }
                }
            }
            if (game.NextLevel != game.Z3M1)
            {
                game.NextLevel = game.Z3M1;
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

            SpawnEnemy(-1, 1, 1000, 1000, 1000, 1000);
            //SpawnEnemy(3, 1, 912,1080 , 912, 1080);
            SpawnEnemy(2, 1, 970, 1265, 855, 1080);
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
