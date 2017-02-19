using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI.Biz.Core.Bot.InvokeWrapper
{
    public class BLManagerInvokerBase
    {
        protected BLInvoker BLInvokerObject { get; set; }

        public BLManagerInvokerBase(string endPointUri, string user)
        {
            BLInvokerObject = new BLInvoker(endPointUri, user);
        }
    }
}
