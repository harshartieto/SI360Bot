using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI.Biz.Core.Bot.InvokeWrapper.Case
{
    public class CaseManagerInvoker : BLManagerInvokerBase
    {
        public CaseManagerInvoker(string endpointUri, string user) : base(endpointUri,user)
        {
        }

        public BotCase FindCaseByNumber(string caseNumber)
        {
            return BLInvokerObject.Call(() => SI.Biz.Core.Bot.BotCaseManager.FindCaseByNumber(caseNumber));
        }

    }
}
