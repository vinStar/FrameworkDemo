using HyBy.FrameWork.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace HyBy.FrameWork.Test
{
    
    
    /// <summary>
    ///这是 EnumDescriptionAttributeTest 的测试类，旨在
    ///包含所有 EnumDescriptionAttributeTest 单元测试
    ///</summary>
    [TestClass()]
    public class EnumDescriptionAttributeTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///GetDescription 的测试
        ///</summary>
        [TestMethod()]
        public void GetDescriptionTest()
        {
            Type enumType = typeof(CommonDeclare.EnumLogOperateCatalog); 
            int enumIntValue = 0; 
            string expected = "添加"; 
            string actual;
            actual = EnumDescriptionAttribute.GetDescription(enumType, enumIntValue);
            Assert.AreEqual(expected, actual, "验证GetDescription方法的正确性。");
        }

        /// <summary>
        ///GetDescriptions 的测试
        ///</summary>
        [TestMethod()]
        public void GetDescriptionsTest()
        {
            Type enumType = typeof(CommonDeclare.EnumLogOperateCatalog); 
            
            Dictionary<int, string> actual;
            actual = EnumDescriptionAttribute.GetDescriptions(enumType);
            Assert.AreEqual("删除", actual[1], "验证GetDescriptions方法的正确性。");
        }

        /// <summary>
        ///GetEnumByDescription 的测试
        ///</summary>
        [TestMethod()]
        public void GetEnumByDescriptionTest()
        {
            Type enumType = typeof(CommonDeclare.EnumLogOperateCatalog); 
            string enumIntDesc = "添加"; 
            int expected = 0; 
            int actual;
            actual = EnumDescriptionAttribute.GetEnumByDescription(enumType, enumIntDesc);
            Assert.AreEqual(expected, actual, "验证GetEnumByDescription方法的正确性。");
        }

        /// <summary>
        ///GetEnumByValue 的测试
        ///</summary>
        [TestMethod()]
        public void GetEnumByValueTest()
        {
            Type enumType = typeof(CommonDeclare.EnumLogOperateCatalog);
            string enumIntDesc = "1";
            int expected = 0;
            int actual;
            actual = EnumDescriptionAttribute.GetEnumByDescription(enumType, enumIntDesc);
            Assert.AreEqual(expected, actual, "验证GetEnumByValue方法的正确性。");
        }

        /// <summary>
        ///GetValueText 的测试
        ///</summary>
        [TestMethod()]
        public void GetValueTextTest()
        {
            Type enumType = typeof(CommonDeclare.EnumLogOperateCatalog);
            int enumIntDesc = 0;
            string expected = "1";
            string actual;
            actual = EnumDescriptionAttribute.GetValueText(enumType, enumIntDesc);
            Assert.AreEqual(expected, actual, "验证GetValueText方法的正确性。");
        }

        /// <summary>
        ///GetValueTexts 的测试
        ///</summary>
        [TestMethod()]
        public void GetValueTextsTest()
        {
            Type enumType = typeof(CommonDeclare.EnumLogOperateCatalog);
            Dictionary<int, string> actual;
            actual = EnumDescriptionAttribute.GetValueTexts(enumType);
            Assert.AreEqual("2", actual[1], "验证GetValueTexts方法的正确性。");
        }
    }
}
