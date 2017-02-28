using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI.Biz.Core.Bot
{
    [Serializable]
    public class BotCase
    {
        public int Recno { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Notes { get; set; }

        public BotContact OurRef { get; set; }

        public BotContact OrgUnit { get; set; }

        public string ScrapCode { get; set; }

        public int? PreserveYears { get; set; }

        public string AccessGroup { get; set; }

        public string Name { get; set; }
    }

    [Serializable]
    public class BotContact
    {
        public int Recno { get; set; }

        public string SearchName { get; set; }

        public string Email { get; set; }
    }

    [Serializable]
    public class BotCaseStatus
    {
        public int Recno { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }
    }
}
