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
    class Fence:Obstacles
    {
        BoundingBox boundingBox;
        public Fence(Vector2 newPosition):base(newPosition)
        {
            boundingBox = new BoundingBox();
            radius = 6;
            layerSwitchingSize.X = textureSize.X - textureSize.X / 2;
            layerSwitchingSize.Y = textureSize.Y - textureSize.Y / 2;
            HitBoxPosition = new Vector2(position.X + 16, position.Y + 24);
            source = "Objects Textures/FenceHorizontal";
        }

        public override void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>(source);
            textureSize = new Vector2(texture.Width, texture.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)Positon.X, (int)Positon.Y, (int)textureSize.X, (int)textureSize.Y), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, layerDepth: 0.6f);
        }
    }
}
