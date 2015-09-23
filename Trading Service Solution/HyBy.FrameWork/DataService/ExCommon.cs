using HyBy.FrameWork.DAService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyBy.FrameWork.DAService.ExCommon
{
    public enum OrderEnum
    {
        Asc,
        Desc
    }

    public class DbExecuteEntity
    {
        protected Hashtable Conditions { get; private set; }

        public string SearchID { get; set; }

        public string SqlText { get; set; }

        protected CommandType _commandType = CommandType.StoredProcedure;
        public virtual CommandType CommandType
        {
            get { return _commandType; }
            set { _commandType = value; }
        }

        public DbExecuteEntity()
        {
            Conditions = new Hashtable();
        }

        public DbExecuteEntity(string searchID):this()
        {
            this.SearchID = searchID;
        }

        public virtual object this[string key]
        {
            get
            {
                if (Conditions.ContainsKey(key))
                {
                    return Conditions[key];
                }
                return null;
            }
            set
            {
                if (Conditions.ContainsKey(key))
                {
                    Conditions[key] = value;
                }
                else
                {
                    Conditions.Add(key, value);
                }
            }
        }

        public virtual string ToSearchString(string identify="@")
        {
            return this.SearchID;
        }

        public virtual void Clear()
        {
            SearchID = string.Empty;
            Conditions.Clear();
        }

        public IEnumerable ConditionKeys
        {
            get { return Conditions.Keys; }
        }
    }

    public class SearchEntity:DbExecuteEntity
    {
        List<string> SearchItems { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public override CommandType CommandType
        {
            get
            {
                return base._commandType;
            }
            set
            {
                base._commandType = value;
            }
        }

        public Dictionary<string, OrderEnum> Orders { get; set; }

        public override object this[string key]
        {
            get
            {
                if (Conditions.ContainsKey(key))
                {
                    return Conditions[key];
                }
                return null;
            }
            set
            {
                if (Conditions.ContainsKey(key))
                {
                    if (value == null)
                    {
                        Conditions.Remove(key);
                    }
                    else
                    {
                        Conditions[key] = value;
                    }
                }
                else
                {
                    if (value != null)
                    {
                        Conditions.Add(key, value);
                    }
                }
            }
        }

        public string ExtensionCondition { get; set; }

        public SearchEntity():base()
        {
            base._commandType = CommandType.Text;
            SearchItems = new List<string>();
            Orders = new Dictionary<string, OrderEnum>();
        }

        public SearchEntity(string searchID)
            : this()
        {
            base.SearchID = searchID;
        }

        public SearchEntity(string searchID, CommandType ctype)
            : this(searchID)
        {
            base._commandType = ctype;
        }

        public void AddSearchItems(params string[] items)
        {
            SearchItems.AddRange(items);
        }

        public override void Clear()
        {
            base.Clear();

            SearchItems.Clear();
            ExtensionCondition = string.Empty;
            PageIndex = PageSize = 0;
            Orders.Clear();
            CommandType = CommandType.Text;
        }

        public override string ToSearchString(string identify = "@")
        {
            if (CommandType == CommandType.StoredProcedure)
            {
                return SearchID;
            }


            if (CommandType == CommandType.Text && !string.IsNullOrEmpty(SqlText))
            {
                return SqlText;
            }



            #region 初始化Search

            string search = string.Empty;
            if (SearchItems.Count > 0)
            {
                foreach (var s in SearchItems)
                {
                    search += (s + ",");
                }
                search = search.Substring(0, search.Length - 1);
            }
            else
            {
                search = "*";
            }

            #endregion

            #region 初始化Where

            string where = "1=1";
            foreach (var key in Conditions.Keys)
            {
                where += string.Format(" and {0}={1}{0}", key, identify);
            }
            if (!string.IsNullOrEmpty(ExtensionCondition))
            {
                where += " and " + ExtensionCondition;
            }

            #endregion

            #region 初始化Order

            string order = string.Empty;
            bool oexists = false;
            foreach (var o in Orders)
            {
                if (!oexists)
                {
                    order = string.Format("order by {0} {1}", o.Key, o.Value == OrderEnum.Asc ? "asc" : "desc");
                    oexists = true;
                }
                else
                {
                    order += string.Format(",{0} {1}", o.Key, o.Value == OrderEnum.Asc ? "asc" : "desc");
                }
            }

            #endregion

            #region 生成sql语句

            string sql = string.Empty;
            if (PageIndex == 0)//不分页查询
            {
                sql = string.Format("select {0} from {1} where {2} {3}", search, SearchID, where, order);
            }
            else
            {
                sql = string.Format("select s.* from (select {0},ROW_NUMBER() over({1}) row from {2} where {3})s where s.row>{4}startIndex and s.row<={4}endIndex;select @outcount=count(1) from {2} where {3};", search, order, SearchID, where, identify);
            }

            #endregion

            return sql;
            
        }
    }

    public class CommonResult
    {
        public Int64 ResultID { get; set; }

        public string Message { get; set; }
    }

    public class CommonResult<T>:IEnumerable<T>
    {
        int _recordCount = 0;
        public List<T> Datas { get; set; }

        public DataTable DTable { get; set; }

        public int RecordCount
        {
            get
            {
                if(PageCount==0)
                {
                    return Datas.Count;
                }
                else
                {
                    return _recordCount;
                }
            }
            set
            {
                _recordCount = value;
            }
        }

        public int PageCount { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            return Datas.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Datas.GetEnumerator();
        }

        public T this[int i]
        {
            get
            {
                return Datas[i];
            }
        }

        public Object Tag { get; set; }
    }
}
