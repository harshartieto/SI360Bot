using SI.Biz.Core.Bot.Case;
using SI.Biz.Core.Bot.Case.RepositoryLogics;
using SI.Biz.Core.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SI.Biz.Core.Bot
{
    /// <summary>
    ///This class contains a
    /// </summary>
    public class BotCaseManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<BotCase> GetMyOpenCases(string userId)
        {
            var caseRepository = GetBotRepositoryObject();
            return caseRepository.GetMyOpenCases(userId, new FindMyOpenCases()).ToList();
        }

        public static List<BotCase> FindMyCasesFromLastWeek(string userId)
        {
            var caseRepository = GetBotRepositoryObject();
            return caseRepository.FindMyCasesFromLastWeek(userId, new FindMyCasesFromLastWeek()).ToList();
        }

        public static BotCase FindCaseByNumber(string caseId)
        {
            var caseRepository = GetBotRepositoryObject();
            return caseRepository.FindCaseByNumber(caseId, new FindCaseByNumber());
        }

        public static List<BotCase> FindCasesByTitle(string title)
        {
            var caseRepository = GetBotRepositoryObject();
            return caseRepository.FindCasesByTitle(title, new FindCasesByTitle()).ToList();
        }

        public static List<BotCase> FindCasesByResponsible(string responsiblePerson)
        {
            var caseRepository = GetBotRepositoryObject();
            return caseRepository.FindCasesByResponsible(responsiblePerson, new FindCasesByResponsible()).ToList();
        }

        public static List<BotCase> FindCasesByStatus(string status)
        {
            var caseRepository = GetBotRepositoryObject();
            return caseRepository.FindCasesByStatus(status, new FindCasesByStatus()).ToList();
        }

        public static List<BotCase> FindCasesByClassCode(string classCode)
        {
            var caseRepository = GetBotRepositoryObject();
            return caseRepository.FindCasesByClassCode(classCode, new FindCasesByClassCode()).ToList();
        }

        private static BotCaseRepository GetBotRepositoryObject()
        {
            return new BotCaseRepository();
        }
        
    }


}
