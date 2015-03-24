using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankA.Services.StatementFiles;

namespace BankA.Api.Tests.Services
{
    [TestClass]
    public class StatementFileServiceTest
    {
        [TestMethod]
        public void GetBankTest()
        {
            var svc = new StatementFileService();
            svc.GetStatementMap(1);

        }
    }
}
