using Microsoft.Xna.Framework;
using System.Collections.Generic;
using LandsOfTalaria.Entities.Enemies;
using System;

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
            size = new Vector2(32,32);
            speedRunningAway = new Vector2(240, 240);
            speedWandering = new Vector2(80, 80);
            skinPath[0] = "Entities Textures/WolfMoveRight";
            skinPath[1] = "Entities Textures/WolfMoveLeft";
            skinPath[2] = "Entities Textures/WolfMoveUp";
            skinPath[3] = "Entities Textures/WolfMoveDown";
            random = new Random();
        }

        protected override void randomizeWanderPoint()
        {
            wanderPoint.X = random.Next((int)startingPosition.X - 150, (int)startingPosition.X + 150);
            wanderPoint.Y = random.Next((int)startingPosition.Y - 150, (int)startingPosition.Y + 150);
        }
    }
}
