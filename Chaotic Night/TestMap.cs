using System;
using System.Collections.Generic;
using Roy_T.AStar.Grids;
using Roy_T.AStar.Primitives;
using Roy_T.AStar.Paths;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    public class TestMap : Screen
    {
        SpriteFont font;
        PlayableCharacter PlayerCha;
        List<Enemy> Enemies = new List<Enemy>();
        private Texture2D HitboxTest;
        private KeyboardState PlayerKeyboard;
        private MouseState PlayerMouse;
        private Vector2 MousePos;
        private Vector2 dPos;
        private float WeaponRot;
        float elapsed;
        List<GameObject> GameObj = new List<GameObject>();
        Camera GameCamera;
        Game1 game;
        /*
         World To Screen = ObjPos-CamPos
         Screen To World = ObjPos-CamPos
         */
        Random RAND;
        //A-Star
        Grid _Grid;
        GridSize GridS;
        Size CellSize;
        Velocity TraversalVelocity = Velocity.FromKilometersPerHour(1);


        //Debug
        private bool Debuging = false;
        public static int ScreenW;
        public static int ScreenH;
        Character FollowedCharacter = null;
        public bool ShowGrid = false;
        public bool CamFollowEnemy = false;
        int EnemyNum = 1;
        public TestMap(Game1 game,EventHandler SEvent) : base(SEvent)
        {
            //set game's screen
            ScreenW = game._graphics.PreferredBackBufferWidth;
            ScreenH = game._graphics.PreferredBackBufferHeight;
            this.game = game;
            RAND = new Random();

            GameCamera = new Camera(ScreenW, ScreenH);
            PlayerCha = new PlayableCharacter(new Vector2(48, 48));
            PlayerCha.Load(game.Content, game._spriteBatch);
            font = game.Content.Load<SpriteFont>("BM_Space_8");
            HitboxTest = game.Content.Load<Texture2D>("Player-Placeholder");
            // TODO: use this.Content to load your game content here
            for (int i = 0; i < 5; i++)
            {
                GameObj.Add(new GameObject(480 + (32 * i), 480));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 5; i < 10; i++)
            {
                GameObj.Add(new GameObject(480 - 32, 480 + (32 * (i - 4))));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 10; i < 15; i++)
            {
                GameObj.Add(new GameObject(480 + (32 * (i - 10)), 480 + (32 * 6)));
                GameObj[i].Load(game.Content, game._spriteBatch);
            }
            for (int i = 0; i < 3; i++)
            {
                Enemies.Add(new Enemy(new Vector2(RAND.Next(0, 1920))));
                Enemies[i].Load(game.Content, game._spriteBatch);
            }
        }
        public override void Update(GameTime gameTime)
        {
            PlayerKeyboard = Keyboard.GetState();
            PlayerMouse = Mouse.GetState();
            #region Player Movement
            int HitDisX = 10;
            int HitDisY = 10;
            if (PlayerKeyboard.IsKeyDown(Keys.W))
            {
                foreach (GameObject Wall in GameObj)
                {
                    if (PlayerCha.ColisionCheck(Wall.GetHitbox()) == false)
                    {
                        if (PlayerCha.GetHitbox().Top - PlayerCha.GetSpeed() < Wall.GetHitbox().Bottom &&
                            PlayerCha.GetHitbox().Bottom > Wall.GetHitbox().Bottom &&
                            PlayerCha.GetHitbox().Right > Wall.GetHitbox().Left &&
                            PlayerCha.GetHitbox().Left < Wall.GetHitbox().Right)
                        {
                            if (PlayerCha.GetHitbox().Top - Wall.GetHitbox().Bottom < HitDisY)
                            {
                                HitDisY = PlayerCha.GetHitbox().Top - Wall.GetHitbox().Bottom;
                            }
                        }
                    }
                }
                if (HitDisY < PlayerCha.GetSpeed())
                {
                    PlayerCha.MoveUp(HitDisY);
                }
                else
                {
                    PlayerCha.MoveUp();
                }
            }
            else if (PlayerKeyboard.IsKeyUp(Keys.W))
            {
                HitDisY = 10;
            }
            if (PlayerKeyboard.IsKeyDown(Keys.S))
            {
                foreach (GameObject Wall in GameObj)
                {
                    if (PlayerCha.ColisionCheck(Wall.GetHitbox()) == false)
                    {
                        if (PlayerCha.GetHitbox().Bottom + PlayerCha.GetSpeed() > Wall.GetHitbox().Top &&
                            PlayerCha.GetHitbox().Top < Wall.GetHitbox().Top &&
                            PlayerCha.GetHitbox().Right > Wall.GetHitbox().Left &&
                            PlayerCha.GetHitbox().Left < Wall.GetHitbox().Right)
                        {
                            if (Wall.GetHitbox().Top - PlayerCha.GetHitbox().Bottom < HitDisY)
                            {
                                HitDisY = Wall.GetHitbox().Top - PlayerCha.GetHitbox().Bottom;
                            }
                        }
                    }
                }
                if (HitDisY < PlayerCha.GetSpeed())
                {
                    PlayerCha.MoveDown(HitDisY);
                }
                else
                {
                    PlayerCha.MoveDown();
                }
            }
            else if (PlayerKeyboard.IsKeyUp(Keys.S))
            {
                HitDisY = 10;
            }
            if (PlayerKeyboard.IsKeyDown(Keys.A))
            {
                foreach (GameObject Wall in GameObj)
                {
                    if (PlayerCha.ColisionCheck(Wall.GetHitbox()) == false)
                    {
                        if (PlayerCha.GetHitbox().Left - PlayerCha.GetSpeed() < Wall.GetHitbox().Right &&
                            PlayerCha.GetHitbox().Right > Wall.GetHitbox().Right &&
                            PlayerCha.GetHitbox().Bottom > Wall.GetHitbox().Top &&
                            PlayerCha.GetHitbox().Top < Wall.GetHitbox().Bottom)
                        {

                            if (PlayerCha.GetHitbox().Left - Wall.GetHitbox().Right < HitDisX)
                            {
                                HitDisX = PlayerCha.GetHitbox().Left - Wall.GetHitbox().Right;
                            }
                        }
                    }
                }
                if (HitDisX < PlayerCha.GetSpeed())
                {
                    PlayerCha.MoveLeft(HitDisX);
                }
                else
                {
                    PlayerCha.MoveLeft();
                }

            }
            else if (PlayerKeyboard.IsKeyUp(Keys.A))
            {
                HitDisX = 10;
            }
            if (PlayerKeyboard.IsKeyDown(Keys.D))
            {
                foreach (GameObject Wall in GameObj)
                {
                    if (PlayerCha.ColisionCheck(Wall.GetHitbox()) == false)
                    {
                        if (PlayerCha.GetHitbox().Right + PlayerCha.GetSpeed() > Wall.GetHitbox().Left &&
                            PlayerCha.GetHitbox().Left < Wall.GetHitbox().Left &&
                            PlayerCha.GetHitbox().Bottom > Wall.GetHitbox().Top &&
                            PlayerCha.GetHitbox().Top < Wall.GetHitbox().Bottom)
                        {
                            if (Wall.GetHitbox().Left - PlayerCha.GetHitbox().Right < HitDisX)
                            {
                                HitDisX = Wall.GetHitbox().Left - PlayerCha.GetHitbox().Right;
                            }
                        }
                    }
                }
                if (HitDisX < PlayerCha.GetSpeed())
                {
                    PlayerCha.MoveRight(HitDisX);
                }
                else
                {
                    PlayerCha.MoveRight();
                }
            }
            else if (PlayerKeyboard.IsKeyUp(Keys.D))
            {
                HitDisX = 10;
            }
            #endregion
            #region Player Attack
            if (PlayerMouse.LeftButton != ButtonState.Pressed && PlayerMouse.RightButton != ButtonState.Pressed && PlayerCha.GetAttacking() == false)
            {
                MousePos = new Vector2(PlayerMouse.X, PlayerMouse.Y);
                dPos = MousePos - (PlayerCha.GetOrigin() - GameCamera.GetCamPos());
                WeaponRot = (float)Math.Atan2(dPos.Y, dPos.X);
            }
            else if (PlayerMouse.LeftButton == ButtonState.Pressed)
            {
                foreach (Enemy enemy in Enemies)
                {
                    if (PlayerCha.GetWeapon().GetType() == typeof(RangeWeapons))
                    {
                        PlayerCha.DoM1Action(enemy);
                        break;
                    }
                    else
                    {
                        PlayerCha.DoM1Action(enemy);
                    }
                }
                PlayerCha.GetWeapon().Attacking = true;
            }
            else if (PlayerMouse.RightButton == ButtonState.Pressed)
            {
                if (PlayerCha.GetSP() > 0)
                {
                    foreach (Enemy enemy in Enemies)
                    {
                        PlayerCha.DoM2Action(enemy);
                    }
                    PlayerCha.GetWeapon().Attacking = true;
                }
            }
            if (PlayerCha.GetWeapon().UpdateAnim == true)
            {
                PlayerCha.UpdateWeaponFrame(elapsed);
            }
            #endregion
            if (PlayerKeyboard.IsKeyDown(Keys.F1))
            {
                if (Debuging == false)
                {
                    Debuging = true;
                }
                else if (Debuging == true)
                {
                    Debuging = false;
                }
            }
            foreach (Enemy enemy in Enemies)
            {
                if (enemy.GetHP() > 0)
                {
                    if (enemy.GetAttackRange().Intersects(PlayerCha.GetHitbox()))
                    {
                        enemy.Attack(PlayerCha);
                    }
                    if (enemy.EnemyWeapon.UpdateAnim == true)
                    {
                        enemy.UpdateWeaponFrame(elapsed);
                    }
                    foreach (GameObject Wall in GameObj)
                    {
                        enemy.CalculateHitDis(Wall);
                    }
                    if (PlayerCha.GetWeapon().GetBullets() != null)
                    {
                        foreach (Bullet i in PlayerCha.GetWeapon().GetBullets())
                        {
                            if (i.Hitbox.Intersects(enemy.GetHitbox()))
                            {
                                enemy.SubtraceHP(PlayerCha.GetWeapon().Damage);
                                PlayerCha.GetWeapon().GetBullets().Remove(i);
                                break;
                            }
                        }
                    }
                    enemy.UpdateCharacter(elapsed);
                    //enemy.CalculatePath(Grid, PlayerCha);
                    enemy.MoveTo(PlayerCha.CharacterPos);
                }
                /*else
                {
                    Enemies.Remove(enemy);
                    break;
                }*/
            }
            foreach (GameObject j in GameObj)
            {
                if (PlayerCha.GetWeapon().GetBullets() != null)
                {
                    foreach (Bullet i in PlayerCha.GetWeapon().GetBullets())
                    {
                        if (i.Hitbox.Intersects(j.GetHitbox()))
                        {
                            PlayerCha.GetWeapon().GetBullets().Remove(i);
                            break;
                        }
                    }
                }
            }

            PlayerCha.UpdateCharacter(elapsed);

            elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            // TODO: Add your update logic here
            if (Debuging == false)
            {
                GameCamera.FollowCharacter(PlayerCha);
            }
            else if (Debuging == true)
            {
                bool QPressed = false;
                bool EPressed = false;
                if (PlayerKeyboard.IsKeyDown(Keys.Q))
                {
                    if (QPressed == false)
                    {
                        if (EnemyNum - 1 < 0)
                        {
                            EnemyNum = Enemies.Count - 1;
                        }
                        else
                        {
                            EnemyNum -= 1;
                        }
                        QPressed = true;
                    }
                }
                else if (PlayerKeyboard.IsKeyDown(Keys.E))
                {
                    if (EPressed == false)
                    {
                        if (EnemyNum + 1 > Enemies.Count - 1)
                        {
                            EnemyNum = 0;
                        }
                        else
                        {
                            EnemyNum += 1;
                        }
                        EPressed = true;
                    }
                }
                if (PlayerKeyboard.IsKeyUp(Keys.Q))
                {
                    QPressed = false;
                }
                else if (PlayerKeyboard.IsKeyUp(Keys.E))
                {
                    EPressed = false;
                }
                if (PlayerKeyboard.IsKeyDown(Keys.T))
                {
                    if (CamFollowEnemy == false)
                    {
                        CamFollowEnemy = true;
                    }
                    else
                    {
                        CamFollowEnemy = false;
                    }
                }
                if (CamFollowEnemy == false)
                {
                    FollowedCharacter = Enemies[EnemyNum];
                }
                else
                {
                    FollowedCharacter = PlayerCha;
                }
                if (FollowedCharacter == null)
                {
                    FollowedCharacter = PlayerCha;
                }
                if (PlayerKeyboard.IsKeyDown(Keys.G))
                {
                    //Show Grid
                    ScreenEvent.Invoke(game.TMap2, new EventArgs());
                }
                GameCamera.FollowCharacter(FollowedCharacter);

            }

            /*if(Grid!=null)
            {
                Grid.Update(Vector2.Zero+GameCamera.GetCamPos(), MousePos);
            }*/
        }
        public override void Draw(SpriteBatch _spriteBatch)
        {
            #region Show Debug

            if (Debuging == true)
            {
                _spriteBatch.Draw(HitboxTest, PlayerCha.GetWeaponHitzoneVector() - GameCamera.GetCamPos(), PlayerCha.GetWeapon().GetHitzone(), Color.White);
                _spriteBatch.DrawString(font, WeaponRot.ToString(), new Vector2(PlayerCha.CharacterPos.X - GameCamera.GetCamPos().X, (PlayerCha.CharacterPos.Y + 64) - GameCamera.GetCamPos().Y), Color.White);
                _spriteBatch.DrawString(font, PlayerCha.CharacterPos.ToString(), new Vector2(PlayerCha.CharacterPos.X - GameCamera.GetCamPos().X, (PlayerCha.CharacterPos.Y + 74) - GameCamera.GetCamPos().Y), Color.White);
                _spriteBatch.DrawString(font, PlayerCha.GetAttacking().ToString(), new Vector2(PlayerCha.CharacterPos.X - GameCamera.GetCamPos().X, (PlayerCha.CharacterPos.Y + 84) - GameCamera.GetCamPos().Y), Color.White);
                _spriteBatch.DrawString(font, PlayerCha.GetWeapon().GetFrame().ToString(), new Vector2(PlayerCha.CharacterPos.X - GameCamera.GetCamPos().X, (PlayerCha.CharacterPos.Y + 92) - GameCamera.GetCamPos().Y), Color.White);
                _spriteBatch.DrawString(font, PlayerCha.GetHP().ToString(), new Vector2(PlayerCha.CharacterPos.X - GameCamera.GetCamPos().X, (PlayerCha.CharacterPos.Y + 100) - GameCamera.GetCamPos().Y), Color.White);
                foreach (Enemy enemy in Enemies)
                {
                    _spriteBatch.DrawString(font, enemy.GetHP().ToString(), new Vector2(enemy.CharacterPos.X - GameCamera.GetCamPos().X, (enemy.CharacterPos.Y + 64) - GameCamera.GetCamPos().Y), Color.White);
                }
                _spriteBatch.DrawString(font, MousePos.ToString(), new Vector2(PlayerCha.CharacterPos.X - GameCamera.GetCamPos().X, (PlayerCha.CharacterPos.Y + 124) - GameCamera.GetCamPos().Y), Color.White);
                _spriteBatch.DrawString(font, dPos.ToString(), new Vector2(PlayerCha.CharacterPos.X - GameCamera.GetCamPos().X, (PlayerCha.CharacterPos.Y + 144) - GameCamera.GetCamPos().Y), Color.White);

            }

            #endregion

            _spriteBatch.DrawString(font, "Health : " + PlayerCha.GetHP(), new Vector2(10, 10), Color.White);
            _spriteBatch.DrawString(font, "Skill Point : " + PlayerCha.GetSP(), new Vector2(10, 30), Color.White);
            //there must be a better way of doing this but idk how



            PlayerCha.DrawCharacter(_spriteBatch, WeaponRot, GameCamera.GetCamPos());
            foreach (Enemy enemy in Enemies)
            {
                if(enemy.GetHP()>0)
                {
                    enemy.DrawCharacter(_spriteBatch, GameCamera.GetCamPos());
                }
            }
            foreach (GameObject Wall in GameObj)
            {
                Wall.Draw(GameCamera.GetCamPos());
            }
        }
    }
}
