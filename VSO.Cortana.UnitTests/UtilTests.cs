using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VSO.Cortana.Service.Util;

namespace VSO.Cortana.UnitTests
{
    [TestClass]
    public class UtilTests
    {
        [TestMethod]
        public void Test_IntArrayToDelimitedString_WorksForMultipleInts()
        {
            int[] nums = { 34, 45, 56 };
            var result = nums.ToCommaString();
            Assert.AreEqual("34,45,56", result);
        }

        [TestMethod]
        public void Test_IntArrayToDelimitedString_WorksForSingleInts()
        {
            int[] nums = { 34 };
            var result = nums.ToCommaString();
            Assert.AreEqual("34", result);
        }

        [TestMethod]
        public void BasicAuth_CreatesValueCorrectly()
        {
            string userName = "somedude";
            string password = "p@ssw3rd";
            var result = Auth.GetBasicAuthHeaderValue(userName, password);
            Assert.AreEqual("c29tZWR1ZGU6cEBzc3czcmQ=", result);
            
        }
    }
}
