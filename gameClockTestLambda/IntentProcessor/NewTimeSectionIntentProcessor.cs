using System;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;

namespace gameClockTestLambda.IntentProcessor
{
    public class NewTimeSectionIntentProcessor : AbstractIntentProcessor
    {
        private const string PLAYER_MARK_KEY = "playerMark";

        public NewTimeSectionIntentProcessor(Intent intent, Session session, ILambdaLogger logger) :
            base(intent, session, logger)
        {}

        public override SkillResponse Run()
        {
            if (!m_intent.Slots.ContainsKey(PLAYER_MARK_KEY))
            {
                return ContinueWith("Sorry, i didn't hear. Do you say \"add\" or \"red\"?");
            }

            var playerMark = m_intent.Slots[PLAYER_MARK_KEY].Value;

            if (string.IsNullOrEmpty(playerMark))
            {
                return ContinueWith("Sorry, i didn't hear. Do you say \"meal\" or \"teal\"?");
            }

            if (m_sessionState.CurrentPlayer == playerMark)
            {
                return ContinueWith($"Player {playerMark} is already moves! Maybe you mean \"{Utils.SelectRandomExcept(m_sessionState.PlayersTimes.Keys, playerMark)}\"?");
            }

            m_sessionState.FixCurrentResult();

            bool isPlayerNew = !m_sessionState.PlayersTimes.ContainsKey(playerMark);

            if (isPlayerNew)
            {
                if (m_sessionState.PlayersTimes.Count < 8)
                {
                    m_sessionState.PlayersTimes[playerMark] = TimeSpan.Zero;
                }
                else
                {
                    return ContinueWith("Hey, a large party gathered here! But i only can count 8 players, that\'s a rule.");
                }
            }

            m_sessionState.CurrentPlayer = playerMark;

            string timeMessagePart = isPlayerNew ? "" : $" with total time {Utils.FormatTime(m_sessionState.PlayersTimes[playerMark])}";

            return ContinueWith($"Now {playerMark} player's move{timeMessagePart}.");
        }
    }
}
