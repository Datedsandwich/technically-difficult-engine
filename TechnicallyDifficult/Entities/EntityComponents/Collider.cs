using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using TechnicallyDifficult.Interfaces;
using TechnicallyDifficult.Core;

namespace TechnicallyDifficult.Entities.EntityComponents
{
    public abstract class Collider : EntityComponent
    {
        // dimensions for a collider.
        public float width, height;
        public Vector2 size;
        public Vector2 center;
        public Vector2 position;

        public Collider()
        {
            if (entity != null)
            {
                position = this.entity.transform.position;
            }
        }

        public override void Update(GameTime gameTime)
        {
            CheckForCollisions();
        }

        public virtual void Collision(BoxCollider other)
        {

        }

        public virtual void Collision(CircleCollider other)
        {

        }

        public virtual void Collision(PlaneCollider other)
        {

        }

        public virtual void CheckForCollisions()
        {
            // Get a reference to the entity list.
            List<IEntity> entityList = SceneManager.Instance.CurrentScene.Entities.Entities;
            // Then we check for collisions against all other types of collider.
            for (int i = 0; i < entityList.Count; i++)
            {
                if (entityList[i].GetComponent<BoxCollider>() != null)
                {
                    BoxCollider otherCollider = (BoxCollider)entityList[i].GetComponent<BoxCollider>();
                    Collision(otherCollider);
                }

                if (entityList[i].GetComponent<PlaneCollider>() != null)
                {
                    PlaneCollider otherCollider = (PlaneCollider)entityList[i].GetComponent<PlaneCollider>();
                    Collision(otherCollider);
                }

                if (entityList[i].GetComponent<CircleCollider>() != null)
                {
                    CircleCollider otherCollider = (CircleCollider)entityList[i].GetComponent<CircleCollider>();
                    Collision(otherCollider);
                }
            }
        }
    }
}
