using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace DaemonTools
{
    public class LoadSceneManager : Singleton<LoadSceneManager>
    {
        private int m_loadProgress;
        /// <summary>
        /// 加载进度
        /// </summary>
        public int LoadProgress
        {
            get
            {
                return m_loadProgress;
            }
        }

        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="sceneName">场景名称</param>
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="sceneId">场景Index</param>
        public void LoadScene(int sceneId)
        {
            SceneManager.LoadScene(sceneId);
        }

        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="sceneName">场景名称</param>
        /// <param name="callBack">完成回调函数</param>
        /// <param name="loadBefore">加载场景前加载配置</param>
        public void LoadSceneAsync(string sceneName, UnityAction callBack = null, UnityAction loadBefore = null)
        {
            SceneManager.LoadScene("LoadingScene");
            Daemon.Instance.StartCoroutine(LoadScene(sceneName, callBack, loadBefore));
        }

        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="sceneId">场景名称</param>
        /// <param name="callBack">完成回调函数</param>
        /// <param name="loadBefore">加载场景前加载配置</param>
        public void LoadSceneAsync(int sceneId, UnityAction callBack = null, UnityAction loadBefore = null)
        {
            SceneManager.LoadScene("LoadingScene");
            Daemon.Instance.StartCoroutine(LoadScene(sceneId, callBack, loadBefore));
        }

        IEnumerator LoadScene(int sceneId, UnityAction callBack = null, UnityAction loadBefore = null)
        {
            yield return null;
            yield return Daemon.Instance.StartCoroutine(LoadSceneBefore(loadBefore));
            AsyncOperation asyncOperationScene = SceneManager.LoadSceneAsync(sceneId);
            while (!asyncOperationScene.isDone)
            {
                m_loadProgress = (int)(asyncOperationScene.progress * 100.0f + 1.0f);
                if (asyncOperationScene.progress >= 0.9f)
                {
                    if (null != callBack)
                    {
                        callBack.Invoke();
                    }
                    asyncOperationScene.allowSceneActivation = true;
                    m_loadProgress = 0;
                }
                yield return null;
            }

        }

        IEnumerator LoadScene(string sceneName, UnityAction callBack = null, UnityAction loadBefore = null)
        {
            yield return null;
            yield return Daemon.Instance.StartCoroutine(LoadSceneBefore(loadBefore));
            AsyncOperation asyncOperationScene = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncOperationScene.isDone)
            {
                m_loadProgress = (int)(asyncOperationScene.progress * 100.0f + 1.0f);
                if (asyncOperationScene.progress >= 0.9f)
                {
                    if (null != callBack)
                    {
                        callBack.Invoke();
                    }
                    asyncOperationScene.allowSceneActivation = true;
                    m_loadProgress = 0;
                }
                yield return null;
            }

        }

        IEnumerator LoadSceneBefore(UnityAction loadBefore = null)
        {
            if (null != loadBefore)
            {
                loadBefore.Invoke();
            }
            yield return null;
        }

    }
}
