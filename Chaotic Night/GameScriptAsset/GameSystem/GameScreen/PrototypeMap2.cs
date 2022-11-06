using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    public class PrototypeMap2 : GameplayScreen
    {
        public PrototypeMap2(Game1 game, EventHandler SEvent) : base(game,SEvent)
        {
            MapTex = game.Content.Load<Texture2D>("Map_Prototype2_New");
            SpawnLC(1740, 612);
            for (int i = 0; i < 36; i++)
            {
                GameObj.Add(new GameObject(50 + (24 * i), 80));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 36; i < 75; i++)
            {
                GameObj.Add(new GameObject(40, 80 + (24 * (i - 36))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 75; i < 150; i++)
            {
                GameObj.Add(new GameObject(64 + (24 * (i - 75)), 1020));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 150; i < 172; i++)
            {
                GameObj.Add(new GameObject(1864, 1020 - (24 * (i - 150))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 172; i < 213; i++)
            {
                GameObj.Add(new GameObject(900+(24*(i-172)), 510));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 213; i < 230; i++)
            {
                GameObj.Add(new GameObject(900, 510-(24*(i-213))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            //Pickup.Add(new HealthPotion(RAND.Next(64, 1804), RAND.Next(534, 900)));
            Pickup.Add(new SkillPotion(RAND.Next(64, 1804), RAND.Next(534, 900)));
            LoadCollectable();
            SpawnEnemy(0,3, 1804, 900, 64, 534);
        }
        public override void Update(GameTime gameTime)
        {
            if(RoomIsReset==false)
            {
                if (LC.GetHitbox().Intersects(PlayerCha.GetHitbox()))
                {
                    if (EnemyAmount <= 0)
                    {
                        if(game.CRS.RandomIsDone==true)
                        {
                            game.CRS.RandomIsDone = false;
                        }
                        ScreenEvent.Invoke(game.CRS, new EventArgs());
                    }
                }
            }
            base.Update(gameTime);
        }
        public override void ResetRoom()
        {
            base.ResetRoom();
            SpawnEnemy(0,3, 1804, 900, 64, 534);
            //Pickup.Add(new HealthPotion(RAND.Next(64, 1804), RAND.Next(534, 900)));
            Pickup.Add(new SkillPotion(RAND.Next(64, 1804), RAND.Next(534, 900)));
            LoadCollectable();
        }
        public override void Reload()
        {
            game.ProtoMap2.ResetRoom();
            game.ProtoMap2.PlayerCha.GetWeapon().ClearBullet();
            game.ProtoMap2.LoadCharacterStats();
            game.ProtoMap2.GameCamera.CamPos = new Vector2(300, 500) - new Vector2(ScreenW / 2, ScreenH / 2);
        }
    }
}

