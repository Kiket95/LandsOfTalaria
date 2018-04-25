﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace LandsOfTalaria.Objects
{
    class SunflowerPlant : SmallObstacle
    {
        public SunflowerPlant(Vector2 newPosition) : base(newPosition)
        {
            radius = 4;
            boundingSphere = new BoundingSphere(new Vector3(position.X+16, position.Y+48, 0), radius);
            hitBoxPosition = new Vector2(position.X+16, position.Y+48);
            source = "Objects Textures/SunflowerPlant";
        }
        public override bool didCollide(BoundingSphere entityHitboxSphere, List<Obstacles> obtaclesLayersList)
        {
            foreach (Obstacles obstacle in obtaclesLayersList)
            {
                if (entityHitboxSphere.Intersects(obstacle.boundingSphere))
                    return true;
            }
            return false;
        }
    }
}
