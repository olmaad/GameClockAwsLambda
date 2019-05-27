using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;

namespace gameClockTestLambda.IntentProcessor
{
    public class PauseIntentProcessor : AbstractIntentProcessor
    {
        public PauseIntentProcessor(Intent intent, Session session, ILambdaLogger logger) :
            base(intent, session, logger)
        {}

        public override SkillResponse Run()
        {
            m_sessionState.FixCurrentResult();
            m_sessionState.CurrentPlayer = "";

            return ContinueWith("I set timer on pause.");
        }
    }
}
