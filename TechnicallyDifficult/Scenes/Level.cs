using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using TechnicallyDifficult.Entities;
using TechnicallyDifficult.Core;
using TechnicallyDifficult.GameData;

using Microsoft.Xna.Framework.Input;

namespace TechnicallyDifficult.Scenes
{
    public class Level : Scene
    {
        public override void Initialize()
        {
            base.Initialize();
            Entities.AddEntity<Background>();
            Entities.AddEntity<Floor>();
            Entities.AddEntity< Player>();
            Entities.AddEntity<UpperFloor>();
            Entities.AddEntity<UpperFloor>();
            Entities.Entities[Entities.Entities.Count - 1].transform.SetPosition(new Vector2(240f, 620f));
            Entities.AddEntity<UpperFloor>();
            Entities.Entities[Entities.Entities.Count - 1].transform.SetPosition(new Vector2(240f, 400f));
            Entities.AddEntity<Score>();
            Entities.Entities[Entities.Entities.Count - 1].transform.SetPosition(new Vector2(850f, 150f));
            Entities.AddEntity<Patient>();
            Entities.Entities[Entities.Entities.Count - 1].transform.SetPosition(new Vector2(800f, 800f));
            Entities.AddEntity<Patient>();
            Entities.Entities[Entities.Entities.Count - 1].transform.SetPosition(new Vector2(500f, 1000f));
            Entities.AddEntity<Patient>();
            Entities.Entities[Entities.Entities.Count - 1].transform.SetPosition(new Vector2(1000f, 0f));
            Entities.AddEntity<Patient>();
            Entities.Entities[Entities.Entities.Count - 1].transform.SetPosition(new Vector2(400f, 400f));
            Entities.AddEntity<Patient>();
            Entities.Entities[Entities.Entities.Count - 1].transform.SetPosition(new Vector2(1200f, 800f));
            Entities.AddEntity<Patient>();
            Entities.Entities[Entities.Entities.Count - 1].transform.SetPosition(new Vector2(1200f, 200f));
        }

        public override void EndScene()
        {
            // Get a reference to the Score entity.
            Score score = Entities.FindEntityWithTag("Score") as Score;
            // Add the new score to the ScoreData list.
            ScoreManager.Instance.AddHighScore("New Player", score.value);
            // and then finally, change scene.
            SceneManager.Instance.ChangeScene(SceneManager.Instance.scenes[1]);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(InputManager.Instance.KeyPressed(Keys.Escape))
            {
                EndScene();
            }
        }
    }
}
