using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chaotic_Night
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        SpriteFont font;
        PlayableCharacter PlayerCha;
        Enemy Test;
        private Texture2D HitboxTest;
        private KeyboardState PlayerKeyboard;
        private MouseState PlayerMouse;
        private Vector2 MousePos;
        private Vector2 dPos;
        private float WeaponRot;
        float elapsed;
        GameObject Wall;
        Camera GameCamera;

        //Debug
        private bool Debuging = false;
        public static int ScreenW;
        public static int ScreenH;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //set game's screen
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            ScreenW = _graphics.PreferredBackBufferWidth;
            ScreenH = _graphics.PreferredBackBufferHeight;


        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GameCamera = new Camera();
            PlayerCha = new PlayableCharacter();
            Test = new Enemy(new Vector2(100,100));
            Wall = new GameObject(250, 250);
            PlayerCha.Load(Content,_spriteBatch);
            Test.Load(Content, _spriteBatch);
            Wall.Load(Content, _spriteBatch);
            font = Content.Load<SpriteFont>("BM_Space_8");
            HitboxTest = Content.Load<Texture2D>("Player-Placeholder");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            PlayerKeyboard = Keyboard.GetState();
            PlayerMouse = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            #region Player Movement
            int HitDisX = 0;
            int HitDisY = 0;
            if (PlayerKeyboard.IsKeyDown(Keys.W))
            {
                if (PlayerCha.ColisionCheck(Wall.GetHitbox()) == false)
                {
                    HitDisY = PlayerCha.GetHitbox().Top - Wall.GetHitbox().Bottom;
                    if (PlayerCha.GetHitbox().Top - PlayerCha.GetSpeed() < Wall.GetHitbox().Bottom &&
                        PlayerCha.GetHitbox().Bottom > Wall.GetHitbox().Bottom &&
                        PlayerCha.GetHitbox().Right > Wall.GetHitbox().Left &&
                        PlayerCha.GetHitbox().Left < Wall.GetHitbox().Right)
                    {
                        PlayerCha.MoveUp(HitDisY);
                    }
                    else
                    {
                        PlayerCha.MoveUp();
                    }
                }
            }
            if (PlayerKeyboard.IsKeyDown(Keys.S))
            {
                if (PlayerCha.ColisionCheck(Wall.GetHitbox()) == false)
                {
                    HitDisY = Wall.GetHitbox().Top - PlayerCha.GetHitbox().Bottom;
                    if (PlayerCha.GetHitbox().Bottom + PlayerCha.GetSpeed() > Wall.GetHitbox().Top &&
                        PlayerCha.GetHitbox().Top < Wall.GetHitbox().Top &&
                        PlayerCha.GetHitbox().Right > Wall.GetHitbox().Left &&
                        PlayerCha.GetHitbox().Left < Wall.GetHitbox().Right)
                    {
                        PlayerCha.MoveDown(HitDisY);
                    }
                    else
                    {
                        PlayerCha.MoveDown();
                    }
                }
            }
            if (PlayerKeyboard.IsKeyDown(Keys.A))
            {
                if (PlayerCha.ColisionCheck(Wall.GetHitbox()) == false)
                {
                    HitDisX = PlayerCha.GetHitbox().Left - Wall.GetHitbox().Right;
                    if (PlayerCha.GetHitbox().Left - PlayerCha.GetSpeed() < Wall.GetHitbox().Right &&
                        PlayerCha.GetHitbox().Right > Wall.GetHitbox().Right &&
                        PlayerCha.GetHitbox().Bottom > Wall.GetHitbox().Top &&
                        PlayerCha.GetHitbox().Top < Wall.GetHitbox().Bottom)
                    {
                        PlayerCha.MoveLeft(HitDisX);
                    }
                    else
                    {
                        PlayerCha.MoveLeft();
                    }
                }

            }
            if (PlayerKeyboard.IsKeyDown(Keys.D))
            {
                if (PlayerCha.ColisionCheck(Wall.GetHitbox()) == false)
                {
                    HitDisX = Wall.GetHitbox().Left - PlayerCha.GetHitbox().Right;
                    if (PlayerCha.GetHitbox().Right + PlayerCha.GetSpeed() > Wall.GetHitbox().Left &&
                        PlayerCha.GetHitbox().Left < Wall.GetHitbox().Left &&
                        PlayerCha.GetHitbox().Bottom > Wall.GetHitbox().Top &&
                        PlayerCha.GetHitbox().Top < Wall.GetHitbox().Bottom)
                    {
                        PlayerCha.MoveRight(HitDisX);
                    }
                    else
                    {
                        PlayerCha.MoveRight();
                    }
                }
            }
            #endregion
            #region Player Attack
            if (PlayerMouse.LeftButton!=ButtonState.Pressed && PlayerMouse.RightButton != ButtonState.Pressed && PlayerCha.GetAttacking()==false)
            {
                MousePos = new Vector2(PlayerMouse.X, PlayerMouse.Y);
                dPos = MousePos - (PlayerCha.GetOrigin()-GameCamera.GetCamPos()); //using middle point of screen because I'm too lazy to make the World Cord to Screen Cord Algorithm
                WeaponRot = (float)Math.Atan2(dPos.Y, dPos.X);
            }
            else if(PlayerMouse.LeftButton == ButtonState.Pressed)
            {
                PlayerCha.DoM1Action(Test);
            }
            else if (PlayerMouse.RightButton == ButtonState.Pressed)
            {
                if (PlayerCha.GetSP() > 0)
                {
                    PlayerCha.DoM2Action(Test);
                }
            }
            if (PlayerCha.GetAttacking()==true)
            {
                PlayerCha.UpdateWeaponFrame(elapsed);
            }
            #endregion
            if (PlayerKeyboard.IsKeyDown(Keys.F1))
            {
                if(Debuging==false)
                {
                    Debuging = true;
                }
                else if(Debuging==true)
                {
                    Debuging = false;
                }
            }
            if(Test.GetHP()>0)
            {
                if (Test.GetAttackRange().Intersects(PlayerCha.GetHitbox()))
                {
                    Test.Attack(PlayerCha);
                }
                if (Test.GetAttacking() == true)
                {
                    Test.UpdateWeaponFrame(elapsed);
                }
                Test.MoveTo(PlayerCha.GetPos());
            }

            PlayerCha.UpdateCharacter(elapsed);
            Test.UpdateCharacter(elapsed);

            elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            // TODO: Add your update logic here
            GameCamera.FollowCharacter(PlayerCha);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            _spriteBatch.DrawString(font, "Health : " +PlayerCha.GetHP(), new Vector2(10,10), Color.White);
            _spriteBatch.DrawString(font, "Skill Point : " + PlayerCha.GetSP(), new Vector2(10 , 30), Color.White);
            //there must be a better way of doing this but idk how

            #region Show Debug

            if (Debuging == true)
            {
                _spriteBatch.Draw(HitboxTest, PlayerCha.GetWeaponHitzoneVector()-GameCamera.GetCamPos(), PlayerCha.GetWeapon().GetHitzone(), Color.White);
                _spriteBatch.DrawString(font, WeaponRot.ToString(), new Vector2(PlayerCha.CharacterPos.X - GameCamera.GetCamPos().X, (PlayerCha.CharacterPos.Y + 64) - GameCamera.GetCamPos().Y), Color.White);
                _spriteBatch.DrawString(font, PlayerCha.CharacterPos.ToString(), new Vector2(PlayerCha.CharacterPos.X - GameCamera.GetCamPos().X, (PlayerCha.CharacterPos.Y + 74) - GameCamera.GetCamPos().Y), Color.White);
                _spriteBatch.DrawString(font, PlayerCha.GetAttacking().ToString(), new Vector2(PlayerCha.CharacterPos.X - GameCamera.GetCamPos().X, (PlayerCha.CharacterPos.Y + 84) - GameCamera.GetCamPos().Y), Color.White);
                _spriteBatch.DrawString(font, PlayerCha.GetWeapon().GetFrame().ToString(), new Vector2(PlayerCha.CharacterPos.X - GameCamera.GetCamPos().X, (PlayerCha.CharacterPos.Y + 92) - GameCamera.GetCamPos().Y), Color.White);
                _spriteBatch.DrawString(font, PlayerCha.GetHP().ToString(), new Vector2(PlayerCha.CharacterPos.X - GameCamera.GetCamPos().X, (PlayerCha.CharacterPos.Y + 100) - GameCamera.GetCamPos().Y), Color.White);
                _spriteBatch.DrawString(font, Test.GetHP().ToString(), new Vector2(Test.CharacterPos.X - GameCamera.GetCamPos().X, (Test.CharacterPos.Y + 64) - GameCamera.GetCamPos().Y), Color.White);
                _spriteBatch.DrawString(font, MousePos.ToString(), new Vector2(PlayerCha.CharacterPos.X - GameCamera.GetCamPos().X, (PlayerCha.CharacterPos.Y + 124) - GameCamera.GetCamPos().Y), Color.White);
                _spriteBatch.DrawString(font, dPos.ToString(), new Vector2(PlayerCha.CharacterPos.X-GameCamera.GetCamPos().X,( PlayerCha.CharacterPos.Y + 144) - GameCamera.GetCamPos().Y), Color.White);

            }

            #endregion

            PlayerCha.DrawCharacter(_spriteBatch, WeaponRot,GameCamera.GetCamPos());
            Test.DrawCharacter(_spriteBatch, GameCamera.GetCamPos());
            Wall.Draw(GameCamera.GetCamPos());

            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
