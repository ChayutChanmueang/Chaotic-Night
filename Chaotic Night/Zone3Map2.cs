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
    public class Zone3Map2 : GameplayScreen
    {
        public Zone3Map2(Game1 game, EventHandler SEvent) : base(game, SEvent)
        {
            MapTex = game.Content.Load<Texture2D>("Tileset_Zone3_2(1)");
            SpawnLC(1008, 1800);
            SpawnPos = new Vector2(140, 215);
            GameCamera.CamPos = PlayerCha.GetOrigin() - new Vector2(ScreenW / 2, ScreenH / 2);
            SK = new Shopkeeper(345, 750);
            SK.Load(game.Content, game._spriteBatch, "Hum", 216, 216);
            for (int i = 0; i < 13; i++) //1
            {
                GameObj.Add(new GameObject(26 + (24 * i), 141));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 13; i < 31; i++)//2
            {
                GameObj.Add(new GameObject(330, 141 + (24 * (i - 13))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 31; i < 55; i++)//3
            {
                GameObj.Add(new GameObject(330+(24*(i-31)), 576));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 55; i < 57; i++)//4
            {
                GameObj.Add(new GameObject(906 , 576 + (24 * (i - 55))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 57; i < 59; i++)//5
            {
                GameObj.Add(new GameObject(906 + (24 * (i - 57)), 642));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 59; i < 60; i++)//6
            {
                GameObj.Add(new GameObject(954 , 576 + (24 * (i - 59))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 60; i < 62; i++)//7
            {
                GameObj.Add(new GameObject(954 + (24 * (i - 60)), 672 ));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 62; i < 74; i++)//8
            {
                GameObj.Add(new GameObject(1008, 672 + (24 * (i - 62))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 74; i < 80; i++)//9
            {
                GameObj.Add(new GameObject(1008 + (24 * (i - 74)), 960));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 80; i < 116; i++)//10
            {
                GameObj.Add(new GameObject(1152 , 960 + (24 * (i - 80))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 116; i < 129; i++)//11
            {
                GameObj.Add(new GameObject(840 + (24 * (i - 116)), 1824));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 129; i < 147; i++)//12
            {
                GameObj.Add(new GameObject(840, 1392 + (24 * (i - 129))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 147; i < 171; i++)//13
            {
                GameObj.Add(new GameObject(264 + (24 * (i - 147)), 1392));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 171; i < 173; i++)//14
            {
                GameObj.Add(new GameObject(264, 1344 + (24 * (i - 171))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 173; i < 175; i++)//15
            {
                GameObj.Add(new GameObject(216 + (24 * (i - 173)), 1344));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 175; i < 177; i++)//16
            {
                GameObj.Add(new GameObject(216 , 1296 + (24 * (i - 175))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 177; i < 179; i++)//17
            {
                GameObj.Add(new GameObject(166 + (24 * (i - 177)), 1296));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 179; i < 189; i++)//18
            {
                GameObj.Add(new GameObject(166, 1032 + (24 * (i - 177))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 189; i < 194; i++)//19
            {
                GameObj.Add(new GameObject(26 + (24 * (i - 189)), 1056));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 194; i < 232; i++)//20
            {
                GameObj.Add(new GameObject(26, 144 + (24 * (i - 194))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }



            SpawnEnemy(0, 2, 820, 1120, 750, 772);
            SpawnEnemy(1, 1, 820, 1120, 750, 772);

            Pickup.Add(new SkillPotion(RAND.Next(750, 820), RAND.Next(772, 1120)));
            Pickup.Add(new HealthPotion(RAND.Next(750, 820), RAND.Next(772, 1120)));
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


            SpawnEnemy(0, 2, 820, 1120, 750, 772);
            SpawnEnemy(1, 1, 820, 1120, 750, 772);

            Pickup.Add(new SkillPotion(RAND.Next(750, 820), RAND.Next(772, 1120)));
            Pickup.Add(new HealthPotion(RAND.Next(750, 820), RAND.Next(772, 1120)));
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
