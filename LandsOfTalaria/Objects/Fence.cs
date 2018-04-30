using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace LandsOfTalaria.Objects
{
    class Fence : SmallObstacle{
        public Fence(Vector2 newPosition) : base(newPosition){
            collisionShape = CollisionShape.Rectangle;
            boundingBox = new BoundingBox(new Vector3(position.X + 16, position.Y+24, 0), new Vector3(position.X + 48, position.Y + 32, 0));
            source = "Objects Textures/FenceHorizontal";
        }
    }
}
