using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandsOfTalaria.Objects
{
    class SunflowerPlant : Obstacles
    {
        public SunflowerPlant(Vector2 newPosition) : base(newPosition)
        {
            radius = 16;
            layerSwitchingSize.X = textureSize.X - textureSize.X / 2;
            layerSwitchingSize.Y = textureSize.Y - textureSize.Y / 2;
            HitBoxPosition = new Vector2(position.X + 120, position.Y + 240);
            source = "Objects Textures/SunflowerPlant";
        }
    }
}
