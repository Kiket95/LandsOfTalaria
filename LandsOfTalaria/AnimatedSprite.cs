using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LandsOfTalaria
{
    public class AnimatedSprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        private double timer;
        private double speed;
        float rotation;

        public AnimatedSprite(Texture2D texture, int rows, int columns,float rotation)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            speed = 0.15D;
            timer = speed;
            this.rotation = rotation;
            
        }

        public void Update(GameTime gameTime,float animationSpeed)
        {
            timer -= gameTime.ElapsedGameTime.TotalSeconds* animationSpeed;
            
            if(timer <= 0)
            {
                currentFrame++;
                timer = speed;
            }

            if (currentFrame == totalFrames)
                currentFrame = 0;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            //spriteBatch.Draw(bigTreeTextureLower, new Rectangle((int)obstacle.Positon.X, (int)obstacle.Positon.Y + 224, 224, 75), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, layerDepth: 0.4f);
            spriteBatch.Draw(Texture,destinationRectangle, sourceRectangle,Color.White,0,Vector2.Zero,SpriteEffects.None,layerDepth: 0.5f);
        }

        public void setFrame(int newFrame)
        {
            currentFrame = newFrame;
        }
    }
}