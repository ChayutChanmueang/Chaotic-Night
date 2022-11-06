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
    public class Zone1Map5 : GameplayScreen
    {
        public Zone1Map5(Game1 game, EventHandler SEvent) : base(game, SEvent)
        {
            MapTex = game.Content.Load<Texture2D>("Tileset_Zone1_5");
            SpawnPos = new Vector2(898, 290);
            GameCamera.CamPos = PlayerCha.GetOrigin() - new Vector2(ScreenW / 2, ScreenH / 2);
            SpawnLC(228, 1560);
            SK = new Shopkeeper(630, 520);
            SK.Load(game.Content, game._spriteBatch, "Hum", 216, 216);
            for (int i = 0; i < 28; i++) //1
            {
                GameObj.Add(new GameObject(30 + (24 * i), 478));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 28; i < 42; i++) //2
            {
                GameObj.Add(new GameObject(702, 142 + (24 * (i - 28))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 42; i < 59; i++) //3
            {
                GameObj.Add(new GameObject(702 + (24 * (i - 42)), 142));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 59; i < 102; i++) //4
            {
                GameObj.Add(new GameObject(1098 , 142 + (24 * (i - 59))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 102; i < 130; i++) //5
            {
                GameObj.Add(new GameObject(432 + (24 * (i - 102)), 1152 ));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 130; i < 148; i++) //6
            {
                GameObj.Add(new GameObject(432 , 1152 + (24 * (i - 130))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 148; i < 165; i++) //7
            {
                GameObj.Add(new GameObject(30 + (24 * (i - 148)), 1584));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 165; i < 211; i++) //8
            {
                GameObj.Add(new GameObject(30 , 478 + (24 * (i - 165))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }

            SpawnEnemy(0, 2, 950, 952, 54, 704);
            SpawnEnemy(1, 1, 950, 952, 54, 704);

            Pickup.Add(new SkillPotion(RAND.Next(54, 950), RAND.Next(704, 952)));
            Pickup.Add(new HealthPotion(RAND.Next(54, 950), RAND.Next(704, 952)));
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


            SpawnEnemy(0, 2, 950, 1152, 54, 704);
            SpawnEnemy(1, 1, 950, 1152, 54, 704);

            Pickup.Add(new SkillPotion(RAND.Next(54, 950), RAND.Next(704, 952)));
            Pickup.Add(new HealthPotion(RAND.Next(54, 950), RAND.Next(704, 952)));
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
