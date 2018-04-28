using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandsOfTalaria
{
    class CollisionsObject
    {
        public BoundingBox boundingBox;

        public CollisionsObject(Vector2 boundingMinPos, Vector2 boundingMaxPos)
        {
            boundingBox = new BoundingBox(new Vector3(boundingMinPos.X,boundingMinPos.Y,0),new Vector3(boundingMaxPos.X,boundingMaxPos.Y,0));
        }
    }
}
