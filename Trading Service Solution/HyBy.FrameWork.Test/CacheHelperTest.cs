using HyBy.FrameWork.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace HyBy.FrameWork.Test
{
    
    
    /// <summary>
    ///这是 CacheHelperTest 的测试类，旨在
    ///包含所有 CacheHelperTest 单元测试
    ///</summary>
    [TestClass()]
    public class CacheHelperTest
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
        ///SetCache 的测试
        ///</summary>
        [TestMethod()]
        public void SetCacheTest()
        {
            string CacheKey = "HyBy1@123";
            object expected = "rxzs1.com";
            object actual;
            CacheHelper.SetCache(CacheKey, expected);
            actual = CacheHelper.GetCache(CacheKey);
            Assert.AreEqual(expected, actual, "验证设置缓存和获取缓存方法的正确性。");
        }

        /// <summary>
        ///SetCache 的测试,带过期时间
        ///</summary>
        [TestMethod()]
        public void SetCacheTimeOutTest()
        {
            string CacheKey = "HyBy2@123";
            object expected = "rxzs2.com";
            object actual;
            CacheHelper.SetCache(CacheKey, expected, new TimeSpan(0, 0, 2));
            actual = CacheHelper.GetCache(CacheKey);
            Assert.AreEqual(expected, actual);

            Thread.Sleep(2000);

            actual = CacheHelper.GetCache(CacheKey);
            Assert.IsNull(actual, "验证设置缓存和获取缓存方法（带过期时间）的正确性。");

        }

        /// <summary>
        ///SetCache 的测试,带绝对过期时间
        ///</summary>
        [TestMethod()]
        public void SetCacheAbsTimeOutTest()
        {
            string CacheKey = "HyBy3@123";
            object expected = "rxzs3.com";
            object actual;
            CacheHelper.SetCache(CacheKey, expected, DateTime.Now.AddSeconds(2), System.Web.Caching.Cache.NoSlidingExpiration);
            actual = CacheHelper.GetCache(CacheKey);
            Assert.AreEqual(expected, actual);

            Thread.Sleep(2000);

            actual = CacheHelper.GetCache(CacheKey);
            Assert.IsNull(actual, "验证设置缓存和获取缓存方法（带绝对过期时间）的正确性。");

        }

        /// <summary>
        ///RemoveAllCache 的测试
        ///</summary>
        [TestMethod()]
        public void RemoveAllCacheTest()
        {
            string CacheKey = "HyBy4@123";
            object expected = "rxzs4.com";
            object actual;
            CacheHelper.SetCache(CacheKey, expected);
            actual = CacheHelper.GetCache(CacheKey);
            Assert.AreEqual(expected, actual);

            CacheHelper.RemoveAllCache(CacheKey);
            actual = CacheHelper.GetCache(CacheKey);
            Assert.IsNull(actual, "验证移除缓存方法的正确性。");

            CacheHelper.SetCache(CacheKey, expected);
            actual = CacheHelper.GetCache(CacheKey);
            Assert.AreEqual(expected, actual);

            CacheHelper.RemoveAllCache();
            actual = CacheHelper.GetCache(CacheKey);
            Assert.IsNull(actual, "验证移除缓存方法的正确性。");

        }

    }
}
