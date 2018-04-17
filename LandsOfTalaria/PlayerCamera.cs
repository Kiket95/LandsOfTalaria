using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandsOfTalaria
{
    class PlayerCamera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Player player)
        {
            var position = Matrix.CreateTranslation(-player.Position.X - 16, -player.Position.Y - 16, 0);
            var offset = Matrix.CreateTranslation(Game1.screenWidth / 2, Game1.screenHeight / 2, 0);
            Transform = position * offset;
        }

    }
}
