using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechnicallyDifficult.Entities;
using TechnicallyDifficult.Graphics;

namespace TechnicallyDifficult.GameData
{
    public class ScoreBackground : Entity
    {
        Image image;

        public override void Initialize()
        {
            base.Initialize();
            image = new Image("Graphics/Tower", transform.position, 1, 1);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            image.position = transform.position;
        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            image.LoadContent(Content);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            image.Draw(spriteBatch, new Microsoft.Xna.Framework.Rectangle((int)transform.position.X, (int)transform.position.Y, 400, 600));
        }
    }
}
