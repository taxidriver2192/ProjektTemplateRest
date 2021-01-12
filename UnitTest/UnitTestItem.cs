using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTemplate.Controllers;
using TemplateLib.Models;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        private ItemsController _unitTestGenerateList;

        [TestInitialize]
        public void BeforeEachTest()
        {
            _unitTestGenerateList = new ItemsController();
        }

        [TestMethod]
        public void TestMethod1()
        {
            var list = new List<Item>(_unitTestGenerateList.Get());
            Assert.AreEqual(8, list.Count);
        }
    }
}