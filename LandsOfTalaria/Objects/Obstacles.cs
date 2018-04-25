using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace LandsOfTalaria.Objects
{
    class Obstacles
    {
        public enum CollisionShape { Rectangle, Circle };
        public CollisionShape collisionShape;
        protected static float layer;
        public Vector2 position;
        public Texture2D texture;
        public int radius;
        public Vector2 hitBoxPosition;
        public Vector2 textureSize;
        protected string source;
        public BoundingBox boundingBox;
        public BoundingSphere boundingSphere;

        public virtual void Draw(SpriteBatch spriteBatch) { }

        public Obstacles(Vector2 newPosition){
            position = newPosition;
            layer += 0.001f;
        }
      
        public virtual void LoadContent(ContentManager contentManager){
            texture = contentManager.Load<Texture2D>(source);
            textureSize.Y = texture.Height;
            textureSize.X = texture.Width;
        }

        internal static bool didCollide(BoundingBox entityHitbox, List<Obstacles> obstaclesLayersList, BoundingSphere entityHitSphere){
            foreach (Obstacles obstacle in obstaclesLayersList)
            {
                if(obstacle.collisionShape == CollisionShape.Circle){
                    if (entityHitSphere.Intersects(obstacle.boundingSphere))
                        return true;
                }
                if (obstacle.collisionShape == CollisionShape.Rectangle){
                    if (entityHitbox.Intersects(obstacle.boundingBox))
                        return true;
                }
            }
            return false;
        }

        //public bool whichCollision(BoundingBox entityHitbox, List<Obstacles> obtaclesLayersList)
        //{
        //    foreach (Obstacles obstacle in obtaclesLayersList)
        //    {
        //        if (obstacle.collisionShape == CollisionShape.Rectangle)
        //        {
        //            if (entityHitbox.Intersects(obstacle.boundingBox))
        //                return true;
        //        }
        //    }
        //    return false;
        //}


        public static bool isBehind(Vector2 temporaryPosition, List<Obstacles> obtaclesLayersList, Vector2 size){
            foreach (Obstacles obstacle in obtaclesLayersList){
                if (obstacle.GetType() == typeof(SunflowerPlant)){
                    if ((int)temporaryPosition.Y + size.Y <= obstacle.position.Y + 72 &&
                        (int)temporaryPosition.Y+32 <= obstacle.position.Y + 96 &&
                        (int)temporaryPosition.Y + 15 >= obstacle.position.Y &&
                        (int)temporaryPosition.X + 32 >= obstacle.position.X &&
                        (int)temporaryPosition.X <= obstacle.position.X + obstacle.textureSize.X &&
                        (int)temporaryPosition.Y >= obstacle.position.Y - 32)
                    {
                        Console.WriteLine("Sunflower");
                        return true;
                    }
                }

                else if (obstacle.GetType() == typeof(Fence)){
                    if ((int)temporaryPosition.X <= obstacle.position.X + 32 &&
                        (int)temporaryPosition.X + 32 >= obstacle.position.X &&
                        (int)temporaryPosition.Y + 24 <= obstacle.position.Y + 32 &&
                        (int)temporaryPosition.Y + 24 >= obstacle.position.Y - 16)
                    {
                        Console.WriteLine("Fence");
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
