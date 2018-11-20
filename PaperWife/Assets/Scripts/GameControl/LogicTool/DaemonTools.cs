using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaemonTools
{
    /// <summary>
    /// Unity Monobehavior 单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        protected static T m_instance = null;

        public static T Instance
        {
            get
            {
                if (null == m_instance)
                {
                    T[] instances = FindObjectsOfType<T>();
                    if (instances != null)
                    {
                        for (int i = 0; i < instances.Length; i++)
                        {
                            Destroy(instances[i].gameObject);
                        }
                    }
                    GameObject go = new GameObject();
                    go.name = typeof(T).Name;
                    m_instance = go.AddComponent<T>();
                }
                return m_instance;
            }

        }

        protected void Awake()
        {
            m_instance = this as T;
            gameObject.name = typeof(T).Name;
        }

    }
    /// <summary>
    /// C#单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> where T : class, new()
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    Singleton<T>._instance = Activator.CreateInstance<T>();
                }
                return Singleton<T>._instance;
            }
        }
        static Singleton()
        {
            Singleton<T>._instance = Activator.CreateInstance<T>();
        }

        public static void CreateInstance()
        {
            if (_instance == null)
            {
                Singleton<T>._instance = Activator.CreateInstance<T>();
            }
        }
        public static void DestroyInstance()
        {
            if (Singleton<T>._instance != null)
            {
                Singleton<T>._instance = (T)((object)null);
            }
        }

        public static T GetInstance()
        {
            if (Singleton<T>._instance == null)
            {
                Singleton<T>._instance = Activator.CreateInstance<T>();
            }
            return Singleton<T>._instance;
        }
    }

    public struct BettingData
    {
        public string Name;
        public float Weight;

        BettingData(string name, float weight)
        {
            Name = name;
            Weight = weight;
        }

        static string BettingWheel(List<BettingData> datas)
        {
            //随机数
            UnityEngine.Random.seed = System.DateTime.Now.Second;
            float rand = UnityEngine.Random.Range(0, 1);
            //算总值
            float all = 0;
            for (int i = 0; i < datas.Count; i++)
            {
                all += datas[i].Weight;
            }
            //制作赌轮
            List<float> wheel = new List<float>();
            for (int i = 0; i < datas.Count; i++)
            {
                if (i == 0)
                    wheel.Add(datas[i].Weight / all);
                else
                    wheel.Add(wheel[i - 1] + datas[i].Weight / all);
            }
            //对照奖励
            for (int i = 0; i < wheel.Count; i++)
            {
                if (i == 0)
                {
                    if (rand < wheel[i])
                    {
                        return datas[i].Name;
                    }
                    else if(rand<wheel[i]&& rand>wheel[i-1])
                    {
                        return datas[i].Name;
                    }
                }
            }
            Debug.LogError("Betting Wheel Data Error!");
            return string.Empty;
        }
    }

}