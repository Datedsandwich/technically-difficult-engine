using Microsoft.Xna.Framework.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TechnicallyDifficult.GameData
{
    public class ScoreManager
    {
        private static ScoreManager instance;       // Singleton.

        public static ScoreManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScoreManager();

                return instance;
            }
        }


        public List<ScoreData> scoreData;
        public string fileName;

        public ScoreManager()
        {
            Initialize();
        }

        public void Initialize()
        {
            fileName = "Scores.xml";
            // Get the path of the save game
            string fullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

            // Check to see if the file exists
            if (!File.Exists(fullPath))
            {
                // If the file doesn't exist, make a fake one...
                // Create the data to save
                List<ScoreData> data = new List<ScoreData>();
                // Totally real data values that we actually did.
                // For real.
                data.Add(new ScoreData("Luke", new TimeSpan(10, 05, 57)));
                data.Add(new ScoreData("Livvy", new TimeSpan(9, 48, 82)));
                data.Add(new ScoreData("Richard", new TimeSpan(6, 24, 39)));
                // Actually save the scores from above.
                SaveHighScores(data, fileName);
            }

            scoreData = LoadHighScores(fileName);
        }

        public static void SaveHighScores(List<ScoreData> data, string fileName)
        {
            // Get the path of the save game
            string fullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

            // Open the file, creating a new file if there is not already one existing.
            FileStream stream = File.Open(fullPath, FileMode.OpenOrCreate);
            try
            {
                // Convert the object to XML data and put it in the stream
                XmlSerializer serializer = new XmlSerializer(typeof(List<ScoreData>));
                serializer.Serialize(stream, data);
            }
            finally
            {
                // Close the file
                stream.Close();
            }
        }

        public static List<ScoreData> LoadHighScores(string fileName)
        {
            List<ScoreData> data;

            // Get the path of the save game
            string fullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

            // Open the file
            FileStream stream = File.Open(fullPath, FileMode.OpenOrCreate,
            FileAccess.Read);
            try
            {
                // Read the data from the file
                XmlSerializer serializer = new XmlSerializer(typeof(List<ScoreData>));
                data = (List<ScoreData>)serializer.Deserialize(stream);
            }
            finally
            {
                // Close the file
                stream.Close();
            }
            // Return a reference to the data we just loaded.
            return (data);
        }

        public void AddHighScore(string name, TimeSpan score)
        {
            scoreData = LoadHighScores(fileName);
            // Add a new element to the Scores list
            scoreData.Add(new ScoreData(name, score));
            // Sort the scores, so the highest scores are at the top!
            scoreData = scoreData.OrderByDescending(o => o.totalSeconds).ToList<ScoreData>();
            // Save the ordered list to file.
            SaveHighScores(scoreData, fileName);
        }
    }
}
