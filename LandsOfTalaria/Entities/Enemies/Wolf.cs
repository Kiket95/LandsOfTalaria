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

        public Wolf(Vector2 newPosition,Vector2 screenCenter) :base(newPosition,screenCenter){
            position = newPosition;
            startingPosition = position;
            health = 10;
            speed = new Vector2(160,160);
            radius = 16;
            speedRunningAway = new Vector2(240, 240);
            speedWandering = new Vector2(80, 80);
            skinPath[0] = "Entities Textures/WolfMoveRight";
            skinPath[1] = "Entities Textures/WolfMoveLeft";
            skinPath[2] = "Entities Textures/WolfMoveUp";
            skinPath[3] = "Entities Textures/WolfMoveDown";
        }
    }
}
