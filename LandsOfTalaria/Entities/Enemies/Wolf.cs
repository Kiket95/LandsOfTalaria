using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended;
using LandsOfTalaria.Objects;
using System.Collections.Generic;
using LandsOfTalaria.Entities.Enemies;

namespace LandsOfTalaria
{
    class Wolf : Enemy
    {

        public static List<Wolf> wolfes = new List<Wolf>();

        public Wolf(Vector2 newPosition):base(newPosition)
        {
            position = newPosition;
            startingPosition = position;
            health = 10;
            speed = 160;
            radius = 16;
            source[0] = "Entities Textures/WolfMoveRight";
            source[1] = "Entities Textures/WolfMoveLeft";
            source[2] = "Entities Textures/WolfMoveUp";
            source[3] = "Entities Textures/WolfMoveDown";
            roamDistance = new Vector2(100,100);
            viewDistance = 320;
        }
    }
}
