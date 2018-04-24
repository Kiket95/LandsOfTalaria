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
        private bool isSprint;
        private Vector2 temporarySpeed;
        public List<Obstacles> obtaclesLayersList;
        public BoundingSphere boundingSphere;
        public BoundingBox boundingBox;
        Vector2 temporaryPosition;
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
            obtaclesLayersList = new List<Obstacles>();
            animatedSprite = new AnimatedSprite[4];
            position = new Vector2(2000, 100);
            radius = 16;
            speed = new Vector2(300,300);
            runSpeed = 1;
            depth = 0.5f;
        }

        public void Update(GameTime gameTime)
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

            animatedSprite[0] = new AnimatedSprite(walkingFrames[0], 1, 3, 1); //WALK RIGHT
            animatedSprite[1] = new AnimatedSprite(walkingFrames[1], 1, 3, 1); //LEFT
            animatedSprite[2] = new AnimatedSprite(walkingFrames[2], 1, 3, 1); //UP
            animatedSprite[3] = new AnimatedSprite(walkingFrames[3], 1, 3, 1); //DOWN
            size = new Vector2(walkingFrames[0].Width, walkingFrames[0].Height);
        }

        public void movingPlayer()
        {
            if (keyboardState.IsKeyDown(Keys.LeftShift))
            {
                temporarySpeed.X = 300;
                temporarySpeed.Y = 300;
                runSpeed = 1.5f;
                timerTick = 0.25f;
                isSprint = true;
            }
            if (keyboardState.IsKeyUp(Keys.LeftShift))
            {
                temporarySpeed.X = 150;
                temporarySpeed.Y = 150;
                runSpeed = 1f;
                timerTick = 0.35f;
                isSprint = false;
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
                            speed = temporarySpeed;
                            position.X += speed.X * dt;
                        }
                        break;
                    case Direction.Left:
                        temporaryPosition.X -= speed.X * dt;
                        boundingBox = new BoundingBox(new Vector3(temporaryPosition.X + 8, temporaryPosition.Y + 8, 0), new Vector3(temporaryPosition.X + 24, temporaryPosition.Y + 24, 0));
                        boundingSphere = new BoundingSphere(new Vector3(temporaryPosition.X, temporaryPosition.Y, 0), radius);
                        if (!didCollide(boundingBox, boundingSphere))
                        {
                            speed = temporarySpeed;
                            position.X -= speed.X * dt;
                        }
                        break;
                    case Direction.Up:
                        temporaryPosition.Y -= speed.Y * dt;
                        boundingBox = new BoundingBox(new Vector3(temporaryPosition.X + 8, temporaryPosition.Y + 8, 0), new Vector3(temporaryPosition.X + 24, temporaryPosition.Y + 24, 0));
                        boundingSphere = new BoundingSphere(new Vector3(temporaryPosition.X, temporaryPosition.Y, 0), radius);
                        if (!didCollide(boundingBox, boundingSphere))
                        {
                            speed = temporarySpeed;
                            position.Y -= speed.Y * dt;
                        }
                        break;
                    case Direction.Down:
                        temporaryPosition.Y += speed.Y * dt;
                        boundingBox = new BoundingBox(new Vector3(temporaryPosition.X + 8, temporaryPosition.Y + 8, 0), new Vector3(temporaryPosition.X + 24, temporaryPosition.Y + 24, 0));
                        boundingSphere = new BoundingSphere(new Vector3(temporaryPosition.X, temporaryPosition.Y, 0), radius);
                        if (!didCollide(boundingBox,boundingSphere))
                        {
                            speed = temporarySpeed;
                            position.Y += speed.Y * dt;
                        }
                        break;
                    default: break;
                }
                isBehind();
            }
        }
        public void isBehind()
        {
            if (Obstacles.isBehind(temporaryPosition, obtaclesLayersList, size))
            {
                animatedSprite[0].depth = 0.3f;
                animatedSprite[1].depth = 0.3f;
                animatedSprite[2].depth = 0.3f;
                animatedSprite[3].depth = 0.3f;
            }
            else
            {
                animatedSprite[0].depth = 0.5f;
                animatedSprite[1].depth = 0.5f;
                animatedSprite[2].depth = 0.5f;
                animatedSprite[3].depth = 0.5f;
            }
        }

        public bool didCollide(BoundingBox boundingBox,BoundingSphere boundingSphere)
        {
            foreach(Obstacles obstacle in obtaclesLayersList)
            {
             if(obstacle.GetType() == typeof(SunflowerPlant))
                    if (obstacle.didCollide(boundingSphere, obtaclesLayersList))
                    return true;
             if (obstacle.GetType() == typeof(Fence))
                    if(obstacle.didCollide(boundingBox, obtaclesLayersList))
                    return true;
            }
            return false;
        }        
    }
}
