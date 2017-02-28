using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Microsoft.Bot.Builder.FormFlow;

namespace test
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
               await  Conversation.SendAsync(activity, () => new CaseDialog());
            }
            else
            {
               // HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        [Serializable]
        public class CaseDialog : IDialog<object> {
            public CaseDialog() { 
            }

            public Task StartAsync(IDialogContext context)
            {
                throw new NotImplementedException();
            }
        }

    }
}