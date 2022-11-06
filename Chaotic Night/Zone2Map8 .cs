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
    public class Zone2Map8 : GameplayScreen
    {
        public Zone2Map8(Game1 game, EventHandler SEvent) : base(game, SEvent)
        {
            MapTex = game.Content.Load<Texture2D>("Tileset_Zone2_8");
            SpawnLC(1692, 578);
            SpawnPos = new Vector2(60, 455);
            GameCamera.CamPos = PlayerCha.GetOrigin() - new Vector2(ScreenW / 2, ScreenH / 2);
            CanGetReward = false;
            for (int i = 0; i < 71; i++) //1
            {
                GameObj.Add(new GameObject(24 + (24 * i), 44));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 71; i < 99; i++) //2
            {
                GameObj.Add(new GameObject(1722, 44 + (24 * (i-71))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 99; i < 170; i++) //3
            {
                GameObj.Add(new GameObject(24 + (24 * (i-99)), 720));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 170; i < 198; i++) //4
            {
                GameObj.Add(new GameObject(24 , 44 + (24 * (i - 170))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 198; i < 208; i++) //5
            {
                GameObj.Add(new GameObj_IgnoreBullets(24 + (24 * (i - 198)), 226));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 208; i < 212; i++) //6
            {
                GameObj.Add(new GameObj_IgnoreBullets(254, 226 + (24 * (i - 208))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 212; i < 229; i++) //7
            {
                GameObj.Add(new GameObj_IgnoreBullets(254 + (24 * (i - 212)), 336 ));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 229; i < 233; i++) //8
            {
                GameObj.Add(new GameObj_IgnoreBullets(682 , 226 + (24 * (i - 229))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 233; i < 249; i++) //9
            {
                GameObj.Add(new GameObj_IgnoreBullets(682 + (24 * (i - 233)), 229 ));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 249; i < 253; i++) //10
            {
                GameObj.Add(new GameObj_IgnoreBullets(1070, 226 + (24 * (i - 249))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 253; i < 270; i++) //11
            {
                GameObj.Add(new GameObj_IgnoreBullets(1070 + (24 * (i - 253)), 336));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 270; i < 274; i++) //12
            {
                GameObj.Add(new GameObj_IgnoreBullets(1498 , 226 + (24 * (i - 270))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 274; i < 284; i++) //13
            {
                GameObj.Add(new GameObj_IgnoreBullets(1498 + (24 * (i - 274)), 229));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 284; i < 294; i++) //14
            {
                GameObj.Add(new GameObj_IgnoreBullets(24 + (24 * (i - 284)), 684));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 294; i < 298; i++) //15 
            {
                GameObj.Add(new GameObj_IgnoreBullets(254, 576 + (24 * (i - 294))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 298; i < 315; i++) //16 *
            {
                GameObj.Add(new GameObj_IgnoreBullets(254 + (24 * (i - 298)), 576));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 315; i < 319; i++) //17 
            {
                GameObj.Add(new GameObj_IgnoreBullets(682, 576 + (24 * (i - 315))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 319; i < 335; i++) //18 *
            {
                GameObj.Add(new GameObj_IgnoreBullets(682 + (24 * (i - 319)), 684));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 335; i < 339; i++) //19 
            {
                GameObj.Add(new GameObj_IgnoreBullets(1070, 576 + (24 * (i - 335))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 339; i < 356; i++) //20 *
            {
                GameObj.Add(new GameObj_IgnoreBullets(1070 + (24 * (i - 339)), 576));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 356; i < 360; i++) //21
            {
                GameObj.Add(new GameObj_IgnoreBullets(1498, 576 + (24 * (i - 336))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 360; i < 370; i++) //21
            {
                GameObj.Add(new GameObj_IgnoreBullets(1498 + (24 * (i - 360)), 684));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            BS.Add(new BulletShooter(new Vector2(360, 75), 2, 1.57f, game));
            BS.Add(new BulletShooter(new Vector2(408, 75), 2, 1.57f, game));
            BS.Add(new BulletShooter(new Vector2(458, 75), 5, 1.57f, game));
            BS.Add(new BulletShooter(new Vector2(504, 75), 5, 1.57f, game));
            BS.Add(new BulletShooter(new Vector2(552, 75), 2, 1.57f, game));
            BS.Add(new BulletShooter(new Vector2(600, 75), 2, 1.57f, game));
            BS.Add(new BulletShooter(new Vector2(648, 75), 5, 1.57f, game));
            BS.Add(new BulletShooter(new Vector2(696, 75), 5, 1.57f, game));

            BS.Add(new BulletShooter(new Vector2(1166, 75), 2, 1.57f, game));
            BS.Add(new BulletShooter(new Vector2(1214, 75), 2, 1.57f, game));
            BS.Add(new BulletShooter(new Vector2(1262, 75), 5, 1.57f, game));
            BS.Add(new BulletShooter(new Vector2(1310, 75), 5, 1.57f, game));
            BS.Add(new BulletShooter(new Vector2(1358, 75), 2, 1.57f, game));
            BS.Add(new BulletShooter(new Vector2(1406, 75), 2, 1.57f, game));
            BS.Add(new BulletShooter(new Vector2(1458, 75), 5, 1.57f, game));
            BS.Add(new BulletShooter(new Vector2(1502, 75), 5, 1.57f, game));

            SpawnEnemy(-1, 1, 1000, 1000, 1000, 1000);
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
            foreach (BulletShooter i in BS)
            {
                i.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                foreach (Bullet j in i.Bullets)
                {
                    if (j.Hitbox.Intersects(PlayerCha.GetHitbox()))
                    {
                        PlayerCha.SubtraceHP(j.Damage);
                        i.Bullets.Remove(j);
                        break;
                    }
                }
            }
            foreach (GameObject j in GameObj)
            {
                foreach(BulletShooter k in BS)
                {
                    foreach (Bullet i in k.Bullets)
                    {
                        if (i.Hitbox.Intersects(j.GetHitbox()))
                        {
                            if (j.GetType() != typeof(GameObj_IgnoreBullets))
                            {
                                k.Bullets.Remove(i);
                                break;
                            }
                        }
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
            foreach (BulletShooter i in BS)
            {
                foreach(Bullet j in i.Bullets)
                {
                    j.Draw(_spriteBatch, GameCamera.GetCamPos());
                }

            }
        }
        public override void ResetRoom()
        {
            base.ResetRoom();
            foreach(BulletShooter i in BS)
            {
                i.ClearBullet();
            }

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
