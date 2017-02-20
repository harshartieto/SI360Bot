using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SI.Linq.Meta;

namespace SI.Biz.Core.Bot.InvokeWrapper.FakeImplementation.Case
{
    public class FakeCaseManagerInvoker : BLManagerInvokerBase
    {
        public FakeCaseManagerInvoker(string endpointUri, string user)
            : base(endpointUri, user)
        {
        }

         public BotCase FindCaseByNumber(string caseNumber)
        {
            var result = BLInvokerObject.Context.Case.Where(x => x.Recno == 123).Select(x => x.Recno).OprFirstOrDefault();
            return null;
        }

        
    }
}
