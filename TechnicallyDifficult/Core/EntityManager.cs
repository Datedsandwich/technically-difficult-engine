using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using TechnicallyDifficult.Entities;
using TechnicallyDifficult.Entities.EntityComponents;
using TechnicallyDifficult.Interfaces;

namespace TechnicallyDifficult.Core
{
    /*
     * This class will occur in every gamescreen
     * ALl entities within the screen with be stored here
     * It will cycle through the list of entities and then update and render them to the screen
     * We can then call SceneManager.Instance.CurrentScene.Entities.Update/Render() 
     */
    public class EntityManager : IGameObject
    {
        public List<IEntity> Entities;

        public EntityManager()
        {
            Initialize();
        }

        public void Initialize()
        {
            // Initialize the list of Entities.
            Entities = new List<IEntity>();
        }

        public void Update(GameTime gameTime)
        {
            // Call Update on every entity on this list.
            for (int i = 0; i < Entities.Count; i++)
            {
                // Update is a method in the IGameObject interface, not IEntity.
                // So a cast is required before it can be called.
                IGameObject entity = Entities[i] as IGameObject;
                entity.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Call Draw on every entity on the list
            for (int i = 0; i < Entities.Count; i++)
            {
                // Draw is a method in the IGameObject interface, not IEntity.
                // So a cast is required before it can be called.
                IGameObject entity = Entities[i] as IGameObject;
                entity.Draw(spriteBatch);
            }
        }

        public void LoadContent(ContentManager Content)
        {
            // Call LoadContent on every entity on the list.
            for (int i = 0; i < Entities.Count; i++)
            {
                // LoadContent is a method in the IGameObject interface, not IEntity.
                // So a cast is required before it can be called.
                IGameObject entity = Entities[i] as IGameObject;
                entity.LoadContent(Content);
            }
        }

        public void UnloadContent()
        {
            // Call UnloadContent on every entity on the list.
            for (int i = 0; i < Entities.Count; i++)
            {
                // UnloadContent is a method in the IGameObject interface, not IEntity.
                // So a cast is required before it can be called.
                IGameObject entity = Entities[i] as IGameObject;
                entity.UnloadContent();
            }
            Entities = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void AddEntity(Entity entity)
        {
            // Add a new entity to the list.
            Entities.Add(entity);
            entity.Initialize();
        }

        public void AddEntity<T>() where T : IEntity, new()
        {
            // Generic version of AddEntity.
            // Create a new EntityComponent
            IEntity entity = new T();
            // Add it to the list of components on this entity.
            Entities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            // Remove entity from list.
            Entities.Remove(entity);
        }

        public Entity FindEntityWithTag(string _tag)
        {
            // Get a reference to an Entity based on it's tag.
            // Will return the FIRST INSTANCE of an entity with the specified tag in the list.
            for(int i = 0; i < Entities.Count; i++)
            {
                // If the entity's tag is equal to the tag we're looking for
                if(Entities[i].tag == _tag)
                {
                    // Return reference to the entity found.
                    return Entities[i] as Entity;
                }
            }
            // If we didn't find an entity with a matching tag, return null.
            return null;
        }
    }
}
