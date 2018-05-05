using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LandsOfTalaria
{
    public class SpriteAtlas
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;

        public SpriteAtlas(Texture2D texture, int rows, int columns ,int currentFrame)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            this.currentFrame = currentFrame;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, float depth)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.None, layerDepth: depth);
        }
    }
}