using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled.Graphics;

namespace LandsOfTalaria
{
    public enum Direction { Right, Left, Up, Down, UpRight };
    enum GameStates { DEAD, PAUSE, ALIVE}
    
    public class Game1 : Game{
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Player player2;
        SceneManager sceneManager;
        TiledMapRenderer tiledMapRenderer;
        PlayerCamera playerCamera;
        PlayerCamera player2Camera;
        Vector2 screenCenter;
        Viewport defaultView;
        Viewport leftView;
        Viewport rightView;


        //  GameStates state;
        public static int screenHeight;
        public static int screenWidth;

        public Game1(){
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            screenHeight = graphics.PreferredBackBufferHeight = 20*32;
            screenWidth = graphics.PreferredBackBufferWidth = 40*32;
            screenCenter = new Vector2(screenWidth / 2, screenHeight / 2);
        }

        protected override void Initialize(){
            graphics.SynchronizeWithVerticalRetrace = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tiledMapRenderer = new TiledMapRenderer(GraphicsDevice);
            playerCamera = new PlayerCamera();
            player2Camera = new PlayerCamera();

            player = new Player("Player Textures/playerMoveRight", "Player Textures/playerMoveLeft", "Player Textures/playerMoveUp", "Player Textures/playerMoveDown");
            player2 = new Player("Player Textures/player2MoveRight", "Player Textures/player2MoveLeft", "Player Textures/player2MoveUp", "Player Textures/player2MoveDown");
            sceneManager = new SceneManager(player,player2, playerCamera,player2Camera, tiledMapRenderer,spriteBatch, screenCenter);
            base.Initialize();
        }

        protected override void LoadContent() {
            sceneManager.LoadContent(this.Content);
            defaultView = GraphicsDevice.Viewport;
            leftView = defaultView;
            rightView = defaultView;
            leftView.Width = leftView.Width /2;
            rightView.Width = rightView.Width / 2;
            rightView.X = leftView.Width;
            GraphicsDevice.Viewport = defaultView;
        }

        protected override void UnloadContent(){
        }

        protected override void Update(GameTime gameTime){
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            sceneManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime){
            GraphicsDevice.Clear(Color.Black);
            sceneManager.Draw(GraphicsDevice,leftView,rightView,defaultView);

            base.Draw(gameTime);
        }
    }
}
