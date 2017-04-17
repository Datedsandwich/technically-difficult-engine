using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using TechnicallyDifficult.Entities.EntityComponents;

namespace TechnicallyDifficult.Entities.EntityComponents
{
    public class Text : EntityComponent
    {
        public string text;
        private SpriteFont Font;

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent(ContentManager Content)
        {
            Font = Content.Load<SpriteFont>("Font");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(text != null)
            {
                spriteBatch.DrawString(Font, text, entity.transform.position, Color.White);
            }
        }

        public void SetText(string value)
        {
            text = value;
        }
    }
}
