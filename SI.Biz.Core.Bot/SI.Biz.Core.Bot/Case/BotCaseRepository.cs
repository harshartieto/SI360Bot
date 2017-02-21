
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

        IEnumerable<BotCase> FindCasesByStatus(int status, IFindCasesByStatus objCaseByStatus);

        IEnumerable<BotCase> GetMyOpenCases(string userId, IFindMyOpenCases objGetMyOpenCases);

        IEnumerable<BotCase> FindCasesByResponsible(int ourRefKey, IFindCasesByResponsible objCaseByResponsible);

        IEnumerable<BotContact> GetAllResponsiblePersons(string searchName, IFindAllResponsiblePersons objCaseByClassCode);

    }

    class BotCaseRepository : IBotCaseRepository
    {
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

        public IEnumerable<BotCase> FindCasesByResponsible(int ourRefKey, IFindCasesByResponsible objCaseByResponsible)
        {
            return objCaseByResponsible.GetCasesByResponsible(ourRefKey);
        }

        public IEnumerable<BotCase> FindCasesByStatus(int status, IFindCasesByStatus objCaseByStatus)
        {
            return objCaseByStatus.GetCasesByStatus(status);
        }
    }
}
