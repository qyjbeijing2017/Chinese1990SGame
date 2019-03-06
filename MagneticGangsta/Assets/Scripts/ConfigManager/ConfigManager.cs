using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaemonTools
{
    public class ConfigManager : Singleton<ConfigManager>
    {
        //声明配置表名
        const string ConfigExampleName = "configExample";
        const string UIPanelConfigName = "UIPanel";


        //声明配置表存储
        public Dictionary<int, ConfigExample> ConfigExampleData;
        public Dictionary<string, UIPanelConfig> UIPanelConfigData;

        //用于游戏开始前启动
        public void InitUIConfig(){
            UIPanelConfigData = ConfigFactory<UIPanelConfig>.InitConfigs(UIPanelConfigName);
        }

        //实例化配置表
        public void InitConfigManager()
        {
            ConfigExampleData = ConfigFactory<ConfigExample>.InitConfigs(ConfigExampleName);
        }
    }
}