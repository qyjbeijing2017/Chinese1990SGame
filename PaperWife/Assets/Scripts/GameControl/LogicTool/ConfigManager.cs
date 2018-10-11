using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaemonTools
{
    public class ConfigManager : Singleton<ConfigManager>
    {
        //声明配置表名
        const string TestConfigName = "test";
        
        //声明配置表存储
        public Dictionary<int, TestConfig> TestConfigData;

        //实例化配置表
        public void InitConfigManager()
        {
            TestConfigData = ConfigFactory<TestConfig>.InitConfigs(TestConfigName);
        }

    }



}