using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using TechnicallyDifficult.Core;
using TechnicallyDifficult.Interfaces;

namespace TechnicallyDifficult.Scenes
{
    public abstract class Scene : IGameObject
    {
        public Scene nextScreen;
        //public Type Type;
        public EntityManager Entities;


        public Scene()
        {
            Initialize();
        }


        public virtual void Initialize()
        {
            Entities = new EntityManager();
        }

        public virtual void OnSceneActive()
        {

        }

        public virtual void EndScene()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            InputManager.Instance.Update();
            Entities.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Entities.Draw(spriteBatch);
        }

        public virtual void LoadContent(ContentManager Content)
        {
            Entities.LoadContent(Content);
        }

        public virtual void UnloadContent()
        {
            Entities.UnloadContent();
        }

    }
}
