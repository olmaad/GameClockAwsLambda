using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using System;
using System.Linq;

namespace gameClockTestLambda.IntentProcessor
{
    public class ResultIntentProcessor : AbstractIntentProcessor
    {
        public ResultIntentProcessor(Intent intent, Session session, ILambdaLogger logger) :
            base(intent, session, logger)
        {}

        public override SkillResponse Run()
        {
            if (m_sessionState.PlayersTimes.Count == 0)
            {
                return ContinueWith("No results. Start timer having said player mark or say \"help\".");
            }

            m_sessionState.FixCurrentResult();

            string message = "Current results:";

            TimeSpan totalGameDuration = TimeSpan.Zero;

            for(int it = 0; it < m_sessionState.PlayersTimes.Count; ++it)
            {
                if (it != 0)
                {
                    message += ",";
                }

                var pair = m_sessionState.PlayersTimes.ElementAt(it);

                totalGameDuration += pair.Value;

                message += $" {pair.Key} - {Utils.FormatTime(pair.Value)}";
            }

            message += $". Total game duration - {Utils.FormatTime(totalGameDuration)}.";

            return ContinueWith(message);
        }
    }
}
