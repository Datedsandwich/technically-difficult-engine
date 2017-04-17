using System;
using Microsoft.Xna.Framework;

using TechnicallyDifficult.Core;

namespace TechnicallyDifficult.Entities.EntityComponents
{
    public enum ForceMode
    {
        Acceleration,               // Force as an acceleration, taking into account mass.
        VelocityChange              // Force as a direct change to velocity.
    }

    public class PhysicsBody : EntityComponent
    {
        public float mass = 1;              // the mass of the object, defaulted as 1
        public Vector2 position;            // The position of the object, will be equal to Transform.position;
        public Vector2 velocity;            // The velocity of the object, will be applied to Transform every update.
        public Vector2 acceleration;        // Gradual increase in velocity.
        private Vector2 reflectionVelocity; // Direction and speed for the entity to be reflected when resolving a collision.
        public float drag;                  // Reduction in velocity every update.
        public float restitutionCoefficient;// Used to simulate material properties.

        public Vector2 MaxVelocity;         // Maximum velocity for this object.
        public Vector2 MinVelocity;         // Minimum velocity for this object.

        private bool useGravity;             // Whether or not this entity should be effected by Gravity.

        public PhysicsBody()
        {
            // Initialize and set up this components values.
            velocity = Vector2.Zero;
            reflectionVelocity = Vector2.Zero;
            MaxVelocity = new Vector2(30f, 30f);
            MinVelocity = new Vector2(-30f, -100f);
            useGravity = true;
            drag = 0.1f;
            restitutionCoefficient = 1f;
        }

        public override void Update(GameTime gameTime)
        {
            this.position = entity.transform.position;
            CapVelocity();
            // If we're using gravity, apply it every update.
            //Console.WriteLine(useGravity);
            if (useGravity)
            {
                AddForce(new Vector2(0f, Physics.gravity), ForceMode.Acceleration);
            }
            // Apply reflection velocity and drag.
            velocity -= velocity * drag;
            //Console.WriteLine(velocity);
            // Apply velocity to the position of the entity this component is attatched to.
            //Console.WriteLine("Velocity in Update: " + velocity);
            entity.transform.SetPosition(this.position + this.velocity);
            // Reset acceleration.
            acceleration = Vector2.Zero;
        }

        public void AddForce(Vector2 force, ForceMode forceMode)
        {
            if (forceMode == ForceMode.Acceleration)
            {
                // Add Force as an Acceleration to the PhysicsBody's velocity;
                acceleration = force / mass;
                velocity += acceleration;
            }
            else if (forceMode == ForceMode.VelocityChange)
            {
                // Directly alter velocity
                velocity = force;
            }
        }

        private void CapVelocity()
        {
            // Ensure that velocity never exceeds it's minimum or maximum value
            velocity = Vector2.Clamp(velocity, MinVelocity, MaxVelocity);
        }

        public void CollisionResolution(CircleCollider other, Vector2 normal)
        {
            // When two CircleColliders collide, physics occurs.
            float cv;                       // Closing Velocity
            if (other.entity.GetComponent<PhysicsBody>() != null)
            {
                // If the colliding entity has a PhysicsBody, we do physics using both our and their velocity.
                PhysicsBody otherPhysics = (PhysicsBody)other.entity.GetComponent<PhysicsBody>();

                // We calculate closing velocity by getting the dot product of the closing velocity direction and the collision normal.
                cv = restitutionCoefficient * (Vector2.Dot(otherPhysics.velocity - this.velocity, normal));
            }
            else
            {
                // If the colliding entity does not have a PhysicsBody, we do physics using only our own velocity.
                cv = restitutionCoefficient * (Vector2.Dot(this.velocity * 2, normal));
            }
            // Then the reflectionVelocity is the closing velocity multiplied by the collision normal.
            reflectionVelocity = normal * cv;
            // Finally, the reflection velocity is applied to the velocity of this object.
            // This happens here, rather than in Update in order to apply the reflection velocity only whilst the objects are colliding.
            velocity += reflectionVelocity;
        }

        public void CollisionResolution(PlaneCollider other, Vector2 normal)
        {
            //Console.WriteLine("Physics with Plane");
            float cv;                       // Closing Velocity
            //Console.WriteLine("Velocity: " + this.velocity);
            //Console.WriteLine("Dot Product: " + Vector2.Dot(this.velocity * 2, normal));
            cv = restitutionCoefficient * (Vector2.Dot(this.velocity * 2, normal));
            //Console.WriteLine("CV: " + cv);
            reflectionVelocity = -normal * cv;
            // Console.WriteLine(normal);
            //Console.WriteLine("Reflection Velocity: " + reflectionVelocity);
            velocity += reflectionVelocity;
        }
    }
}
