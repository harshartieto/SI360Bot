using SI.Biz.Core.Bot.InvokeWrapper;
using SI.Biz.Core.Bot.InvokeWrapper.Case;
using SI.Biz.Core.Bot.InvokeWrapper.FakeImplementation.Case;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI.Biz.Core.Bot.TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            const string EndPointUri = "http://localhost:8088/SI.WS.Core/CommonHost.svc/ManagerFactory";

            using(SimpleImpersonation.Impersonation.LogonUser("no","rd-super","De02mo",SimpleImpersonation.LogonType.Service))
            {
                CaseManagerInvoker invoker = new CaseManagerInvoker(EndPointUri,"no\\rd-super");
                invoker.FindCaseByNumber("123");
            }
        }
    }
}
