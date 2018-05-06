using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LandsOfTalaria
{
    public class SpriteAtlas
    {
        public void Draw(SpriteBatch spriteBatch, Vector2 location, float depth, int currentFrame, Texture2D texture, int rows, int columns)
        {
            int width = texture.Width / columns;
            int height = texture.Height / rows;
            int row = (int)((float)currentFrame / (float)columns);
            int column = currentFrame % columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 32, 32);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.None, layerDepth: 0.4f);
        }
    }
}