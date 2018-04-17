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
        Texture2D bigTree1Texture;
        public BigTree1(Vector2 newPosition) :base(newPosition) {
            radius = 32;
        }
    }
}
