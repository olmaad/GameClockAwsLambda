using System;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using gameClockTestLambda.IntentProcessor;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace gameClockTestLambda
{
    public class Function
    {
        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            var logger = context.Logger;

            switch (input.Request)
            {
                case LaunchRequest launchRequest: return HandleLaunch(launchRequest, logger);
                case IntentRequest intentRequest: return HandleIntent(intentRequest, input.Session, logger);
                case SessionEndedRequest endedRequest: return HandleEnded(endedRequest, logger);
            }

            throw new NotImplementedException($"Unknown request type - {input.Request.Type}");
        }

        private SkillResponse HandleLaunch(LaunchRequest launchRequest, ILambdaLogger logger)
        {
            logger.LogLine($"LaunchRequest made");

            return ResponseBuilder.Ask("Ok. When you will be ready to start - tell me first player color.", null, new Session
            {
                Attributes = new System.Collections.Generic.Dictionary<string, object>
                {
                    { "sessionState", new SessionState() }
                }
            });
        }

        private SkillResponse HandleIntent(IntentRequest intentRequest, Session session, ILambdaLogger logger)
        {
            try
            {
                var intent = intentRequest.Intent;

                IIntentProcessor processor;

                switch (intentRequest.Intent.Name)
                {
                    case "NewTimeSectionIntent":
                        {
                            processor = new NewTimeSectionIntentProcessor(intent, session, logger);
                            break;
                        }
                    case "CurrentPlayerIntent":
                        {
                            processor = new CurrentPlayerIntentProcessor(intent, session, logger);
                            break;
                        }
                    case "ResultIntent":
                        {
                            processor = new ResultIntentProcessor(intent, session, logger);
                            break;
                        }
                    case "PauseIntent":
                        {
                            processor = new PauseIntentProcessor(intent, session, logger);
                            break;
                        }
                    case "ResetIntent":
                        {
                            processor = new ResetIntentProcessor(intent, session, logger);
                            break;
                        }
                    case "AMAZON.HelpIntent":
                        {
                            processor = new HelpIntentProcessor(intent, session, logger);
                            break;
                        }
                    case "AMAZON.CancelIntent":
                        {
                            processor = new StopIntentProcessor(intent, session, logger);
                            break;
                        }
                    case "AMAZON.StopIntent":
                        {
                            processor = new StopIntentProcessor(intent, session, logger);
                            break;
                        }
                    case "AMAZON.NavigateHomeIntent":
                        {
                            processor = new HomeIntentProcessor(intent, session, logger);
                            break;
                        }
                    default:
                        {
                            processor = new FallbackIntentProcessor(intent, session, logger);
                            break;
                        }
                }

                return processor.Run();
            }
            catch (Exception e)
            {
                return ResponseBuilder.Tell($"I\'m broke :( Can you report it - <{e.Message}>, to developer, please?");
            }
        }

        private SkillResponse HandleEnded(SessionEndedRequest endedRequest, ILambdaLogger logger)
        {
            logger.LogLine($"EndedRequest made");

            return null;
        }
    }

}
