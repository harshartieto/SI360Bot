
using SI.Biz.Core.Compatibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI.Biz.Core.Bot.Compatibility
{
    public class BotCaseManager : LegacyManager
    {
        public string FindCaseByNumber(string id, string caseNumber)
        {
            var botCase = Invoke(id, () => SI.Biz.Core.Bot.BotCaseManager.FindCaseByNumber(caseNumber));
            return string.Empty;
        }

        public string GetMyOpenCases(string id, string userId)
        {
            var botCase = Invoke(id, () =>  SI.Biz.Core.Bot.BotCaseManager.GetMyOpenCases(userId));
            return string.Empty;
        }
    }
}
