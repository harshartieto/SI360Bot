using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI.Biz.Core.Bot
{
    public class BotCase
    {
        public int Recno { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int? External { get; set; }

        public string Notes { get; set; }

        public int? OurRef { get; set; }
    }
}
