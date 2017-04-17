using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TechnicallyDifficult.Interfaces;
using TechnicallyDifficult.Entities.EntityComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TechnicallyDifficult.Entities
{
    public abstract class Entity : IEntity, ICollisionListener, IGameObject
    {
        public Transform transform { get { return _transform; } }
        // This entities tag, which can be used to find this entity with the Entity Manager FindEntityWithTag method.
        public string tag { get { return _tag; } }
        // A list of all components that this entity has
        private List<IEntityComponent> components = new List<IEntityComponent>();
        // A concrete reference to the transform of this entity. All entities have a transform.
        private Transform _transform;
        protected string _tag;

        public Entity()
        {
            // Constructor only calls Initialize. Entity constructor must be parameterless.
            Initialize();
        }

        public virtual void Initialize()
        {
            // Initialize is required as part of the IGameObject interface. Everything must implement this interface.
            _transform = (Transform)AddComponent<Transform>();
            _tag = "Untagged";
        }

        public virtual void Update(GameTime gameTime)
        {
            // Update is required as part of the IGameObject interface. Everything must implement this interface.
            transform.Update(gameTime);
            for (int i = 0; i < components.Count; i++)
            {
                IGameObject component = components[i] as IGameObject;
                component.Update(gameTime);
            }
        }

        public virtual void OnCollision(BoxCollider other)
        {
            // Whilst BoxCollider collisions are implemented, they do not use Physics.
        }

        public virtual void OnCollision(PlaneCollider other, Vector2 normal)
        {
            // PlaneColliders use physics.
            PhysicsBody physicsBody = GetComponent<PhysicsBody>() as PhysicsBody;

            if (physicsBody != null)
            {
                physicsBody.CollisionResolution(other, normal);
            }
        }

        public virtual void OnCollision(CircleCollider other, Vector2 normal)
        {
            // CircleColliders use physics.
            PhysicsBody physicsBody = GetComponent<PhysicsBody>() as PhysicsBody;

            if (physicsBody != null)
            {
                physicsBody.CollisionResolution(other, normal);
            }
        }

        public virtual void LoadContent(ContentManager content)
        {
            // LoadContent is required as part of the IGameObject interface. Everything must implement this interface.
            for (int i = 0; i < components.Count; i++)
            {
                IGameObject component = components[i] as IGameObject;
                component.LoadContent(content);
            }
        }

        public virtual void UnloadContent()
        {
            // UnloadContent is required as part of the IGameObject interface. Everything must implement this interface.
            components = null;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // Draw is required as part of the IGameObject interface. Everything must implement this interface.
            for (int i = 0; i < components.Count; i++)
            {
                IGameObject component = components[i] as IGameObject;
                component.Draw(spriteBatch);
            }
        }

        // This method allows us to add components of any type inheriting from EntityComponent
        // to this entity. Components are then added to the list of components.
        public IEntityComponent AddComponent<T>() where T : IEntityComponent, new()
        {
            // This method is required as part of the IEntity interface.
            // Create a new EntityComponent
            IEntityComponent entityComponent = new T();
            // Pass it the reference to this entity.
            entityComponent.SetEntity(this);
            // Add it to the list of components on this entity.
            components.Add(entityComponent);
            // Return :D
            return entityComponent;
        }

        // This method allows us to grab a reference to a component on this entity.
        // by searching through the list of components for a component of the type specified.
        public IEntityComponent GetComponent<T>() where T : IEntityComponent
        {
            // This method is required as part of the IEntity interface.
            Type type = typeof(T);
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType() == type)
                {
                    // If components[i] is of the type we want, return.
                    return components[i];
                }
                else
                {
                    // Otherwise, continue the loop;
                    continue;
                }
            }
            // If no components of type are found, return null
            return null;
        }
    }
}
