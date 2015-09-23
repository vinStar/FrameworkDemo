
using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HyBy.FrameWork.DAService.ExCommon
{
    public class DbExecuteEntity
    {
        protected System.Data.CommandType _commandType;

        public DbExecuteEntity()
        {
            this._commandType = System.Data.CommandType.StoredProcedure;
            this.Conditions = new Hashtable();
        }

        public DbExecuteEntity(string searchID) : this()
        {
            this.SearchID = searchID;
        }

        public virtual void Clear()
        {
            this.SearchID = string.Empty;
            this.Conditions.Clear();
        }

        public virtual string ToSearchString([Optional, DefaultParameterValue("@")] string identify)
        {
            return this.SearchID;
        }

        public virtual System.Data.CommandType CommandType
        {
            get
            {
                return this._commandType;
            }
            set
            {
            }
        }

        public IEnumerable ConditionKeys
        {
            get
            {
                return this.Conditions.Keys;
            }
        }

        protected Hashtable Conditions { get; private set; }

        public virtual object this[string key]
        {
            get
            {
                if (this.Conditions.ContainsKey(key))
                {
                    return this.Conditions[key];
                }
                return null;
            }
            set
            {
                if (this.Conditions.ContainsKey(key))
                {
                    this.Conditions[key] = value;
                }
                else
                {
                    this.Conditions.Add(key, value);
                }
            }
        }

        public string SearchID { get; set; }
    }
}

