﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace LandsOfTalaria.Objects
{
    class Fence : SmallObstacle
    {
        public Fence(Vector2 newPosition) : base(newPosition)
        {
            radius = 4;
            boundingBox = new BoundingBox(new Vector3(position.X + 8, position.Y+24, 0), new Vector3(position.X + 32, position.Y + 32, 0));
            source = "Objects Textures/FenceHorizontal";
        }

        public override bool didCollide(BoundingBox entityHitboxSphere, List<Obstacles> obtaclesLayersList)
        {
            foreach (Obstacles obstacle in obtaclesLayersList)
            {
                if (entityHitboxSphere.Intersects(obstacle.boundingBox))
                    return true;
            }
            return false;
        }

        
    }
}
