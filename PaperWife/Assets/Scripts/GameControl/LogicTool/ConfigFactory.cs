using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace DaemonTools
{


    public class ConfigFactory<T> where T : BaseConfig, new()
    {
        const string m_path = "Configs/";

        const int m_dataStartIndex = 2;

        public class DicHelper
        {
            List<List<string>> m_List;
            public DicHelper(List<List<string>> Input)
            {
                m_List = Input;
            }

            //实现TestHelper与int的隐式转换
            public static implicit operator Dictionary<int, T>(DicHelper helper)
            {
                Dictionary<int, T> dic = new Dictionary<int, T>();
                for (int i = m_dataStartIndex; i < helper.m_List.Count; i++)
                {
                    int configKey = 0;
                    int.TryParse(helper.m_List[i][0], out configKey);
                    if (!dic.ContainsKey(configKey))
                    {
                        T config = new T();
                        config.InitConfig(helper.m_List[i]);
                        dic.Add(configKey, config);
                    }
                    else
                    {
                        Debug.LogError("has content a key:" + configKey.ToString());
                    }
                }
                return dic;


            }

            // 实现TestHelper与string的隐式转换

            public static implicit operator Dictionary<string, T>(DicHelper helper)
            {
                Dictionary<string, T> dic = new Dictionary<string, T>();
                for (int i = m_dataStartIndex; i < helper.m_List.Count; i++)
                {
                    string configKey = string.Empty;
                    configKey = helper.m_List[i][0];
                    if (!dic.ContainsKey(configKey))
                    {
                        T config = new T();
                        config.InitConfig(helper.m_List[i]);
                        dic.Add(configKey, config);
                    }
                    else
                    {
                        Debug.LogError("has content a key:" + configKey);
                    }
                }
                return dic;

            }

            // 实现TestHelper与float的隐式转换
            public static implicit operator Dictionary<float, T>(DicHelper helper)
            {
                Dictionary<float, T> dic = new Dictionary<float, T>();
                for (int i = m_dataStartIndex; i < helper.m_List.Count; i++)
                {
                    float configKey = 0;
                    float.TryParse(helper.m_List[i][0], out configKey);
                    if (!dic.ContainsKey(configKey))
                    {
                        T config = new T();
                        config.InitConfig(helper.m_List[i]);
                        dic.Add(configKey, config);
                    }
                    else
                    {
                        Debug.LogError("has content a key:" + configKey.ToString());
                    }
                }
                return dic;
            }
        }


        public static DicHelper InitConfigs(string configName)
        {

            TextAsset csvFile = Resources.Load(m_path + configName) as TextAsset;
            List<List<string>> csvList = CsvReader.Csv2List(csvFile);
            return new DicHelper(csvList);
        }
    }

    public abstract class BaseConfig
    {
        public BaseConfig()
        {

        }
        /// <summary>
        /// 用于解析文本
        /// </summary>
        /// <param name="m_data"></param>
        public abstract void InitConfig(List<string> m_data);
    }

}
