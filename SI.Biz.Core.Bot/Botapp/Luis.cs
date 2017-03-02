using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Botapp
{

    [LuisModel("68557cf1-5505-4e6e-a4fc-32d8b2c996b3", "f4f9679aa3034cfc832f42c9777c9368")]
    public class Luis : LuisDialog<object>
    {
        const string url = "/api/bot/";
        [LuisIntent("FindCaseByNumber")]
        public async Task GetCaseByNumberLuis(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Enter  case Number");
            context.Wait(MessageReceived);
        }
    }
}