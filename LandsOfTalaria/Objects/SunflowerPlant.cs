using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LandsOfTalaria.Objects
{
    class SunflowerPlant : Obstacles
    {
        public SunflowerPlant(Vector2 newPosition) : base(newPosition)
        {
            radius = 4;
            layerSwitchingSize.X = textureSize.X - textureSize.X / 2;
            layerSwitchingSize.Y = textureSize.Y - textureSize.Y / 2;
            HitBoxPosition = new Vector2(position.X+16, position.Y+48);
            source = "Objects Textures/SunflowerPlant";
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
