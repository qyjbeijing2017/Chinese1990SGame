using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
                    DontDestroyOnLoad(go);

                }
                return m_instance;
            }

        }

        protected void Awake()
        {
            m_instance = this as T;
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
                    Singleton<T>._instance = new T();
                }
                return _instance;
            }
        }
        static Singleton() { }

        public static void CreateInstance()
        {
            if (_instance == null)
            {
                Singleton<T>._instance = new T();
            }
        }
        public static void DestroyInstance()
        {
            if (Singleton<T>._instance != null)
            {
                Singleton<T>._instance = (T)((object)null);
            }
        }
    }

}

