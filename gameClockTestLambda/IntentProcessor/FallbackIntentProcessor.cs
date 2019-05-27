using System;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;

namespace gameClockTestLambda.IntentProcessor
{
    public class FallbackIntentProcessor : AbstractIntentProcessor
    {
        public FallbackIntentProcessor(Intent intent, Session session, ILambdaLogger logger) :
            base(intent, session, logger)
        {}

        public override SkillResponse Run()
        {
            return ContinueWith("Sorry, i don\'t understand what you want.");
        }
    }
}
