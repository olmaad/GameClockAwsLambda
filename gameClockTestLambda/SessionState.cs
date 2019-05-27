using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace gameClockTestLambda
{
    public class SessionState
    {
        public enum ClockStateEnum
        {
            ClockStateBegin,
            ClockStateRunning,
            ClockStatePaused
        }

        [JsonProperty("clockState")]
        public ClockStateEnum ClockState { get; set; } = ClockStateEnum.ClockStateBegin;

        [JsonProperty("currentPlayer")]
        public string CurrentPlayer { get; set; } = "";

        [JsonProperty("currentTimestamp")]
        public DateTime CurrentTimestamp { get; set; }

        [JsonProperty("playersTimes")]
        public Dictionary<string, TimeSpan> PlayersTimes { get; set; } = new Dictionary<string, TimeSpan>();

        public void Reset()
        {
            CurrentPlayer = "";
            PlayersTimes.Clear();
        }

        public void FixCurrentResult()
        {
            if (PlayersTimes.ContainsKey(CurrentPlayer))
            {
                PlayersTimes[CurrentPlayer] += (DateTime.Now - CurrentTimestamp);
            }

            CurrentTimestamp = DateTime.Now;
        }
    }
}
