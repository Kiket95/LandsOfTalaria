using LandsOfTalaria.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandsOfTalaria.Entities
{
    class Entity
    {
        public List<Obstacles> obtaclesLayersList;
        protected Direction direction = Direction.Down;
        protected int health = 10;
        protected AnimatedSprite[] animatedSprite = new AnimatedSprite[4];
        protected AnimatedSprite animatedSpriteWalking;
        protected Texture2D[] walkingFrames;
        protected Vector2 position;
        protected int radius;
        protected Vector2 speed;
        protected bool isMoving = false;
        protected float dt;
        protected float runSpeed;
    }
}
