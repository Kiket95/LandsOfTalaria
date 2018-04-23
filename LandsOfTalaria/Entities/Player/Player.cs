﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended;
using LandsOfTalaria.Objects;
using LandsOfTalaria.Entities;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using MonoGame.Extended.Timers;

namespace LandsOfTalaria
{
    
    class Player:Entity
    {
        SoundEffect grassStep;
        private KeyboardState keyboardStateOld = Keyboard.GetState();
        private KeyboardState keyboardState;
        private float timer;
        private float timerTick = 0.4f;
        float depth;

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
            speed = new Vector2(150,150);
            runSpeed = 1;
            depth = 0.4f;
        }

        public void Update(GameTime gameTime) {

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

        public void Draw(SpriteBatch spriteBatch)
        {
            animatedSpriteWalking.Draw(spriteBatch, new Vector2(position.X - 16, position.Y - 16));
        }

        public void LoadContent(ContentManager contentManager)
        {
            grassStep = contentManager.Load<SoundEffect>("Music/grassStep");

            walkingFrames = new Texture2D[]
            {
             contentManager.Load<Texture2D>("Player Textures/playerMoveRight"),
             contentManager.Load<Texture2D>("Player Textures/playerMoveLeft"),
             contentManager.Load<Texture2D>("Player Textures/playerMoveUp"),
             contentManager.Load<Texture2D>("Player Textures/playerMoveDown"),
            };

            animatedSprite[0] = new AnimatedSprite(walkingFrames[0], 1, 3, 1, depth); //WALK RIGHT
            animatedSprite[1] = new AnimatedSprite(walkingFrames[1], 1, 3, 1, depth); //LEFT
            animatedSprite[2] = new AnimatedSprite(walkingFrames[2], 1, 3, 1, depth); //UP
            animatedSprite[3] = new AnimatedSprite(walkingFrames[3], 1, 3, 1, depth); //DOWN
        }

        public void movingPlayer()
        {
            Vector2 temporaryPosition = position;
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
            if (keyboardState.IsKeyDown(Keys.Space) && (keyboardStateOld.IsKeyUp(Keys.Space)))
            {
               PlayerAttack.playerAttacks.Add(new PlayerAttack(position, direction));
            }

            foreach(Obstacles obstacle in Obstacles.obstacles)
            {
                if(obstacle.position.Y > position.Y)
                {
                    depth = 0.8f;
                }
                if (obstacle.position.Y <= position.Y)
                {
                    depth = 0.4f;
                }
            }

            if (Position != Vector2.Zero)
            {
                Position.Normalize();
            }

            keyboardStateOld = keyboardState;

            
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
                        if (!Obstacles.didCollide(temporaryPosition, radius))
                        {
                            position.X += speed.X * dt;
                        }
                        break;
                    case Direction.Left:
                        temporaryPosition.X -= speed.X * dt;
                        if (!Obstacles.didCollide(temporaryPosition, radius))
                        {
                            position.X -= speed.X * dt;
                        }
                        break;
                    case Direction.Up:
                        temporaryPosition.Y -= speed.Y * dt;
                        if (!Obstacles.didCollide(temporaryPosition, radius))
                        {
                            position.Y -= speed.Y * dt;
                        }
                        break;
                    case Direction.Down:
                        temporaryPosition.Y += speed.Y * dt;
                        if (!Obstacles.didCollide(temporaryPosition, radius))
                        {
                            position.Y += speed.Y * dt;
                        }
                        break;
                    default: break;
                }
            
            }
                
        }

    }
}
