namespace HyBy.FrameWork.DAService
{
    using HyBy.FrameWork.Common;
    using System;

    public class ConnectionString
    {
        public static string GetCurrentConnectionString()
        {
            return ConfigurationHelper.GetConnectionStringSettings("default").ConnectionString;
        }

        public static string GetOldSysConnectionString()
        {
            return ConfigurationHelper.GetConnectionStringSettings("oldsys").ConnectionString;
        }
    }
}

