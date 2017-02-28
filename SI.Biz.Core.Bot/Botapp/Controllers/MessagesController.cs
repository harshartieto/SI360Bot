using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Dialogs;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Botapp
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
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                Activity istypingreply = activity.CreateReply();
                istypingreply.Type = ActivityTypes.Typing;
                istypingreply.Text = "typing...";
                await connector.Conversations.ReplyToActivityAsync(istypingreply);

                await Conversation.SendAsync(activity, () => new CaseDialog());

            }
            else
            {
               // HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        [Serializable]
        public class CaseDialog : IDialog<object>
        {
            const string url = "/api/bot/";
            //Activity caseactivity;
            public CaseDialog()
            {
                //caseactivity = activity;
            }
            public async Task StartAsync(IDialogContext context)
            {
                await context.PostAsync("Hi,how can i help You...");
                context.Wait(MessageReceivedStart);
            }
            public async Task MessageReceivedStart(IDialogContext context, IAwaitable<IMessageActivity> argument)
            {
                var message = await argument;
                if (message.Text.Contains("number"))
                {
                    await context.PostAsync("Enter case number:");
                    context.Wait(GetCaseByNumber);
                }
                else if (message.Text.Contains("title"))
                {
                    await context.PostAsync("Enter case name:");
                   // context.Wait(GetCaseByTitle);
                }

                else if (message.Text.Contains("status"))
                {
                   // context.Wait(GetCaseBystatus);
                }

                else if (message.Text.Contains("responsible") || message.Text.Contains("person"))
                {
                    await context.PostAsync("Enter responsible person's name");
                    //context.Wait(GetCaseByResponsible);
                }

                else if (message.Text.Equals("Find case info by number", StringComparison.InvariantCultureIgnoreCase))
                {
                    await context.PostAsync("Enter case number:");

                    context.Wait(GetCaseByNumber);
                }
                else if (message.Text.Equals("Find case info by title", StringComparison.InvariantCultureIgnoreCase))
                {
                    await context.PostAsync("Enter case name:");
                  //  context.Wait(GetCaseByTitle);
                }

                else
                {
                    context.Wait(MessageReceivedStart);
                }
            }
            public async Task GetCaseByNumber(IDialogContext context, IAwaitable<IMessageActivity> argument)
            {
                var caseVariable = await argument;
                string caseNumber = caseVariable.Text;

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:63526/");

                HttpResponseMessage response = await client.GetAsync(url + "/CaseByNumber?caseNumber=" + caseNumber);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                result = result.Replace("k__BackingField", "").Replace("<", "").Replace(">", "");
                var jSerializer = new JavaScriptSerializer();
                var caseObject = jSerializer.Deserialize<BotCase>(result);

                var replyMessage = context.MakeMessage();
                replyMessage.Text = " Here is the case info i found..";

                replyMessage.Attachments = new List<Attachment>();
                List<CardAction> cardButtons = new List<CardAction>();
                CardAction caseButton = new CardAction()
                {
                    Value = "https://en.wikipedia.org/wiki/Pig_Latin",
                    Type = "openUrl",
                    Title = "More details"
                };
                cardButtons.Add(caseButton);
                HeroCard a = new HeroCard();

                HeroCard caseCard = new HeroCard()
                {
                    Title = caseNumber + "  :  " + caseObject.Description,
                    Subtitle = caseObject.Title,
                    Buttons = cardButtons,
                    Text = caseObject.OrgUnit.SearchName
                };

                Attachment caseAttachment = caseCard.ToAttachment();
                replyMessage.Attachments.Add(caseAttachment);

                await context.PostAsync(replyMessage);
                context.Wait(MessageReceivedStart);
            }
            
        }
    }
}