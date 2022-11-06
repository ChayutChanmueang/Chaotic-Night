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
    public class Zone1Map9 : GameplayScreen
    {
        public Zone1Map9(Game1 game, EventHandler SEvent) : base(game, SEvent)
        {
            MapTex = game.Content.Load<Texture2D>("Tileset_Zone1_9");
            SpawnLC(280, 1855);
            SpawnPos = new Vector2(230, 290);
            GameCamera.CamPos = PlayerCha.GetOrigin() - new Vector2(ScreenW / 2, ScreenH / 2);
            CanGetReward = false;
            for (int i = 0; i < 21; i++) //1
            {
                GameObj.Add(new GameObject(30 + (24 * i), 144));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 21; i < 93; i++) //2
            {
                GameObj.Add(new GameObject(522 , 144 + (24 * (i-21))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 93; i < 114; i++) //3
            {
                GameObj.Add(new GameObject(30 + (24 * (i-93)), 1872));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 114; i < 186; i++) //4
            {
                GameObj.Add(new GameObject(30, 144 + (24 * (i - 114))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 186; i < 258; i++) //5
            {
                GameObj.Add(new GameObj_IgnoreBullets(106 , 144 + (24 * (i - 186))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 258; i < 330; i++) //6
            {
                GameObj.Add(new GameObj_IgnoreBullets(445, 144 + (24 * (i - 258))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 330; i < 334; i++) //7
            {
                GameObj.Add(new GameObj_IgnoreBullets(106 + (24 * (i - 330)), 444));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 334; i < 338; i++) //8
            {
                GameObj.Add(new GameObj_IgnoreBullets(350 + (24 * (i - 334)), 444));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 338; i < 342; i++) //9
            {
                GameObj.Add(new GameObj_IgnoreBullets(106 + (24 * (i - 338)), 874));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 342; i < 346; i++) //10
            {
                GameObj.Add(new GameObj_IgnoreBullets(350 + (24 * (i - 342)), 874));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 346; i < 350; i++) //11
            {
                GameObj.Add(new GameObj_IgnoreBullets(106 + (24 * (i - 346)), 1260));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 350; i < 354; i++) //12
            {
                GameObj.Add(new GameObj_IgnoreBullets(350 + (24 * (i - 350)), 1260));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 354; i < 358; i++) //13
            {
                GameObj.Add(new GameObj_IgnoreBullets(106 + (24 * (i - 354)), 1690));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 358; i < 362; i++) //14
            {
                GameObj.Add(new GameObj_IgnoreBullets(350 + (24 * (i - 358)), 1690));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 362; i < 380; i++) //15
            {
                GameObj.Add(new GameObj_IgnoreBullets(202 , 444 + (24 * (i - 362))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 380; i < 398; i++) //16
            {
                GameObj.Add(new GameObj_IgnoreBullets(350, 444 + (24 * (i - 380))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 398; i < 416; i++) //17
            {
                GameObj.Add(new GameObj_IgnoreBullets(202, 1260 + (24 * (i - 398))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 416; i < 434; i++) //18
            {
                GameObj.Add(new GameObj_IgnoreBullets(350, 1260 + (24 * (i - 416))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }

            BS.Add(new BulletShooter(new Vector2(500, 576), 2, 3.14f, game));
            BS.Add(new BulletShooter(new Vector2(500, 576+(48)), 2, 3.14f, game));
            BS.Add(new BulletShooter(new Vector2(500, 576 + (48*2)), 2, 3.14f, game));
            BS.Add(new BulletShooter(new Vector2(500, 576 + (48*3)), 2, 3.14f, game));
            BS.Add(new BulletShooter(new Vector2(500, 576 + (48*4)), 2, 3.14f, game));
            BS.Add(new BulletShooter(new Vector2(500, 576 + (48*5)), 2, 3.14f, game));
            BS.Add(new BulletShooter(new Vector2(500, 576 + (48*6)), 2, 3.14f, game));
            BS.Add(new BulletShooter(new Vector2(500, 576+(48*7)), 2, 3.14f, game));

            BS.Add(new BulletShooter(new Vector2(500, 1391), 2, 3.14f, game));
            BS.Add(new BulletShooter(new Vector2(500, 1391 + (48)), 2, 3.14f, game));
            BS.Add(new BulletShooter(new Vector2(500, 1391 + (48 * 2)), 2, 3.14f, game));
            BS.Add(new BulletShooter(new Vector2(500, 1391 + (48 * 3)), 2, 3.14f, game));
            BS.Add(new BulletShooter(new Vector2(500, 1391 + (48 * 4)), 2, 3.14f, game));
            BS.Add(new BulletShooter(new Vector2(500, 1391 + (48 * 5)), 2, 3.14f, game));
            BS.Add(new BulletShooter(new Vector2(500, 1391 + (48 * 6)), 2, 3.14f, game));
            BS.Add(new BulletShooter(new Vector2(500, 1391 + (48 * 7)), 2, 3.14f, game));

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
                foreach (BulletShooter k in BS)
                {
                    foreach (Bullet i in k.Bullets)
                    {
                        if (i.Hitbox.Intersects(j.GetHitbox()))
                        {
                            if (j.GetType() != typeof(GameObj_IgnoreBullets))
                            {
                                //k.Bullets.Remove(i);
                                //break;
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
                foreach (Bullet j in i.Bullets)
                {
                    j.Draw(_spriteBatch, GameCamera.GetCamPos());
                }

            }
        }
        public override void ResetRoom()
        {
            base.ResetRoom();
            foreach (BulletShooter i in BS)
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
