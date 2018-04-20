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
            spriteBatch.Begin(transformMatrix: playerCamera.Transform, samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.FrontToBack);
            DrawLayer();
            spriteBatch.End();

        }

        public void DrawObstacle()
        {
            foreach (Obstacles obstacle in Obstacles.obstacles)
            {
                obstacle.Draw(spriteBatch);
            }
        }

        public void LoadTrees()
        {
            int x = 2700;
            int y = 0;
            for (int i = 0;i<10;i++)
            {
                Obstacles.obstacles.Add(new BigTree1(new Vector2(x,y)));
                Obstacles.obstacles.Add(new BigTree1(new Vector2(x + 100, y)));
                Obstacles.obstacles.Add(new BigTree1(new Vector2(x + 200, y )));
                Obstacles.obstacles.Add(new BigTree1(new Vector2(x + 300, y )));
                y += 250;

            }
            Obstacles.obstacles.Add(new BigTree1(new Vector2(2000,200)));
            Obstacles.obstacles.Add(new BigTree1(new Vector2(2000,500)));
            Obstacles.obstacles.Add(new SunflowerPlant(new Vector2(1500, 424)));
            Obstacles.obstacles.Add(new SunflowerPlant(new Vector2(1400, 400)));
            Obstacles.obstacles.Add(new SunflowerPlant(new Vector2(1300, 376)));
        }

        public void DrawLayer()
        {
            tiledMapRenderer.Draw(startingLoc.GetLayer("1"), playerCamera.Transform);
            DrawObstacle();
            DrawPlayer();
            DrawEnemies();
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
            foreach (PlayerAttack playerAttack in PlayerAttack.playerAttacks)
            {

                //            spriteBatch.Draw(upperPart, new Rectangle((int)Positon.X, (int)Positon.Y, (int)textureSize.X, (int)textureSize.Y), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, layerDepth: 0.6f);

            //    spriteBatch.Draw(attackSprite, new Rectangle((int)playerAttack.position.X, (int)playerAttack.position.Y, 16, 16), null, Color.White,0,Vector2.Zero,SpriteEffects.None,layerDepth:0.45f);
                //     if (Obstacles.didCollide(playerAttack.position, playerAttack.Radius))
                //         playerAttack.Collided = true;
            }
        }
    }
}
