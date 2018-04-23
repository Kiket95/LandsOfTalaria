using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LandsOfTalaria.Objects
{
    class Obstacles
    {
        public Vector2 position;
        public Texture2D texture;
        protected int radius;
        protected Vector2 hitBoxPosition;
        public Vector2 textureSize;
        protected string source;
        public Vector2 layerSwitchingSize;
        public static List<Obstacles> obstacles = new List<Obstacles>();

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
            this.position = newPosition;
        }

        public static bool didCollide(Vector2 otherPosition, int otherRadius)
        {
            foreach(Obstacles obstacle in Obstacles.obstacles)
            {
               int sumOfRadiuses =  obstacle.radius + otherRadius;
                if (Vector2.Distance(obstacle.HitBoxPosition, otherPosition) < sumOfRadiuses)
                    return true;
            }
            return false;
        }

        public virtual void LoadContent(ContentManager contentManager)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
