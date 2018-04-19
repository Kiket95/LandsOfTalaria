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

        protected AnimatedSprite[] animatedSprite = new AnimatedSprite[4];
        protected AnimatedSprite animatedSpriteWalking;
        protected Texture2D[] walkingFrames;
        protected KeyboardState keyboardStateOld = Keyboard.GetState();
        protected Vector2 position;
        protected Vector2 startingPosition;
        protected KeyboardState keyboardState;
        protected Vector2 moveDirection;
        protected Random random;
        protected int nextValueX;
        protected int nextValueY;
        protected string[] source = new string[4];
        protected int health;
        protected int speed;
        protected int oldSpeed;
        protected int radius;
        protected int attackRadius;
        protected bool isMoving = false;
        protected float dt;
        protected Vector2 roamingPosition;
        protected Vector2 roamDistance;
        protected int viewDistance;

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
        public int AttackRadius
        {
            get { return attackRadius; }
            set { attackRadius = value; }
        }

        public Enemy(Vector2 newPosition) {
            position = newPosition;
            random = new Random();
            startingPosition = position;
            roamingPosition = startingPosition;
        }

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            if (Vector2.Distance(position, playerPosition) < 32)
            {
                speed = 0;
            }
            keyboardState = Keyboard.GetState();
            dt = (float)gameTime.ElapsedGameTime.TotalSeconds;


            if ((int)Vector2.Distance(position,playerPosition) <= viewDistance && (int)Vector2.Distance(startingPosition, playerPosition)<=960)
            {
                speed = 160;
                moveDirection = playerPosition - Position;
            }
            else
            {
            Wandering(playerPosition);
            }
            moveDirection.Normalize();
            position += dt * moveDirection * speed;

            animatedSpriteWalking = animatedSprite[(int)direction];

            if (isMoving)
                animatedSpriteWalking.Update(gameTime);
            else
                animatedSpriteWalking.setFrame(1);
            isMoving = false;
            movingEnemy();
        }

        public void movingEnemy()
        {
            if (keyboardState.IsKeyDown(Keys.D))
            {

                direction = Direction.Right;
                if (!Obstacles.didCollide(position, radius))
                {
                    isMoving = true;
                }
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                direction = Direction.Left;

                if (!Obstacles.didCollide(position, radius))
                {
                    isMoving = true;
                }
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                direction = Direction.Up;

                if (!Obstacles.didCollide(position, radius))
                {
                    isMoving = true;
                }
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                direction = Direction.Down;

                if (!Obstacles.didCollide(position, radius))
                {
                    isMoving = true;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animatedSpriteWalking.Draw(spriteBatch, new Vector2(position.X - radius, position.Y - radius));
        }

        public void LoadContent(ContentManager contentManager)
        {
            oldSpeed = speed;
            walkingFrames = new Texture2D[]
            {
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
            speed = 50;
            if (Vector2.Distance(playerPosition, position) < 16)
                speed = 0;

            if (Vector2.Distance(position,roamingPosition) < 16)
            {
                nextValueX = random.Next((int)startingPosition.X - (int)roamDistance.X, (int)startingPosition.X + (int)roamDistance.X);
                nextValueY = random.Next((int)startingPosition.Y - (int)roamDistance.Y, (int)startingPosition.Y + (int)roamDistance.Y);
                roamingPosition = roamingPosition = new Vector2(nextValueX, nextValueY);
            }
            moveDirection = roamingPosition - position;
        }
    }
}
