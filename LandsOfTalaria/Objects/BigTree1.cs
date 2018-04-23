using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Content;
using System;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;

namespace LandsOfTalaria.Objects
{
    class BigTree1:Trees
    {
        public BigTree1(Vector2 newPosition) :base(newPosition) {
            radius = 16;
            layerSwitchingSize.X = 225;
            layerSwitchingSize.Y = 225;
            HitBoxPosition = new Vector2(position.X + 120, position.Y + 240);
            source = "Objects Textures/Trees/BigTree1Upper";
            source2 = "Objects Textures/Trees/BigTree1Lower";
            Console.WriteLine(layer);
        }

        public override void LoadContent(ContentManager contentManager)
        {
            upperPart = contentManager.Load<Texture2D>(source);
            lowerPart = contentManager.Load<Texture2D>(source2);
            textureSize = new Vector2(upperPart.Width, upperPart.Height);
            textureSize2 = new Vector2(lowerPart.Width, lowerPart.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(upperPart, new Rectangle((int)Positon.X, (int)Positon.Y, (int)textureSize.X, (int)textureSize.Y), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, layerDepth: 0.6f + layer);
            spriteBatch.Draw(lowerPart, new Rectangle((int)Positon.X, (int)Positon.Y + (int)textureSize.Y, (int)textureSize2.X, (int)textureSize2.Y), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, layerDepth: 0.4f + layer);
        }

    }
}
