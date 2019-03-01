using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaemonTools
{
    public delegate void ResourceLoadCallBack(Object obj);
    public class ResourceLoadManager : Singleton<ResourceLoadManager>
    {
        
        public T LoadResourc<T>(string path) where T : Object
        {           
            T resource = Resources.Load(path, typeof(T)) as T;
            return resource;
        }

        private ResourceRequest m_request;
        private int m_progress;
        public int LoadProgress
        {
            get
            {
                return m_progress;
            }
        }
        public void LoadResourceAsync(string path, ResourceLoadCallBack callBack)
        {
            Daemon.Instance.StartCoroutine(LoadResourceNow(path, callBack));
        }
        IEnumerator LoadResourceNow(string path, ResourceLoadCallBack callBack)
        {
            m_request = Resources.LoadAsync(path);
            while(!m_request.isDone)
            {
                m_progress = (int)(m_request.progress * 100.0f + 1.0f);
                yield return null;
            }
            callBack.Invoke(m_request.asset);
            m_progress = 0;
            yield return null;
        }
    }

}
