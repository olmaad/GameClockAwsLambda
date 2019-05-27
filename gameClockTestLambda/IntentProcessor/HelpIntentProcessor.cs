using System;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Amazon.Lambda.Core;

namespace gameClockTestLambda.IntentProcessor
{
    public class HelpIntentProcessor : AbstractIntentProcessor
    {
        public HelpIntentProcessor(Intent intent, Session session, ILambdaLogger logger) :
            base(intent, session, logger)
        {}

        public override SkillResponse Run()
        {
            return ContinueWith(@"Let me explain how to use that simple game clock. All players must choose own color. 
Then, you must tell me that color, when you starting your move. At move end next 
player must do the same thing. If you want to listen current results, who moves now, or put timer on 
pause - ask me. Say ""stop"" or ""exit"", when you end playing game.");
        }
    }
}
