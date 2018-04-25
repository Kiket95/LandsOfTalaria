using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended;
using LandsOfTalaria.Objects;
using LandsOfTalaria.Entities;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using MonoGame.Extended.Timers;
using System.Collections.Generic;
using System;

namespace LandsOfTalaria
{
    
    class Player:Entity
    {
        SoundEffect grassStep;
        private KeyboardState keyboardStateOld = Keyboard.GetState();
        private KeyboardState keyboardState;
        private float timer;
        private float timerTick = 0.4f;
        public Vector2 Position{
            get { return position;}
            set { position = value;}
        }
        private int Health{
            get { return health;}
            set { health = value;}
        }
        
        public Player()
        {
            animatedSprite = new AnimatedSprite[4];
            position = new Vector2(2000, 100);
            radius = 16;
            speed = new Vector2(300,300);
            runSpeed = 1;
            animatedSpriteWalking = animatedSprite[(int)direction];
            skinPath[0] = "Player Textures/playerMoveRight";
            skinPath[1] = "Player Textures/playerMoveLeft";
            skinPath[2] = "Player Textures/playerMoveUp";
            skinPath[3] = "Player Textures/playerMoveDown";
        }

        public override void Update(GameTime gameTime, Vector2 playerPosition)
        {
            keyboardState = Keyboard.GetState();
            dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer += dt;
            animatedSpriteWalking = animatedSprite[(int)direction];
            

            if (isMoving)
                animatedSpriteWalking.Update(gameTime, runSpeed);
            else
                animatedSpriteWalking.setFrame(1);
            isMoving = false;
            movingPlayer();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            animatedSpriteWalking.Draw(spriteBatch, new Vector2(position.X - 16, position.Y - 16));
        }

        public override void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager);
            grassStep = contentManager.Load<SoundEffect>("Music/grassStep");
        }

        public void movingPlayer()
        {
            if (keyboardState.IsKeyDown(Keys.LeftShift))
            {
                speed.X = 300;
                speed.Y = 300;
                runSpeed = 1.5f;
                timerTick = 0.25f;
            }
            if (keyboardState.IsKeyUp(Keys.LeftShift))
            {
                speed.X = 150;
                speed.Y = 150;
                runSpeed = 1f;
                timerTick = 0.35f;
            }

            if (keyboardState.IsKeyDown(Keys.D)) {
                direction = Direction.Right;
                isMoving = true;
            }

            if (keyboardState.IsKeyDown(Keys.A)) {
                direction = Direction.Left;
                isMoving = true;
            }
            if (keyboardState.IsKeyDown(Keys.W)) {
                direction = Direction.Up;
                isMoving = true;

            }
            if (keyboardState.IsKeyDown(Keys.S)) {
                direction = Direction.Down;
                isMoving = true;
            }

           
            if (keyboardState.IsKeyDown(Keys.Space) && (keyboardStateOld.IsKeyUp(Keys.Space)))
            {
               PlayerAttack.playerAttacks.Add(new PlayerAttack(position, direction));
            }

            keyboardStateOld = keyboardState;
            temporaryPosition = position;
            if (isMoving)
            {
                
                if (timer >= timerTick)
                {
                    grassStep.Play(0.4f, 0.0f, 0.0f);
                    timer = 0 ;
                }
                switch (direction)
                {
                    case Direction.Right:
                        temporaryPosition.X += speed.X * dt;
                        boundingBox = new BoundingBox(new Vector3(temporaryPosition.X+8, temporaryPosition.Y+8, 0), new Vector3(temporaryPosition.X + 24, temporaryPosition.Y + 24, 0));
                        boundingSphere = new BoundingSphere(new Vector3(temporaryPosition.X, temporaryPosition.Y, 0), radius);
                        if (!didCollide(boundingBox, boundingSphere))
                        {
                            position.X += speed.X * dt;
                        }
                        break;
                    case Direction.Left:
                        temporaryPosition.X -= speed.X * dt;
                        boundingBox = new BoundingBox(new Vector3(temporaryPosition.X + 8, temporaryPosition.Y + 8, 0), new Vector3(temporaryPosition.X + 24, temporaryPosition.Y + 24, 0));
                        boundingSphere = new BoundingSphere(new Vector3(temporaryPosition.X, temporaryPosition.Y, 0), radius);
                        if (!didCollide(boundingBox, boundingSphere))
                        {
                            position.X -= speed.X * dt;
                        }
                        break;
                    case Direction.Up:
                        temporaryPosition.Y -= speed.Y * dt;
                        boundingBox = new BoundingBox(new Vector3(temporaryPosition.X + 8, temporaryPosition.Y + 8, 0), new Vector3(temporaryPosition.X + 24, temporaryPosition.Y + 24, 0));
                        boundingSphere = new BoundingSphere(new Vector3(temporaryPosition.X, temporaryPosition.Y, 0), radius);
                        if (!didCollide(boundingBox, boundingSphere))
                        {
                            position.Y -= speed.Y * dt;
                        }
                        break;
                    case Direction.Down:
                        temporaryPosition.Y += speed.Y * dt;
                        boundingBox = new BoundingBox(new Vector3(temporaryPosition.X + 8, temporaryPosition.Y + 8, 0), new Vector3(temporaryPosition.X + 24, temporaryPosition.Y + 24, 0));
                        boundingSphere = new BoundingSphere(new Vector3(temporaryPosition.X, temporaryPosition.Y, 0), radius);
                        if (!didCollide(boundingBox,boundingSphere))
                        {
                            position.Y += speed.Y * dt;
                        }
                        break;
                    default: break;
                }
                isBehind();
            }
        }
      
    }
}
