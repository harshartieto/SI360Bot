using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace SI.Biz.Core.Bot.UnitTest.Case
{
    [TestClass]
    public class CaseManagerTests
    {

        private SI.Biz.Core.Bot.Compatibility.BotCaseManager _caseManagerInvoker;

        [TestInitialize]
        public void Initialize()
        {
            _caseManagerInvoker = new SI.Biz.Core.Bot.Compatibility.BotCaseManager();
        }

        [TestMethod]
        public void FindCaseMyNumber_Finds360CaseByItsNumber_ReturnsBotCaseObject()
        {
            var result = _caseManagerInvoker.FindCaseByNumber("no\\rd-super", "16/00001");
            Assert.IsNotNull(result);
            //Assert.IsTrue(!string.IsNullOrEmpty(caseDetail));
        }
    }
}
