using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TechnicallyDifficult.Interfaces
{
    interface IGameObject
    {
        void Initialize();
        void Update(GameTime gameTime);
        void LoadContent(ContentManager content);
        void UnloadContent();
        void Draw(SpriteBatch spriteBatch);
    }
}
