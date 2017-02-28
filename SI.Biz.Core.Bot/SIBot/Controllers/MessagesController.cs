using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System.Collections.Generic;
using Microsoft.Bot.Builder.FormFlow;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SIBot
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
            if (activity != null && activity.GetActivityType() == ActivityTypes.Message)
            {
               await Conversation.SendAsync(activity, () => new RestwithLuis(activity));
               
            }
            else
            {
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
        [Serializable]
        [LuisModel("3d800e5e-736b-4ffd-82b5-86d0e56875ac", "c1c5093a4b384c1887b612d52a79c2f0")]
        public class RestwithLuis : LuisDialog<object>
        {
            const string url = "/api/bot/";
            Activity _activity;
            public RestwithLuis(Activity activity) {
                this._activity = activity;
            }
            [LuisIntent("FindCaseByNumber")]
            public async Task GetCaseByNumber(IDialogContext context, LuisResult result)
            {
                await context.PostAsync("Enter the case Number");
                context.Wait(RecieveCaseNumber);
            }
            public async Task RecieveCaseNumber(IDialogContext context, IAwaitable<IMessageActivity> argument)
            {
                var caseVariable = await argument;
                string caseNumber = caseVariable.Text;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:63526/");

                HttpResponseMessage response = await client.GetAsync(url + "/CaseByNumber?caseNumber=" + caseNumber);
                response.EnsureSuccessStatusCode();
                var obj = await response.Content.ReadAsAsync<BotCase>();
                BotCase caseObject = obj;
                await context.PostAsync("Title : " + caseObject.Title + "Description : " + caseObject.Description +"Name :" +caseObject.Name +"Responsible person : "+caseObject.OrgUnit.SearchName);
                context.Wait(MessageReceived);
            }

            [LuisIntent("FindCasesByTitle")]
            public async Task GetCaseByTitle(IDialogContext context, LuisResult result)
            {
                await context.PostAsync("Enter the case title");
                context.Wait(RecieveCaseTitle);
            }
            public async Task RecieveCaseTitle(IDialogContext context, IAwaitable<IMessageActivity> argument)
            {
                var caseVariable = await argument;
                string caseTitle = caseVariable.Text;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:63526/");

                HttpResponseMessage response = await client.GetAsync(url + "/CasesByTitle?title=" + caseTitle);
                response.EnsureSuccessStatusCode();
                var cases = await response.Content.ReadAsAsync<IEnumerable<BotCase>>();
                List<BotCase> caseList = cases.ToList();
                foreach(BotCase caseObject in caseList )
                await context.PostAsync("RecNo : " + caseObject.Recno + "Title : " + caseObject.Title + "Description : " + caseObject.Description + "Name :" + caseObject.Name + "Responsible person : " + caseObject.OurRef.SearchName);
                
                context.Wait(MessageReceived);
            }

            [LuisIntent("FindCasesByStatus")]
            public async Task GetCaseByStatus(IDialogContext context, LuisResult result)
            {
                //HttpClient client = new HttpClient();
                //client.BaseAddress = new Uri("http://localhost:63526/");

                //HttpResponseMessage response = await client.GetAsync(url + "/GetAllCaseStatus/");
                //response.EnsureSuccessStatusCode();
                
                //var caseStatuses = await response.Content.ReadAsAsync<IEnumerable<BotCaseStatus>>();
                //List<BotCaseStatus> casestatusList = caseStatuses.ToList();
                await Conversation.SendAsync(_activity, MakeRootDialog);
                context.Wait(MessageReceived);
            }
            internal static IDialog<JObject> MakeRootDialog()
            {
                return Chain.From(() => FormDialog.FromForm(CaseStatusFlow.BuildJsonFormExplicit));
            }
            [LuisIntent("")]
            public async Task None(IDialogContext context, LuisResult result)
            {
                await context.PostAsync("I have no idea what you are talking about.");
                context.Wait(MessageReceived);
            }

        }
    }
}