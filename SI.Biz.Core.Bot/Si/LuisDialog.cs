using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace Si
{
    [LuisModel("3d800e5e-736b-4ffd-82b5-86d0e56875ac", "c1c5093a4b384c1887b612d52a79c2f0")]
    [Serializable]
    public class Luisdialog : LuisDialog<object>
    {
        const string url = "/api/bot/";

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I have no idea what you are talking about.");
            context.Wait(MessageReceived);
        }


       [LuisIntent("FindCaseByNumber")]
        public async Task GetCaseByNumberLuis(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Enter  case Number");
            context.Wait(GetCaseByNumber);
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

           string url_360 = "http://localhost:3000/locator.aspx?name=DMS.Case.Details.Simplified.2&recno=RECNO&module=Case&subtype=2";
           url_360 = url_360.Replace("RECNO", caseObject.Recno.ToString());
           List<CardAction> cardButtons = new List<CardAction>();
           CardAction caseButton = new CardAction()
           {
               Value = url_360,
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
           context.Wait(MessageReceived);
       }


        [LuisIntent("FindCasesByTitle")]
        public async Task GetCaseByTitleLuis(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Enter the case title");
            context.Wait(GetCaseByTitle);
        }
        public async Task GetCaseByTitle(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {

            var caseVariable = await argument;
            string caseTitle = caseVariable.Text;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:63526/");

            HttpResponseMessage response = await client.GetAsync(url + "/CasesByTitle?title=" + caseTitle);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            result = result.Replace("k__BackingField", "").Replace("<", "").Replace(">", "");
            var jSerializer = new JavaScriptSerializer();
            var cases = jSerializer.Deserialize<IEnumerable<BotCase>>(result);

            var replyMessage = context.MakeMessage();
            replyMessage.Text = "  Here are the cases I found..";
            replyMessage.AttachmentLayout = "carousel";
            replyMessage.Attachments = new List<Attachment>();

            List<BotCase> caseList = cases.ToList();


            foreach (BotCase caseObject in caseList)
            {
                List<CardAction> cardButtons = new List<CardAction>();
                string url_360 = "http://localhost:3000/locator.aspx?name=DMS.Case.Details.Simplified.2&recno=RECNO&module=Case&subtype=2";
                url_360 = url_360.Replace("RECNO", caseObject.Recno.ToString());
                CardAction caseButton = new CardAction()
                {
                    Value = url_360,
                    Type = "openUrl",
                    Title = "More details"
                };
                cardButtons.Add(caseButton);
                HeroCard a = new HeroCard();

                HeroCard caseCard = new HeroCard()
                {
                    Title = caseObject.Title,
                    Subtitle = caseObject.Description,
                    Buttons = cardButtons,
                    Text = caseObject.OrgUnit.SearchName
                };
                Attachment caseAttachment = caseCard.ToAttachment();
                caseAttachment.ContentType = "application/vnd.microsoft.card.hero";

                replyMessage.Attachments.Add(caseAttachment);
            }
            await context.PostAsync(replyMessage);
            context.Wait(MessageReceived);
        }


        [LuisIntent("FindCasesByStatus")]
        public async Task GetCaseByStatusLuis(IDialogContext context, LuisResult result)
        {
            context.Wait(GetCaseBystatus);
        }
        public async Task GetCaseBystatus(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var r = await argument;
            var statusform = new FormDialog<GetAllCaseStatus>(new GetAllCaseStatus(), GetAllCaseStatus.BuildForm, FormOptions.PromptInStart, null);
            context.Call<GetAllCaseStatus>(statusform, FormComplete);
        }
        private async Task FormComplete(IDialogContext context, IAwaitable<GetAllCaseStatus> result1)
        {
            GetAllCaseStatus order = null;
            try
            {
                order = await result1;
                string status = order.status.ToString();
                string statusKey = "";
                switch (status)
                {
                    case "CA":
                        statusKey = "8";
                        break;
                    case "NF":
                        statusKey = "7";
                        break;
                    case "CE":
                        statusKey = "9";
                        break;
                    case "CC":
                        statusKey = "17";
                        break;
                    case "CL":
                        statusKey = "6";
                        break;
                    case "IP":
                        statusKey = "5";
                        break;
                    case "R":
                        statusKey = "4";
                        break;
                    default:
                        statusKey = "8";
                        break;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:63526/");

                HttpResponseMessage response = await client.GetAsync(url + "/CasesByStatus?statusKey=" + statusKey);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                result = result.Replace("k__BackingField", "").Replace("<", "").Replace(">", "");
                var jSerializer = new JavaScriptSerializer();
                var cases = jSerializer.Deserialize<IEnumerable<BotCase>>(result);
                var replyMessage = context.MakeMessage();
                replyMessage.Text = "  Here are the cases I found..";
                replyMessage.AttachmentLayout = "carousel";
                replyMessage.Attachments = new List<Attachment>();

                List<BotCase> caseList = cases.ToList();


                foreach (BotCase caseObject in caseList)
                {
                    string url_360 = "http://localhost:3000/locator.aspx?name=DMS.Case.Details.Simplified.2&recno=RECNO&module=Case&subtype=2";
                    url_360 = url_360.Replace("RECNO", caseObject.Recno.ToString());
                    List<CardAction> cardButtons = new List<CardAction>();
                    CardAction caseButton = new CardAction()
                    {
                        Value = url_360,
                        Type = "openUrl",
                        Title = "More details"
                    };
                    cardButtons.Add(caseButton);
                    HeroCard a = new HeroCard();

                    HeroCard caseCard = new HeroCard()
                    {
                        Title = caseObject.Title,
                        Subtitle = caseObject.Description,
                        Buttons = cardButtons,
                        Text = caseObject.OrgUnit.SearchName
                    };
                    Attachment caseAttachment = caseCard.ToAttachment();
                    caseAttachment.ContentType = "application/vnd.microsoft.card.hero";

                    replyMessage.Attachments.Add(caseAttachment);
                }
                await context.PostAsync(replyMessage);
            }
            catch (OperationCanceledException)
            {
                // await context.PostAsync("You canceled the form!");
                return;
            }

            if (order != null)
            {
                // await context.PostAsync("Your Pizza Order: " + order.ToString());
            }
            else
            {
                await context.PostAsync("Form returned empty response!");
            }

            context.Wait(MessageReceived);
        }

    }
}