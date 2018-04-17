using MonoGame.Extended;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using Microsoft.Xna.Framework.Content;

namespace LandsOfTalaria
{
    class FarmScene
    {
        Player player;
        PlayerCamera playerCamera;
        TiledMapRenderer tiledMapRenderer;
        TiledMap startingLoc;
        public Texture2D attackSprite;

        public FarmScene(Player player, PlayerCamera playerCamera, TiledMapRenderer tiledMapRenderer)
        {
            this.player = player;
            this.playerCamera = playerCamera;
            this.tiledMapRenderer = tiledMapRenderer;        }
        
        public void LoadContent(ContentManager contentManager)
        {
            startingLoc = contentManager.Load<TiledMap>("Scenes Maps/Farm");
            player.LoadContent(contentManager);
            attackSprite = contentManager.Load<Texture2D>("Attacks Textures/ball");
        }
        
        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            playerCamera.Follow(player);
            foreach (PlayerAttack playerAttack in PlayerAttack.playerAttacks)
            {
                playerAttack.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch,GraphicsDevice graphicsDevice)
        {
            graphicsDevice.BlendState = BlendState.AlphaBlend;
            graphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            graphicsDevice.RasterizerState = RasterizerState.CullNone;

            spriteBatch.Begin(transformMatrix: playerCamera.Transform);
            tiledMapRenderer.Draw(startingLoc.GetLayer("1"), playerCamera.Transform);
            spriteBatch.End();

            graphicsDevice.BlendState = BlendState.AlphaBlend;
            graphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            graphicsDevice.RasterizerState = RasterizerState.CullNone;

            spriteBatch.Begin(transformMatrix: playerCamera.Transform, samplerState: SamplerState.PointWrap);
            player.Draw(spriteBatch);
            foreach (PlayerAttack playerAttack in PlayerAttack.playerAttacks)
            {
                spriteBatch.Draw(attackSprite, new Vector2(playerAttack.Position.X - 8, playerAttack.Position.Y - 8), Color.White);
            }
            spriteBatch.End();

            graphicsDevice.BlendState = BlendState.AlphaBlend;
            graphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            graphicsDevice.RasterizerState = RasterizerState.CullNone;

            spriteBatch.Begin(transformMatrix: playerCamera.Transform);
            tiledMapRenderer.Draw(startingLoc.GetLayer("2"), playerCamera.Transform);
            tiledMapRenderer.Draw(startingLoc.GetLayer("3"), playerCamera.Transform);
            spriteBatch.End();
        }
    }
}
