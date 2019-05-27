using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;

namespace gameClockTestLambda.IntentProcessor
{
    public class CurrentPlayerIntentProcessor : AbstractIntentProcessor
    {
        public CurrentPlayerIntentProcessor(Intent intent, Session session, ILambdaLogger logger) :
            base(intent, session, logger)
        {}

        public override SkillResponse Run()
        {
            if (string.IsNullOrEmpty(m_sessionState.CurrentPlayer))
            {
                return ContinueWith("Nobody moves now.");
            }

            m_sessionState.FixCurrentResult();

            return ContinueWith($"Now moves {m_sessionState.CurrentPlayer} player with total time {Utils.FormatTime(m_sessionState.PlayersTimes[m_sessionState.CurrentPlayer])}.");
        }
    }
}
