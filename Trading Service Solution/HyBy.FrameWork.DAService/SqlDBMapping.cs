namespace HyBy.FrameWork.DAService
{
    using System;
    using System.Data;

    public class SqlDBMapping : DBMapping
    {
        private SqlDbType _type;

        public SqlDBMapping(string first, string second) : base(first, second)
        {
            this._type = SqlDbType.VarChar;
        }

        public SqlDBMapping(string first, string second, SqlDbType dbType) : base(first, second)
        {
            this._type = SqlDbType.VarChar;
            this._type = dbType;
        }

        public SqlDBMapping(string first, string second, SqlDbType dbType, int size) : base(first, second, DbType.String, size)
        {
            this._type = SqlDbType.VarChar;
            this._type = dbType;
        }

        public SqlDbType SqlType
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

