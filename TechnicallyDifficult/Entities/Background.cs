using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TechnicallyDifficult.Graphics;

namespace TechnicallyDifficult.Entities
{
    public class Background : Entity
    {
        Image image;

        public override void Initialize()
        {
            base.Initialize();
            transform.SetPosition(new Vector2(0,0));
            image = new Image("Graphics/Background", transform.position, 1, 1);
        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            image.LoadContent(Content);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            image.Draw(spriteBatch);
        }
    }
}
