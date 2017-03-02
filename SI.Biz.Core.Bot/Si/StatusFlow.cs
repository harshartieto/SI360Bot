using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.FormFlow.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Si
{
    public class StatusFlow
    {
        //public static IForm<JObject> BuildJsonFormExplicit()
        //{
        //    Dictionary<string, int> caa = new Dictionary<string, int>();

        //    List<BotCaseStatus> statusList = new List<BotCaseStatus>();
        //    var obj1 = new BotCaseStatus(1, "e");
        //    var obj2 = new BotCaseStatus(2, "s");
        //    statusList.Add(obj1);
        //    statusList.Add(obj2);
        //    foreach (var i in statusList)
        //        caa.Add(i.Code, i.Recno);
        //    string stream = "{\"type\": \"object\",\"required\": [\"Length\" ],\"Templates\": {\"NotUnderstood\": {\"Patterns\": [ \"I do not understand , Try again, I don't get\" ]},\"EnumSelectOne\": {\"Patterns\": [ \"Choose Status? {||}\" ],\"ChoiceStyle\": \"Auto\"}},\"properties\": {\"Length\": {\"Prompt\": {\"Patterns\": [ \"What size of sandwich do you want? {||}\" ]},\"type\": [\"string\",\"null\"],\"enum\": [";
        //    int length = statusList.Count();
        //    for (int i = 0; i < length; i++)
        //    {
        //        stream += "\"" + statusList[i].Code + "\"";
        //        if ((i + 1) == length)
        //        {
        //            continue;
        //        }
        //        stream += ",";
        //    }
        //    // \"SixInch\",\"FootLong\"
        //    stream += "]} }}";
        //    var schema = JObject.Parse(stream);
           
        //    var builder = new FormBuilderJson(schema);
        //    //builder.Build();
        //}
    }
}