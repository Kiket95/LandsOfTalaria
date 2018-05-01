using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using LandsOfTalaria.Objects;
using Microsoft.Xna.Framework.Media;
using LandsOfTalaria.Entities;

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
        public static List<Obstacles> obstaclesList;
        public static List<Entity> entitiesList;
        public static TiledMapObject[] obstaclesLayerFromMap;
        public static TiledMapObject[] collisionsLayerFromMap;
        public static TiledMapObject[] entitiesLayerFromMap;
        public static TiledMapObject[] transitionsLayerFromMap;
        public static List<CollisionsObject> collisionsObjectList;

        //  public List<TiledMapObject> mapLayerObstacles;
        public FarmScene(Player player, PlayerCamera playerCamera, TiledMapRenderer tiledMapRenderer,SpriteBatch spriteBatch,Vector2 screenCenter){
            obstaclesList = new List<Obstacles>();
            entitiesList = new List<Entity>();
            collisionsObjectList = new List<CollisionsObject>();
            this.player = player;
            this.playerCamera = playerCamera;
            this.tiledMapRenderer = tiledMapRenderer;
            this.spriteBatch = spriteBatch;
            this.screenCenter = screenCenter;
        }
        
        public void LoadContent(ContentManager contentManager){
            Song song = contentManager.Load<Song>("Music/FarmScene");
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            startingLoc = contentManager.Load<TiledMap>("Scenes Maps/Farm");
            obstaclesLayerFromMap = startingLoc.GetLayer<TiledMapObjectLayer>("obstaclesLayerFromMap").Objects;
            collisionsLayerFromMap = startingLoc.GetLayer<TiledMapObjectLayer>("collisionLayerFromMap").Objects;
            entitiesLayerFromMap = startingLoc.GetLayer<TiledMapObjectLayer>("entitiesLayerFromMap").Objects;
            transitionsLayerFromMap = startingLoc.GetLayer<TiledMapObjectLayer>("transitionsLayerFromMap").Objects;
            LoadTrees();
            LoadEnemies();
            foreach(TiledMapObject collisionObjectFromMap in collisionsLayerFromMap)
            {
                collisionsObjectList.Add(new CollisionsObject(collisionObjectFromMap.Position,new Vector2(collisionObjectFromMap.Position.X + collisionObjectFromMap.Size.Width,collisionObjectFromMap.Position.Y+collisionObjectFromMap.Size.Height)));
            }

            foreach(Obstacles obstacle in obstaclesList)
            {
                obstacle.LoadContent(contentManager);
            }
            player.LoadContent(contentManager);
            attackSprite = contentManager.Load<Texture2D>("Attacks Textures/ball");

            foreach (Entity entity in entitiesList)
            {
                entity.LoadContent(contentManager);
            }
        }

        public void Update(GameTime gameTime){
            //player.obstaclesLayersList = obstacles;
            player.Update(gameTime);

            foreach (Entity entity in entitiesList)
            {
            //    entity.obstaclesLayersList = obstacles;
                entity.Update(gameTime);
                entity.playerPosition = player.Position;
            }
            playerCamera.Follow(player);
            //foreach (PlayerAttack playerAttack in PlayerAttack.playerAttacks){
            //    playerAttack.Update(gameTime);
            //}
            tiledMapRenderer.Update(startingLoc, gameTime);

        }

        public void Draw(GraphicsDevice graphicsDevice){

            graphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            graphicsDevice.RasterizerState = RasterizerState.CullNone;
            tiledMapRenderer.Draw(startingLoc.GetLayer("1"), playerCamera.Transform);
            tiledMapRenderer.Draw(startingLoc.GetLayer("1.5"), playerCamera.Transform);
            tiledMapRenderer.Draw(startingLoc.GetLayer("ANIMATED"), playerCamera.Transform);
            tiledMapRenderer.Draw(startingLoc.GetLayer("Special layer"), playerCamera.Transform);
            tiledMapRenderer.Draw(startingLoc.GetLayer("Special layer2"), playerCamera.Transform);
            spriteBatch.Begin(transformMatrix: playerCamera.Transform, sortMode: SpriteSortMode.FrontToBack,depthStencilState: DepthStencilState.DepthRead,blendState: BlendState.AlphaBlend);
            DrawLayer();
            spriteBatch.End();
            spriteBatch.Begin(depthStencilState: DepthStencilState.DepthRead, blendState: BlendState.AlphaBlend);
            player.gui.showPlayerHP(spriteBatch);
            for (int i = 2; i < 8; i++)
            {
                tiledMapRenderer.Draw(startingLoc.GetLayer(i.ToString()), playerCamera.Transform);
            }
            spriteBatch.End();
            //foreach (PlayerAttack playerAttack in PlayerAttack.playerAttacks){
            //    spriteBatch.Draw(attackSprite, new Rectangle((int)playerAttack.position.X, (int)playerAttack.position.Y, 16, 16), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, layerDepth: 0.01f);
            //    //     if (Obstacles.didCollide(playerAttack.position, playerAttack.Radius))
            //    //         playerAttack.Collided = true;
            //}
        }

        public void DrawObstacle(){
            foreach (Obstacles obstacle in obstaclesList)
            {
                obstacle.Draw(spriteBatch);
            }
        }

        public void LoadTrees() {
            int x = 1000;
            int y = 200;
            for (int i = 0;i<30;i++){
                obstaclesList.Add(new Fence(new Vector2(x, y)));
                x += 32;
            }
            foreach (var obstacleFromMap in obstaclesLayerFromMap)
            {
                string type;
                obstacleFromMap.Properties.TryGetValue("Type", out type);
                if(type.Equals("BigTree1"))
                {
                    obstaclesList.Add(new BigTree1(obstacleFromMap.Position));
                }
                if (type.Equals("Sunflower"))
                {
                    obstaclesList.Add(new SunflowerPlant(obstacleFromMap.Position));
                }
            }
        }

        public void DrawLayer(){
            DrawObstacle();
            DrawEnemies();
            DrawPlayer();
        }

        public void LoadEnemies()
        {
            foreach (var entitiesFromMap in entitiesLayerFromMap)
            {
                string type;
                entitiesFromMap.Properties.TryGetValue("Type", out type);
                if (type.Equals("Wolf"))
                {
                    entitiesList.Add(new Wolf(entitiesFromMap.Position,screenCenter));
                }
            }
        }

        public void DrawEnemies(){
            foreach(Entity entity in entitiesList)
            {
                entity.Draw(spriteBatch);
            }
        }

        public void DrawPlayer(){
            player.Draw(spriteBatch);
        }
    }
}
