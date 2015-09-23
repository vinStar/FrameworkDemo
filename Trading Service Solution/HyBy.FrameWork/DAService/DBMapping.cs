using System.Data;
 
namespace HyBy.FrameWork.DAService
{
    public class DBMapping
    {
        protected string _first;
        protected string _second;
        protected int _size;
        protected DbType _type;
        
        public DBMapping()
        {
        }

        public DBMapping(string first, string second) : this(first, second, DbType.String, 0)
        {
            this._first = first;
            this._second = second;
        }

        public DBMapping(string first, string second, DbType type) : this(first, second, type, 0)
        {
        }

        public DBMapping(string first, string second, DbType type, int size)
        {
            this._first = first;
            this._second = second;
            this._type = type;
            this._size = size;
        }

        public virtual string First
        {
            get
            {
                return this._first;
            }
            set
            {
                this._first = value;
            }
        }

        public System.Data.OracleClient.OracleType OracleType { get; set; }

        public virtual string Second
        {
            get
            {
                return this._second;
            }
            set
            {
                this._second = value;
            }
        }

        public virtual int Size
        {
            get
            {
                return this._size;
            }
            set
            {
                this._size = value;
            }
        }

        public SqlDbType SqlType { get; set; }

        public DbType Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
    }
}

