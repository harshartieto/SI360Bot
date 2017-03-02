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
    [LuisModel("269c348a-91e3-4506-bbac-c7a8ba7682f8", "c1c5093a4b384c1887b612d52a79c2f0")]
    [Serializable]
    public class Luisdialog : LuisDialog<object>
    {
        [LuisIntent("")]
        public async Task GetAll(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Enter the case");
            context.Wait(MessageReceived);
        }
        
        [LuisIntent("FindCaseByNumber")]
        public async Task GetCaseByNumber(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Enter the case number");
            context.Wait(GetCaseByNumber);
        }

        [LuisIntent("FindCaseByTitle")]
        public async Task GetCaseByTitle(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Enter the case name");
            context.Wait(GetCaseByTitle);
        }
        public List<BotCaseStatus> statusL;

        [LuisIntent("FindCaseByStatus")]
        public async Task GetCaseByStatus(IDialogContext context, LuisResult s)
        {
            //var statusform = new FormDialog<GetAllCaseStatus>(new GetAllCaseStatus(), GetAllCaseStatus.BuildForm, FormOptions.PromptInStart, null);
            //context.Call<GetAllCaseStatus>(statusform, FormComplete);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:63526/");

            HttpResponseMessage response = await client.GetAsync(url + "/GetAllCaseStatus/");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            result = result.Replace("k__BackingField", "").Replace("<", "").Replace(">", "");
            var jSerializer = new JavaScriptSerializer();
            var cases = jSerializer.Deserialize<IEnumerable<BotCaseStatus>>(result);
            List<string> statusList = new List<string>();
            foreach (BotCaseStatus status in cases)
            {
                statusList.Add(status.Code);
            }
            statusL = cases.ToList();
            PromptOptions<string> options = new PromptOptions<string>("Choose a status",
             "Sorry please try again", "", statusList, 2);
            PromptDialog.Choice<string>(context, FormComplete, options);
        }

        [LuisIntent("FindCaseByResponsible")]
        public async Task GetCaseByResponsibleLuis(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Enter responsible person's name");
            context.Wait(GetCaseByResponsible);
        }
        [LuisIntent("GetById")]
        public async Task GetById(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Enter the case Id");
            context.Wait(MessageReceived);
        }
        const string url = "/api/bot/";
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

        private async Task FormComplete(IDialogContext context, IAwaitable<string> result1)
        {
            var chosenStatus = await result1;
            string statusKey = "";
            foreach (BotCaseStatus status in statusL)
            {
                if (status.Code == chosenStatus)
                {
                    statusKey = status.Recno.ToString();
                }
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
            context.Wait(MessageReceived);


        }
        public async Task GetCaseByResponsible(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var caseVariable = await argument;
            string responsiblePerson = caseVariable.Text;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:63526/");

            HttpResponseMessage response = await client.GetAsync(url + "/GetAllResponsiblePersons?responsible=" + responsiblePerson);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            result = result.Replace("k__BackingField", "").Replace("<", "").Replace(">", "");
            var jSerializer = new JavaScriptSerializer();
            var contactInfo = jSerializer.Deserialize<IEnumerable<BotContact>>(result);
            List<BotContact> contactList = contactInfo.ToList();
            string reckno = "";
            foreach (var contact in contactList)
            {
                reckno = contact.Recno.ToString();
            }
            HttpResponseMessage res = await client.GetAsync(url + "/GetAllCasesOfResponsiblePerson?contactId=" + reckno);
            res.EnsureSuccessStatusCode();
            var result1 = await res.Content.ReadAsStringAsync();
            result = result1.Replace("k__BackingField", "").Replace("<", "").Replace(">", "");
            var jSerializer1 = new JavaScriptSerializer();
            var cases = jSerializer.Deserialize<IEnumerable<BotCase>>(result);
            List<BotCase> caseList = cases.ToList();

            var replyMessage = context.MakeMessage();
            replyMessage.Text = "  Here are the cases I found..";
            replyMessage.AttachmentLayout = "carousel";
            replyMessage.Attachments = new List<Attachment>();

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
            context.Wait(MessageReceived);
        }

    }
}