using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace LandsOfTalaria
{

    class PlayerAttack
    {
        public Vector2 position;
        private int speed = 800;
        private int radius = 16;
        public static List<PlayerAttack> playerAttacks = new List<PlayerAttack>();
        public int Radius{
            get { return radius; }
            set { radius = value; }
        }

        public Vector2 Position{
            get { return position; }
        }

        public Direction direction;
        public PlayerAttack(Vector2 newPosition,Direction newDirection){
            position = newPosition;
            direction = newDirection;
        }

        public void Update(GameTime gameTime){

           float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch(direction){
                case Direction.Right:
                    position.X += speed * dt;
                    break;
                case Direction.Left:
                    position.X -= speed * dt;
                    break;
                case Direction.Up:
                    position.Y -= speed * dt;
                    break;
                case Direction.Down:
                    position.Y += speed * dt;
                    break;
                default:
                    break;
            }
        }
    }
}
