using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
        public Vector2 layerSwitchingSize;
        public BoundingBox boundingBox;
      //  public static List<Obstacles> obstacles = new List<Obstacles>();

        public Vector2 Positon
        {
            get { return position; }
            set { position = value; }
        }
        public Vector2 HitBoxPosition
        {
            get { return hitBoxPosition; }
            set { hitBoxPosition = value; }
        }

        public int Radius
        {
            get { return radius; }
            set { radius = value; }
        }

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

     /*   public static bool didCollide(Vector2 temporaryPosition,List<Obstacles> obtaclesLayersList)
        {
            foreach (Obstacles obstacle in obtaclesLayersList)
            {
                int sumOfRadiuses = obstacle.radius + 16;
                if (Vector2.Distance(obstacle.HitBoxPosition, temporaryPosition) < sumOfRadiuses)
                    return true;
            }
            return false;
        }
        */

        public static bool didCollide(Vector2 entityPosition, List<Obstacles> obtaclesLayersList)
        {
            BoundingSphere sphere = new BoundingSphere(new Vector3((int)entityPosition.X, entityPosition.Y,0),12);
            foreach (Obstacles obstacle in obtaclesLayersList)
            {
                obstacle.boundingBox = new BoundingBox(new Vector3(obstacle.position.X, obstacle.position.Y, 0), new Vector3(obstacle.position.X + 36, obstacle.position.Y + 32, 0));
                if (sphere.Intersects(obstacle.boundingBox))
                    return true;
            }
            return false;
        }
        
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
