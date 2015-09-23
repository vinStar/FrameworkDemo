using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HyBy.FrameWork.Common
{
    public class OracleParameterHelper
    {
        public static string FormatParameter(CommandType type, string parameter)
        {
            switch (type)
            {
                case CommandType.Text:
                    if (parameter.Length < 2)
                        return string.Empty;
                    if (parameter.Substring(0, 2).ToLower() != "v_")
                    {
                        if (parameter.IndexOf(':') == 0)
                            return parameter.Substring(1);
                        else
                            return parameter;
                    }
                    return parameter.Substring(2);
                case CommandType.StoredProcedure:
                    if (parameter.Length < 2)
                        return string.Empty;
                    if (parameter.Substring(0, 2).ToLower() != "v_")
                    {
                        if (parameter.IndexOf(':') == 0)
                            return "V_" + parameter.Substring(1);
                        else
                            return "V_" + parameter;
                    }
                    return parameter;
                default:
                    break;
            }
            return parameter;
        }
    }
}
