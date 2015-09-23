using HyBy.FrameWork.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;

namespace HyBy.FrameWork.Test
{
    
    
    /// <summary>
    ///这是 DataTableHelperTest 的测试类，旨在
    ///包含所有 DataTableHelperTest 单元测试
    ///</summary>
    [TestClass()]
    public class DataTableHelperTest
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
        ///ToDataTable 的测试
        ///</summary>
        [TestMethod()]
        public void ToDataTableTest()
        {
            List<TestEntity> list = new List<TestEntity>();
            TestEntity entity = new TestEntity();
            entity.id = 1;
            entity.name = "a";
            list.Add(entity);
            entity = new TestEntity();
            entity.id = 2;
            entity.name = "b";
            list.Add(entity);
            entity = new TestEntity();
            entity.id = 3;
            entity.name = "c";
            list.Add(entity);

            int expected = 3; 
            int actual;
            actual = list.ToDataTable<TestEntity>().Rows.Count;
            Assert.AreEqual(expected, actual, "验证ToDataTable方法的正确性。");
            
        }

        /// <summary>
        ///ToList 的测试
        ///</summary>
        [TestMethod()]
        public void ToListTest()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", Type.GetType("System.Int32"));
            dt.Columns.Add("name", Type.GetType("System.String"));
            DataRow dr = dt.NewRow();
            dr[0] = 1;
            dr[1] = "a";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = 2;
            dr[1] = "b";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = 3;
            dr[1] = "c";
            dt.Rows.Add(dr);

            int expected = 3;
            int actual;
            actual = dt.ToList<TestEntity>().Count;
            Assert.AreEqual(expected, actual, "验证ToList方法的正确性。");

        }

    }

    public class TestEntity {
        public int id { get; set; }
        public string name { get; set; }
    }
}
