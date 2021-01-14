using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTemplate.Controllers;
using TemplateLib.Models;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        private ItemController _unitTestGenerateList;

        [TestInitialize]
        public void BeforeEachTest()
        {
            _unitTestGenerateList = new ItemController();
        }

        [TestMethod]
        public void Test8ItemsInList()
        {
            var list = new List<Food>(_unitTestGenerateList.Get());
            Assert.AreEqual(8, list.Count);
        }
    }
}