using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using TechnicallyDifficult.Entities;
using TechnicallyDifficult.Entities.EntityComponents;

namespace TechnicallyDifficult.GameData
{
    public class Score : Entity
    {
        public Text TextDisplay;
        public TimeSpan value;

        public override void Initialize()
        {
            base.Initialize();
            _tag = "Score";
            TextDisplay = AddComponent<Text>() as Text;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            TextDisplay.text = gameTime.TotalGameTime.ToString();
            value = gameTime.TotalGameTime;
        }
    }
}
