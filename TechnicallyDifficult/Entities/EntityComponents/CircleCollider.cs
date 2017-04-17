using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using TechnicallyDifficult.Core;
using TechnicallyDifficult.Interfaces;

namespace TechnicallyDifficult.Entities.EntityComponents
{
    public class CircleCollider : Collider
    {
        public float radius;
        // Set up the colliders dimensions.
        public void SetColliderDimensions(float _width, float _height, float _radius)
        {
            // When adding a Collider component to an Entity, call this to set up it's dimensions.
            this.position = entity.transform.position;
            // The Width of the Entity
            this.width = _width;
            // The Height of the Entity
            this.height = _height;
            // A Vector2 representation of the width and height of the entity.
            this.size = new Vector2(width, height);
            // The radius of the circle.
            this.radius = _radius;
        }

        public override void Update(GameTime gameTime)
        {
            // Move collider position
            this.position = entity.transform.position;
            // Update the center position of the circle.
            //Console.WriteLine("Center of Circle: " + center);
            this.center = entity.transform.position + (size / 2);
            // Check for collisions
            base.Update(gameTime);
        }

        public override void Collision(CircleCollider other)
        {
            if (other.entity.Equals(this.entity))
            {
                return;
            }
            // ABVector is the vector between this object and the other object.
            Vector2 ABVector = other.center - this.center;
            // Distance Between Centers is the distance between the 2 collider centers.
            float distanceBetweenCenters = ABVector.Length();
            // If this is greater than the sum of the radii, a collision has occured.
            if (distanceBetweenCenters <= this.radius + other.radius)
            {
                // The collision normal is the ABVector, converted into a Unit vector (or normalized)
                Vector2 normal = ABVector;
                normal.Normalize();
                // We correct interpenetration BEFORE physics has a chance to occur.
                // The distanceIntersecting is the distance between the centers, minus the sum of the radii.
                float distanceIntersecting = (distanceBetweenCenters - (radius + other.radius));
                // We push each object along the collision normal as far as they have interpenetrated.
                entity.transform.SetPosition(entity.transform.position + (normal * distanceIntersecting));
                // Now that we have corrected interpenetration, we call OnCollision on the entity,
                // which will perform physics behaviours if it has a PhysicsBody component.
                ICollisionListener collisionEntity = entity as ICollisionListener;
                collisionEntity.OnCollision(other, normal);
            }
        }

        public override void Collision(PlaneCollider other)
        {
            // Create a new Vector2, with position equal to the Plane's position.
            Vector2 testVector = other.position;
            // TestVariable is what the distance is checked against.
            float testVariable;
            if (other.horizontal)
            {
                if(this.position.X < other.position.X || this.position.X > other.position.X + other.width)
                {
                    return;
                }
                // If the Plane is horizontal
                // Shift the TestVector along the X-Axis so it is equal to this colliders center.
                testVector.X = this.center.X;
                testVariable = this.size.Y / 2;
            }
            else
            {
                // If the plane is Vertical
                // Shift the Testvector along the Y-Axis so it is equal to this colliders center.
                testVector.Y = this.center.Y;
                testVariable = this.size.X / 2;
            }

            if (Vector2.Distance(this.center, testVector) <= testVariable)
            {
                // Collision normal
                Vector2 normal;
                if (other.horizontal)
                {
                    if (testVector.Y >= this.center.Y)
                    {
                        // If we're above the plane, the normal is directly up.
                        normal = -Vector2.UnitY;
                        // Correct interpenetration.
                        entity.transform.SetPosition(entity.transform.position + (normal * Vector2.Distance(new Vector2(this.center.X, this.center.Y + testVariable), testVector)));
                    }
                    else
                    {
                        // If we're below the plane, the normal is directly down.
                        normal = Vector2.UnitY;
                        // Correct interpenetration.
                        entity.transform.SetPosition(entity.transform.position + (normal * Vector2.Distance(new Vector2(this.center.X, this.position.Y), testVector)));
                    }
                }
                else
                {
                    if (testVector.X <= testVariable)
                    {
                        // If we're to the right of the plane
                        normal = Vector2.UnitX;
                        // Correct interpenetration.
                        entity.transform.SetPosition(entity.transform.position + (normal * Vector2.Distance(new Vector2(this.center.X, this.position.Y), testVector)));
                    }
                    else
                    {
                        normal = -Vector2.UnitX;
                        entity.transform.SetPosition(entity.transform.position + (normal * Vector2.Distance(new Vector2(this.center.X + testVariable, this.center.Y), testVector)));
                    }
                }
                // Resolve the collision.
                ICollisionListener collisionEntity = entity as ICollisionListener;
                collisionEntity.OnCollision(other, normal);
            }
        }
    }
}
