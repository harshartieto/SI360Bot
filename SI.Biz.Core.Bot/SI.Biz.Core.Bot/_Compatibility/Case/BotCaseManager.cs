
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

        #region Bot Dialog Invokes

        public string FindCaseByNumber(string id, string caseNumber)
        {
            var botCase = Invoke(id, () => SI.Biz.Core.Bot.BotCaseManager.FindCaseByNumber(caseNumber));
            return string.Empty;
        }

        public string FindCaseByStatus(string id, string caseStatus)
        {
            var botCase = Invoke(id, () => SI.Biz.Core.Bot.BotCaseManager.FindCasesByStatus(caseStatus));
            return string.Empty;
        }

        public string FindCasesByTitle(string id, string caseTitle)
        {
            var botCase = Invoke(id, () => SI.Biz.Core.Bot.BotCaseManager.FindCasesByTitle(caseTitle));
            return string.Empty;
        }

        public string GetMyOpenCases(string id, string userId)
        {
            var botCase = Invoke(id, () => SI.Biz.Core.Bot.BotCaseManager.GetMyOpenCases(userId));
            return string.Empty;
        }

        #endregion

        #region Bot Form Flow

        public string GetAllResponsiblePersons(string id, string searchName)
        {
            var botCase = Invoke(id, () => SI.Biz.Core.Bot.BotCaseManager.GetAllResponsiblePersons(searchName));
            return string.Empty;
        }

        public string FindCasesByResponsible(string id, string contactId)
        {
            var botCase = Invoke(id, () => SI.Biz.Core.Bot.BotCaseManager.FindCasesByResponsible(contactId));
            return string.Empty;
        }

        #endregion

    }
}
