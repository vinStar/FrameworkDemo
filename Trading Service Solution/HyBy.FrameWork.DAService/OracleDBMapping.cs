namespace HyBy.FrameWork.DAService
{
    using System;
    using System.Data;
    using System.Data.OracleClient;

    public class OracleDBMapping : DBMapping
    {
        private System.Data.OracleClient.OracleType _type;

        public OracleDBMapping(string first, string second) : base(first, second)
        {
            this._type = System.Data.OracleClient.OracleType.VarChar;
        }

        public OracleDBMapping(string first, string second, System.Data.OracleClient.OracleType dbType) : base(first, second)
        {
            this._type = System.Data.OracleClient.OracleType.VarChar;
            this._type = dbType;
        }

        public OracleDBMapping(string first, string second, System.Data.OracleClient.OracleType dbType, int size) : base(first, second, DbType.String, size)
        {
            this._type = System.Data.OracleClient.OracleType.VarChar;
            this._type = dbType;
        }

        public System.Data.OracleClient.OracleType OracleType
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

