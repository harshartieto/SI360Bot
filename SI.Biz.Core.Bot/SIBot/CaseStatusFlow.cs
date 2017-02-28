using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.FormFlow.Json;
using Newtonsoft.Json.Linq;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace SIBot
{
    public class CaseStatusFlow
    {
        const string url = "/api/bot/";

        public static IForm<JObject> BuildJsonFormExplicit()
        {
            List<BotCaseStatus> statusList = new List<BotCaseStatus>();
            statusList = GetStatusList().Result.ToList();
            string jsonString = "{\"References\": [ \"Microsoft.Bot.Sample.AnnotatedSandwichBot.dll\"],\"Imports\": [ \"Microsoft.Bot.Sample.AnnotatedSandwichBot.Resource\"],\"type\": \"object\",\"required\": [\"Length\" ],\"Templates\": {\"NotUnderstood\": {\"Patterns\": [ \"I do not understand , Try again, I don't get\" ]},\"EnumSelectOne\": {\"Patterns\": [ \"Choose Status? {||}\" ],\"ChoiceStyle\": \"Auto\"}},\"properties\": {\"Length\": {\"Prompt\": {\"Patterns\": [ \"What size of sandwich do you want? {||}\" ]},\"type\": [\"string\",\"null\"],\"enum\": [";
            int length = statusList.Count();
            for (int i = 0; i < length; i++)
            {
                jsonString += "\"" + statusList[i].Code + "\"";
                if ((i + 1) == length)
                {
                    continue;
                }
                jsonString += ",";
            }
            jsonString += "]} }}";
            var schema = JObject.Parse(jsonString);
            OnCompletionAsyncDelegate<JObject> processOrder = async (context, state) =>
            {
                 
            };
            var builder = new FormBuilderJson(schema);
            return builder
                        .Message("Welcome to the sandwich order bot")
                        .Field("Length")
                         .Confirm(async (state) =>
                         {
                             return new PromptAttribute("");
                         })
                        .AddRemainingFields()
                        .Message("Thanks for ordering a sandwich!")
                        .OnCompletion(processOrder)
                .Build();
        }

        public static async Task<IEnumerable<BotCaseStatus>> GetStatusList() {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:63526/");

            HttpResponseMessage response = await client.GetAsync(url + "/GetAllCaseStatus/");
            response.EnsureSuccessStatusCode();

            var caseStatuses = await response.Content.ReadAsAsync<IEnumerable<BotCaseStatus>>();
            List<BotCaseStatus> casestatusList = caseStatuses.ToList();
            return casestatusList;
        }

    }
}