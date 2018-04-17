using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended;

namespace LandsOfTalaria
{
    class Player
    {
        
        Direction direction = Direction.Down;

        private int health = 10;
        private int speed;
        private bool isMoving = false;
        float dt;
        private Vector2 position = new Vector2(200, 200);
        public AnimatedSprite[] animatedSprite = new AnimatedSprite[4];
        public AnimatedSprite animatedSpriteWalking;
        public Texture2D[] walkingFrames;
        private KeyboardState keyboardStateOld = Keyboard.GetState();
        KeyboardState keyboardState;

        public Vector2 Position{
            get { return position;}
            set { position = value;}
        }
        private int Health{
            get { return health;}
            set { health = value;}
        }

        public void Update(GameTime gameTime) {

            keyboardState = Keyboard.GetState();
            dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            animatedSpriteWalking = animatedSprite[(int)direction];

            if (isMoving)
                animatedSpriteWalking.Update(gameTime);
            else
                animatedSpriteWalking.setFrame(1);
            isMoving = false;
            movingPlayer();
        }

        public void movingPlayer()
        {
            if(keyboardState.IsKeyDown(Keys.D)) {
                direction = Direction.Right;
                position.X += speed * dt;

                isMoving = true;
            }
            if (keyboardState.IsKeyDown(Keys.A)) {
                direction = Direction.Left;
                position.X -= speed * dt;

                isMoving = true;
            }
            if (keyboardState.IsKeyDown(Keys.W)) {
                direction = Direction.Up;
                position.Y -= speed * dt;

                isMoving = true;
            }
            if (keyboardState.IsKeyDown(Keys.S)) {
                direction = Direction.Down;
                position.Y += speed * dt;

                isMoving = true;
            }
            if (keyboardState.IsKeyDown(Keys.LeftShift))
            {
                speed = 250;
            }
            if (keyboardState.IsKeyUp(Keys.LeftShift))
            {
                speed = 150;
            }
            if (keyboardState.IsKeyDown(Keys.Space) && (keyboardStateOld.IsKeyUp(Keys.Space)))
            {
                PlayerAttack.playerAttacks.Add(new PlayerAttack(position,direction));
            }
            keyboardStateOld = keyboardState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animatedSpriteWalking.Draw(spriteBatch,new Vector2(position.X - 16,position.Y - 16));
        }

        public void LoadContent(ContentManager contentManager)
        {
            walkingFrames = new Texture2D[]
            {
             contentManager.Load<Texture2D>("Player Textures/playerMoveRight"),
             contentManager.Load<Texture2D>("Player Textures/playerMoveLeft"),
             contentManager.Load<Texture2D>("Player Textures/playerMoveUp"),
             contentManager.Load<Texture2D>("Player Textures/playerMoveDown"),
            };

            animatedSprite[0] = new AnimatedSprite(walkingFrames[0], 1, 3); //WALK RIGHT
            animatedSprite[1] = new AnimatedSprite(walkingFrames[1], 1, 3); //LEFT
            animatedSprite[2] = new AnimatedSprite(walkingFrames[2], 1, 3); //UP
            animatedSprite[3] = new AnimatedSprite(walkingFrames[3], 1, 3); //DOWN
        }
    }
}
