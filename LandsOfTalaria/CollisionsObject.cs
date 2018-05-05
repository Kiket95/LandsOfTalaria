using Microsoft.Xna.Framework;

namespace LandsOfTalaria
{
    class CollisionsObject
    {
        public BoundingBox boundingBox;

        public CollisionsObject(Vector2 boundingMinPos, Vector2 boundingMaxPos)
        {
            boundingBox = new BoundingBox(new Vector3(boundingMinPos.X+32,boundingMinPos.Y+16,0),new Vector3(boundingMaxPos.X,boundingMaxPos.Y,0));
        }
    }
}
