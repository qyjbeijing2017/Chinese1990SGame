using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaemonTools
{
    public class ConfigManager : Singleton<ConfigManager>
    {

        public void Start()
        {
            List<List<string>> Array;
            //读取csv二进制文件
            TextAsset binAsset = Resources.Load("Configs/test", typeof(TextAsset)) as TextAsset;

            string allText = binAsset.text;

            allText = allText.Replace("\n", string.Empty);
            string[] lineText = allText.Split("\r"[0]);


            for (int i = 0; i < lineText.Length; i++)
            {
                if (lineText[i] != string.Empty)
                {
                    string[] attributeText
                }
            }

        }
    }

}
