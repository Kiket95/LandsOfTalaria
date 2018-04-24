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
using Microsoft.Xna.Framework.Input;
using TiledSharp;

namespace LandsOfTalaria
{
    class FarmScene
    {
        Player player;
        PlayerCamera playerCamera;
        TiledMapRenderer tiledMapRenderer;
        TiledMap startingLoc;
        public Texture2D attackSprite;
        SpriteBatch spriteBatch;
        Vector2 screenCenter;
        public List<Obstacles> obstacles;

        public FarmScene(Player player, PlayerCamera playerCamera, TiledMapRenderer tiledMapRenderer,SpriteBatch spriteBatch,Vector2 screenCenter)
        {
            obstacles = new List<Obstacles>();
            this.player = player;
            this.playerCamera = playerCamera;
            this.tiledMapRenderer = tiledMapRenderer;
            this.spriteBatch = spriteBatch;
            this.screenCenter = screenCenter;
        }
        
        public void LoadContent(ContentManager contentManager)
        {
            Song song = contentManager.Load<Song>("Music/FarmScene");
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;

            startingLoc = contentManager.Load<TiledMap>("Scenes Maps/Farm");
            player.LoadContent(contentManager);
            LoadTrees();
            LoadEnemies();
            foreach(Obstacles obstacle in obstacles)
            {
                obstacle.LoadContent(contentManager);
            }
            attackSprite = contentManager.Load<Texture2D>("Attacks Textures/ball");

            foreach (Enemy enemy in Enemy.enemies)
            {
                enemy.LoadContent(contentManager);
            }
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            player.obtaclesLayersList = obstacles;
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
            spriteBatch.Begin(transformMatrix: playerCamera.Transform, sortMode: SpriteSortMode.FrontToBack,depthStencilState: DepthStencilState.DepthRead,blendState: BlendState.NonPremultiplied);
            DrawLayer();

            foreach (PlayerAttack playerAttack in PlayerAttack.playerAttacks)
            {
                spriteBatch.Draw(attackSprite, new Rectangle((int)playerAttack.position.X, (int)playerAttack.position.Y, 16, 16), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, layerDepth: 0.01f);
                //     if (Obstacles.didCollide(playerAttack.position, playerAttack.Radius))
                //         playerAttack.Collided = true;
            }

            spriteBatch.End();

        }

        public void DrawObstacle()
        {
            foreach (Obstacles obstacle in obstacles)
            {
                obstacle.Draw(spriteBatch);
            }
        }

        public void LoadTrees()
        {
            int x = 2000;
            int y = 200;
            for (int i = 0;i<30;i++)
            {
                obstacles.Add(new Fence(new Vector2(x, y)));

                x += 32;
            }
            obstacles.Add(new SunflowerPlant(new Vector2(2000,500)));
            obstacles.Add(new SunflowerPlant(new Vector2(2100, 300)));
            obstacles.Add(new SunflowerPlant(new Vector2(2000, 400)));




        }

        public void DrawLayer()
        {
            tiledMapRenderer.Draw(startingLoc.GetLayer("1"), playerCamera.Transform);
            tiledMapRenderer.Draw(startingLoc.GetLayer("2"), playerCamera.Transform);

            DrawObstacle();
            DrawEnemies();
            DrawPlayer();
        }

        public void LoadEnemies()
        {
            Enemy.enemies.Add(new Wolf(new Vector2(1500,100),screenCenter));
            Enemy.enemies.Add(new Wolf(new Vector2(1900, 100), screenCenter));
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
            player.Draw(spriteBatch);

        }
    }
}
