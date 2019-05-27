using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using System;

namespace gameClockTestLambda.IntentProcessor
{
    public abstract class AbstractIntentProcessor : IIntentProcessor
    {
        private const string SESSION_STATE_KEY = "sessionState";

        private Session m_session;

        protected readonly Intent m_intent;
        protected readonly SessionState m_sessionState;
        protected readonly ILambdaLogger m_logger;

        public abstract SkillResponse Run();

        public AbstractIntentProcessor(Intent intent, Session session, ILambdaLogger logger)
        {
            m_intent = intent;
            m_session = session;
            m_logger = logger;

            if (!session.Attributes.ContainsKey(SESSION_STATE_KEY))
            {
                throw new Exception("No state in session !");
            }

            m_sessionState = JsonConvert.DeserializeObject<SessionState>(session.Attributes[SESSION_STATE_KEY].ToString());
        }

        protected SkillResponse ContinueWith(string message)
        {
            m_session.Attributes[SESSION_STATE_KEY] = m_sessionState;

            return ResponseBuilder.Ask(message, null, m_session);
        }

        protected SkillResponse EndWith(string message)
        {
            m_session.Attributes[SESSION_STATE_KEY] = m_sessionState;

            return ResponseBuilder.Tell(message);
        }
    }
}
