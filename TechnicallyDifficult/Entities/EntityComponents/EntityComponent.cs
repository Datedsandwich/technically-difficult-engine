using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using TechnicallyDifficult.Interfaces;

namespace TechnicallyDifficult.Entities.EntityComponents
{
    public abstract class EntityComponent : IEntityComponent, IGameObject
    {
        // All EntityComponents will store a reference to the entity that owns them.
        public IEntity entity { get { return _entity; } }

        private IEntity _entity;

        public void SetEntity(IEntity value)
        {
            _entity = value;
        }

        public virtual void Initialize()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            // All Entity Components implement an Update method, which is called every frame by the Entity.
        }

        public virtual void LoadContent(ContentManager Content)
        {
            // All Entity Components implement a LoadContent method which is called by the Entity when it is loaded.
        }

        public virtual void UnloadContent()
        {
            // All Entity Components implement an UnloadContent method which is called by the Entity when it is loaded.
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // All Entity Components implemnent a Draw method which is called by the Entity.
        }
    }
}
