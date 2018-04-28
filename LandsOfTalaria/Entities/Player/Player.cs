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

    class Player : Entity
    {
        SoundEffect grassStep;
        private static Texture2D healthPoints;
        private KeyboardState keyboardStateOld = Keyboard.GetState();
        private KeyboardState keyboardState;
        private float timer;
        private float timerTick = 0.4f;
        public new static BoundingSphere boundingSphere;
        public new static BoundingBox boundingBox;
        private new static int health;
        public new static int radius;
        public new static Vector2 temporaryPosition;
        public Vector2 Position {
            get { return position; }
            set { position = value; }
        }

        public Player() {
            animatedSprite = new AnimatedSprite[4];
            position = new Vector2(1000, 300);
            radius = 16;
            speed = new Vector2(300, 300);
            runSpeed = 1;
            health = 10;
            animatedSpriteWalking = animatedSprite[(int)direction];
            skinPath[0] = "Player Textures/playerMoveRight";
            skinPath[1] = "Player Textures/playerMoveLeft";
            skinPath[2] = "Player Textures/playerMoveUp";
            skinPath[3] = "Player Textures/playerMoveDown";
            boundingSphere = new BoundingSphere(new Vector3(position.X, position.Y, 0), radius);
            boundingBox = new BoundingBox(new Vector3(position.X + 8, position.Y + 8, 0), new Vector3(position.X + 24, position.Y + 24, 0));
        }

        public override void Update(GameTime gameTime) {
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

        public override void Draw(SpriteBatch spriteBatch) {
            animatedSpriteWalking.Draw(spriteBatch, new Vector2(position.X - 16, position.Y - 16));
        }

        public override void LoadContent(ContentManager contentManager) {
            base.LoadContent(contentManager);
            grassStep = contentManager.Load<SoundEffect>("Music/grassStep");
            healthPoints = contentManager.Load<Texture2D>("Player Textures/Heart");
        }

        public void movingPlayer() {
            if (keyboardState.IsKeyDown(Keys.LeftShift))
            {
                speed.X = 300;
                speed.Y = 300;
                runSpeed = 1.5f;
                timerTick = 0.25f;
            }
            if (keyboardState.IsKeyUp(Keys.LeftShift)) {
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

            if (keyboardState.IsKeyDown(Keys.Space) && (keyboardStateOld.IsKeyUp(Keys.Space))) {
                PlayerAttack.playerAttacks.Add(new PlayerAttack(position, direction));
            }
            temporaryPosition = position;
            keyboardStateOld = keyboardState;

            if (isMoving) {
                if (timer >= timerTick) {
                    grassStep.Play(0.4f, 0.0f, 0.0f);
                    timer = 0;
                }
                switch (direction) {
                    case Direction.Right:
                        temporaryPosition.X += speed.X * dt;
                        if (!didCollide() && !didCollideEntity())
                        {
                            position.X += speed.X * dt;
                        }
                        break;
                    case Direction.Left:
                        temporaryPosition.X -= speed.X * dt;
                        if (!didCollide() && !didCollideEntity())
                        {
                            position.X -= speed.X * dt;
                        }
                        break;
                    case Direction.Up:
                        temporaryPosition.Y -= speed.Y * dt;
                        if (!didCollide() && !didCollideEntity())
                        {
                            position.Y -= speed.Y * dt;
                        }
                        break;
                    case Direction.Down:
                        temporaryPosition.Y += speed.Y * dt;
                        if (!didCollide() && !didCollideEntity())
                        {
                            position.Y += speed.Y * dt;
                        }
                        break;
                    default: break;
                }
                isBehind();
            }
        }

        public override void isBehind()
        {
            if (Obstacles.isBehind(temporaryPosition, size))
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

        public override bool didCollide()
        {
            foreach (Obstacles obstacle in FarmScene.obstaclesList)
            {
                if (obstacle.collisionShape == Obstacles.CollisionShape.Circle)
                {
                    boundingSphere = new BoundingSphere(new Vector3(temporaryPosition.X, temporaryPosition.Y, 0), radius);
                    if (Obstacles.didCollide(boundingSphere, obstacle))
                        return true;
                }
                if (obstacle.collisionShape == Obstacles.CollisionShape.Rectangle)
                {
                    boundingBox = new BoundingBox(new Vector3(temporaryPosition.X + 8, temporaryPosition.Y + 8, 0), new Vector3(temporaryPosition.X + 24, temporaryPosition.Y + 24, 0));
                    if (Obstacles.didCollide(boundingBox, obstacle))
                        return true;
                }
            }
            foreach(CollisionsObject collisionsObject in FarmScene.collisionsObjectList)
            {
                {
                    boundingBox = new BoundingBox(new Vector3(temporaryPosition.X + 8, temporaryPosition.Y + 8, 0), new Vector3(temporaryPosition.X + 24, temporaryPosition.Y + 24, 0));
                    if (boundingBox.Intersects(collisionsObject.boundingBox))
                        return true;
                }
            }
            return false;
        }

        protected bool didCollideEntity()
        {
            foreach (Entity otherEntity in FarmScene.entitiesList)
            {
                if (boundingBox.Intersects(otherEntity.boundingBox))
                {
                    return true;
                }
            }
            return false;
        }

        public static void showPlayerHP(SpriteBatch spriteBatch)
        {
            for(int i=0;i<health;i++)
            {
                spriteBatch.Draw(healthPoints, new Rectangle(8+i*16,8, 16, 16), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, layerDepth: 0.9f);
            }
        }

    }
}
