using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    protected static T _instance = null;

    public static T Instance
    {
        get
        {
            if (null == _instance)
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
                _instance = go.AddComponent<T>();
                DontDestroyOnLoad(go);

            }
            return  _instance;
        }

    }

    private void Awake()
    {
        _instance = this as T;
    }

}
