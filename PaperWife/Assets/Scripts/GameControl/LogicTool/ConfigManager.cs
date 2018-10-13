using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaemonTools
{
    public class ConfigManager : Singleton<ConfigManager>
    {
        //声明配置表名
        const string TestConfigName = "test";
        const string Test10_11ConfigName = "test10_11";
        
        //声明配置表存储
        public Dictionary<int, TestConfig> TestConfigData;
        public Dictionary<int, Test10_11Config> Test10_11ConfigData;

        //实例化配置表
        public void InitConfigManager()
        {
            TestConfigData = ConfigFactory<TestConfig>.InitConfigs(TestConfigName);
            Test10_11ConfigData = ConfigFactory<Test10_11Config>.InitConfigs(Test10_11ConfigName);
        }

    }



}