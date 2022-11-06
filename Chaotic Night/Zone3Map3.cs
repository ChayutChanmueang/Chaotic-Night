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
    public class Zone3Map3 : GameplayScreen
    {
        public Zone3Map3(Game1 game, EventHandler SEvent) : base(game, SEvent)
        {
            MapTex = game.Content.Load<Texture2D>("Tileset_Zone3_3(1)");
            SpawnPos = new Vector2(66, 404);
            GameCamera.CamPos = PlayerCha.GetOrigin() - new Vector2(ScreenW / 2, ScreenH / 2);
            SpawnLC(1698, 1094);
            SK = new Shopkeeper(515, 615);
            SK.Load(game.Content, game._spriteBatch, "Hum", 216, 216);
            for (int i = 0; i < 35; i++) //1
            {
                GameObj.Add(new GameObject(24 + (24 * i), 142));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 35; i < 41; i++) //2
            {
                GameObj.Add(new GameObject(24 , 142 + (24 * (i - 35))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 41; i < 55; i++) //3
            {
                GameObj.Add(new GameObject(864 + (24 * (i - 41)), 286));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 55; i < 57; i++) //4
            {
                GameObj.Add(new GameObject(1200, 286 + (24 * (i - 55))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 57; i < 58; i++) //5
            {
                GameObj.Add(new GameObject(1200 + (24 * (i - 57)), 336));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 58; i < 60; i++) //6
            {
                GameObj.Add(new GameObject(1242 , 336 + (24 * (i - 58))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 60; i < 62; i++) //7
            {
                GameObj.Add(new GameObject(1242 + (24 * (i - 60)), 384));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 62; i < 80; i++) //8
            {
                GameObj.Add(new GameObject(1290, 384 + (24 * (i - 62))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 80; i < 98; i++) //9
            {
                GameObj.Add(new GameObject(1290 + (24 * (i - 80)), 816));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 98; i < 116; i++) //10
            {
                GameObj.Add(new GameObject(1722, 816 + (24 * (i - 98))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 116; i < 151; i++) //11
            {
                GameObj.Add(new GameObject(890 + (24 * (i - 116)), 1248));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 151; i < 157; i++) //12
            {
                GameObj.Add(new GameObject(890, 1104 + (24 * (i - 151))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 157; i < 171; i++) //13
            {
                GameObj.Add(new GameObject(552 + (24 * (i - 157)), 1104));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 171; i < 173; i++) //14
            {
                GameObj.Add(new GameObject(552 , 1056 + (24 * (i - 171))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 173; i < 175; i++) //15
            {
                GameObj.Add(new GameObject(504 + (24 * (i - 173)), 1056));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 175; i < 177; i++) //16
            {
                GameObj.Add(new GameObject(504 , 1008 + (24 * (i - 175))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 177; i < 179; i++) //17
            {
                GameObj.Add(new GameObject(456 + (24 * (i - 177)), 1008 ));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 179; i < 197; i++) //18
            {
                GameObj.Add(new GameObject(456, 576 + (24 * (i - 179))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 197; i < 215; i++) //19
            {
                GameObj.Add(new GameObject(24 + (24 * (i - 197)), 576));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 215; i < 233; i++) //20
            {
                GameObj.Add(new GameObject(24 , 142 + (24 * (i - 215))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }

            SpawnEnemy(0, 2, 1090, 900, 680, 620);
            SpawnEnemy(1, 1, 1090, 900, 680, 620);

            Pickup.Add(new SkillPotion(RAND.Next(680, 1090), RAND.Next(620, 1090)));
            Pickup.Add(new HealthPotion(RAND.Next(680, 1090), RAND.Next(620, 1090)));
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


            SpawnEnemy(0, 2, 1090, 900, 680, 620);
            SpawnEnemy(1, 1, 1090, 900, 680, 620);

            Pickup.Add(new SkillPotion(RAND.Next(680, 1090), RAND.Next(620, 1090)));
            Pickup.Add(new HealthPotion(RAND.Next(680, 1090), RAND.Next(620, 1090)));
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
