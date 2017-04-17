using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechnicallyDifficult.Entities;
using TechnicallyDifficult.Entities.EntityComponents;

namespace TechnicallyDifficult.GameData
{
    class ScoreDisplay : Entity
    {
        public Text text;

        public override void Initialize()
        {
            base.Initialize();
            text = AddComponent<Text>() as Text;
        }
    }
}
