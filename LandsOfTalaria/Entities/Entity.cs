using LandsOfTalaria.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        protected Direction direction = Direction.Down;
        protected AnimatedSprite[] animatedSprite = new AnimatedSprite[4];
        protected AnimatedSprite animatedSpriteWalking;
        protected Texture2D[] walkingFrames;
        public BoundingBox boundingBox;
        public BoundingSphere boundingSphere;
        protected Vector2 speed;
        public Vector2 size;
        protected Vector2 position;
        public Vector2 temporaryPosition;
        public Vector2 playerPosition;
        public int radius;
        protected int health;
        protected bool isMoving = false;
        protected float dt;
        protected float runSpeed;
        protected float layerDepth;
        protected String[] skinPath = new String[4];

        public virtual void Draw(SpriteBatch spriteBatch) { }

        public Entity() {
            layerDepth = 0.5f;
            boundingSphere = new BoundingSphere(new Vector3(position.X, position.Y, 0), radius);
            boundingBox = new BoundingBox(new Vector3(position.X + 8, position.Y + 8, 0), new Vector3(position.X + 24, position.Y + 24, 0));
        }

        public virtual void LoadContent(ContentManager contentManager) {
            walkingFrames = new Texture2D[]{
             contentManager.Load<Texture2D>(skinPath[0]),
             contentManager.Load<Texture2D>(skinPath[1]),
             contentManager.Load<Texture2D>(skinPath[2]),
             contentManager.Load<Texture2D>(skinPath[3]),
            };

            animatedSprite[0] = new AnimatedSprite(walkingFrames[0], 1, 3, 1); //WALK RIGHT
            animatedSprite[1] = new AnimatedSprite(walkingFrames[1], 1, 3, 1); //LEFT
            animatedSprite[2] = new AnimatedSprite(walkingFrames[2], 1, 3, 1); //UP
            animatedSprite[3] = new AnimatedSprite(walkingFrames[3], 1, 3, 1); //DOWN
            size = new Vector2(walkingFrames[0].Width, walkingFrames[0].Height);
        }

        public virtual void isBehind() {
            if (Obstacles.isBehind(temporaryPosition, size)) {
                animatedSprite[0].depth = 0.31f;
                animatedSprite[1].depth = 0.31f;
                animatedSprite[2].depth = 0.31f;
                animatedSprite[3].depth = 0.31f;
            }
            else {
                animatedSprite[0].depth = 0.51f;
                animatedSprite[1].depth = 0.51f;
                animatedSprite[2].depth = 0.51f;
                animatedSprite[3].depth = 0.51f;
            }
        }
        public virtual bool didCollide() {
            foreach (Obstacles obstacle in FarmScene.obstaclesList) {
                if (obstacle.collisionShape == Obstacles.CollisionShape.Circle) {
                    boundingSphere = new BoundingSphere(new Vector3(temporaryPosition.X, temporaryPosition.Y, 0), radius);
                    if (Obstacles.didCollide(boundingSphere, obstacle))
                        return true;
                }
                if (obstacle.collisionShape == Obstacles.CollisionShape.Rectangle) {
                    boundingBox = new BoundingBox(new Vector3(temporaryPosition.X + 8, temporaryPosition.Y + 8, 0), new Vector3(temporaryPosition.X + 24, temporaryPosition.Y + 24, 0));
                    if (Obstacles.didCollide(boundingBox, obstacle))
                        return true;
                }
            }
            return false;
        }

        public virtual void Update(GameTime gameTime){
            animatedSpriteWalking = animatedSprite[(int)direction];
        }
    }
}
