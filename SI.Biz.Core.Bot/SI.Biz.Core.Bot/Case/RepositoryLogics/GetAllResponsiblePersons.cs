using SI.Biz.Core.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SI.Linq.Meta;

namespace SI.Biz.Core.Bot.Case.RepositoryLogics
{
    interface IFindAllResponsiblePersons
    {
        IEnumerable<SI.Biz.Core.Bot.BotContact> GetAllResponsiblePersons(string responsible);
    }

    class FindAllResponsiblePersons : IFindAllResponsiblePersons
    {
        public IEnumerable<BotContact> GetAllResponsiblePersons(string responsible)
        {
            var responsibles = Get.Context.Contact.Where(ct => ct.SearchName.OprLike(responsible))
                            .Select(ct => new { ct.Recno, ct.SearchName, ct.Email });

            var responsibleList = new List<BotContact>();
            responsibles.ForEach(x => responsibleList.Add(new BotContact { Recno = x.Recno, Email = x.Email, SearchName = x.SearchName }));
            return responsibleList;
        }
    }
}
