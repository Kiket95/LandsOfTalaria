using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Content;
namespace LandsOfTalaria.Objects
{
    class BigTree1:Obstacles
    {
        public BigTree1(Vector2 newPosition) :base(newPosition) {
            radius = 16;
            HitBoxPosition = new Vector2(position.X + 120, position.Y + 240);
        }
    }
}
