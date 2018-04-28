using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Gui;


namespace LandsOfTalaria.GUI
{
    class Gui
    {
        private Texture2D healthPoints;
        private int health;
        private Texture2D manaPoints;
        private int mana;

        public Gui(int health,int mana)
        {
            this.health = health;
            this.mana = mana;
        }
        public void showPlayerHP(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < health; i++)
            {
                spriteBatch.Draw(healthPoints, new Rectangle(8 + i * 16, 8, 16, 16), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, layerDepth: 0.9f);
            }
            for (int i = 0; i < mana; i++)
            {
                spriteBatch.Draw(manaPoints, new Rectangle(8 + i * 16, 32, 16, 16), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, layerDepth: 0.9f);

            }

        }
        public void LoadContent(ContentManager contentManager)
        {
            healthPoints = contentManager.Load<Texture2D>("Player Textures/Heart");
            manaPoints = contentManager.Load<Texture2D>("Player Textures/Mana");
        }

    }

    
}
