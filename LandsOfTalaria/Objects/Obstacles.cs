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
            this.position = newPosition;
            layer += 0.001f;
        }

      
        public virtual void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>(source);
            textureSize.Y = texture.Height;
            textureSize.X = texture.Width;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
