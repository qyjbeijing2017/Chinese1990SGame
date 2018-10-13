using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaemonTools {
	public class UIManager : Singleton<UIManager> {

		#region tools
		Canvas m_canvas;
		delegate void LoadPanelCallBack (UIBase ui);
		Dictionary<string, UIBase> m_uiDic = new Dictionary<string, UIBase> ();
		List<string> m_uiStack = new List<string> ();
		void LoadNewPanelAsync (string panelName, LoadPanelCallBack callBack) {
			if (ConfigManager.Instance.UIPanelConfigData.ContainsKey (panelName)) {
				UIPanelConfig uiData = ConfigManager.Instance.UIPanelConfigData[panelName];
				if (uiData.Path != string.Empty) {
					ResourceLoadManager.Instance.LoadResourceAsync (uiData.Path, (obj) => {
						GameObject go = Daemon.Instantiate (obj as GameObject);
						go.transform.SetParent (Canvas.transform, false);
						go.SetActive (false);
						UIBase ui = go.GetComponent<UIBase> ();
						m_uiDic.Add (panelName, ui);
						callBack.Invoke (ui);
					});

				} else {
					Debug.LogError ("UIPath is empty!");
				}
			} else {
				Debug.LogError ("The UiPanel is not exist!");
			}
		}
		#endregion

		/// <summary>
		/// UI主画布
		/// </summary>
		/// <value></value>
		public Canvas Canvas {
			get {
				return m_canvas;
			}
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public void Init () {
			m_canvas = Daemon.FindObjectOfType<Canvas> ();
			Daemon.DontDestroyOnLoad (m_canvas);
		}
		/// <summary>
		/// 打开某个Ui
		/// </summary>
		/// <param name="panelName">Ui的名字</param>
		/// <param name="push">是否入栈</param>
		/// <param name="value">自定义数值</param>
		public void Open (string panelName, bool push, object[] value = null) {

			if (m_uiDic.ContainsKey (panelName)) {
				m_uiDic[panelName].show (false, value);
				m_uiDic[panelName].transform.SetAsLastSibling ();
				m_uiDic[panelName].gameObject.SetActive (true);

				if (push) {
					if (m_uiStack.Count > 0) {
						if (m_uiDic.ContainsKey (m_uiStack[m_uiStack.Count - 1])) {
							m_uiDic[m_uiStack[m_uiStack.Count - 1]].gameObject.SetActive (false);
						}
					}
					m_uiStack.Add (panelName);
				}

			} else {
				LoadNewPanelAsync (panelName, (ui) => {
					ui.show (true, value);
					ui.gameObject.SetActive (true);
					ui.transform.SetAsLastSibling ();

					if (push) {
						if (m_uiStack.Count > 0) {
							if (m_uiDic.ContainsKey (m_uiStack[m_uiStack.Count - 1])) {
								m_uiDic[m_uiStack[m_uiStack.Count - 1]].gameObject.SetActive (false);
							}
						}
						m_uiStack.Add (panelName);
					}

				});

			}

		}
		/// <summary>
		/// 关闭栈顶Ui
		/// </summary>
		public void close () {
			if (m_uiStack.Count > 1) {
				string panel2Name = m_uiStack[m_uiStack.Count - 2];
				if (m_uiDic.ContainsKey (panel2Name)) {
					m_uiDic[panel2Name].gameObject.SetActive (true);
					m_uiDic[panel2Name].transform.SetAsLastSibling ();
				} else {
					Debug.Log ("The uipanel has been destroyed！");
					m_uiStack.Remove (panel2Name);
				}
			} else {
				if (m_uiStack.Count > 0) {
					string panel1Name = m_uiStack[m_uiStack.Count - 1];
					if (m_uiDic.ContainsKey (panel1Name)) {
						m_uiDic[panel1Name].close ();
						m_uiDic[panel1Name].gameObject.SetActive (false);
					}
					m_uiStack.Remove (panel1Name);
				} else {
					Debug.Log ("uiStack is empty!");
				}
			}

		}
		/// <summary>
		/// 直接关闭Ui不管是否在栈内
		/// </summary>
		/// <param name="panelName">ui的名字</param>
		public void close (string panelName) {
			if (m_uiDic.ContainsKey (panelName)) {
				m_uiDic[panelName].close ();
				m_uiDic[panelName].gameObject.SetActive (false);
			}
		}

	}

}