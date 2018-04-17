using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using MonoGame.Extended;


namespace LandsOfTalaria
{
    public enum Direction { Right, Left, Up, Down };
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        SceneManager sceneManager;
        TiledMapRenderer tiledMapRenderer;
        PlayerCamera playerCamera;

        public static int screenHeight;
        public static int screenWidth;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            screenHeight = graphics.PreferredBackBufferHeight = 20*32;
            screenWidth = graphics.PreferredBackBufferWidth = 40*32;
        }

        protected override void Initialize()
        {
            tiledMapRenderer = new TiledMapRenderer(GraphicsDevice);
            playerCamera = new PlayerCamera();
            player = new Player();
            sceneManager = new SceneManager(player, playerCamera, tiledMapRenderer);
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            sceneManager.LoadContent(this.Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            sceneManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);
            sceneManager.Draw(spriteBatch,GraphicsDevice);

            base.Draw(gameTime);
        }
    }
}
