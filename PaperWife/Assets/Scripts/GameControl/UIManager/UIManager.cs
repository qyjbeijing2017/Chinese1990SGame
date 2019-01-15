using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DaemonTools
{
    public class UIManager : Singleton<UIManager>
    {

        #region tools
        Canvas m_canvas;
        delegate void LoadPanelCallBack(UIBase ui);
        Dictionary<string, UIBase> m_uiDic = new Dictionary<string, UIBase>();
        List<string> m_uiStack = new List<string>();
        #endregion

        /// <summary>
        /// UI主画布
        /// </summary>
        /// <value></value>
        public Canvas Canvas
        {
            get
            {
                return m_canvas;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            m_canvas = Daemon.FindObjectOfType<Canvas>();
            if (!m_canvas)
            {
                m_canvas = Daemon.Instantiate(new Canvas());
            }
            Daemon.DontDestroyOnLoad(m_canvas);
        }
        /// <summary>
        /// 打开某个Ui
        /// </summary>
        /// <param name="panelName">Ui的名字</param>
        /// <param name="push">是否入栈</param>
        /// <param name="value">自定义数值</param>
        public void Open(string panelName, bool push = true, params object[] value)
        {

            if (m_uiDic.ContainsKey(panelName))
            {
                m_uiDic[panelName].show(false, value);
                m_uiDic[panelName].transform.SetAsLastSibling();
                m_uiDic[panelName].gameObject.SetActive(true);

                if (push)
                {
                    if (m_uiStack.Count > 0)
                    {
                        if (m_uiDic.ContainsKey(m_uiStack[m_uiStack.Count - 1]))
                        {
                            m_uiDic[m_uiStack[m_uiStack.Count - 1]].gameObject.SetActive(false);
                        }
                    }
                    m_uiStack.Add(panelName);
                }

            }
            else
            {
                if (ConfigManager.Instance.UIPanelConfigData.ContainsKey(panelName))
                {
                    UIPanelConfig uiData = ConfigManager.Instance.UIPanelConfigData[panelName];
                    if (uiData.Path != string.Empty)
                    {
                        GameObject go = Daemon.Instantiate(Resources.Load(uiData.Path) as GameObject);
                        go.transform.SetParent(Canvas.transform, false);
                        go.SetActive(false);
                        UIBase ui = go.GetComponent<UIBase>();
                        m_uiDic.Add(panelName, ui);

                        ui.show(true, value);
                        ui.gameObject.SetActive(true);
                        ui.transform.SetAsLastSibling();

                        if (push)
                        {
                            if (m_uiStack.Count > 0)
                            {
                                if (m_uiDic.ContainsKey(m_uiStack[m_uiStack.Count - 1]))
                                {
                                    m_uiDic[m_uiStack[m_uiStack.Count - 1]].gameObject.SetActive(false);
                                }
                            }
                            m_uiStack.Add(panelName);
                        }
                    }
                    else
                    {
                        Debug.LogError("UIPath is empty!");
                    }

                }
                else
                {
                    Debug.LogError("The UiPanel is not exist!");
                }





            }

        }
        /// <summary>
        /// 关闭栈顶Ui
        /// </summary>
        public void close()
        {
            if (m_uiStack.Count > 1)
            {
                string panel2Name = m_uiStack[m_uiStack.Count - 2];
                if (m_uiDic.ContainsKey(panel2Name))
                {
                    m_uiDic[panel2Name].gameObject.SetActive(true);
                    m_uiDic[panel2Name].transform.SetAsLastSibling();
                }
                else
                {
                    Debug.Log("The uipanel has been destroyed！");
                    m_uiStack.Remove(panel2Name);
                }
            }
            if (m_uiStack.Count > 0)
            {
                string panel1Name = m_uiStack[m_uiStack.Count - 1];
                if (m_uiDic.ContainsKey(panel1Name))
                {
                    m_uiDic[panel1Name].close();
                    m_uiDic[panel1Name].gameObject.SetActive(false);
                }
                m_uiStack.Remove(panel1Name);
            }
            else
            {
                Debug.Log("uiStack is empty!");

            }

        }
        /// <summary>
        /// 直接关闭Ui不管是否在栈内
        /// </summary>
        /// <param name="panelName">ui的名字</param>
        public void close(string panelName)
        {
            if (m_uiDic.ContainsKey(panelName))
            {
                m_uiDic[panelName].close();
                m_uiDic[panelName].gameObject.SetActive(false);
            }
            if (m_uiStack.Contains(panelName))
            {
                m_uiStack.Remove(panelName);
            }
        }
        /// <summary>
        /// 清空场景当中的UI的GameObject
        /// </summary>
        public void clear()
        {

            for (int i = 0; i < m_uiStack.Count; i++)
            {
                if (m_uiDic.ContainsKey(m_uiStack[i]))
                {
                    Daemon.Destroy(m_uiDic[m_uiStack[i]].gameObject);
                    m_uiDic.Remove(m_uiStack[i]);
                }
            }
            m_uiStack.Clear();
        }
        /// <summary>
        /// 关闭所有UI但是不清除物体
        /// </summary>
        public void closeAll()
        {
            var enumator = m_uiDic.GetEnumerator();
            while (enumator.MoveNext())
            {
                enumator.Current.Value.gameObject.SetActive(false);
            }
            m_uiStack.Clear();
        }
        /// <summary>
        /// 清除所有UI不管是不是在栈堆中，慎用！
        /// </summary>
        public void clearAll()
        {
            var enumator = m_uiDic.GetEnumerator();
            while (enumator.MoveNext())
            {
                Daemon.Destroy(enumator.Current.Value.gameObject);
            }
            m_uiDic.Clear();
            m_uiStack.Clear();
        }

        public T GetUI<T>(string id)where T:UIBase
        {
            if (m_uiDic.ContainsKey(id))
            {
                return m_uiDic[id] as T;
            }
            else
            {
                return null;
            }
        }

        public UIBase GetUI(string id)
        {
            if (m_uiDic.ContainsKey(id))
            {
                return m_uiDic[id];
            }
            else
            {
                return null;
            }
        }

        public T GetUI<T>() where T:UIBase
        {
            Type type = typeof(T);
            var enumerator = m_uiDic.GetEnumerator();
            while (enumerator.MoveNext())
            {
                UIBase item = enumerator.Current.Value;
                if(item.GetType().Name == type.Name)
                {
                    return item as T;
                }
                
            }
            return null;
        }

        public T[] GetUIs<T>() where T : UIBase
        {
            List<T> uis = new List<T>();
            Type type = typeof(T);
            var enumerator = m_uiDic.GetEnumerator();
            while (enumerator.MoveNext())
            {
                UIBase item = enumerator.Current.Value;
                if (item.GetType().Name == type.Name)
                {
                    uis.Add(item as T);
                }
            }
            if (uis.Count == 0)
            {
                return null;
            }
            else
            {  
                return uis.ToArray();
            }
        }

        public UIBase GetActiveUI()
        {
            UIBase ui = m_uiDic[m_uiStack[m_uiStack.Count - 1]];
            if (ui.gameObject.activeSelf)
            {
                return ui; 
            }
            return null;
        }
    }

}