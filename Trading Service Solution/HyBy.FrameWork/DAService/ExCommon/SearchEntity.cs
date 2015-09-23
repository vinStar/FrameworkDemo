namespace HyBy.FrameWork.DAService.ExCommon
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class SearchEntity : DbExecuteEntity
    {
        public SearchEntity()
        {
            base._commandType = System.Data.CommandType.Text;
            this.SearchItems = new List<string>();
            this.Orders = new Dictionary<string, OrderEnum>();
        }

        public SearchEntity(string searchID) : this()
        {
            base.SearchID = searchID;
        }

        public SearchEntity(string searchID, System.Data.CommandType ctype) : this(searchID)
        {
            base._commandType = ctype;
        }

        public void AddSearchItems(params string[] items)
        {
            this.SearchItems.AddRange(items);
        }

        public override void Clear()
        {
            base.Clear();
            this.SearchItems.Clear();
            this.ExtensionCondition = string.Empty;
            this.PageIndex = this.PageSize = 0;
            this.Orders.Clear();
            this.CommandType = System.Data.CommandType.Text;
        }

        public override string ToSearchString([Optional, DefaultParameterValue("@")] string identify)
        {
            if (this.CommandType == System.Data.CommandType.StoredProcedure)
            {
                return base.SearchID;
            }
            string str = string.Empty;
            if (this.SearchItems.Count > 0)
            {
                foreach (string str2 in this.SearchItems)
                {
                    str = str + str2 + ",";
                }
                str = str.Substring(0, str.Length - 1);
            }
            else
            {
                str = "*";
            }
            string str3 = "1=1";
            foreach (object obj2 in base.Conditions.Keys)
            {
                str3 = str3 + string.Format(" and {0}={1}{0}", obj2, identify);
            }
            if (!string.IsNullOrEmpty(this.ExtensionCondition))
            {
                str3 = str3 + " and " + this.ExtensionCondition;
            }
            string str4 = string.Empty;
            bool flag = false;
            foreach (KeyValuePair<string, OrderEnum> pair in this.Orders)
            {
                if (!flag)
                {
                    str4 = string.Format("order by {0} {1}", pair.Key, (((OrderEnum) pair.Value) == OrderEnum.Asc) ? "asc" : "desc");
                    flag = true;
                }
                else
                {
                    str4 = str4 + string.Format(",{0} {1}", pair.Key, (((OrderEnum) pair.Value) == OrderEnum.Asc) ? "asc" : "desc");
                }
            }
            string str5 = string.Empty;
            if (this.PageIndex == 0)
            {
                str5 = string.Format("select {0} from {1} where {2} {3}", new object[] { str, base.SearchID, str3, str4 });
            }
            else
            {
                str5 = string.Format("select s.* from (select {0},ROW_NUMBER() over({1}) row from {2} where {3})s where s.row>{4}startIndex and s.row<={4}endIndex;select @outcount=count(1) from {2} where {3};", new object[] { str, str4, base.SearchID, str3, identify });
            }
            return str5;
        }

        public override System.Data.CommandType CommandType
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

        public string ExtensionCondition { get; set; }

        public override object this[string key]
        {
            get
            {
                if (base.Conditions.ContainsKey(key))
                {
                    return base.Conditions[key];
                }
                return null;
            }
            set
            {
                if (base.Conditions.ContainsKey(key))
                {
                    if (value == null)
                    {
                        base.Conditions.Remove(key);
                    }
                    else
                    {
                        base.Conditions[key] = value;
                    }
                }
                else if (value != null)
                {
                    base.Conditions.Add(key, value);
                }
            }
        }

        public Dictionary<string, OrderEnum> Orders { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        private List<string> SearchItems { get; set; }
    }
}

