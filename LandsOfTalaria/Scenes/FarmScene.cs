using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using LandsOfTalaria.Objects;
using Microsoft.Xna.Framework.Media;
using LandsOfTalaria.Entities;
using Microsoft.Xna.Framework.Input;

namespace LandsOfTalaria
{
    class FarmScene
    {
        Player player;
        Player player2;
        PlayerCamera playerCamera;
        PlayerCamera player2Camera;
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
        public FarmScene(Player player,Player player2, PlayerCamera playerCamera,PlayerCamera player2Camera, TiledMapRenderer tiledMapRenderer,SpriteBatch spriteBatch,Vector2 screenCenter)
        {
            obstaclesList = new List<Obstacles>();
            entitiesList = new List<Entity>();
            collisionsObjectList = new List<CollisionsObject>();
            this.player = player;
            this.player2 = player2;
            this.playerCamera = playerCamera;
            this.player2Camera = player2Camera;
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
            player2.LoadContent(contentManager);

            attackSprite = contentManager.Load<Texture2D>("Attacks Textures/ball");

            foreach (Entity entity in entitiesList)
            {
                entity.LoadContent(contentManager);
            }
            player2.Position = new Vector2(500,500);
            player.Position = new Vector2(1000, 500);

        }

        public void Update(GameTime gameTime){
            //player.obstaclesLayersList = obstacles;
            //Keys UP, Keys LEFT, Keys RIGHT, Keys DOWN
            player.Update(gameTime, Keys.W, Keys.A, Keys.D,Keys.S);
            player2.Update(gameTime, Keys.Up, Keys.Left, Keys.Right, Keys.Down);
            player2.otherPlayer = player;
            player.otherPlayer = player2;

            foreach (Entity entity in entitiesList)
            {
                //    entity.obstaclesLayersList = obstacles;
                entity.player = player;
                entity.player2 = player2;
                entity.Update(gameTime);

            }
            playerCamera.Follow(player);
            player2Camera.Follow(player2);
            //foreach (PlayerAttack playerAttack in PlayerAttack.playerAttacks){
            //    playerAttack.Update(gameTime);
            //}
            tiledMapRenderer.Update(startingLoc, gameTime);
        }

        public void Draw(GraphicsDevice graphicsDevice, Viewport leftView,Viewport rightView,Viewport defaultView){

            graphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            graphicsDevice.RasterizerState = RasterizerState.CullNone;
            graphicsDevice.Viewport = defaultView;


            graphicsDevice.Viewport = leftView;
            tiledMapRenderer.Draw(startingLoc.GetLayer("1"),playerCamera.Transform);
            tiledMapRenderer.Draw(startingLoc.GetLayer("1.5"), playerCamera.Transform);
            tiledMapRenderer.Draw(startingLoc.GetLayer("ANIMATED"), playerCamera.Transform);
            tiledMapRenderer.Draw(startingLoc.GetLayer("Special layer"), playerCamera.Transform);
            tiledMapRenderer.Draw(startingLoc.GetLayer("Special layer2"), playerCamera.Transform);
            spriteBatch.Begin(transformMatrix: playerCamera.Transform, sortMode: SpriteSortMode.FrontToBack,depthStencilState: DepthStencilState.DepthRead,blendState: BlendState.AlphaBlend);
            DrawLayer();
            player.Draw(spriteBatch);
            player2.Draw(spriteBatch);
            spriteBatch.End();

            graphicsDevice.Viewport = rightView;
            tiledMapRenderer.Draw(startingLoc.GetLayer("1"),player2Camera.Transform);
            tiledMapRenderer.Draw(startingLoc.GetLayer("1.5"), player2Camera.Transform);
            tiledMapRenderer.Draw(startingLoc.GetLayer("ANIMATED"), player2Camera.Transform);
            tiledMapRenderer.Draw(startingLoc.GetLayer("Special layer"), player2Camera.Transform);
            tiledMapRenderer.Draw(startingLoc.GetLayer("Special layer2"), player2Camera.Transform);
            spriteBatch.Begin(transformMatrix: player2Camera.Transform, sortMode: SpriteSortMode.FrontToBack, depthStencilState: DepthStencilState.DepthRead, blendState: BlendState.AlphaBlend);
            DrawLayer();
            player2.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();
            graphicsDevice.Viewport = leftView;
            spriteBatch.Begin(depthStencilState: DepthStencilState.DepthRead, blendState: BlendState.AlphaBlend);
            player.gui.showPlayerHP(spriteBatch);
            for (int i = 2; i < 8; i++)
            {
                tiledMapRenderer.Draw(startingLoc.GetLayer(i.ToString()), playerCamera.Transform);
            }
            spriteBatch.End();

            graphicsDevice.Viewport = rightView;
            spriteBatch.Begin(depthStencilState: DepthStencilState.DepthRead, blendState: BlendState.AlphaBlend);
            player2.gui.showPlayerHP(spriteBatch);
            for (int i = 2; i < 8; i++)
            {
                tiledMapRenderer.Draw(startingLoc.GetLayer(i.ToString()), player2Camera.Transform);
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
           //         obstaclesList.Add(new SunflowerPlant(obstacleFromMap.Position));
                }
                if (type.Equals("Fence"))
                {
                    if(obstacleFromMap.Name.Equals("FenceHorizontal"))
                    for (int i = (int)obstacleFromMap.Position.X; i < (int)obstacleFromMap.Position.X + (int)obstacleFromMap.Size.Width; i+=32)
                    {
                        obstaclesList.Add(new Fence(new Vector2(i,obstacleFromMap.Position.Y),"horizontal"));
                    }
                    if (obstacleFromMap.Name.Equals("FenceVertical"))
                    for (int i = (int)obstacleFromMap.Position.Y; i < (int)obstacleFromMap.Position.Y + obstacleFromMap.Size.Height; i += 32)
                    {
                        obstaclesList.Add(new Fence(new Vector2(obstacleFromMap.Position.X,i ), "vertical"));
                    }
                    if (obstacleFromMap.Name.Equals("FenceUpLeft"))
                        obstaclesList.Add(new Fence(obstacleFromMap.Position, "UpLeft"));
                    if (obstacleFromMap.Name.Equals("FenceUpRight"))
                        obstaclesList.Add(new Fence(obstacleFromMap.Position, "UpRight"));
                    if (obstacleFromMap.Name.Equals("FenceDownLeft"))
                        obstaclesList.Add(new Fence(obstacleFromMap.Position, "DownLeft"));
                    if (obstacleFromMap.Name.Equals("FenceDownRight"))
                        obstaclesList.Add(new Fence(obstacleFromMap.Position, "DownRight"));
                }
            }
        }

        public void DrawLayer(){
            DrawObstacle();
            DrawEnemies();
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

    }
}
