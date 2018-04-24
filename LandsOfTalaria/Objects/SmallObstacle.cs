using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandsOfTalaria.Objects
{
    class SmallObstacle:Obstacles
    {
        public SmallObstacle(Vector2 newPosition):base(newPosition){}
        public override void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>(source);
            textureSize = new Vector2(texture.Width, texture.Height);
        }

        public override bool didCollide(BoundingSphere entityHitboxSphere, List<Obstacles> obtaclesLayersList)
        {
            foreach (Obstacles obstacle in obtaclesLayersList)
            {
                if (entityHitboxSphere.Intersects(obstacle.boundingSphere))
                    return true;
            }
            return false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, (int)textureSize.X, (int)textureSize.Y), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, layerDepth: 0.4f);
        }
    }
}
