﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaemonTools
{
    public class ConfigManager : Singleton<ConfigManager>
    {
        //声明配置表名
        const string UIPanelConfigName = "UIPanel";
        const string Level1DialogName = "Level1Dialog";
        const string Level1ReplyName = "Level1Reply";
        const string PlayerConfigName = "PlayerConfig";

        //声明配置表存储
        public Dictionary<string, UIPanelConfig> UIPanelConfigData;
        public Dictionary<int, Level1DialogConfig> Level1DialogConfigData;
        public Dictionary<int, Level1ReplyConfig> Level1ReplyConfigData;
        public Dictionary<string, PlayerConfig> PlayerConfigData;

        //用于游戏开始前启动
        public void InitUIConfig(){
            UIPanelConfigData = ConfigFactory<UIPanelConfig>.InitConfigs(UIPanelConfigName);           
        }

        //实例化配置表
        public void InitConfigManager()
        {
            Level1DialogConfigData = ConfigFactory<Level1DialogConfig>.InitConfigs(Level1DialogName);
            Level1ReplyConfigData = ConfigFactory<Level1ReplyConfig>.InitConfigs(Level1ReplyName);
            PlayerConfigData = ConfigFactory<PlayerConfig>.InitConfigs(PlayerConfigName);
        }
    }
}