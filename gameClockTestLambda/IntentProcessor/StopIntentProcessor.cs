using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;

namespace gameClockTestLambda.IntentProcessor
{
    public class StopIntentProcessor : AbstractIntentProcessor
    {
        public StopIntentProcessor(Intent intent, Session session, ILambdaLogger logger) :
            base(intent, session, logger)
        {}

        public override SkillResponse Run()
        {
            return EndWith("I stopped timer. Let's play again some other time.");
        }
    }
}
