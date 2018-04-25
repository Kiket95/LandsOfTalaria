using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace LandsOfTalaria.Entities.Enemies
{

    class Enemy:Entity
    {
        public static List<Enemy> enemies = new List<Enemy>();
       
        protected enum State { Wander,Chase,RunAway}
        protected State state;
        protected enum Flag {Xpos,Ypos}
        protected Flag sideFlag;
        protected Vector2 startingPosition;
        Random random = new Random();
        protected Vector2 screenCenter;
        protected Vector2 speedChasing;
        protected Vector2 speedRunningAway;
        protected Vector2 speedWandering;
        protected Vector2 wanderPoint;
        protected int sizeOfView;
        protected int teritorySize;
       
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        public Vector2 Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public int Radius
        {
            get { return radius; }
            set { radius = value; }
        }
 
        public Enemy(Vector2 newPosition,Vector2 screenCenter) {
            position = newPosition;
            this.startingPosition = position;
            this.screenCenter = screenCenter;
            speedChasing = new Vector2(160, 160);
            wanderPoint.X = startingPosition.X;
            wanderPoint.Y = startingPosition.Y;
            teritorySize = 450;
            sizeOfView = 200;
            runSpeed = 1;

        }

        public override void Update(GameTime gameTime) {

            animatedSpriteWalking = animatedSprite[(int)direction];
            float distanceFromPlayer = Vector2.Distance(position, playerPosition);
            float distancePlayerSpawn = Vector2.Distance(startingPosition, playerPosition);
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
                    playerChasing(playerPosition, dt);

                    if ((int)distanceFromPlayer < 16) {
                        isMoving = false;
                        speed = new Vector2(0, 0);
                    }
                    else speed = speedChasing;

                    if ((int)distancePlayerSpawn > teritorySize)
                        state = State.Wander;
                    break;
                case State.Wander:
                     Wandering(playerPosition,dt);
                    speed = speedWandering;
                    break;
                default: break;
            }
            runSpeed = speed.X / 100;

            if (isMoving)
                animatedSpriteWalking.Update(gameTime,runSpeed);
            else
                animatedSpriteWalking.setFrame(1);
            isMoving = false;
        }

        public override void Draw(SpriteBatch spriteBatch){
            animatedSpriteWalking.Draw(spriteBatch, new Vector2(position.X - radius, position.Y - radius));
        }

        public void playerChasing(Vector2 playerPosition,float dt){
            if (sideFlag == Flag.Xpos){
                if ((int)position.X >= (int)playerPosition.X){
                    direction = Direction.Left;
                    isMoving = true;
                    position.X -= speed.X * dt;
                }
                else if ((int)position.X < (int)playerPosition.X){
                    direction = Direction.Right;
                    isMoving = true;
                    position.X += speed.X * dt;
                }
            }
            if (sideFlag == Flag.Ypos){
                if ((int)position.Y >= (int)playerPosition.Y){
                    direction = Direction.Up;
                    isMoving = true;
                    position.Y -= speed.Y * dt;
                }
                else if ((int)position.Y < (int)playerPosition.Y){
                    direction = Direction.Down;
                    isMoving = true;
                    position.Y += speed.Y * dt;
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
            if (sideFlag == Flag.Xpos){
                if ((int)position.X >= (int)wanderPoint.X) {
                    direction = Direction.Left;
                    isMoving = true;
                    position.X -= speed.X * dt;
                }
                else if ((int)position.X < (int)wanderPoint.X){
                    direction = Direction.Right;
                    isMoving = true;
                    position.X += speed.X * dt;
                }
            }
            if (sideFlag == Flag.Ypos){
                if ((int)position.Y >= (int)wanderPoint.Y){
                    direction = Direction.Up;
                    isMoving = true;
                    position.Y -= speed.Y * dt;
                }
                else if ((int)position.Y < (int)wanderPoint.Y){
                    direction = Direction.Down;
                    isMoving = true;
                    position.Y += speed.Y * dt;
                }
            }
            if (Math.Abs(wanderPoint.X - position.X) < 4){
                sideFlag = Flag.Ypos;
            }
            if (Math.Abs(wanderPoint.Y - position.Y) < 4){
                sideFlag = Flag.Xpos;
            }

            if (Vector2.Distance(position, wanderPoint) < 16){
                wanderPoint.X = random.Next((int)startingPosition.X - 150, (int)startingPosition.X + 150);
                wanderPoint.Y = random.Next((int)startingPosition.Y - 150, (int)startingPosition.Y + 150);
            }
           
        }
    }
}
