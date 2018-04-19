using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using LandsOfTalaria.Objects;
using LandsOfTalaria.Entities.Enemies;
using MonoGame.Extended;
using System;
using Microsoft.Xna.Framework.Media;

namespace LandsOfTalaria
{
    class FarmScene
    {
        Player player;
        PlayerCamera playerCamera;
        TiledMapRenderer tiledMapRenderer;
        TiledMap startingLoc;
        public Texture2D attackSprite;
        Texture2D bigTreeTextureUpper;
        Texture2D bigTreeTextureLower;
        SpriteBatch spriteBatch;

        public FarmScene(Player player, PlayerCamera playerCamera, TiledMapRenderer tiledMapRenderer,SpriteBatch spriteBatch)
        {
            
            this.player = player;
            this.playerCamera = playerCamera;
            this.tiledMapRenderer = tiledMapRenderer;
            this.spriteBatch = spriteBatch;

        }
        
        public void LoadContent(ContentManager contentManager)
        {
            Song song = contentManager.Load<Song>("Music/FarmScene"); 
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            startingLoc = contentManager.Load<TiledMap>("Scenes Maps/Farm");
            player.LoadContent(contentManager);
            LoadTrees();
            LoadEnemies();
            attackSprite = contentManager.Load<Texture2D>("Attacks Textures/ball");
            bigTreeTextureUpper = contentManager.Load<Texture2D>("Objects Textures/BigTree1Upper");
            bigTreeTextureLower = contentManager.Load<Texture2D>("Objects Textures/BigTree1Lower");
            foreach (Enemy enemy in Enemy.enemies)
            {
                enemy.LoadContent(contentManager);
            }
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            playerCamera.Follow(player);
            foreach (PlayerAttack playerAttack in PlayerAttack.playerAttacks)
            {
                playerAttack.Update(gameTime);
            }
            foreach (Enemy enemy in Enemy.enemies)
            {
                enemy.Update(gameTime,new Vector2((int)player.Position.X,(int)player.Position.Y));
            }
        }

        public void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.BlendState = BlendState.AlphaBlend;
            graphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            graphicsDevice.RasterizerState = RasterizerState.CullNone;

            DrawLowerLayer();

            graphicsDevice.BlendState = BlendState.AlphaBlend;
            graphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            graphicsDevice.RasterizerState = RasterizerState.CullNone;

            DrawPlayer();

            graphicsDevice.BlendState = BlendState.AlphaBlend;
            graphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            graphicsDevice.RasterizerState = RasterizerState.CullNone;

            DrawUpperLayer();
           
        }

        public void DrawUpperObstacle()
        {
            foreach(Obstacles obstacle in Obstacles.obstacles)
            {
                spriteBatch.Draw(bigTreeTextureUpper, obstacle.Positon,Color.LightYellow);
            }
        }
        public void DrawLowerObstacle()
        {
            foreach (Obstacles obstacle in Obstacles.obstacles)
            {
                spriteBatch.Draw(bigTreeTextureLower, new Vector2(obstacle.Positon.X, obstacle.Positon.Y+224), Color.White);
            }
        }

        public void LoadTrees()
        {
            int x = 2700;
            int y = 0;
            for (int i = 0;i<10;i++)
            {
                Obstacles.obstacles.Add(new BigTree1(new Vector2(x,y)));
                Obstacles.obstacles.Add(new BigTree1(new Vector2(x + 200, y)));
                Obstacles.obstacles.Add(new BigTree1(new Vector2(x + 100, y + 50)));

                y += 250;
            }
            Obstacles.obstacles.Add(new BigTree1(new Vector2(2000,200)));
        }

        public void DrawLowerLayer()
        {
            spriteBatch.Begin(transformMatrix: playerCamera.Transform,samplerState: SamplerState.PointClamp);
            tiledMapRenderer.Draw(startingLoc.GetLayer("1"), playerCamera.Transform);
            DrawLowerObstacle();
            DrawEnemies();
            spriteBatch.End();
        }

        public void DrawUpperLayer()
        {
            spriteBatch.Begin(transformMatrix: playerCamera.Transform);
            DrawUpperObstacle();
            spriteBatch.End();
        }

        public void LoadEnemies()
        {
            Enemy.enemies.Add(new Wolf(new Vector2(1500,100)));
        }

        public void DrawEnemies()
        {
            foreach(Enemy enemy in Enemy.enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }

        public void DrawPlayer()
        {
            spriteBatch.Begin(transformMatrix: playerCamera.Transform, samplerState: SamplerState.PointWrap);
            player.Draw(spriteBatch);
            foreach (PlayerAttack playerAttack in PlayerAttack.playerAttacks)
            {
                spriteBatch.Draw(attackSprite, new Vector2(playerAttack.Position.X - 8, playerAttack.Position.Y - 8), Color.White);
                //     if (Obstacles.didCollide(playerAttack.position, playerAttack.Radius))
                //         playerAttack.Collided = true;
            }
            spriteBatch.End();
        }
    }
}
