using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.ViewportAdapters;

namespace Chaotic_Night
{
    public class PrototypeMap1 : GameplayScreen
    {
        TiledMap _tileMap;
        TiledMapRenderer _tileMapRenderer;
        TiledMapObjectLayer _WallTiledObj;

        public PrototypeMap1(Game1 game, EventHandler SEvent) : base(game, SEvent)
        {
            MapTex = game.Content.Load<Texture2D>("Map_Prototype1_New");
            SpawnLC(1740, 612);
            /*_tileMap = game.Content.Load<TiledMap>("ProtoMap_1");
            _tileMapRenderer = new TiledMapRenderer(game.GraphicsDevice, _tileMap);

            foreach(TiledMapObjectLayer layer in _tileMap.ObjectLayers)
            {
                if(layer.Name=="Wall_Object")
                {
                    _WallTiledObj = layer;
                }
            }*/
            /*foreach(TiledMapObject obj in _WallTiledObj.Objects)
            {
                //Point2 position = new Point2(obj.Position.X, obj.Position.Y);
            }*/
            SK = new Shopkeeper(300, 600);
            SK.Load(game.Content, game._spriteBatch, "Hum",216,216);
            for (int i = 0; i < 74; i++)
            {
                GameObj.Add(new GameObject(135 + (24 * i), 250));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 74; i < 107; i++)
            {
                GameObj.Add(new GameObject(1800, 250 + (24 * (i - 74))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 107; i < 181; i++)
            {
                GameObj.Add(new GameObject(135 + (24 * (i - 107)), 944));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 181; i < 215; i++)
            {
                GameObj.Add(new GameObject(100, 250 + (24 * (i - 181))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            SpawnEnemy(0,3, 1740, 824, 124, 274);
            //SpawnEnemy(1, 2, 1740, 824, 124, 274);
            SpawnEnemy(2, 1, 1740, 824, 124, 274);
        }
        public override void Update(GameTime gameTime)
        {
            if(RoomIsReset==false)
            {
                if (LC.GetHitbox().Intersects(PlayerCha.GetHitbox()))
                {
                    if (EnemyAmount <= 0 )
                    {
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
            
            //SpawnEnemy(0,3, 1740, 824, 124, 274);
            //SpawnEnemy(1, 2, 1740, 824, 124, 274);
            SpawnEnemy(3, 1, 1740, 824, 124, 274);
        }
        public override void Reload()
        {
            ResetRoom();
            PlayerCha.GetWeapon().ClearBullet();
            LoadCharacterStats();
            GameCamera.CamPos = new Vector2(300, 500) - new Vector2(ScreenW / 2, ScreenH / 2);
        }
    }
}
