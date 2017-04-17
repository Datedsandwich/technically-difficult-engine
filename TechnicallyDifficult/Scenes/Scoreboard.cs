using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TechnicallyDifficult.Entities;
using TechnicallyDifficult.Entities.EntityComponents;
using TechnicallyDifficult.GameData;
using TechnicallyDifficult.Interfaces;

namespace TechnicallyDifficult.Scenes
{
    public class Scoreboard : Scene
    {

        public List<Text> names;                                            // List of Text EntityComponents to display names.
        public List<Text> scores;                                           // List of Text EntityComponents to display scores.

        public override void Initialize()
        {
            base.Initialize();
            // Initialize lists.
            names = new List<Text>();
            scores = new List<Text>();
            // Add new Background Entity to scene.
            Entities.AddEntity<Background>();

            Entities.AddEntity<ScoreBackground>();
            Entities.Entities[Entities.Entities.Count - 1].transform.SetPosition(new Vector2(750f, 50f));
            // Create a text element that says "High Scores!!!!!" at position 900,50
            CreateNewText("High Scores!!!!!", new Vector2(900f, 50f));
            // Set an initial starting position for the Scoreboard.
            Vector2 textPosition = new Vector2(800f, 150f);
            // Loop through and create the actual scoreboard. Holds a maximum of 10 values.
            for (int i = 0; i < 10; i++)
            {
                // Text position for this particular element is in the same horizontal position as initial point,
                // but is 50 pixels lower per index.
                Vector2 newTextPosition = textPosition + new Vector2(0f, 50f * i);
                // Create an empty text entity at that position.
                CreateNewText(newTextPosition);
                // Add the empty Text entity to the list of names.
                names.Add(Entities.Entities[Entities.Entities.Count - 1].GetComponent<Text>() as Text);
                // Text position for this particular element is in the same horizontal position as initial point + 200 pixels,
                // and is 50 pixels lower per index.
                newTextPosition = textPosition + new Vector2(200f, 50f * i);
                // Create an empty text entity at that position.
                CreateNewText(newTextPosition);
                // and add it to the list of scores.
                scores.Add(Entities.Entities[Entities.Entities.Count - 1].GetComponent<Text>() as Text);
            }
        }

        public void CreateNewText(Vector2 position)
        {
            // Create an Entity with an empty Text component, at the position specified.
            Entities.AddEntity<ScoreDisplay>();
            IEntity entity = Entities.Entities[Entities.Entities.Count - 1];
            entity.transform.SetPosition(position);
        }

        public void CreateNewText(string textValue, Vector2 position)
        {
            // Create an Entity with a Text component, at the position specified.
            // and give it a text value.
            Entities.AddEntity<ScoreDisplay>();
            IEntity entity = Entities.Entities[Entities.Entities.Count - 1];
            entity.transform.SetPosition(position);
            Text text = entity.GetComponent<Text>() as Text;
            text.SetText(textValue);
        }

        public override void OnSceneActive()
        {
            base.OnSceneActive();
            // When this scene becomes active, get the latest score values from the ScoreManager and append the Leaderboard.
            for(int i = 0; i < Math.Min(ScoreManager.Instance.scoreData.Count, names.Count); i++)
            {
                // Set the text values for both lists.
                names[i].SetText(ScoreManager.Instance.scoreData[i].PlayerName);
                scores[i].SetText(ScoreManager.Instance.scoreData[i].ToTimeSpan().ToString());
            }
        }
    }
}
