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
        public void Test_IntArrayToDelimitedString()
        {
            int[] nums = { 34, 45, 56 };
            var result = nums.ToCommaString();
            Assert.AreEqual("34,45,56", result);
        }
    }
}
