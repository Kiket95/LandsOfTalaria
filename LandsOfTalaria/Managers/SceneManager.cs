﻿using Microsoft.Xna.Framework;
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
        FarmScene farmScene;
        SpriteBatch spriteBatch;

        public SceneManager(Player player, PlayerCamera playerCamera, TiledMapRenderer tiledMapRenderer,SpriteBatch spriteBatch)
        {
            this.player = player;
            this.tiledMapRenderer = tiledMapRenderer;
            this.playerCamera = playerCamera;
            this.spriteBatch = spriteBatch;
            farmScene = new FarmScene(this.player, this.playerCamera, this.tiledMapRenderer,this.spriteBatch);
        }

        public void LoadContent(ContentManager contentManager)
        {
            farmScene.LoadContent(contentManager);
        }

        public void Update(GameTime gameTime)
        {
            farmScene.Update(gameTime);
        }

        public void Draw(GraphicsDevice graphicsDevice)
        {
            farmScene.Draw(graphicsDevice);
        } 
    }
}
