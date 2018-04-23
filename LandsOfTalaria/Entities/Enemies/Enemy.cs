using LandsOfTalaria.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled.Graphics;
using System;
using System.Collections.Generic;

namespace LandsOfTalaria.Entities.Enemies
{

    class Enemy
    {
        protected Direction direction = Direction.Down;
        public static List<Enemy> enemies = new List<Enemy>();

        protected enum State { Wander,Chase,RunAway}
        protected State state;

        protected AnimatedSprite[] animatedSprite = new AnimatedSprite[4];
        protected AnimatedSprite animatedSpriteWalking;
        protected Texture2D[] walkingFrames;
        protected Vector2 position;
        protected Vector2 startingPosition;
        protected KeyboardState keyboardState;
        protected Vector2 moveDirection;
        protected bool isMoving = false;
        Random random = new Random();
        protected Vector2 screenCenter;
        protected string[] source = new string[4];
        protected int health;
        protected int speed;
        protected int speedChasing;
        protected int speedRunningAway;
        protected int speedWandering;
        protected Vector2 wanderPoint;
        protected int radius;
        protected const float turnSpeed = 0.2f;


        public Vector2 MoveDirection {
            get { return moveDirection; }
            set { moveDirection = value; }
        }

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
        public int Speed
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
            speedChasing = speed;
            wanderPoint.X = startingPosition.X;
            wanderPoint.Y = startingPosition.Y;
        }

        public void Update(GameTime gameTime, Vector2 playerPosition){
            keyboardState = Keyboard.GetState();
            animatedSpriteWalking = animatedSprite[(int)direction];
            float distanceFromPlayer = Vector2.Distance(position, playerPosition);
            float distancePlayerSpawn = Vector2.Distance(startingPosition, playerPosition);
            float distanceFromSpawn = Vector2.Distance(position,startingPosition);
            float distanceFromWanderPoints = Vector2.Distance(position,wanderPoint);
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if ((distanceFromPlayer > 150)){
                    state = State.Wander;
            }
            if (distanceFromPlayer <= 150)
            {
                if(distancePlayerSpawn <= 450)
                state = State.Chase;
            }
            if (health < 2)
            {
                state = State.RunAway;
            }

            switch(state){
                case State.RunAway:

                    break;
                case State.Chase:
                    moveDirection = playerPosition - Position;
                    speed = 160;
                    if (MoveDirection != Vector2.Zero)
                        moveDirection.Normalize();
                    if (distanceFromPlayer < 16)
                        speed = 0;
                    if (distancePlayerSpawn > 450)
                        state = State.Wander;
                        break;
                case State.Wander:
                    Wandering(playerPosition);
                    speed = speedWandering;
                    break;
                default: break;
            }

            position += dt * moveDirection * speed;

            if (isMoving)
                animatedSpriteWalking.Update(gameTime);
            else
            animatedSpriteWalking.setFrame(1);
            isMoving = false;
            movingEnemy();
        }

        public void movingEnemy()
        {
            if (keyboardState.IsKeyDown(Keys.D)){

                direction = Direction.Right;
                isMoving = true;
            }

            if (keyboardState.IsKeyDown(Keys.A)){
                direction = Direction.Left;
                isMoving = true;
            }
            if (keyboardState.IsKeyDown(Keys.W)){
                direction = Direction.Up;
                isMoving = true;
            }
            if (keyboardState.IsKeyDown(Keys.S)){
                direction = Direction.Down;
                isMoving = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch){
            animatedSpriteWalking.Draw(spriteBatch, new Vector2(position.X - radius, position.Y - radius));
        }

        public void LoadContent(ContentManager contentManager){
            walkingFrames = new Texture2D[]{
             contentManager.Load<Texture2D>(source[0]),
             contentManager.Load<Texture2D>(source[1]),
             contentManager.Load<Texture2D>(source[2]),
             contentManager.Load<Texture2D>(source[3]),
            };

            animatedSprite[0] = new AnimatedSprite(walkingFrames[0], 1, 3,1); //WALK RIGHT
            animatedSprite[1] = new AnimatedSprite(walkingFrames[1], 1, 3,1); //LEFT
            animatedSprite[2] = new AnimatedSprite(walkingFrames[2], 1, 3,1); //UP
            animatedSprite[3] = new AnimatedSprite(walkingFrames[3], 1, 3,1); //DOWN
        }

        public void Wandering(Vector2 playerPosition)
        {

            if (Vector2.Distance(position, wanderPoint) < 16)
            {
                wanderPoint.X = random.Next((int)startingPosition.X - 150, (int)startingPosition.X + 150);
                wanderPoint.Y = random.Next((int)startingPosition.Y - 150, (int)startingPosition.Y + 150);
            }
            moveDirection = wanderPoint - position;
            if (MoveDirection != Vector2.Zero)
                moveDirection.Normalize();
        }
    }
}
