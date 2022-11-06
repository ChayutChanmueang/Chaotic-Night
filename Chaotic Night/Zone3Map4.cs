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
    public class Zone3Map4 : GameplayScreen
    {
        public Zone3Map4(Game1 game, EventHandler SEvent) : base(game, SEvent)
        {
            MapTex = game.Content.Load<Texture2D>("Tileset_Zone3_4(1)");
            SpawnPos = new Vector2(1190, 300);
            GameCamera.CamPos = PlayerCha.GetOrigin() - new Vector2(ScreenW / 2, ScreenH / 2);
            SpawnLC(625, 1320);
            SK = new Shopkeeper(1020, 245);
            SK.Load(game.Content, game._spriteBatch, "Hum", 216, 216);
            for (int i = 0; i < 32; i++) //1
            {
                GameObj.Add(new GameObject(30 + (24 * i), 576));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 32; i < 50; i++) //2
            {
                GameObj.Add(new GameObject(798 , 142 + (24 * (i - 32))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 50; i < 83; i++) //3
            {
                GameObj.Add(new GameObject(798 + (24 * (i - 50)), 142));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 83; i < 119; i++) //4
            {
                GameObj.Add(new GameObject(1578, 142 + (24 * (i - 83))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 119; i < 151; i++) //5
            {
                GameObj.Add(new GameObject(810 + (24 * (i - 119)), 1008));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 151; i < 165; i++) //6
            {
                GameObj.Add(new GameObject(810, 1008 + (24 * (i - 151))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 165; i < 182; i++) //7
            {
                GameObj.Add(new GameObject(414 + (24 * (i - 165)), 1344 ));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 182; i < 196; i++) //8
            {
                GameObj.Add(new GameObject(414 , 1008 + (24 * (i - 182))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 196; i < 212; i++) //9
            {
                GameObj.Add(new GameObject(30 + (24 * (i - 196)), 1008));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 212; i < 230; i++) //10
            {
                GameObj.Add(new GameObject(30, 576 + (24 * (i - 212))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }

            SpawnEnemy(0, 2, 1380, 852, 1190, 720);
            SpawnEnemy(1, 1, 1380, 852, 1190, 720);

            Pickup.Add(new SkillPotion(RAND.Next(1190, 1380), RAND.Next(720, 854)));
            Pickup.Add(new HealthPotion(RAND.Next(1190, 1380), RAND.Next(720, 854)));
            LoadCollectable();
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


            SpawnEnemy(0, 2, 1380, 852, 1190, 720);
            SpawnEnemy(1, 1, 1380, 852, 1190, 720);

            Pickup.Add(new SkillPotion(RAND.Next(1190, 1380), RAND.Next(720, 854)));
            Pickup.Add(new HealthPotion(RAND.Next(1190, 1380), RAND.Next(720, 854)));
            LoadCollectable();
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
