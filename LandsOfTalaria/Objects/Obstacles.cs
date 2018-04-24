using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace LandsOfTalaria.Objects
{
    class Obstacles
    {

        protected static float layer;
        public Vector2 position;
        public Texture2D texture;
        public int radius;
        public Vector2 hitBoxPosition;
        public Vector2 textureSize;
        protected string source;
        public BoundingBox boundingBox;
        public BoundingSphere boundingSphere;

        public Obstacles(Vector2 newPosition)
        {
            position = newPosition;
            layer += 0.001f;
        }
      
        public virtual void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>(source);
            textureSize.Y = texture.Height;
            textureSize.X = texture.Width;
        }

        public virtual bool didCollide(BoundingBox entityHitboxSphere, List<Obstacles> obtaclesLayersList)
        {
            return false;
        }
        public virtual bool didCollide(BoundingSphere entityHitboxSphere, List<Obstacles> obtaclesLayersList)
        {
            return false;
        }

        public static bool isBehind(Vector2 temporaryPosition, List<Obstacles> obtaclesLayersList, Vector2 size)
        {
            foreach (Obstacles obstacle in obtaclesLayersList)
            {
                if (obstacle.GetType() == typeof(Fence))
                {
                    if ((int)temporaryPosition.X <= obstacle.position.X + 32 &&
                        (int)temporaryPosition.X+32 >= obstacle.position.X &&
                        (int)temporaryPosition.Y + 24 < obstacle.position.Y + 32 &&
                        (int)temporaryPosition.Y + 24 > obstacle.position.Y - 32)
                    {
                        Console.WriteLine("Fence");
                        return true;
                    }
                }
                if (obstacle.GetType() == typeof(SunflowerPlant))
                {
                    if ((int)temporaryPosition.Y + size.Y < obstacle.position.Y + 70 &&
                        (int)temporaryPosition.Y + 32 >= obstacle.position.Y &&
                        (int)temporaryPosition.Y < obstacle.position.Y + 96 &&
                        (int)temporaryPosition.Y + 10 >= obstacle.position.Y &&
                        (int)temporaryPosition.X + 32 >= obstacle.position.X &&
                        (int)temporaryPosition.X <= obstacle.position.X + obstacle.textureSize.X &&
                        (int)temporaryPosition.Y > obstacle.position.Y - 34)
                    {
                        Console.WriteLine("Sunflower");
                        return true;
                    }
                }
            }
            return false;
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
