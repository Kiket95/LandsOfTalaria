using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LandsOfTalaria.Objects
{
    class SunflowerPlant : SmallObstacle
    {
        public SunflowerPlant(Vector2 newPosition) : base(newPosition)
        {
            radius = 4;
            boundingSphere = new BoundingSphere(new Vector3(position.X+16, position.Y+48, 0), radius);
            HitBoxPosition = new Vector2(position.X+16, position.Y+48);
            source = "Objects Textures/SunflowerPlant";
        }

        public override bool isBehind(Vector2 temporaryPosition, List<Obstacles> obtaclesLayersList)
        {
            foreach (Obstacles obstacle in obtaclesLayersList)
            {
                //  if (entityPosition.Y+entitySize.Y < obstacle.position.Y+obstacle.textureSize.Y && entityPosition.Y + entitySize.Y > obstacle.position.Y && entityPosition.X > obstacle.position.X && entityPosition.X < obstacle.position.X+obstacle.textureSize.X)
                // DOLNA LINIA GRACZA WYŻEJ OD DOLNEJ LINII OBIEKTU &&  GORNA LINIA GRACZA WYZEJ OD 32PIXELI OD GORNEJ LINIII OBIEKTU  OD DOLNEJ LINII OBIEKTU &&  GRACZ POMIEDZY PRAWA && LEWA SCIANA OBIEKTU
                if ((int)temporaryPosition.Y > obstacle.position.Y && (int)temporaryPosition.Y + 32 > obstacle.position.Y && (int)temporaryPosition.X < obstacle.position.X + 32 && (int)temporaryPosition.X + 32 > obstacle.position.X && (int)temporaryPosition.Y < obstacle.position.Y + 32)
                    return true;
            }
            return false;
        }
    }
}
