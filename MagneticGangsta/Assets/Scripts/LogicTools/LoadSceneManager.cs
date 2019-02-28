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
        /// <param name="loadSceneMode">加载模式</param>
        public void LoadScene(string sceneName,LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(sceneName,loadSceneMode);
        }

        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="sceneId">场景Index</param>
        /// <param name="loadSceneMode">加载模式</param>
        public void LoadScene(int sceneId, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(sceneId, loadSceneMode);
        }

        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="sceneName">场景</param>
        /// <param name="callBack">回调</param>
        /// <param name="loadBefore">加载之前</param>
        /// <param name="loadSceneMode">加载模式</param>
        public void LoadSceneAsync(string sceneName, UnityAction callBack = null, UnityAction loadBefore = null, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            
            SceneManager.LoadScene("LoadingScene");
            UIManager.Instance.Open("LoadingPanel");
            Daemon.Instance.StartCoroutine(LoadScene(sceneName, callBack, loadBefore, loadSceneMode));
        }

        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="sceneId">场景index</param>
        /// <param name="callBack">回调</param>
        /// <param name="loadBefore">加载之前</param>
        /// <param name="loadSceneMode">加载模式</param>
        public void LoadSceneAsync(int sceneId, UnityAction callBack = null, UnityAction loadBefore = null, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene("LoadingScene");
            UIManager.Instance.Open("LoadingPanel");
            Daemon.Instance.StartCoroutine(LoadScene(sceneId, callBack, loadBefore, loadSceneMode));
        }

        IEnumerator LoadScene(int sceneId, UnityAction callBack = null, UnityAction loadBefore = null, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {            
            yield return Daemon.Instance.StartCoroutine(LoadSceneBefore(loadBefore));
            AsyncOperation asyncOperationScene = SceneManager.LoadSceneAsync(sceneId, loadSceneMode);
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

        IEnumerator LoadScene(string sceneName, UnityAction callBack = null, UnityAction loadBefore = null, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {            
            yield return Daemon.Instance.StartCoroutine(LoadSceneBefore(loadBefore));
            AsyncOperation asyncOperationScene = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
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
