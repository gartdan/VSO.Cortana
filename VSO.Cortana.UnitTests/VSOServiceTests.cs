using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSO.Cortana.Service;

namespace VSO.Cortana.UnitTests
{
    [TestClass]
    public class VSOServiceTests
    {
        private string UserName;
        private string Password;

        public VSOServiceTests()
        {
            Init();
        }

        private void Init()
        {
            string accountInfo = "../../passwords.IGNORE.txt"; //my account info. don't check this into version control
            var lines = File.ReadAllLines(accountInfo);
            this.UserName = lines[0];
            this.Password = lines[1];
        }

        private VSOService GetVSOService()
        {
            return new VSOService("dgartner", this.UserName, this.Password);
        }

        [TestMethod]
        public async Task VSOService_RetrievesASingleWorkItemById()
        {
            int testId = 682;
            VSOService service = GetVSOService();
            var response = await service.GetWorkItemById(testId);
            Assert.IsNotNull(response);
            Assert.AreEqual(testId, response.Id);
        }



        [TestMethod]
        public async Task VSOService_RetrieveMultipleWorkItemsById()
        {
            int[] testIds = { 563, 564 };
            var service = GetVSOService();
            var response = await service.GetWorkItemsById(testIds);
            Assert.IsNotNull(response);
            Assert.AreEqual(2, response.Count());
        }
    }
}
