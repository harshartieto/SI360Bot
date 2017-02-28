
using SI.Biz.Core.Bot.Case.RepositoryLogics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI.Biz.Core.Bot.Case
{

    interface IBotCaseRepository
    {
        
        BotCase FindCaseByNumber(string caseNumber, IFindCaseByNumber objCaseByNumber);

        IEnumerable<BotCase> FindCasesByTitle(string title, IFindCasesByTitle objCaseByTitle);

        IEnumerable<BotCase> FindCasesByStatus(string status, IFindCasesByStatus objCaseByStatus);

        IEnumerable<BotCase> GetMyOpenCases(string userId, IFindMyOpenCases objGetMyOpenCases);

        IEnumerable<BotCase> FindCasesByResponsible(string ourRefKey, IFindCasesByResponsible objCaseByResponsible);

        IEnumerable<BotContact> GetAllResponsiblePersons(string searchName, IFindAllResponsiblePersons objCaseByClassCode);
        IEnumerable<BotCaseStatus> GetAllCaseStatus(IFindAllCaseStatus objGetAllCaseStatus);
    }

    class BotCaseRepository : IBotCaseRepository
    {
        public IEnumerable<BotCaseStatus> GetAllCaseStatus(IFindAllCaseStatus objGetAllCaseStatus) {
            return objGetAllCaseStatus.GetAllCaseStatus();
        }

        public IEnumerable<BotCase> GetMyOpenCases(string userId, IFindMyOpenCases objGetMyOpenCases)
        {
            return objGetMyOpenCases.GetMyOpenCases(userId);
        }

        public IEnumerable<BotContact> GetAllResponsiblePersons(string searchName, IFindAllResponsiblePersons objAllResponsiblePersons)
        {
            return objAllResponsiblePersons.GetAllResponsiblePersons(searchName);
        }

        public BotCase FindCaseByNumber(string caseNumber, IFindCaseByNumber objCaseByNumber)
        {
            return objCaseByNumber.GetCaseByNumber(caseNumber);
        }

        public IEnumerable<BotCase> FindCasesByTitle(string title, IFindCasesByTitle objCaseByTitle)
        {
            return objCaseByTitle.GetCasesByTitle(title);
        }

        public IEnumerable<BotCase> FindCasesByResponsible(string ourRefKey, IFindCasesByResponsible objCaseByResponsible)
        {
            return objCaseByResponsible.GetCasesByResponsible(ourRefKey);
        }

        public IEnumerable<BotCase> FindCasesByStatus(string status, IFindCasesByStatus objCaseByStatus)
        {
            return objCaseByStatus.GetCasesByStatus(status);
        }
    }
}
