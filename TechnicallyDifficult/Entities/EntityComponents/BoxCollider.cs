using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using TechnicallyDifficult.Core;
using TechnicallyDifficult.Interfaces;

namespace TechnicallyDifficult.Entities.EntityComponents
{
    public class BoxCollider : Collider
    {
        // Set up the colliders dimensions.
        public void SetColliderDimensions(float _width, float _height)
        {
            // When adding a Collider component to an Entity, call this to set up it's dimensions.
            this.position = entity.transform.position;
            this.width = _width;
            this.height = _height;
            this.size = new Vector2(width, height);
            this.center = position;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.position = entity.transform.position;
            this.center = entity.transform.position + (size / 2);
        }

        public override void Collision(BoxCollider other)
        {
            if (other.entity.Equals(this.entity))
            {
                return;
            }
            // If the distance between the two collider's center is greater than the combined halfSize's, then a collision has occured.
            Vector2 centers = new Vector2(Math.Abs(this.center.X - other.center.X), Math.Abs(this.center.Y - other.center.Y));
            Vector2 halves = (this.size + other.size) / 2;
            if ((centers.X < halves.X) && (centers.Y < halves.Y))
            {
                // Inform the Entity that they have been collided with.
                ICollisionListener collisionEntity = entity as ICollisionListener;
                collisionEntity.OnCollision(other);
            }
        }
    }
}
