using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechnicallyDifficult.GameData
{
    [Serializable]
    public struct ScoreData
    {
        public string PlayerName;                   // Name of the player
        public int hours;                           // The hours they kept the game going.
        public int minutes;                         // and the minutes
        public int seconds;                         // and the seconds

        public int totalSeconds;                    // Total Seconds the game was running for. This is used to order the list.

        public ScoreData(string playerName, TimeSpan score)
        {
            PlayerName = playerName;
            // Take the values from the TimeSpan and seperate them into integers.
            // TimeSpan is not serializable.
            hours = score.Hours;
            minutes = score.Minutes;
            seconds = score.Seconds;

            totalSeconds = (int)score.TotalSeconds;
        }

        public TimeSpan ToTimeSpan()
        {
            // Convert the integers back into a TimeSpan, for display.
            return new TimeSpan(hours, minutes, seconds);
        }
    }
}
