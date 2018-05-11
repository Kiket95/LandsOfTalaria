using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LandsOfTalaria.Entities.Enemies
{

    class Enemy:Entity
    {
       
        protected enum State { Wander,Chase,RunAway}
        protected State state;
        protected enum Flag {Xpos,Ypos}
        protected Flag sideFlag;
        protected Vector2 startingPosition;
        protected Random random;
        protected Vector2 screenCenter;
        protected Vector2 speedChasing;
        protected Vector2 speedRunningAway;
        protected Vector2 speedWandering;
        protected Vector2 wanderPoint;
        protected int sizeOfView;
        protected int teritorySize;
        private Texture2D healthPoints;

        public Enemy(Vector2 newPosition,Vector2 screenCenter) {
            position = newPosition;
            startingPosition = position;
            this.screenCenter = screenCenter;
            speedChasing = new Vector2(160, 160);
            wanderPoint.X = startingPosition.X;
            wanderPoint.Y = startingPosition.Y;
            teritorySize = 450;
            sizeOfView = 200;
            runSpeed = 1;
            size.X = 32;
            size.Y = 32;
        }

        public override void Update(GameTime gameTime) {

            animatedSpriteWalking = animatedSprite[(int)direction];
            float distanceFromPlayer = Vector2.Distance(position, player.Position);
            float distancePlayerSpawn = Vector2.Distance(startingPosition, player.Position);
            float distanceFromSpawn = Vector2.Distance(position, startingPosition);
            float distanceFromWanderPoints = Vector2.Distance(position, wanderPoint);
            dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (((int)distanceFromPlayer > sizeOfView)) {
                state = State.Wander;
            }
            if ((int)distanceFromPlayer <= sizeOfView){
                if ((int)distancePlayerSpawn <= 450)
                    state = State.Chase;
            }
            if (health < 2) {
                state = State.RunAway;
            }

            switch (state) {
                case State.RunAway:

                    break;
                case State.Chase:
                    playerChasing(player.Position, dt);
                    speed = speedChasing;
                    if ((int)distancePlayerSpawn > teritorySize)
                        state = State.Wander;
                    break;
                case State.Wander:
                     Wandering(player.Position,dt);
                    speed = speedWandering;
                    break;
                default: break;
            }
            isBehind();

            runSpeed = speed.X / 100;

            if (isMoving)
                animatedSpriteWalking.Update(gameTime,runSpeed);
            else
                animatedSpriteWalking.setFrame(1);
            isMoving = false;
        }

        public override void Draw(SpriteBatch spriteBatch){
            animatedSpriteWalking.Draw(spriteBatch, new Vector2(position.X - 16, position.Y - 16));
            for (int i = 0; i < health; i++)
            {
                spriteBatch.Draw(healthPoints, new Rectangle(i * 8 + (int)position.X - (int)size.X/2, (int)position.Y-(int)size.Y, 8, 8), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, layerDepth: 0.9f);
            }
        }

        public void playerChasing(Vector2 playerPosition,float dt){
            temporaryPosition = position;
            if (sideFlag == Flag.Xpos){
                if ((int)position.X >= (int)playerPosition.X){
                    temporaryPosition.X -= speed.X * dt;
                    direction = Direction.Left;
                    if (!didCollidePlayer() && !didCollide())
                    {
                        isMoving = true;
                        position.X -= speed.X * dt;
                    }
                }
                else if ((int)position.X < (int)playerPosition.X){
                    temporaryPosition.X += speed.X * dt;
                    direction = Direction.Right;
                    if (!didCollidePlayer() && !didCollide())
                    {
                        isMoving = true;
                        position.X += speed.X * dt;
                    }
                }
            }
            if (sideFlag == Flag.Ypos){
                if ((int)position.Y >= (int)playerPosition.Y){
                    temporaryPosition.Y -= speed.Y * dt;
                    direction = Direction.Up;
                    if (!didCollidePlayer() && !didCollide())
                    {
                        isMoving = true;
                        position.Y -= speed.Y * dt;
                    }
                }
                else if ((int)position.Y < (int)playerPosition.Y)
                {
                    temporaryPosition.Y += speed.Y * dt;
                    direction = Direction.Down;
                    if (!didCollidePlayer() && !didCollide())
                    {
                        isMoving = true;
                        position.Y += speed.Y * dt;
                    }
                }
            }
            if (Math.Abs(playerPosition.X - position.X) < 4){
                sideFlag = Flag.Ypos;
            }
            if (Math.Abs(playerPosition.Y - position.Y) < 4){
                sideFlag = Flag.Xpos;
            }
        }

        public void Wandering(Vector2 playerPosition,float dt){
            temporaryPosition = position;
            if (sideFlag == Flag.Xpos){
                if ((int)position.X >= (int)wanderPoint.X) {
                    temporaryPosition.X -= speed.X * dt;
                    direction = Direction.Left;
                    if (!didCollidePlayer() && !didCollide())
                    {
                        isMoving = true;
                        position.X -= speed.X * dt;
                    }
                    else randomizeWanderPoint();
                }
                else if ((int)position.X < (int)wanderPoint.X){
                    temporaryPosition.X += speed.X * dt;
                    direction = Direction.Right;
                    if (!didCollidePlayer() && !didCollide())
                    {
                        isMoving = true;
                        position.X += speed.X * dt;
                    }
                    else randomizeWanderPoint();
                }
            }
            if (sideFlag == Flag.Ypos){
                if ((int)position.Y >= (int)wanderPoint.Y){
                    temporaryPosition.Y -= speed.Y * dt;
                    direction = Direction.Up;
                    if (!didCollidePlayer() && !didCollide())
                    {
                        isMoving = true;
                        position.Y -= speed.Y * dt;
                    }
                    else randomizeWanderPoint();
                }
                else if ((int)position.Y < (int)wanderPoint.Y){
                    temporaryPosition.Y += speed.Y * dt;
                    direction = Direction.Down;
                    if (!didCollidePlayer() && !didCollide())
                    {
                        isMoving = true;
                        position.Y += speed.Y * dt;
                    }
                    else randomizeWanderPoint();
                }
            }
            if (Math.Abs(wanderPoint.X - position.X) < 4){
                sideFlag = Flag.Ypos;
            }
            if (Math.Abs(wanderPoint.Y - position.Y) < 4){
                sideFlag = Flag.Xpos;
            }

            if (Vector2.Distance(position, wanderPoint) < 16){
                randomizeWanderPoint();
            }
        }
        protected bool didCollidePlayer()
        {
            if (boundingSphere.Intersects(player.boundingSphere)  || boundingSphere.Intersects(player2.boundingSphere))
            {
                return true;
            }
            return false;
        }
        protected virtual void randomizeWanderPoint()
        {
            wanderPoint.X = random.Next((int)startingPosition.X - 150, (int)startingPosition.X + 150);
            wanderPoint.Y = random.Next((int)startingPosition.Y - 150, (int)startingPosition.Y + 150);
        }

        public override void LoadContent(ContentManager contentManager)
        { healthPoints = contentManager.Load<Texture2D>("Player Textures/EntityHeart");
            base.LoadContent(contentManager);
        }

    }
}
