using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace HyBy.FrameWork.Common
{
    /// <summary>
    /// 配置文件访问器
    /// 通过此类的方法读取指定位置的配置文件
    /// </summary>
    public class ConfigurationHelper
    {
        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="configPath">配置文件地址</param>
        /// <returns>将文件读到到类System.Configuration.Configuration中</returns>
        public static System.Configuration.Configuration GetConfiguration()
        {
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
            string path = AppDomain.CurrentDomain.BaseDirectory;
            if (!File.Exists(path + CommonDeclare.ConfigurationFileName))
            {
                path = path.Remove(path.Length - 1);
                path = path.Substring(0, path.LastIndexOf('\\') + 1);
            }
            if (!File.Exists(path + CommonDeclare.ConfigurationFileName))
            {
                path = path.Remove(path.Length - 1);
                path = path.Substring(0, path.LastIndexOf('\\') + 1);
            }
            path += CommonDeclare.ConfigurationFileName;
            configFileMap.ExeConfigFilename = path;
            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            return config;
        }

        public static System.Configuration.ConnectionStringSettings GetConnectionStringSettings(string name)
        {
            System.Configuration.Configuration config = GetConfiguration();

            return config.ConnectionStrings.ConnectionStrings[name];
        }

        public static string GetAppSetting(string name)
        {
            System.Configuration.Configuration config = GetConfiguration();

            if (config.AppSettings.Settings[name] == null)
            {
                return null;
            }
            return config.AppSettings.Settings[name].Value;
        }

        public static ConfigurationSection GetSection(string name)
        {
            Configuration config = GetConfiguration();
            return config.GetSection(name);
        }


        public static ConfigurationSectionGroup GetSectionGroup(string name)
        {
            Configuration config = GetConfiguration();
            return config.GetSectionGroup(name);
        }

    }
}
