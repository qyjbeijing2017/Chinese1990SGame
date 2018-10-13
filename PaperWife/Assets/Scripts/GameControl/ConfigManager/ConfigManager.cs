using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaemonTools
{
    public class ConfigManager : Singleton<ConfigManager>
    {
        //声明配置表名
        const string TestConfigName = "test";
        const string UIPanelConfigName = "uiPanel";
        
        //声明配置表存储
        public Dictionary<int, TestConfig> TestConfigData;
        public Dictionary<string, UIPanelConfig> UIPanelConfigData;

        //用于游戏开始前启动
        public void InitUIConfig(){
            UIPanelConfigData = ConfigFactory<UIPanelConfig>.InitConfigs(UIPanelConfigName);
        }

        //实例化配置表
        public void InitConfigManager()
        {
            TestConfigData = ConfigFactory<TestConfig>.InitConfigs(TestConfigName);
        }

    }



}