using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace LandsOfTalaria.Objects
{
    class Fence : SmallObstacle{
        public Fence(Vector2 newPosition, String state) : base(newPosition){
            collisionShape = CollisionShape.Rectangle;
            this.state = state;
            spriteRows = 5;
            spriteCollumns = 6;
            //source = "Objects Textures/FenceHorizontal";
            switch (state)
                {
                case "horizontal":
                    boundingBox = new BoundingBox(new Vector3(position.X + 16, position.Y + 24, 0), new Vector3(position.X + 48, position.Y + 32, 0));
                    source = "Objects Textures/FenceHorizontal";
                         currentFrame = 1;
                    break;
                case "vertical":
                    boundingBox = new BoundingBox(new Vector3(position.X + 24, position.Y, 0), new Vector3(position.X + 32, position.Y + 32, 0));
                   source = "Objects Textures/FenceVertical";
                    currentFrame = 7;

                    break;
            }

        }
    }
}
