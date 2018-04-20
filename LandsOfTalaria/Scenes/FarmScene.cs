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

namespace LandsOfTalaria
{
    class FarmScene
    {
        Player player;
        PlayerCamera playerCamera;
        TiledMapRenderer tiledMapRenderer;
        TiledMap startingLoc;
        public Texture2D attackSprite;
        float depth;
        KeyboardState keyboardState;
        SpriteBatch spriteBatch;
        Vector2 screenCenter;
        public FarmScene(Player player, PlayerCamera playerCamera, TiledMapRenderer tiledMapRenderer,SpriteBatch spriteBatch,Vector2 screenCenter)
        {
            this.player = player;
            this.playerCamera = playerCamera;
            this.tiledMapRenderer = tiledMapRenderer;
            this.spriteBatch = spriteBatch;
            this.screenCenter = screenCenter;
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
            foreach(Obstacles obstacle in Obstacles.obstacles)
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
            keyboardState = Keyboard.GetState();
            player.Update(gameTime);
            playerCamera.Follow(player);

            foreach(Obstacles obstacle in Obstacles.obstacles)
            {
                float playerObstacleDistance = Vector2.Distance(obstacle.position + new Vector2((int)obstacle.textureSize.X/2,(int)obstacle.textureSize.Y/2),player.Position);

                if (((int)player.Position.Y > (int)obstacle.Positon.Y + 50)  && (playerObstacleDistance < 200))
                    depth = 0.4f;
                else
                    depth = 0.6f;
            }
            
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

            DrawLayer();
           
        }

        public void DrawObstacle()
        {
            foreach (Obstacles obstacle in Obstacles.obstacles)
            {
                if(obstacle.GetType() == typeof(BigTree1))spriteBatch.Draw(obstacle.texture, new Rectangle((int)obstacle.Positon.X, (int)obstacle.Positon.Y, (int)obstacle.textureSize.X, (int)obstacle.textureSize.Y), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, layerDepth: depth);
                if (obstacle.GetType() == typeof(SunflowerPlant)) spriteBatch.Draw(obstacle.texture, new Rectangle((int)obstacle.Positon.X, (int)obstacle.Positon.Y, 32, 64), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, layerDepth: depth);
            }
        }

        public void LoadTrees()
        {
            int x = 2700;
            int y = 0;
            for (int i = 0;i<10;i++)
            {
           //     Obstacles.obstacles.Add(new BigTree1(new Vector2(x,y)));
          //      Obstacles.obstacles.Add(new BigTree1(new Vector2(x + 200, y)));
           //     Obstacles.obstacles.Add(new BigTree1(new Vector2(x + 100, y + 50)));

                y += 250;
            }
            Obstacles.obstacles.Add(new BigTree1(new Vector2(2000,200)));
            Obstacles.obstacles.Add(new SunflowerPlant(new Vector2(1500, 424)));
            Obstacles.obstacles.Add(new SunflowerPlant(new Vector2(1400, 400)));
            Obstacles.obstacles.Add(new SunflowerPlant(new Vector2(1300, 376)));

        }

        public void DrawLayer()
        {
            spriteBatch.Begin(transformMatrix: playerCamera.Transform,samplerState: SamplerState.PointClamp,sortMode: SpriteSortMode.FrontToBack);
            tiledMapRenderer.Draw(startingLoc.GetLayer("1"), playerCamera.Transform);
            DrawObstacle();
            DrawPlayer();
            DrawEnemies();
            spriteBatch.End();
        }

        public void LoadEnemies()
        {
            Enemy.enemies.Add(new Wolf(new Vector2(1500,100),screenCenter));
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
            foreach (PlayerAttack playerAttack in PlayerAttack.playerAttacks)
            {
                spriteBatch.Draw(attackSprite, new Vector2(playerAttack.Position.X - 8, playerAttack.Position.Y - 8), Color.White);
                //     if (Obstacles.didCollide(playerAttack.position, playerAttack.Radius))
                //         playerAttack.Collided = true;
            }
        }
    }
}
