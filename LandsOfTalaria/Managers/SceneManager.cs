using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled.Graphics;
using Microsoft.Xna.Framework.Content;

namespace LandsOfTalaria
{
    class SceneManager
    {
        Player player;
        TiledMapRenderer tiledMapRenderer;
        PlayerCamera playerCamera;
        PlayerCamera player2Camera;
        FarmScene farmScene;
        SpriteBatch spriteBatch;
        Vector2 screenCenter;
        Player player2;
        public SceneManager(Player player,Player player2, PlayerCamera playerCamera,PlayerCamera player2Camera, TiledMapRenderer tiledMapRenderer,SpriteBatch spriteBatch,Vector2 screenCenter)
        {
            this.player = player;
            this.player2 = player2;
            this.tiledMapRenderer = tiledMapRenderer;
            this.playerCamera = playerCamera;
            this.player2Camera = player2Camera;
            this.spriteBatch = spriteBatch;
            this.screenCenter = screenCenter;
            farmScene = new FarmScene(this.player,this.player2, this.playerCamera,this.player2Camera, this.tiledMapRenderer, this.spriteBatch,this.screenCenter);
        }

        public void LoadContent(ContentManager contentManager){
            farmScene.LoadContent(contentManager);

        }

        public void Update(GameTime gameTime){
            farmScene.Update(gameTime);
        }

        public void Draw(GraphicsDevice graphicsDevice, Viewport leftView,
        Viewport rightView, Viewport defaultView)
        {
            farmScene.Draw(graphicsDevice,leftView,rightView,defaultView);
        } 
    }
}
