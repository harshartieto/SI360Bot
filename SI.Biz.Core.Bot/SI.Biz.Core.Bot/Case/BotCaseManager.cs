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

        public static List<BotCase> FindCasesByResponsible(string ourRefKey)
        {
            var caseRepository = GetBotRepositoryObject();
            return caseRepository.FindCasesByResponsible(ourRefKey, new FindCasesByResponsible()).ToList();
        }

        public static List<BotCase> FindCasesByStatus(string statusKey)
        {
            var caseRepository = GetBotRepositoryObject();
            return caseRepository.FindCasesByStatus(statusKey, new FindCasesByStatus()).ToList();
        }

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

        public static List<BotContact> GetAllResponsiblePersons(string searchName)
        {
            var caseRepository = GetBotRepositoryObject();
            return caseRepository.GetAllResponsiblePersons(searchName, new FindAllResponsiblePersons()).ToList();
        }

        public static List<BotCaseStatus> GetAllCaseStatus() {

            var caseRepository = GetBotRepositoryObject();
            return caseRepository.GetAllCaseStatus(new FindAllCaseStatus()).ToList();
        }

        private static BotCaseRepository GetBotRepositoryObject()
        {
            return new BotCaseRepository();
        }
        
    }


}
