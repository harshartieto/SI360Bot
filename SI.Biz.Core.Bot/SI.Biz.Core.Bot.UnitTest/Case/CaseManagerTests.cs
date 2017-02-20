using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SI.Biz.Core.Bot.InvokeWrapper.Case;
using SI.Biz.Core.Bot.InvokeWrapper.FakeImplementation.Case;

namespace SI.Biz.Core.Bot.UnitTest.Case
{
    [TestClass]
    public class CaseManagerTests 
    {

        private FakeCaseManagerInvoker _caseManagerInvoker;

        [TestInitialize]
        public void Initialize()
        {
            _caseManagerInvoker = new FakeCaseManagerInvoker(TestConstants.EndPointUri, TestConstants.User);
        }

        [TestMethod]
        public void FindCaseMyNumber_Finds360CaseByItsNumber_ReturnsBotCaseObject()
        {
            var result = _caseManagerInvoker.FindCaseByNumber("12");
            Assert.IsNotNull(result);
            //Assert.IsTrue(!string.IsNullOrEmpty(caseDetail));
        }
    }
}
